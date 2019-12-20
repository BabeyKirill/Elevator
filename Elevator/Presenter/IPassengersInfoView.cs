using Elevator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Presenter
{
    interface IPassengersInfoView: IView
    {
        void ViewPassengersInfo(List<Passenger> Passengers);
    }
}
