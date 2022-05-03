using FluentValidation;
using MrRobot.Domain.DTOs;

namespace MrRobot.Domain.Validators.Robots;
public class CreateRobotDtoValidator : AbstractValidator<CreateRobotDto>
{
    public CreateRobotDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.AreaSize.Height).GreaterThan(0);
        RuleFor(x => x.AreaSize.Width).GreaterThan(0);
    }
}
