using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PassengerStatus
{
    WaitingForElevator,
    IsInElevator,
    OutFromElevator
}

namespace Elevator.Model
{
    public class Passenger
    {
        public int WantedFloor { get; set; }
        public int CurrentFloor { get; set; }
        public int Weight { get; set; }
        public PassengerStatus Status {get; set;}

        public Passenger(int NumberOfFloors, int NumberOfTheFloor)
        {
            this.CurrentFloor = NumberOfTheFloor;
            Random rnd = new Random();
            if (this.CurrentFloor != 1)
            {
                this.WantedFloor = 1;
            }
            else
            {
                this.WantedFloor = rnd.Next(2, NumberOfFloors + 1);
            }
            this.Weight = rnd.Next(40, 80);
            this.Status = PassengerStatus.WaitingForElevator;
        }
    }
}
