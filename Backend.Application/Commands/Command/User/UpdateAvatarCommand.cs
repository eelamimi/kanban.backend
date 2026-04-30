namespace Backend.Application.Commands.Command;

public class UpdateAvatarCommand : ICommand<byte[]>
{
    public Guid UserProfileId { get; set; }

    public IFormFile Avatar { get; init; }
}
