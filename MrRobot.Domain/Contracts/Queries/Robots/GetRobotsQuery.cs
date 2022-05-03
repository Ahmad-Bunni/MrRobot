using MediatR;
using MrRobot.Domain.DTOs;

namespace MrRobot.Domain.Contracts.Queries.Robots;

// request could include filters
public class GetRobotsQuery : IRequest<IEnumerable<RobotDto>> { }
