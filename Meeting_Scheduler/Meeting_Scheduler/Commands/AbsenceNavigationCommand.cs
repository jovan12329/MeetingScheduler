using log4net.Core;
using Meeting_Scheduler.Common;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Commands
{
    public class AbsenceNavigationCommand:CommandBase
    {

        private readonly NavigationUtility navigationService;
        private string id;
        private Common.ILogger logger = new FileLogger(typeof(AbsenceNavigationCommand));
        public AbsenceNavigationCommand(NavigationUtility ns, string id)
        {

            this.navigationService = ns;
            this.id = id;


        }

        public override void Execute(object parameter)
        {
            logger.Log("Navigating to absence view model of employee!",EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new AbsenceViewModel(navigationService, this.id); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }



    }
}
