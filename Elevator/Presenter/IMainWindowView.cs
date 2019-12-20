using Elevator.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Presenter
{
    interface IMainWindowView : IView
    {
        void SetNumberOfFloors(int n);
        void AddAPassenger(int NumberOfTheFloor);
        void ActivateOuterActiveFloorCheckBox(int NumberOfTheFloor);
        void ActivateInnerActiveFloorCheckBox(int NumberOfTheFloor);
        void MoveElevator(int ElevatorNewFloor);
        void MovePassengerInElevator(int NumberOfTheFloor);
        void MovePassengerFromElevator(int NumberOfTheFloor);
        void DeletePassenger(int NumberOfTheFloor);
        void UpdateMovedMass(int MassIncrease);
        void UpdateTime(double NewTime);
        void IncreaseRides();
        void IncreaseIddleRides();
        void ActivateOverWeight();
        void DeactivateOverWeight();
        event Action SetUp;
        event Action<int> AddButtonClicked;
        event Action StartSimulation;
        event Action StopSimulation;
        event Action<PassengersInfoView> PassengersInfoShown;
    }
}
