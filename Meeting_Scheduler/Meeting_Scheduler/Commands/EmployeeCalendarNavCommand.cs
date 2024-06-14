﻿using Meeting_Scheduler.Common;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Commands
{
    public class EmployeeCalendarNavCommand:CommandBase
    {

        private readonly NavigationUtility navigationService;
        private string id;
        private ILogger logger = new FileLogger(typeof(EmployeeCalendarNavCommand));
        public EmployeeCalendarNavCommand(NavigationUtility ns, string id)
        {

            this.navigationService = ns;
            this.id = id;


        }

        public override void Execute(object parameter)
        {
            logger.Log("Navigating to the calendar employee view!", System.Diagnostics.EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new EmployeeCalendarViewModel(navigationService, this.id); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }




    }
}
