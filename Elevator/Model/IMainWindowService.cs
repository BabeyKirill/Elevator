using System;
using System.Collections.Generic;
using System.Linq;
namespace Elevator.Model
{
    interface IMainWindowService
    {
        bool IsSimulationStarted { get; set; }
        int NumberOfFloors { get; set; }
        Elevator Elevator { get; set; }
        List<Passenger> Passengers { get; set; }

        event Action<int> OuterActiveFloorButtonActivated;
        event Action<int> ElevatorMoved;
        event Action<int> PassengerEnterElevator;
        event Action<int> InnerActiveFloorButtonActivated;
        event Action<int> PassengerOutFromElevator;
        event Action<int> PassengerDisappeared;
        event Action<int> TotalMovedMassIncreased;
        event Action ElevatorMadeRide;
        event Action ElevatorMadeIddleRide;
        event Action<double> TimeUpdated;
        event Action OverWeightActivated;
        event Action OverWeightDeactivated;
        event Action<List<Passenger>> PassengersInfoUpdated;

        void TimerStart();
        void TimerStop();
        void AddAPassenger(int NumberOfTheFloor);  
    }
}
