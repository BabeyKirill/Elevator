using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Presenter
{
    interface IMainWindowView : IView
    {
        void SetNumberOfFloors(decimal n);
        event Action SetUp;
    }
}
