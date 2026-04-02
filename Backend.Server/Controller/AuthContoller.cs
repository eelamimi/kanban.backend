namespace Backend.Server.Controller;

[Route("api/auth")]
[ApiController]
public class AuthContoller(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<RegisterUserResult> RegisterUser([FromBody] RegisterUserCommand command)
    {
        var result = await mediator.Send(command);

        return result;
    }

    [HttpPost]
    [Route("login")]
    public async Task<LoginUserResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await mediator.Send(command);

        return result;
    }

    [HttpPost]
    [Route("verifyToken")]
    public async Task<VerifyTokenResponse> VerifyToken()
    {
        var result = await mediator.Send(new VerifyTokenQuery
        {
            Token = Request.GetAccessTokenFromHeader()
        });

        return result;
    }
}
