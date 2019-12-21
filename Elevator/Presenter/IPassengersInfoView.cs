using Elevator.Model;
using System.Collections.Generic;

namespace Elevator.Presenter
{
    interface IPassengersInfoView: IView
    {
        void ViewPassengersInfo(List<Passenger> Passengers);
    }
}
