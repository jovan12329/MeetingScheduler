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
    public class AddNavigateCommand:CommandBase
    {
        private readonly NavigationUtility navigationService;
        private string id;
        private ILogger logger = new EventViewLogger();
        public AddNavigateCommand(NavigationUtility ns, string id)
        {

            this.navigationService = ns;
            this.id = id;


        }

        public override void Execute(object parameter)
        {
            logger.Log("Navigating to the add user view!",System.Diagnostics.EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new AddUserViewModel(navigationService, this.id); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }

    }
}
