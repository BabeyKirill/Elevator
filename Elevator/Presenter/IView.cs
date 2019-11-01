using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Presenter
{
    interface IView
    {
        void Show();
        void Close();
    }

    public static class Delegates
    {
        public delegate void CallElevator(int floor);
    }
}
