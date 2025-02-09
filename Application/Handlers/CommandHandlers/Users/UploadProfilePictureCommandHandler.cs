using Core;
using Core.Commands.Users;
using MediatR;

namespace Application.Handlers.CommandHandlers.Users;

public class UploadProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommand, bool>
{
    private readonly WorkForceDbContext _context;
    
    public UploadProfilePictureCommandHandler(WorkForceDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        UploadProfilePictureCommand request,
        CancellationToken cancellationToken
    )
    {
        // TODO: use S3 or Azure Blob Storage to store images
        var user = await _context.Users.FindAsync(request.Id);
        if (user == null)
            return false;
        
        user.ProfilePicture = request.Image;
        
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}