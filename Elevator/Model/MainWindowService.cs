using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator.Model
{
    class MainWindowService : IMainWindowService
    {
        public bool IsSimulationStarted { get; set; }
        private int _numberOfFloors;
        public Elevator Elevator { get; set; }
        public List<Passenger> Passengers { get; set; }

        public int NumberOfFloors
        {
            get => _numberOfFloors;
            set
            {
                _numberOfFloors = value;
                this.Passengers = new List<Passenger>();
                this.Elevator = new Elevator(_numberOfFloors);
            }
        }

        public void StartSimulation()
        {
            this.IsSimulationStarted = true;
            for (int i = 0; i < Passengers.Count; i++)
            {
                Thread myThread = new Thread(new ParameterizedThreadStart(PassengerThread));
                myThread.Start(Passengers[i]);
            }
            Thread myThread2 = new Thread(new ParameterizedThreadStart(ElevatorThread));
            myThread2.Start(Elevator);
        }

        public void AddAPassenger(int NumberOfTheFloor)
        {            
            Passenger passenger = new Passenger(_numberOfFloors, NumberOfTheFloor);
            this.Passengers.Add(passenger);
            if (IsSimulationStarted == true)
            {
                Thread myThread = new Thread(new ParameterizedThreadStart(PassengerThread));
                myThread.Start(passenger);
            }
        }

        public event Action<int> OuterActiveFloorButtonActivated;
        public event Action<int> InnerActiveFloorButtonActivated;
        public event Action<int> ElevatorMoved;
        public event Action OverWeight;
        public event Action<int> PassengerEnterElevator;

        public void PassengerThread(object pas)
        {
            Thread.Sleep(4000);
            Passenger passenger = (Passenger)pas;
            //pas = (object)passenger;
            if (Elevator.OuterActiveFloorButtons[passenger.CurrentFloor - 1] == false)
            {
                Elevator.OuterActiveFloorButtons[passenger.CurrentFloor - 1] = true;
                OuterActiveFloorButtonActivated?.Invoke(passenger.CurrentFloor);
            }
            while(IsSimulationStarted)
            {
                if(Elevator.CurrentFloor == passenger.CurrentFloor && passenger.Status == PassengerStatus.WaitingForElevator)
                {
                    if (Elevator.CurrentWeight + passenger.Weight < Elevator.MaximumWeight)
                    {
                        if (Elevator.InnerActiveFloorButtons[passenger.WantedFloor - 1] == false)
                        {
                            Elevator.InnerActiveFloorButtons[passenger.WantedFloor - 1] = true;
                            InnerActiveFloorButtonActivated?.Invoke(passenger.WantedFloor);
                        }
                        passenger.Status = PassengerStatus.IsInElevator;
                        PassengerEnterElevator?.Invoke(passenger.CurrentFloor);
                    }
                    else
                    {
                        OverWeight?.Invoke();
                    }
                }
            }
        }

        public void ElevatorThread(object elev)
        {
            Thread.Sleep(4050);
            Elevator elevator = (Elevator)elev;
            while (this.IsSimulationStarted == true)
            {
                if (elevator.OuterActiveFloorButtons.Contains(true) && !elevator.InnerActiveFloorButtons.Contains(true))
                {
                    for (int i = NumberOfFloors; i > 0; i--)
                    {
                        if (elevator.OuterActiveFloorButtons[i-1] == true)
                        {
                            Thread.Sleep(5000);
                            elevator.CurrentFloor = i;
                            elev = elevator;
                            elevator.InnerActiveFloorButtons[i - 1] = false;
                            elevator.OuterActiveFloorButtons[i - 1] = false;
                            ElevatorMoved?.Invoke(i);
                            break;
                        }
                    }
                }
                else if (!elevator.OuterActiveFloorButtons.Contains(true) && elevator.InnerActiveFloorButtons[0] == true)
                {
                    Thread.Sleep(5000);
                    elevator.CurrentFloor = 1;
                    elevator.InnerActiveFloorButtons[0] = false;
                    elevator.OuterActiveFloorButtons[0] = false;
                    elev = elevator;
                    ElevatorMoved?.Invoke(1);
                }                
            }
        }
    }    
}
