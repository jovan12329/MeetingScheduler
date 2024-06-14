using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using Meeting_Scheduler.ViewModels;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.Common;

namespace Meeting_Scheduler.Commands
{
    public class AppointCommand : CommandBase
    {

        private NavigationUtility navigation;
        private AppointmentViewModel model;
        private AppointmentRepository appointments = new AppointmentRepository();
        private UserRepository repo = new UserRepository();
        private ILogger logger = new EventViewLogger();


        public AppointCommand(AppointmentViewModel loginViewModel, NavigationUtility ns)
        {

            this.model = loginViewModel;
            this.navigation = ns;


        }

        public override void Execute(object parameter)
        {

            if (string.IsNullOrEmpty(model.Name)) { logger.Log("Entered empty name field!",System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter name!"); return; }
            if (string.IsNullOrEmpty(model.Date)) { logger.Log("Entered empty date field!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter date of meeting!"); return; }
            if (string.IsNullOrEmpty(model.StartTime)) { logger.Log("Entered empty start time field!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter start time!"); return; }
            if (string.IsNullOrEmpty(model.EndTime)) { logger.Log("Entered empty end time field!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter end time!"); return; }
            if (string.IsNullOrEmpty(model.Type)) { logger.Log("Entered empty type field!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Choose type!"); return; }
            if (model.Employee == null || model.Employee.ToList().Count == 0) { logger.Log("Attendance wasn't chosen!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Choose attendances!"); return; }
            string pattern = @"^(\d{1,2})-(\bJanuary\b|\bFebruary\b|\bMarch\b|\bApril\b|\bMay\b|\bJune\b|\bJuly\b|\bAugust\b|\bSeptember\b|\bOctober\b|\bNovember\b|\bDecember\b)-(\d{4})$";
            Regex r = new Regex(pattern);
            if (!r.IsMatch(model.Date)) { logger.Log("Wrong date input!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Wrong date input(d-MMMM-yyyy)!"); return; }

            string date = model.Date;
            string start = model.StartTime;
            string end = model.EndTime;

            string dateFormats = "d-MMMM-yyyy";


            CultureInfo ciCurr = new CultureInfo("en-US");

            DateTime datum = DateTime.ParseExact(date, dateFormats, ciCurr);

            int sthours = int.Parse(start.Split(':')[0]);
            int enhours = int.Parse(end.Split(':')[0]);


            DateTime st = new DateTime(datum.Year, datum.Month, datum.Day, sthours, 0, 0);
            DateTime ende = new DateTime(datum.Year, datum.Month, datum.Day, enhours, 0, 0);

            if (st > ende) { logger.Log("Start date can't be bigger than end date!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Start Time can't be bigger than End Time!"); return; }

            if (st < DateTime.Now) { logger.Log("start date can't be less than current date!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("You cannot appoint meeting in this date!"); return; }



            Appointment a = appointments.GetAppointmentsByDateStartFinish(st, ende);

            if (a != null)
            {
                logger.Log("Meeting is alredy scheduled by this date!", System.Diagnostics.EventLogEntryType.Warning);
                MessageBox.Show("Meeting has already been arranged by this date!");
                return;

            }

            foreach (var abc in model.Employee)
            {
                a = new Appointment(model.Id, abc, model.Type, model.Name, st, ende);
                logger.Log("appointment is added!", System.Diagnostics.EventLogEntryType.Information);
                appointments.AddApointment(a);
            }

            if (repo.GetByUsername(model.Id).PartitionKey.Equals("ADMINISTRATOR"))
            {
                logger.Log("Navigating to the admin view!",System.Diagnostics.EventLogEntryType.Information);
                this.navigation.CreateViewModel(() => { return new AdminViewModel(this.navigation, model.Id); });
                this.navigation.Navigate();
                return;
            }

            logger.Log("Navigating to the employee view!", System.Diagnostics.EventLogEntryType.Information);
            this.navigation.CreateViewModel(() => { return new EmployeeViewModel(this.navigation, model.Id); });
            this.navigation.Navigate();




        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }





    }
}
