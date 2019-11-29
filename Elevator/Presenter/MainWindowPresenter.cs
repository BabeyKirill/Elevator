using Elevator.Model;
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

        public MainWindowPresenter(IKernel kernel, IMainWindowView view, IMainWindowService service)
        {
            _kernel = kernel;
            _view = view;
            _service = service;
            _view.AddButtonClicked += AddButtonClicked;
            _view.SetUp += SetUp;
            _service.OuterActiveFloorButtonActivated += OuterActiveFloorButtonActivated;
            _view.StartSimulation += StartSimulation;
            _service.ElevatorMoved += ElevatorMoved;
            _service.InnerActiveFloorButtonActivated += InnerActiveFloorButtonActivated;
            _service.PassengerEnterElevator += PassengerEnterElevator;
            _service.PassengerOutFromElevator += PassengerOutFromElevator;
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
            _service.StartSimulation();
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
