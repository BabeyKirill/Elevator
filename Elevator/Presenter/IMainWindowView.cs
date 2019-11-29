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
        event Action SetUp;
        event Action<int> AddButtonClicked;
        event Action StartSimulation;
        event Action StopSimulation;
    }
}
