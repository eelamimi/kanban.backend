namespace Backend.Server.Controller;

[Authorize]
[Route("api/issues")]
[ApiController]
public class IssueController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IssueResponse> AddIssue([FromForm] AddIssueCommand command)
    {
        var result = await mediator.Send(command);

        return result;
    }
}
