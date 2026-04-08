namespace Backend.Server.Controller;

[Authorize]
[Route("api/projects")]
[ApiController]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{projectId}")]
    public async Task<ProjectResponse> GetProjectDetails([FromRoute] Guid projectId)
    {
        var result = await mediator.Send(new ProjectDetailsQuery
        {
            ProjectId = projectId,
            UserProfileId = Request.GetUserProfileIdFromHeader()
        });

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
