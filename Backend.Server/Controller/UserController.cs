namespace Backend.Server.Controller;

[Authorize]
[Route("api/user")]
[ApiController]
public class UserContoller(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{userId}")]
    public async Task<UserResponse> GetUserInfo(Guid userId)
    {
        var dto = await mediator.Send(new UserQuery 
        { 
            UserId = userId,
            PersonUserId = Request.GetUserProfileIdFromHeader()
        });

        return dto;
    }

    [HttpPut]
    [Route("avatar")]
    public async Task<byte[]> UpdateAvatar([FromForm] UpdateAvatarCommand command)
    {
        command.UserProfileId = Request.GetUserProfileIdFromHeader();

        return await mediator.Send(command);
    }
}
