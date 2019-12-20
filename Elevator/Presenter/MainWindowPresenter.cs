using Elevator.Model;
using Elevator.View;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Presenter
{
    class MainWindowPresenter : IPresenter
    {
        private readonly IKernel _kernel;
        private readonly IMainWindowView _view;
        private readonly IMainWindowService _service;
        private IPassengersInfoView _info;
        public MainWindowPresenter(IKernel kernel, IMainWindowView view, IMainWindowService service)
        {
            _kernel = kernel;
            _view = view;
            _service = service;
            _view.AddButtonClicked += AddButtonClicked;
            _view.SetUp += SetUp;
            _service.OuterActiveFloorButtonActivated += OuterActiveFloorButtonActivated;
            _view.StartSimulation += StartSimulation;
            _view.StopSimulation += StopSimulation;
            _service.ElevatorMoved += ElevatorMoved;
            _service.InnerActiveFloorButtonActivated += InnerActiveFloorButtonActivated;
            _service.PassengerEnterElevator += PassengerEnterElevator;
            _service.PassengerOutFromElevator += PassengerOutFromElevator;
            _service.PassengerDisappeared += PassengerDisappeared;
            _service.TotalMovedMassIncreased += TotalMovedMassIncreased;
            _service.TimeUpdated += UpdateTime;
            _service.ElevatorMadeRide += ElevatorMadeRide;
            _service.ElevatorMadeIddleRide += ElevatorMadeIddleRide;
            _service.OverWeightActivated += OverWeightActivated;
            _service.OverWeightDeactivated += OverWeightDeactivated;
            _view.PassengersInfoShown += PassengersInfoShown;
            _service.PassengersInfoUpdated += PassengersInfoUpdated;
        }
        void PassengersInfoUpdated(List<Passenger> Passengers)
        {
            if (this._info != null)
            {
                _info.ViewPassengersInfo(Passengers);
            }
        }

        void PassengersInfoShown(PassengersInfoView PasInfo)
        {
            this._info = PasInfo;
            _info.ViewPassengersInfo(_service.Passengers);
        }

        public void OverWeightActivated()
        {
            _view.ActivateOverWeight();
        }

        public void OverWeightDeactivated()
        {
            _view.DeactivateOverWeight();
        }

        public void ElevatorMadeIddleRide()
        {
            _view.IncreaseIddleRides();
        }

        public void ElevatorMadeRide()
        {
            _view.IncreaseRides();
        }

        private void UpdateTime(double NewTime)
        {
            _view.UpdateTime(NewTime);
        }

        private void TotalMovedMassIncreased(int MassIncrease)
        {
            _view.UpdateMovedMass(MassIncrease);
        }

        private void PassengerDisappeared(int NumberOfTheFloor)
        {
            _view.DeletePassenger(NumberOfTheFloor);
        }

        private void PassengerOutFromElevator(int NumberOfTheFloor)
        {
            _view.MovePassengerFromElevator(NumberOfTheFloor);
        }

        private void PassengerEnterElevator(int NumberOfTheFloor)
        {
            _view.MovePassengerInElevator(NumberOfTheFloor);
        }

        private void InnerActiveFloorButtonActivated(int NumberOfTheFloor)
        {
            _view.ActivateInnerActiveFloorCheckBox(NumberOfTheFloor);
        }

        private void ElevatorMoved(int ElevatorNewFloor)
        {
            _view.MoveElevator(ElevatorNewFloor);
        }

        private void StartSimulation()
        {
            _service.TimerStart();
        }

        private void StopSimulation()
        {
            _service.TimerStop();
        }

        private void OuterActiveFloorButtonActivated(int NumberOfTheFloor)
        {
            _view.ActivateOuterActiveFloorCheckBox(NumberOfTheFloor);
        }
        private void AddButtonClicked(int NumberOfTheFloor)
        {
            _view.AddAPassenger(NumberOfTheFloor);           
            _service.AddAPassenger(NumberOfTheFloor);            
        }

        private void SetUp()
        {
            _kernel.Get<SetUpPresenter>().Run();
            _view.Close();
        }

        public void Run()
        {
            _view.SetNumberOfFloors(_service.NumberOfFloors);
            _view.Show();
        }
    }
}
