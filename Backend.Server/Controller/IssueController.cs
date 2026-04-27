namespace Backend.Server.Controller;

[Authorize]
[Route("api/issues")]
[ApiController]
public class IssueController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{issuePublicId}")]
    public async Task<IssueResponse> GetIssueDetails([FromRoute] string issuePublicId)
    {
        var result = await mediator.Send(new IssueDeatilsQuery
        {
            PublicId = issuePublicId,
            ProjectId = Request.GetProjectIdFromHeader()
        });

        return result;
    }

    [HttpPost]
    public async Task<IssueResponse> AddIssue([FromForm] AddIssueCommand command)
    {
        var result = await mediator.Send(command);

        return result;
    }

    [HttpPut]
    public async Task<IssueResponse> UpdateIssue([FromForm] UpdateIssueCommand command)
    {
        return await mediator.Send(command);
    }

    [HttpPost]
    [Route("moveIssue")]
    public async Task<StatusCodeResult> MoveIssue([FromBody] MoveIssueCommand command)
    {
        await mediator.Send(command);

        return Ok();
    }

    [HttpPost]
    [Route("commentary")]
    public async Task<IssueResponse> AddCommentary([FromForm] CreateCommentaryWithAttachmentsCommand command)
    {
        return await mediator.Send(command);
    }
}
