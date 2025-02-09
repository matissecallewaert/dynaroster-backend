using System.Net.Http.Json;
using System.Text.Json;
using Core;
using Core.Commands.users;
using Core.Entities;
using Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Handlers.CommandHandlers.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly WorkForceDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CreateUserCommandHandler(
            WorkForceDbContext context,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var role = request.IsEmployee ? UserRole.Worker : UserRole.Manager;
            var newId = Guid.NewGuid();
            var userPayload = new
            {
                Username = request.FirstName + request.LastName,
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = role.ToString(),
                UserId = newId.ToString()
            };

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var userManagementUrl = _configuration["UserManagement:Endpoint"] + "/api/account/register";
                var response = await _httpClient.PostAsJsonAsync(userManagementUrl, userPayload, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new Exception($"Failed to create user in User Management Service: {errorMessage}");
                }

                switch (role)
                {
                    case UserRole.Manager:
                    {
                        var manager = new Manager()
                        {
                            Id = newId,
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            Email = request.Email,
                            PhoneNumber = request.PhoneNumber,
                            Role = role,
                            Address = new Address()
                            {
                                Id = Guid.NewGuid(),
                                Street = request.Address?.Street ?? string.Empty,
                                City = request.Address?.City ?? string.Empty,
                                State = request.Address?.State ?? string.Empty,
                                PostalCode = request.Address?.PostalCode ?? string.Empty,
                                Country = request.Address?.Country ?? string.Empty
                            }
                        };
                        _context.Users.Add(manager);
                        break;
                    }
                    case UserRole.Worker:
                    {
                        var managerId = Guid.Parse(request.ManagerId);
                        var manager = await _context.Managers
                            .AsNoTracking()
                            .Include(m => m.Workers)
                            .FirstOrDefaultAsync(m => m.Id == managerId, cancellationToken);
                        if (manager == null)
                        {
                            throw new Exception("Manager not found.");
                        }
                        
                        var worker = new Worker()
                        {
                            Id = newId,
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            Email = request.Email,
                            PhoneNumber = request.PhoneNumber,
                            Role = role,
                            Address = new Address()
                            {
                                Id = Guid.NewGuid(),
                                Street = request.Address?.Street ?? string.Empty,
                                City = request.Address?.City ?? string.Empty,
                                State = request.Address?.State ?? string.Empty,
                                PostalCode = request.Address?.PostalCode ?? string.Empty,
                                Country = request.Address?.Country ?? string.Empty
                            },
                            ManagerId = manager.Id
                        };
                        
                        manager.Workers.Add(worker);
                        _context.Users.Add(worker);
                        break;
                    }
                    case UserRole.SuperAdmin:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                var loginModel = new
                {
                    Email = request.Email,
                    Password = request.Password
                };
                var loginUrl = _configuration["UserManagement:Endpoint"] + "/api/account/login";
                var tokenResponse = await _httpClient.PostAsJsonAsync(loginUrl, loginModel, cancellationToken);
                
                if (!tokenResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to create user in User Management Service.");
                }
                
                var responseContent = await tokenResponse.Content.ReadAsStringAsync(cancellationToken);
                var responseData = JsonSerializer.Deserialize<TokenResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (responseData == null || string.IsNullOrEmpty(responseData.Token))
                {
                    throw new Exception("Failed to retrieve token from User Management Service.");
                }
                return responseData.Token;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

        }

        private class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
