using MediatR;
using Microsoft.AspNetCore.Mvc;
using MrRobot.Domain.Contracts.Commands.Robots;
using MrRobot.Domain.Contracts.Queries.Robots;
using MrRobot.Domain.DTOs;

namespace MrRobot.API.Controllers;

[Route("[controller]")]
[ApiController]
public class RobotsController : ControllerBase
{
    private readonly ISender _mediator;

    public RobotsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = nameof(AddRobot)), ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateRobotCommandResult))]
    public async Task<IActionResult> AddRobot(CreateRobotDto request, CancellationToken cancellation)
    {
        var command = new CreateRobotCommand(request);

        var result = await _mediator.Send(command, cancellation);

        return Ok(result);
    }

    [HttpDelete("{id:guid}", Name = nameof(RemoveRobot)), ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(RemoveRobotCommandResult))]
    public async Task<IActionResult> RemoveRobot(Guid id, CancellationToken cancellation)
    {
        var command = new RemoveRobotCommand(id);

        var result = await _mediator.Send(command, cancellation);

        return Ok(result);
    }

    [HttpPost("command/{id:guid}", Name = nameof(SendCommandsToRobot)), ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SendCommandsToRobotCommandResult))]
    public async Task<IActionResult> SendCommandsToRobot(Guid id, SendCommandsDto request, CancellationToken cancellation)
    {
        var command = new SendCommandsToRobotCommand(id, request);

        var result = await _mediator.Send(command, cancellation);

        return Ok(result);
    }

    [HttpPost("command/all", Name = nameof(SendCommandsToRobots)), ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SendCommandsToRobotsCommandResult))]
    public async Task<IActionResult> SendCommandsToRobots(SendCommandsDto request, CancellationToken cancellation)
    {
        var command = new SendCommandsToRobotsCommand(request);

        var result = await _mediator.Send(command, cancellation);

        return Ok(result);
    }

    [HttpGet("robots", Name = nameof(GetRobots)), ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRobotsQuery))]
    public async Task<IActionResult> GetRobots(CancellationToken cancellation)
    {
        var query = new GetRobotsQuery();

        var result = await _mediator.Send(query, cancellation);

        return Ok(result);
    }
}
