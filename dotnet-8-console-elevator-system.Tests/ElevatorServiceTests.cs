using dotnet_8_console_elevator_system.Core.Entities;
using dotnet_8_console_elevator_system.Core.Enums;
using dotnet_8_console_elevator_system.Services;

public class ElevatorServiceTests
{
    private static Task NoDelay(int _) => Task.CompletedTask;  // A no-op delay function

    [Fact]
    public void AddDestination_Should_AddNewDestination()
    {
        // Arrange
        var elevator = new Elevator(1);
        var elevatorService = new ElevatorService(elevator, NoDelay, autoStartMove: false); // Disable auto movement for test

        // Act
        elevatorService.AddDestination(5);

        // Assert
        Assert.Contains(5, elevator.DestinationFloors); // Ensure the destination is added
    }

    [Fact]
    public async Task Move_Should_ReachDestinationFloor()
    {
        // Arrange
        var elevator = new Elevator(1);
        var elevatorService = new ElevatorService(elevator, NoDelay, autoStartMove: false); // Control the move manually

        // Act
        elevatorService.AddDestination(5);
        await elevatorService.Move();  // Manually trigger the move

        // Assert
        Assert.Equal(5, elevator.CurrentFloor); // Elevator should reach the destination
        Assert.Empty(elevator.DestinationFloors); // No more destinations
    }

    [Fact]
    public async Task Move_Should_UpdateStateCorrectly()
    {
        // Arrange
        var elevator = new Elevator(1);
        var elevatorService = new ElevatorService(elevator, delayFunc: NoDelay, autoStartMove: false);

        // Act
        elevatorService.AddDestination(5);
        await elevatorService.Move();

        // Assert
        Assert.Equal(ElevatorState.Idle, elevator.State); // Should be idle at the end
    }

    [Fact]
    public async Task Move_Should_HandleMultipleDestinationsCorrectly()
    {
        // Arrange
        var elevator = new Elevator(1);
        var elevatorService = new ElevatorService(elevator, delayFunc: NoDelay, autoStartMove: false);

        // Act
        elevatorService.AddDestination(3);
        elevatorService.AddDestination(6);
        await elevatorService.Move(); // Elevator moves to 3 first, then 6

        // Assert
        Assert.Equal(6, elevator.CurrentFloor); // Final destination is floor 6
        Assert.Empty(elevator.DestinationFloors); // No more destinations
    }

    [Fact]
    public void AddDestination_Should_NotAddDuplicateFloor()
    {
        // Arrange
        var elevator = new Elevator(1);
        var elevatorService = new ElevatorService(elevator, NoDelay, autoStartMove: false);

        // Act
        elevatorService.AddDestination(5);
        elevatorService.AddDestination(5); // Adding the same floor again

        // Assert
        Assert.Single(elevator.DestinationFloors); // Only one instance of floor 5 should exist
    }

    [Fact]
    public async Task Move_Should_SkipFloorIfAlreadyThere()
    {
        // Arrange
        var elevator = new Elevator(5); // Start at floor 5
        var elevatorService = new ElevatorService(elevator, NoDelay, autoStartMove: false);

        // Act
        elevatorService.AddDestination(5); // Adding the same floor the elevator is currently on
        await elevatorService.Move(); // Move should finish immediately

        // Assert
        Assert.Equal(5, elevator.CurrentFloor); // Elevator stays at floor 5
        Assert.Equal(ElevatorState.Idle, elevator.State); // Should go to idle
        Assert.Empty(elevator.DestinationFloors); // No destinations should remain
    }
}
