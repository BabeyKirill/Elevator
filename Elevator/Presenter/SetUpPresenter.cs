using Elevator.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Presenter
{
    class SetUpPresenter : IPresenter
    {
        private readonly IKernel _kernel;
        private readonly ISetUpView _view;
        private readonly IMainWindowService _service;
        public SetUpPresenter(IKernel kernel, ISetUpView view, IMainWindowService service)
        {
            _kernel = kernel;
            _view = view;
            _service = service;

            _view.SetUp += () => SetUp(_view.NumberOfFloors);
        }

        private void SetUp(int numberOfFloors)
        {
            _service.NumberOfFloors = numberOfFloors;
            _kernel.Get<MainWindowPresenter>().Run();
            _view.Close();
        }

        public void Run()
        {
            _view.Show();
        }
    }
}
