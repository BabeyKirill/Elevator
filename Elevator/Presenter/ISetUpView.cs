using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Presenter
{
    interface ISetUpView : IView
    {
        int NumberOfFloors { get; }
        event Action SetUp;
    }
}
