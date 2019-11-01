using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Presenter
{
    interface ISetUpView : IView
    {
        decimal NumberOfFloors { get; }
        event Action SetUp;
    }
}
