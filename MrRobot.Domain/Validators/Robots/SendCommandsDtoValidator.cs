using FluentValidation;
using MrRobot.Domain.DTOs;

namespace MrRobot.Domain.Validators.Robots;
public class SendCommandsDtoValidator : AbstractValidator<SendCommandsDto>
{
    public SendCommandsDtoValidator()
    {
        RuleFor(x => x.Commands).NotEmpty();
        RuleFor(x => x.Commands).Must(RecognizedCommand)
            .WithMessage($"Command is not recognized. Accepted commands are 'Advance', 'Retreat', 'Left', 'Right'");
    }

    private bool RecognizedCommand(string[] commands) => !commands.Any(command => !CommandsList.Contains(command.ToLower()));

    private static readonly string[] CommandsList = { "advance", "retreat", "left", "right" }; // TODO abstract to shared
}
