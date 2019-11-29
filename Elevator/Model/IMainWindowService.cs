using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Model
{
    interface IMainWindowService
    {
        bool IsSimulationStarted { get; set; }
        int NumberOfFloors { get; set; }
        Elevator Elevator { get; set; }
        List<Passenger> Passengers { get; set; }
        void AddAPassenger(int NumberOfTheFloor);
        void StartSimulation();
        event Action<int> OuterActiveFloorButtonActivated;
        event Action<int> ElevatorMoved;
        event Action<int> PassengerEnterElevator;
        event Action<int> InnerActiveFloorButtonActivated;
        event Action<int> PassengerOutFromElevator;
    }
}
