using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Model
{
    interface ITimer
    {
        event EventHandler Tick;

        bool Enabled { get; set; }
        int Interval { get; set; }

        void Start();
        void Stop();
    }
}
