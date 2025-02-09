using MediatR;

namespace Core.Commands.Users;

public class UploadProfilePictureCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public byte[] Image { get; set; }
}