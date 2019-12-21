using System;

namespace Elevator.Presenter
{
    interface ISetUpView : IView
    {
        int NumberOfFloors { get; }
        event Action SetUp;
    }
}
