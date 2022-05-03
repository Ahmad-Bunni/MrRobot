using MrRobot.Domain.DTOs;
using MrRobot.Domain.Shared;
using NUnit.Framework;
using System;

namespace MrRobot.Domain.Tests.Robots.Dtos;

public class RobotDtoTests
{
    [Test]
    public void IsOutOfBound_should_be_true_when_y_axis_is_below_zero()
    {
        // arrange
        var robotDto = new RobotDto(Guid.NewGuid(), "Test Robot Dto", new AreaSize() { Height = 1, Width = 1 }, new Coordinates(1, -1));

        // act
        var sut = robotDto.IsOutOfBound;

        // assert
        Assert.True(sut);
    }

    [Test]
    public void IsOutOfBound_should_be_true_when_x_axis_is_below_zero()
    {
        // arrange
        var robotDto = new RobotDto(Guid.NewGuid(), "Test Robot Dto", new AreaSize() { Height = 1, Width = 1 }, new Coordinates(-1, 1));

        // act
        var sut = robotDto.IsOutOfBound;

        // assert
        Assert.True(sut);
    }

    [Test]
    public void IsOutOfBound_should_be_true_when_y_axis_is_greater_than_height()
    {
        // arrange
        var robotDto = new RobotDto(Guid.NewGuid(), "Test Robot Dto", new AreaSize() { Height = 1, Width = 1 }, new Coordinates(1, 2));

        // act
        var sut = robotDto.IsOutOfBound;

        // assert
        Assert.True(sut);
    }

    [Test]
    public void IsOutOfBound_should_be_true_when_x_axis_is_greater_than_width()
    {
        // arrange
        var robotDto = new RobotDto(Guid.NewGuid(), "Test Robot Dto", new AreaSize() { Height = 1, Width = 1 }, new Coordinates(2, 1));

        // act
        var sut = robotDto.IsOutOfBound;

        // assert
        Assert.True(sut);
    }
}
