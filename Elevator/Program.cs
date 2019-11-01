using Elevator.Model;
using Elevator.Presenter;
using Elevator.View;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elevator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Ninject.StandardKernel kernel = new StandardKernel();
            kernel.Bind<ApplicationContext>().ToConstant(new ApplicationContext());
            kernel.Bind<ISetUpView>().To<SetUpView>();
            kernel.Bind<IMainWindowView>().To<MainWindowView>();
            kernel.Bind<SetUpPresenter>().ToSelf();
            kernel.Bind<MainWindowPresenter>().ToSelf();
            kernel.Bind<IMainWindowService>().To<MainWindowService>().InSingletonScope();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            kernel.Get<SetUpPresenter>().Run();
            Application.Run(kernel.Get<ApplicationContext>());
        }
    }
}
