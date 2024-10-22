using dotnet_8_console_elevator_system.Core.Common;
using dotnet_8_console_elevator_system.Core.Entities;
using dotnet_8_console_elevator_system.Core.Enums;
using dotnet_8_console_elevator_system.DTOs;

namespace dotnet_8_console_elevator_system.Services
{
    public class BuildingService
    {
        private readonly Building _building;
        private readonly Random _random;

        public BuildingService(Building building)
        {
            _building = building;
            _random = new Random();
        }

        // Starts processing requests in an infinite loop (orchestrates behavior)
        public async Task StartProcessingRequestsAsync()
        {
            int? lastRequestedFloor = null;

            while (true)
            {
                // Generate a random floor and direction (high-level logic, not business logic)
                int floor = _random.Next(ConfigConstants.MinFloor, ConfigConstants.MaxFloor + 1); // Floors 1-10
                Direction direction = _random.Next(ConfigConstants.MinDirectionValue, ConfigConstants.MaxDirectionValue + 1) == 0
                    ? Direction.Up
                    : Direction.Down;
                int requestedElevatorId = _random.Next(ConfigConstants.MinElevatorId, ConfigConstants.MaxElevatorId + 1); // Choose an elevator at random

                // Ensure not requesting the same floor consecutively
                if (lastRequestedFloor == floor)
                {
                    continue; // Skip if it's the same as the last requested floor
                }

                // Create the request DTO (high-level coordination)
                var request = new ElevatorRequestDto(floor, direction);

                // Dispatch it to the building (business logic resides inside the building)
                _building.AddRequest(request, requestedElevatorId);

                // Update the last requested floor
                lastRequestedFloor = floor;

                // Wait before creating the next request
                await Task.Delay(ConfigConstants.RequestIntervalMilliseconds); // Delay for the next request
            }
        }
    }
}