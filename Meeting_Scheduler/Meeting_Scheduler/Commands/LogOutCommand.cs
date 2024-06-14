using Meeting_Scheduler.Common;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Meeting_Scheduler.Commands
{
    public class LogoutCommand:CommandBase
    {

        private readonly NavigationUtility navigationService;
        private ILogger logger = new EventViewLogger();
        public LogoutCommand(NavigationUtility ns)
        {

            this.navigationService = ns;


        }

        public override void Execute(object parameter)
        {
            logger.Log("Logging out...",System.Diagnostics.EventLogEntryType.Information);
            logger.Log("Navigating to the login view!",System.Diagnostics.EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new LoginViewModel(navigationService); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }


    }
}
