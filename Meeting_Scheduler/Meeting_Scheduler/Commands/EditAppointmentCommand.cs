using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Meeting_Scheduler.Commands
{
    public class EditAppointmentCommand:CommandBase
    {

        private NavigationUtility navigation;
        private EditAppointmentViewModel model;
        private AppointmentRepository appointments = new AppointmentRepository();
        private UserRepository repo = new UserRepository();
        private ILogger logger= new FileLogger(typeof(EditAppointmentCommand));


        public EditAppointmentCommand(EditAppointmentViewModel loginViewModel, NavigationUtility ns)
        {

            this.model = loginViewModel;
            this.navigation = ns;


        }

        public override void Execute(object parameter)
        {

            if (string.IsNullOrEmpty(model.Name)) { logger.Log("Empty field name !",System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter name!"); return; }
            if (string.IsNullOrEmpty(model.Date)) { logger.Log("Empty field date !", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter date of meeting!"); return; }
            if (string.IsNullOrEmpty(model.StartTime)) { logger.Log("Empty field start time !", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter start time!"); return; }
            if (string.IsNullOrEmpty(model.EndTime)) { logger.Log("Empty field end time !", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter end time!"); return; }
            if (string.IsNullOrEmpty(model.Type)) { logger.Log("Empty field type !", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Choose type!"); return; }
            if (model.Employee == null || model.Employee.ToList().Count == 0) { logger.Log("No chosen attendance!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Choose attendances!"); return; }
            string pattern = @"^(\d{1,2})-(\bJanuary\b|\bFebruary\b|\bMarch\b|\bApril\b|\bMay\b|\bJune\b|\bJuly\b|\bAugust\b|\bSeptember\b|\bOctober\b|\bNovember\b|\bDecember\b)-(\d{4})$";
            Regex r = new Regex(pattern);
            if (!r.IsMatch(model.Date)) { logger.Log("Wrong date input !", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Wrong date input(d-MMMM-yyyy)!"); return; }

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

            if (st > ende) { logger.Log("Start Time can't be bigger than End Time!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Start Time can't be bigger than End Time!"); return; }

            if (st < DateTime.Now) { logger.Log("You cannot appoint meeting in this date!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("You cannot appoint meeting in this date!"); return; }



            Appointment a = appointments.GetAppointmentsByDateStartFinish(st, ende);

            if (a != null)
            {
                logger.Log("Meeting has already been arranged by this date!", System.Diagnostics.EventLogEntryType.Warning);
                MessageBox.Show("Meeting has already been arranged by this date!");
                return;

            }

            List<Appointment> aps = appointments.AppointmentsByDateStartFinish(model._a.StartTime.ToLocalTime(),model._a.EndTime.ToLocalTime());

            aps.ForEach(k => {
  
                appointments.CancelAppointment(k);

            });


            foreach (var abc in model.Employee)
            {
                a = new Appointment(model.Id, abc, model.Type, model.Name, st, ende);
                appointments.AddApointment(a);
                logger.Log("Appointment successfully edited!", System.Diagnostics.EventLogEntryType.Information);
            }

            if (repo.GetByUsername(model.Id).PartitionKey.Equals("ADMINISTRATOR"))
            {
                logger.Log("Navigating to admin view!",System.Diagnostics.EventLogEntryType.Information);
                this.navigation.CreateViewModel(() => { return new AdminViewModel(this.navigation, model.Id); });
                this.navigation.Navigate();
                return;
            }

            logger.Log("Navigating to employee view!", System.Diagnostics.EventLogEntryType.Information);
            this.navigation.CreateViewModel(() => { return new EmployeeViewModel(this.navigation, model.Id); });
            this.navigation.Navigate();




        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }




    }
}
