using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_8_console_elevator_system.Core.Common
{
    public static class ConfigConstants
    {
        // Building/Elevator-related constants
        public const int NumberOfElevators = 4;
        public const int MinFloor = 1;
        public const int MaxFloor = 10;
        public const int MinDirectionValue = 0;
        public const int MaxDirectionValue = 1;
        public const int MinElevatorId = 1;
        public const int MaxElevatorId = NumberOfElevators;

        // Application behavior
        public const int RequestIntervalMilliseconds = 21000; // 21-second delay for requests

        // Elevator movement-related constants
        public const int FloorMovementDelayMilliseconds = 10000; // 10-second delay to move between floors
        public const int FloorStopDelayMilliseconds = 10000; // 10-second stop time when reaching a floor

        // Define an array of colors for elevators
        public static readonly ConsoleColor[] ElevatorColors =
        {
            ConsoleColor.Cyan,
            ConsoleColor.Green,
            ConsoleColor.Yellow,
            ConsoleColor.Magenta,
            ConsoleColor.Blue,   // Add more colors if you like
            ConsoleColor.Red,    // Will cycle through these colors if more elevators
            ConsoleColor.White
        };
    }
}
