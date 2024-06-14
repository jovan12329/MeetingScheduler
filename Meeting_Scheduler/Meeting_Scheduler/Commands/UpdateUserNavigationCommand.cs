using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Entities;
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
    public class UpdateUserNavigatonCommand : CommandBase
    {


        private readonly NavigationUtility navigationService;
        private User id;
        private ILogger logger = new FileLogger(typeof(UpdateUserNavigatonCommand));
        public UpdateUserNavigatonCommand(NavigationUtility ns, User id)
        {

            this.navigationService = ns;
            this.id = id;

        }

        public override void Execute(object parameter)
        {
            logger.Log("Navigating to the update user view !", System.Diagnostics.EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new UpdateUserViewModel(navigationService, id); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }


    }
}
