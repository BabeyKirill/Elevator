using Elevator.Model;
using Elevator.Presenter;
using Elevator.View;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elevator
{
    static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (AllocConsole())
            {
                Ninject.StandardKernel kernel = new StandardKernel();
                kernel.Bind<ApplicationContext>().ToConstant(new ApplicationContext());
                kernel.Bind<ISetUpView>().To<SetUpView>();
                kernel.Bind<IMainWindowView>().To<MainWindowView>();
                kernel.Bind<IPassengersInfoView>().To<PassengersInfoView>();
                kernel.Bind<SetUpPresenter>().ToSelf();
                kernel.Bind<MainWindowPresenter>().ToSelf();
                kernel.Bind<PassengersInfoPresenter>().ToSelf();
                kernel.Bind<IMainWindowService>().To<MainWindowService>().InSingletonScope();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                kernel.Get<SetUpPresenter>().Run();
                Application.Run(kernel.Get<ApplicationContext>());
                FreeConsole();
            }
        }
    }
}
