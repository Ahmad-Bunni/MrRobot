using MrRobot.Domain.Entities;
using MrRobot.Domain.Responses;
using MrRobot.Domain.Shared;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace MrRobot.Domain.Tests.Robots.Dtos;

public class ControlCenterTests
{
    [Test]
    public void CreateRobot_should_return_ok_when_it_is_successfully_created()
    {
        // arrange
        var robot = new Robot("Test", new AreaSize { Height = 1, Width = 1 });

        var controlCenter = new ControlCenter();

        // act
        var sut = controlCenter.CreateRobot(robot);

        // assert
        Assert.AreEqual(CreateRobotDomainStatus.Ok, sut);
    }

    [Test]
    public void CreateRobot_should_return_already_exists_when_robot_name_is_duplicate()
    {
        // arrange
        var robot = new Robot("Test", new AreaSize { Height = 1, Width = 1 });

        var controlCenter = new ControlCenter();

        var createFirstRobotStatus = controlCenter.CreateRobot(robot);

        Debug.Assert(createFirstRobotStatus == CreateRobotDomainStatus.Ok);

        // act
        var sut = controlCenter.CreateRobot(robot);

        // assert
        Assert.AreEqual(CreateRobotDomainStatus.AlreadyExists, sut);
    }

    [Test]
    public void RemoveRobot_should_return_ok_when_it_is_successfully_removed()
    {
        // arrange
        var robot = new Robot("Test", new AreaSize { Height = 1, Width = 1 });

        var controlCenter = new ControlCenter();

        var createFirstRobotStatus = controlCenter.CreateRobot(robot);

        Debug.Assert(createFirstRobotStatus == CreateRobotDomainStatus.Ok);

        // act
        var sut = controlCenter.RemoveRobot(robot.Id);

        // assert
        Assert.AreEqual(RemoveRobotDomainStatus.Ok, sut);
    }

    [Test]
    public void RemoveRobot_should_return_not_found_when_it_robot_id_does_not_exist()
    {
        // arrange
        var controlCenter = new ControlCenter();

        // act
        var sut = controlCenter.RemoveRobot(Guid.NewGuid());

        // assert
        Assert.AreEqual(RemoveRobotDomainStatus.NotFound, sut);
    }

    [Test]
    public void SendCommandToRobotById_should_return_ok_when_it_successfully_updates_coordinates()
    {
        // arrange
        var robot = new Robot("Test", new AreaSize { Height = 1, Width = 1 });

        var controlCenter = new ControlCenter();

        var createFirstRobotStatus = controlCenter.CreateRobot(robot);

        Debug.Assert(createFirstRobotStatus == CreateRobotDomainStatus.Ok);

        var commands = new[] { "advance", "retreat" };

        // act
        var sut = controlCenter.SendCommandToRobotById(robot.Id, commands);

        // assert
        Assert.AreEqual(SendCommandsToRobotDomainStatus.Ok, sut);
    }

    [Test]
    public void SendCommandToRobotById_should_return_not_found_when_robot_id_does_not_exist()
    {
        // arrange
        var commands = new[] { "advance", "retreat" };

        var controlCenter = new ControlCenter();

        // act
        var sut = controlCenter.SendCommandToRobotById(Guid.NewGuid(), commands);

        // assert
        Assert.AreEqual(SendCommandsToRobotDomainStatus.NotFound, sut);
    }

    [Test]
    public void SendCommandToRobotById_should_return_ok_when_successfully_updated_coordinates()
    {
        // arrange
        var robot1 = new Robot("Test1", new AreaSize { Height = 1, Width = 1 });
        var robot2 = new Robot("Test2", new AreaSize { Height = 1, Width = 1 });

        var controlCenter = new ControlCenter();

        var createFirstRobotStatus1 = controlCenter.CreateRobot(robot1);
        var createFirstRobotStatus2 = controlCenter.CreateRobot(robot2);

        Debug.Assert(createFirstRobotStatus1 == CreateRobotDomainStatus.Ok);
        Debug.Assert(createFirstRobotStatus2 == CreateRobotDomainStatus.Ok);

        var commands = new[] { "advance", "retreat" };

        // act
        var sut = controlCenter.SendCommandToRobots(commands);

        // assert
        Assert.AreEqual(SendCommandsToRobotsDomainStatus.Ok, sut);
    }

    [Test]
    public void SendCommandToRobotById_should_return_not_found_when_robots_list_is_empty()
    {
        // arrange
        var commands = new[] { "advance", "retreat" };

        var controlCenter = new ControlCenter();

        // act
        var sut = controlCenter.SendCommandToRobots(commands);

        // assert
        Assert.AreEqual(SendCommandsToRobotsDomainStatus.NotFound, sut);
    }
}