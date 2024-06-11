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


        public EditAppointmentCommand(EditAppointmentViewModel loginViewModel, NavigationUtility ns)
        {

            this.model = loginViewModel;
            this.navigation = ns;


        }

        public override void Execute(object parameter)
        {

            if (string.IsNullOrEmpty(model.Name)) { MessageBox.Show("Enter name!"); return; }
            if (string.IsNullOrEmpty(model.Date)) { MessageBox.Show("Enter date of meeting!"); return; }
            if (string.IsNullOrEmpty(model.StartTime)) { MessageBox.Show("Enter start time!"); return; }
            if (string.IsNullOrEmpty(model.EndTime)) { MessageBox.Show("Enter end time!"); return; }
            if (string.IsNullOrEmpty(model.Type)) { MessageBox.Show("Choose type!"); return; }
            if (model.Employee == null || model.Employee.ToList().Count == 0) { MessageBox.Show("Choose attendances!"); return; }
            string pattern = @"^(\d{1,2})-(\bJanuary\b|\bFebruary\b|\bMarch\b|\bApril\b|\bMay\b|\bJune\b|\bJuly\b|\bAugust\b|\bSeptember\b|\bOctober\b|\bNovember\b|\bDecember\b)-(\d{4})$";
            Regex r = new Regex(pattern);
            if (!r.IsMatch(model.Date)) { MessageBox.Show("Wrong date input(d-MMMM-yyyy)!"); return; }

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

            if (st > ende) { MessageBox.Show("Start Time can't be bigger than End Time!"); return; }

            if (st < DateTime.Now) { MessageBox.Show("You cannot appoint meeting in this date!"); return; }



            Appointment a = appointments.GetAppointmentsByDateStartFinish(st, ende);

            if (a != null)
            {
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
            }

            if (repo.GetByUsername(model.Id).PartitionKey.Equals("ADMINISTRATOR"))
            {
                this.navigation.CreateViewModel(() => { return new AdminViewModel(this.navigation, model.Id); });
                this.navigation.Navigate();
                return;
            }

            this.navigation.CreateViewModel(() => { return new EmployeeViewModel(this.navigation, model.Id); });
            this.navigation.Navigate();




        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }




    }
}
