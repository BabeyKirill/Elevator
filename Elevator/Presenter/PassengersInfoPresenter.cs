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
    class PassengersInfoPresenter : IPresenter
    {
        private readonly IKernel _kernel;
        private readonly IPassengersInfoView _viewInfo;
        private readonly IMainWindowView _view;
        private readonly IMainWindowService _service;
        public int i = 0;
        public PassengersInfoPresenter(IKernel kernel, IMainWindowView view, IPassengersInfoView viewInfo, IMainWindowService service)
        {
            _kernel = kernel;
            _view = view;
            _viewInfo = viewInfo;
            _service = service;

            _service.PassengersInfoUpdated += PassengersInfoUpdated;
        }

        void PassengersInfoUpdated(List<Passenger> Passengers)
        {
            if (this._viewInfo != null)
            {
                _viewInfo.ViewPassengersInfo(Passengers);
            }
        }

        public void Run()
        {
            i++;
            _viewInfo.Show();
            _viewInfo.ViewPassengersInfo(_service.Passengers);
            Console.WriteLine(i);
        }

        public void Close()
        {
            _viewInfo.Close();
        }
    }
}
