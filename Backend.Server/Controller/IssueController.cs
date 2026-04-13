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

    [HttpPost]
    [Route("moveIssue")]
    public async Task<StatusCodeResult> MoveIssue([FromBody] MoveIssueCommand command)
    {
        await mediator.Send(command);

        return Ok();
    }
}
