using dotnet_8_console_elevator_system.DTOs;
using dotnet_8_console_elevator_system.Services;

namespace dotnet_8_console_elevator_system.Core.Entities
{
    public class Building
    {
        private List<ElevatorService> _elevatorServices = new List<ElevatorService>();

        public Building(int numberOfElevators)
        {
            // Create elevators based on the number provided and inject them into ElevatorService
            for (int i = 0; i < numberOfElevators; i++)
            {
                var elevator = new Elevator(i + 1); // Create the elevator entity
                var elevatorService = new ElevatorService(elevator); // Inject the elevator into the service
                _elevatorServices.Add(elevatorService); // Add the service to the list
            }
        }

        // Handle a new elevator request (business logic here)
        public void AddRequest(ElevatorRequestDto request, int requestedElevatorId)
        {
            Console.WriteLine($"\"{request.Direction.ToString().ToLower()}\" request on floor {request.Floor} received in Elevator {requestedElevatorId}.");

            // Assign the request to the specific requested elevator
            ElevatorService selectedElevatorService = GetElevatorServiceById(requestedElevatorId);

            // Add the request floor as a destination for the elevator (this is business logic)
            selectedElevatorService.AddDestination(request.Floor);
        }

        // Find the elevator service by its ID
        private ElevatorService GetElevatorServiceById(int elevatorId)
        {
            return _elevatorServices.First(s => s.GetElevatorId() == elevatorId);
        }
    }
}
