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

            _view.SetUp += SetUp;
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
