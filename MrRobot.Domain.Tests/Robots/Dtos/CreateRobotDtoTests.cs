using MrRobot.Domain.DTOs;
using MrRobot.Domain.Shared;
using NUnit.Framework;

namespace MrRobot.Domain.Tests.Robots.Dtos;

public class CreateRobotDtoTests
{
    [Test]
    public void ToModel_should_convert_robot_dto_and_return_robot_model()
    {
        // arrange
        var robotDto = new CreateRobotDto("Test Robot Dto", new AreaSize() { Height = 1, Width = 1 });

        // act
        var sut = robotDto.ToModel();

        // assert
        Assert.AreEqual(robotDto.Name, sut.Name);
        Assert.AreEqual(robotDto.AreaSize.Width, sut.AreaSize.Width);
        Assert.AreEqual(robotDto.AreaSize.Height, sut.AreaSize.Height);
    }
}