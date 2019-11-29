using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Model
{
    class Elevator
    {
        public int CurrentFloor { get; set; }
        public bool[] InnerActiveFloorButtons { get; set; }
        public bool[] OuterActiveFloorButtons { get; set; }
        public int CurrentWeight { get; set; }
        public readonly int MaximumWeight = 400;

        public Elevator(int NumberOfFloors)
        {
            this.InnerActiveFloorButtons = new bool[NumberOfFloors];
            this.OuterActiveFloorButtons = new bool[NumberOfFloors];
            this.CurrentFloor = 1;
        }
    }
}
