using log4net.Core;
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
    public class RequestSeekingCommand:CommandBase
    {


        private NavigationUtility navigation;
        private SeekingLeaveViewModel model;
        private SeekingLeaveRepository repo= new SeekingLeaveRepository();
        private Common.ILogger logger = new FileLogger(typeof(RequestSeekingCommand));

        public RequestSeekingCommand(SeekingLeaveViewModel loginViewModel, NavigationUtility ns)
        {

            this.model = loginViewModel;
            this.navigation = ns;


        }

        public override void Execute(object parameter)
        {

            if (string.IsNullOrEmpty(model.Reason)) { logger.Log("Empty field reason!",System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter reason!"); return; }
            if (string.IsNullOrEmpty(model.StartDate)) { logger.Log("Empty field start date!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter start date!"); return; }
            if (string.IsNullOrEmpty(model.EndDate)) { logger.Log("Empty field end date!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter end date!"); return; }
            if (string.IsNullOrEmpty(model.Description)) { logger.Log("Empty field description!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Enter description!"); return; }

            
            string pattern = @"^(\d{1,2})-(\bJanuary\b|\bFebruary\b|\bMarch\b|\bApril\b|\bMay\b|\bJune\b|\bJuly\b|\bAugust\b|\bSeptember\b|\bOctober\b|\bNovember\b|\bDecember\b)-(\d{4})$";
            Regex r = new Regex(pattern);

            if (!r.IsMatch(model.StartDate)) { logger.Log("Wrong start date input!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Wrong date input for start date(d-MMMM-yyyy)!"); return; }
            if (!r.IsMatch(model.EndDate)) { logger.Log("Wrong end date input!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Wrong date input for end date(d-MMMM-yyyy)!"); return; }

            
            string start = model.StartDate;
            string end = model.EndDate;

            string dateFormats = "d-MMMM-yyyy";


            CultureInfo ciCurr = new CultureInfo("en-US");

            DateTime datumStart = DateTime.ParseExact(start, dateFormats, ciCurr);
            DateTime datumEnd = DateTime.ParseExact(end, dateFormats, ciCurr);

           


            DateTime reqSt = new DateTime(datumStart.Year, datumStart.Month, datumStart.Day, 0, 0, 0);
            DateTime reqEn = new DateTime(datumEnd.Year, datumEnd.Month, datumEnd.Day, 0, 0, 0);

            if (reqSt >= reqEn) { logger.Log("Start date is bigger than end date !", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Start date can't be bigger or equal End date!"); return; }

            if (reqSt < DateTime.Now) { logger.Log("Start date can't be less than current date!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("You cannot request seeking leave in this date!"); return; }


            SeekingLeave s = new SeekingLeave(model.Id,model.Reason,reqSt,reqEn,model.Description);

            repo.AddSeek(s);

            logger.Log("Seeking leave added successfully !", System.Diagnostics.EventLogEntryType.Information);

            logger.Log("Navigating to the Employee view !", System.Diagnostics.EventLogEntryType.Information);
            this.navigation.CreateViewModel(() => { return new EmployeeViewModel(this.navigation, model.Id); });
            this.navigation.Navigate();


        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }





    }
}
