using dotnet_8_console_elevator_system.Core.Common;
using dotnet_8_console_elevator_system.Core.Entities;
using dotnet_8_console_elevator_system.Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_8_console_elevator_system.Services
{
    public class ElevatorService
    {
        private readonly Elevator _elevator;
        private readonly Func<int, Task> _delayFunc; // Function to control delay (for testing)
        private readonly bool _autoStartMove; // Control whether movement starts automatically

        // Constructor injection
        public ElevatorService(Elevator elevator, Func<int, Task> delayFunc = null, bool autoStartMove = true)
        {
            _elevator = elevator;
            _delayFunc = delayFunc ?? Task.Delay; // Default to real delay if none is provided
            _autoStartMove = autoStartMove;      // Control if movement starts automatically
        }

        public int GetElevatorId()
        {
            return _elevator.Id;
        }

        // Add a new floor to the list of destinations
        public void AddDestination(int floor)
        {
            if (!_elevator.DestinationFloors.Contains(floor) && _elevator.CurrentFloor != floor)
            {
                _elevator.DestinationFloors.Add(floor);
                SetConsoleColor();
                Console.WriteLine($"Car {_elevator.Id}: Added floor {floor} to destinations.");
                Console.ResetColor();

                // If autoStartMove is true and the elevator is idle, start moving
                if (_autoStartMove && _elevator.State == ElevatorState.Idle)
                {
                    Task.Run(() => Move());
                }
            }
        }

        // Method to move the elevator
        public async Task Move()
        {
            while (_elevator.DestinationFloors.Count > 0)
            {
                // Set state to Moving
                _elevator.State = ElevatorState.Moving;

                // Get the next floor the elevator needs to go to
                int nextFloor = _elevator.DestinationFloors.First();
                _elevator.DestinationFloors.RemoveAt(0);

                // Move one floor at a time until reaching the destination
                while (_elevator.CurrentFloor != nextFloor)
                {
                    if (_elevator.CurrentFloor < nextFloor)
                    {
                        _elevator.CurrentFloor++;
                        _elevator.CurrentDirection = Direction.Up;
                    }
                    else if (_elevator.CurrentFloor > nextFloor)
                    {
                        _elevator.CurrentFloor--;
                        _elevator.CurrentDirection = Direction.Down;
                    }

                    SetConsoleColor();
                    Console.WriteLine($"Car {_elevator.Id} is at floor {_elevator.CurrentFloor}");
                    Console.ResetColor();

                    // Skip 10-second delay when floor destination has already reached
                    if (_elevator.CurrentFloor == nextFloor)
                    {
                        continue;
                    }

                    // Simulate the time it takes to move between floors
                    await _delayFunc(ConfigConstants.FloorMovementDelayMilliseconds); // Use injected delay function
                }

                SetConsoleColor();
                Console.WriteLine($"Car {_elevator.Id} has arrived at floor {_elevator.CurrentFloor} and is stopping.");
                Console.ResetColor();
                _elevator.State = ElevatorState.Stopped;

                await _delayFunc(ConfigConstants.FloorStopDelayMilliseconds); // Delay for passengers
            }

            // Set the elevator to Idle
            _elevator.State = ElevatorState.Idle;
            _elevator.CurrentDirection = Direction.Idle;
            SetConsoleColor();
            Console.WriteLine($"Car {_elevator.Id} is idle at floor {_elevator.CurrentFloor}, awaiting the next request.");
            Console.ResetColor();
        }

        // Dynamically set the color based on the elevator ID
        private void SetConsoleColor()
        {
            Console.ForegroundColor = ConfigConstants.ElevatorColors[(_elevator.Id - 1) % ConfigConstants.ElevatorColors.Length];
        }
    }
}
