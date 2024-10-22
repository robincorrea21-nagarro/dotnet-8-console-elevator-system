using dotnet_8_console_elevator_system.Core.Common;
using dotnet_8_console_elevator_system.Core.Entities;
using dotnet_8_console_elevator_system.Services;

// Initialize the Building with multiple elevators
var building = new Building(ConfigConstants.NumberOfElevators);

// Initialize the BuildingService to orchestrate elevator requests
var buildingService = new BuildingService(building);

// Start processing elevator requests asynchronously
await buildingService.StartProcessingRequestsAsync();