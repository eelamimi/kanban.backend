namespace Backend.Application.Commands.CommandHandler;

public class UpdateAvatarCommandHandler(
    IUserProfileRepository userProfileRepository) :
    ICommandHandler<UpdateAvatarCommand, byte[]>
{
    public async Task<byte[]> Handle(UpdateAvatarCommand command, CancellationToken token)
    {
        var userProfile = await userProfileRepository.GetByIdAsync(command.UserProfileId, token);

        using var memoryStream = new MemoryStream();
        await command.Avatar.CopyToAsync(memoryStream, token);
        var fileBytes = memoryStream.ToArray();

        userProfile.Avatar = fileBytes;

        userProfileRepository.Update(userProfile);
        await userProfileRepository.SaveChangesAsync(token);

        return fileBytes;
    }
}
