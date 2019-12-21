using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Elevator.Model
{
    class MainWindowService : IMainWindowService
    {
        public bool IsSimulationStarted { get; set; }
        private int _numberOfFloors;
        private readonly Timer _timer;
        private double time;
        private double PassengersTime;
        private double ElevatorTime;
        public Elevator Elevator { get; set; }
        public List<Passenger> Passengers { get; set; }
        private bool OverWeight = false;

        public event Action<int> OuterActiveFloorButtonActivated;
        public event Action<int> InnerActiveFloorButtonActivated;
        public event Action<int> ElevatorMoved;
        public event Action ElevatorMadeRide;
        public event Action ElevatorMadeIddleRide;
        public event Action<int> TotalMovedMassIncreased;
        public event Action OverWeightActivated;
        public event Action<int> PassengerEnterElevator;
        public event Action<int> PassengerOutFromElevator;
        public event Action<int> PassengerDisappeared;
        public event Action<double> TimeUpdated;
        public event Action OverWeightDeactivated;
        public event Action<List<Passenger>> PassengersInfoUpdated;

        public MainWindowService()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += TimerTick;
        }

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

        public void TimerStart()
        {    
            this.time = 0;
            this.ElevatorTime = 2;
            this.PassengersTime = 0;
            this._timer.Start();
        }

        public void TimerStop()
        {
            this.Passengers.Clear();
            for (int i = 0; i < NumberOfFloors; i++)
            {
                this.Elevator.InnerActiveFloorButtons[i] = false;
                this.Elevator.OuterActiveFloorButtons[i] = false;
            }
            this.Elevator.CurrentFloor = 1;
            this.Elevator.MoveDirection = ElevatorMoveDirection.Undefined;
            PassengersInfoUpdated?.Invoke(this.Passengers);
            this._timer.Stop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            time = time + 1;
            TimeUpdated?.Invoke(time);

            if(time >= ElevatorTime + 4)
            {
                if (OverWeight == true)
                {
                    OverWeight = false;
                    OverWeightDeactivated?.Invoke();
                }
                ElevatorTime = time;
                Console.WriteLine($"ElevatorCurrentFloor: {Elevator.CurrentFloor}");
                Console.WriteLine($"ElevatorMoveDirection: {Elevator.MoveDirection}");
                Console.WriteLine($"ElevatorInnerButtonActive: {Elevator.InnerActiveFloorButtons.Contains(true)}");
                Console.WriteLine();
                ElevatorDoAction();
            }
            if (time >= PassengersTime + 4)
            {
                PassengersTime = time;

                while(true)
                {
                    bool deleted = false;
                    foreach (Passenger pas in Passengers)
                    {
                        if (pas.Status == PassengerStatus.OutFromElevator)
                        {
                            PassengerDisappeared?.Invoke(pas.CurrentFloor);
                            Passengers.Remove(pas);
                            deleted = true;
                            PassengersInfoUpdated?.Invoke(this.Passengers);
                            break;
                        }
                    }
                    if(deleted == false)
                    {
                        break;
                    }
                }

                foreach(Passenger pas in Passengers)
                {
                    PassengerDoAction(pas);                  
                }
            }
        }

        private void PassengerDoAction(Passenger passenger)
        {
            if (passenger.Status == PassengerStatus.WaitingForElevator && Elevator.CurrentFloor == passenger.CurrentFloor)
            {
                if (Elevator.CurrentMass + passenger.Weight < Elevator.MaximumMass)
                {
                    passenger.Status = PassengerStatus.IsInElevator;
                    Elevator.CurrentMass += passenger.Weight;
                    PassengerEnterElevator?.Invoke(Elevator.CurrentFloor);
                    if (Elevator.InnerActiveFloorButtons[passenger.WantedFloor - 1] == false)
                    {
                        Elevator.InnerActiveFloorButtons[passenger.WantedFloor - 1] = true;
                        InnerActiveFloorButtonActivated?.Invoke(passenger.WantedFloor);
                    }
                    PassengersInfoUpdated?.Invoke(this.Passengers);
                }
                else
                {
                    OverWeight = true;
                    OverWeightActivated?.Invoke();
                }
            }
            else if (passenger.Status == PassengerStatus.WaitingForElevator && (Elevator.OuterActiveFloorButtons[passenger.CurrentFloor - 1] == false))
            {
                this.Elevator.OuterActiveFloorButtons[passenger.CurrentFloor - 1] = true;
                OuterActiveFloorButtonActivated?.Invoke(passenger.CurrentFloor);
            }
            else if (passenger.Status == PassengerStatus.IsInElevator && Elevator.CurrentFloor == passenger.WantedFloor)
            {
                passenger.Status = PassengerStatus.OutFromElevator;
                Elevator.CurrentMass -= passenger.Weight;
                PassengerOutFromElevator?.Invoke(Elevator.CurrentFloor);
                TotalMovedMassIncreased?.Invoke(passenger.Weight);
                PassengersInfoUpdated?.Invoke(this.Passengers);
            }
        }

        private void ElevatorDoAction()
        {
            if (!Elevator.InnerActiveFloorButtons.Contains(true) && Elevator.OuterActiveFloorButtons.Contains(true))
            {
                for (int i = NumberOfFloors; i > 0; i--)
                {
                    if (Elevator.OuterActiveFloorButtons[i - 1] == true)
                    {
                        if (i > Elevator.CurrentFloor)
                        {
                            if (Elevator.MoveDirection == ElevatorMoveDirection.Down)
                            {
                                ElevatorMadeRide?.Invoke();
                                ElevatorMadeIddleRide?.Invoke();
                            }
                            Elevator.MoveDirection = ElevatorMoveDirection.Up;
                        }
                        else if (i < Elevator.CurrentFloor)
                        {
                            if (Elevator.MoveDirection == ElevatorMoveDirection.Up)
                            {
                                ElevatorMadeRide?.Invoke();
                                ElevatorMadeIddleRide?.Invoke();
                            }
                            Elevator.MoveDirection = ElevatorMoveDirection.Down;
                        }
                        Elevator.CurrentFloor = i;
                        Elevator.OuterActiveFloorButtons[i - 1] = false;
                        Elevator.InnerActiveFloorButtons[i - 1] = false;
                        ElevatorMoved?.Invoke(i);
                        break;
                    }
                }
            }
            else if (Elevator.InnerActiveFloorButtons.Contains(true))
            {
                if (Elevator.MoveDirection == ElevatorMoveDirection.Undefined)
                {
                    for (int i = NumberOfFloors; i > Elevator.CurrentFloor; i--)
                    {
                        if (Elevator.InnerActiveFloorButtons[i - 1] == true)
                        {
                            Elevator.MoveDirection = ElevatorMoveDirection.Up;
                            break;
                        }
                    }
                    for (int i = 1; i < Elevator.CurrentFloor; i++)
                    {
                        if (Elevator.InnerActiveFloorButtons[i - 1] == true)
                        {
                            Elevator.MoveDirection = ElevatorMoveDirection.Down;
                            break;
                        }
                    }
                }

                if (Elevator.MoveDirection == ElevatorMoveDirection.Up)
                {
                    bool NeedTurn = true;
                    for (int i = Elevator.CurrentFloor; i <= NumberOfFloors; i++)
                    {
                        if (Elevator.InnerActiveFloorButtons[i - 1] == true)
                        {
                            Elevator.CurrentFloor = i;
                            Elevator.OuterActiveFloorButtons[i - 1] = false;
                            Elevator.InnerActiveFloorButtons[i - 1] = false;
                            foreach (Passenger passenger in Passengers)
                            {
                                if (passenger.Status == PassengerStatus.IsInElevator)
                                {
                                    passenger.CurrentFloor = Elevator.CurrentFloor;
                                    PassengersInfoUpdated?.Invoke(this.Passengers);
                                }
                            }
                            NeedTurn = false;
                            ElevatorMoved?.Invoke(i);
                            break;
                        }
                    }
                    if (NeedTurn == true)
                    {
                        Elevator.MoveDirection = ElevatorMoveDirection.Down;
                        ElevatorMadeRide?.Invoke();
                        for (int i = Elevator.CurrentFloor; i > 0; i--)
                        {
                            if (Elevator.InnerActiveFloorButtons[i - 1] == true || Elevator.OuterActiveFloorButtons[i - 1] == true)
                            {
                                Elevator.CurrentFloor = i;
                                Elevator.OuterActiveFloorButtons[i - 1] = false;
                                Elevator.InnerActiveFloorButtons[i - 1] = false;
                                foreach (Passenger passenger in Passengers)
                                {
                                    if (passenger.Status == PassengerStatus.IsInElevator)
                                    {
                                        passenger.CurrentFloor = Elevator.CurrentFloor;
                                        PassengersInfoUpdated?.Invoke(this.Passengers);
                                    }
                                }
                                ElevatorMoved?.Invoke(i);
                                break;
                            }
                        }
                    }
                }
                else if (Elevator.MoveDirection == ElevatorMoveDirection.Down)
                {
                    bool NeedTurn = true;
                    for (int i = Elevator.CurrentFloor; i > 0; i--)
                    {
                        if (Elevator.InnerActiveFloorButtons[i - 1] == true || Elevator.OuterActiveFloorButtons[i - 1] == true)
                        {
                            Elevator.CurrentFloor = i;
                            Elevator.OuterActiveFloorButtons[i - 1] = false;
                            Elevator.InnerActiveFloorButtons[i - 1] = false;
                            foreach (Passenger passenger in Passengers)
                            {
                                if (passenger.Status == PassengerStatus.IsInElevator)
                                {
                                    passenger.CurrentFloor = Elevator.CurrentFloor;
                                    PassengersInfoUpdated?.Invoke(this.Passengers);
                                }
                            }
                            NeedTurn = false;
                            ElevatorMoved?.Invoke(i);
                            break;
                        }
                    }
                    if (NeedTurn == true)
                    {
                        Elevator.MoveDirection = ElevatorMoveDirection.Up;
                        ElevatorMadeRide?.Invoke();
                        for (int i = Elevator.CurrentFloor; i <= NumberOfFloors; i++)
                        {
                            if (Elevator.InnerActiveFloorButtons[i - 1] == true)
                            {
                                Elevator.CurrentFloor = i;
                                Elevator.OuterActiveFloorButtons[i - 1] = false;
                                Elevator.InnerActiveFloorButtons[i - 1] = false;
                                foreach (Passenger passenger in Passengers)
                                {
                                    if (passenger.Status == PassengerStatus.IsInElevator)
                                    {
                                        passenger.CurrentFloor = Elevator.CurrentFloor;
                                        PassengersInfoUpdated?.Invoke(this.Passengers);
                                    }
                                }
                                NeedTurn = false;
                                ElevatorMoved?.Invoke(i);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void AddAPassenger(int NumberOfTheFloor)
        {            
            Passenger passenger = new Passenger(_numberOfFloors, NumberOfTheFloor);
            Passengers.Add(passenger);
            PassengersInfoUpdated?.Invoke(this.Passengers);
        }        
    }    
}
