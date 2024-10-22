using dotnet_8_console_elevator_system.Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_8_console_elevator_system.DTOs
{
    public class ElevatorRequestDto
    {
        public int Floor { get; set; }
        public Direction Direction { get; set; }

        public ElevatorRequestDto(int floor, Direction direction)
        {
            Floor = floor;
            Direction = direction;
        }
    }
}
