namespace Backend.Server.Controller;

[Authorize]
[Route("api/projects")]
[ApiController]
public class ProjectController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{projectId}")]
    public async Task<ProjectResponse> GetProjectDetails(
        [FromRoute] Guid projectId,
        [FromQuery] Guid? authorId = null,
        [FromQuery] Guid? assigneeId = null)
    {
        return await mediator.Send(new ProjectDetailsQuery
        {
            ProjectId = projectId,
            UserProfileId = Request.GetUserProfileIdFromHeader(),
            AuthorId = authorId,
            AssigneeId = assigneeId,
        });
    }

    [HttpPost]
    public async Task<ProjectResponse> CreateProject([FromBody] CreateProjectCommand command)
    {
        command.UserProfileId = Request.GetUserProfileIdFromHeader();

        return await mediator.Send(command);
    }

    [HttpPut]
    public async Task<StatusCodeResult> UpdateProject([FromBody] UpdateProjectCommand command)
    {
        await mediator.Send(command);

        return Ok();
    }
}
