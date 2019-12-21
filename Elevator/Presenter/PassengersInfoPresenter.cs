using Elevator.Model;
using Ninject;
using System.Collections.Generic;

namespace Elevator.Presenter
{
    class PassengersInfoPresenter : IPresenter
    {
        private readonly IKernel _kernel;
        private readonly IPassengersInfoView _view;
        private readonly IMainWindowService _service;

        public PassengersInfoPresenter(IKernel kernel, IPassengersInfoView view, IMainWindowService service)
        {
            _kernel = kernel;
            _view = view;
            _service = service;

            _service.PassengersInfoUpdated += PassengersInfoUpdated;
        }

        private void PassengersInfoUpdated(List<Passenger> Passengers)
        {
            if (this._view != null)
            {
                _view.ViewPassengersInfo(Passengers);
            }
        }

        public void Run()
        {
            _view.Show();
            _view.ViewPassengersInfo(_service.Passengers);
        }

        public void Close()
        {
            _view.Close();
        }
    }
}
