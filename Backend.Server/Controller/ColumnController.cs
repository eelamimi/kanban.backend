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

    [HttpPut]
    public async Task<StatusCodeResult> UpdateName([FromBody] UpdateColumnNameCommand command)
    {
        await mediator.Send(command);

        return Ok();
    }

    [HttpDelete]
    public async Task<StatusCodeResult> DeleteColumn([FromBody] DeleteColumnCommand command)
    {
        await mediator.Send(command);

        return Ok();
    }

    [HttpPut]
    [Route("updatePosition")]
    public async Task<StatusCodeResult> UpdatePosition([FromBody] UpdatePositionCommand command)
    {
        await mediator.Send(command);

        return Ok();
    }

    [HttpPut]
    [Route("updateRelation")]
    public async Task<StatusCodeResult> UpdateRelation([FromBody] UpdateRelationCommand command)
    {
        await mediator.Send(command);

        return Ok();
    }
}
