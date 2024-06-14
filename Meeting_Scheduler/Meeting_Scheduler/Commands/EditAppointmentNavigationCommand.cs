using log4net.Core;
using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Commands
{
    public class EditAppointmentNavigationCommand:CommandBase
    {
        private readonly NavigationUtility navigationService;
        private readonly string model;
        private Appointment a;
        private Common.ILogger logger = new FileLogger(typeof(EditAppointmentNavigationCommand));
        public EditAppointmentNavigationCommand(NavigationUtility ns, string u,Appointment a)
        {

            this.navigationService = ns;
            this.model = u;
            this.a = a;

        }

        public override void Execute(object parameter)
        {
            logger.Log("Navigating to the edit appointment view!",System.Diagnostics.EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new EditAppointmentViewModel(navigationService,a, model); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }


    }
}
