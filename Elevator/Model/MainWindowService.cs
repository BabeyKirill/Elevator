using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Model
{
    class MainWindowService : IMainWindowService
    {
        private decimal _numberOfFloors;

        public decimal NumberOfFloors
        {
            get => _numberOfFloors;
            set
            {
                _numberOfFloors = value;
            }
        }
    }
}
