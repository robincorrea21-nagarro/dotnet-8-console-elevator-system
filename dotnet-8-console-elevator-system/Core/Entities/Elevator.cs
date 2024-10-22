using dotnet_8_console_elevator_system.Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_8_console_elevator_system.Core.Entities
{
    public class Elevator
    {
        public int Id { get; } // Elevator ID
        public int CurrentFloor { get; set; } // Elevator's current floor
        public Direction CurrentDirection { get; set; } // Up, Down, or Idle
        public ElevatorState State { get; set; } // Moving, Stopped, Idle

        // A queue to keep track of where the elevator needs to go
        public List<int> DestinationFloors = new List<int>();

        // Constructor to create an elevator
        public Elevator(int id)
        {
            Id = id;
            CurrentFloor = 0; // Starts on the ground floor
            CurrentDirection = Direction.Idle; // Not moving initially
            State = ElevatorState.Idle; // Initially idle
        }
    }
}
