﻿using Meeting_Scheduler.Common;
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
    public class ScheduleNavigationCommand : CommandBase
    {

        private readonly NavigationUtility navigationService;
        private readonly string model;
        private ILogger logger= new EventViewLogger();
        public ScheduleNavigationCommand(NavigationUtility ns, string u)
        {

            this.navigationService = ns;
            this.model = u;


        }

        public override void Execute(object parameter)
        {
            logger.Log("Navigating to the appointment view!", System.Diagnostics.EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new AppointmentViewModel(navigationService, model); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }




    }
}
