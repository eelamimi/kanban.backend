namespace Backend.Server.Controller;

[Authorize]
[Route("api/columns")]
[ApiController]
public class ColumnController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ColumnResponse> AddColumn([FromBody] CreateColumnCommand command)
    {
        var result = await mediator.Send(command);

        return result;
    }

    [HttpPost]
    [Route("updateRelation")]
    public async Task<StatusCodeResult> UpdateRelation([FromBody] UpdateRelationCommand command)
    {
        await mediator.Send(command);

        return Ok();
    }
}
