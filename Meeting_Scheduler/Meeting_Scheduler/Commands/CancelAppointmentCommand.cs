﻿using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Commands
{
    public class CancelAppointmentCommand:CommandBase
    {

        private readonly NavigationUtility navigationService;
        private AppointmentItemViewModel viewModel;
        private readonly UserRepository userRepository = new UserRepository();
        private readonly AppointmentRepository pointRepository = new AppointmentRepository();
        private string admin;
        private ILogger logger = new FileLogger(typeof(CancelAppointmentCommand));
        public CancelAppointmentCommand(AppointmentItemViewModel vm, NavigationUtility ns, string a)
        {
            this.viewModel = vm;
            this.navigationService = ns;
            this.admin = a;


        }

        public override void Execute(object parameter)
        {
            List<Appointment> cen = pointRepository.AppointmentsByDateStartFinish(viewModel._a.StartTime.ToLocalTime(), viewModel._a.EndTime.ToLocalTime());

            cen.ForEach(c => { logger.Log("Appointment cancelled!",System.Diagnostics.EventLogEntryType.Information); pointRepository.CancelAppointment(c); });

            logger.Log("Navigating to the employee view!",System.Diagnostics.EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new EmployeeViewModel(navigationService, this.admin); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }





    }
}
