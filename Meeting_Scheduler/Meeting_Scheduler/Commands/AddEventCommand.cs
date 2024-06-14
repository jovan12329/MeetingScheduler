using log4net.Core;
using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using ILogger = Meeting_Scheduler.Common.ILogger;

namespace Meeting_Scheduler.Commands
{
    public class AddEventCommand:CommandBase
    {

        private NavigationUtility navigation;
        private EventViewModel model;
        private SpecialEventRepository repo = new SpecialEventRepository();
        private ILogger logger = new EventViewLogger();
        public AddEventCommand(EventViewModel loginViewModel, NavigationUtility ns)
        {

            this.model = loginViewModel;
            this.navigation = ns;


        }

        public override void Execute(object parameter)
        {

            if (string.IsNullOrEmpty(model.Name)) { logger.Log("Empty field name!", EventLogEntryType.Warning); MessageBox.Show("Enter reason!"); return; }
            if (string.IsNullOrEmpty(model.Type)) { logger.Log("Empty field type!", EventLogEntryType.Warning); MessageBox.Show("Choose type!"); return; }
            if (string.IsNullOrEmpty(model.StartDate)) { logger.Log("Empty field start date!", EventLogEntryType.Warning); MessageBox.Show("Enter start date!"); return; }
            if (string.IsNullOrEmpty(model.EndDate)) { logger.Log("Empty field end date!", EventLogEntryType.Warning); MessageBox.Show("Enter end date!"); return; }
            if (string.IsNullOrEmpty(model.Description)) { logger.Log("Empty field description!", EventLogEntryType.Warning); MessageBox.Show("Enter description!"); return; }


            string pattern = @"^(\d{1,2})-(\bJanuary\b|\bFebruary\b|\bMarch\b|\bApril\b|\bMay\b|\bJune\b|\bJuly\b|\bAugust\b|\bSeptember\b|\bOctober\b|\bNovember\b|\bDecember\b)-(\d{4})$";
            Regex r = new Regex(pattern);

            if (!r.IsMatch(model.StartDate)) { logger.Log("Wrong start date input!", EventLogEntryType.Warning); MessageBox.Show("Wrong date input for start date(d-MMMM-yyyy)!"); return; }
            if (!r.IsMatch(model.EndDate)) { logger.Log("Wrong end date input", EventLogEntryType.Warning); MessageBox.Show("Wrong date input for end date(d-MMMM-yyyy)!"); return; }


            string start = model.StartDate;
            string end = model.EndDate;

            string dateFormats = "d-MMMM-yyyy";


            CultureInfo ciCurr = new CultureInfo("en-US");

            DateTime datumStart = DateTime.ParseExact(start, dateFormats, ciCurr);
            DateTime datumEnd = DateTime.ParseExact(end, dateFormats, ciCurr);




            DateTime reqSt = new DateTime(datumStart.Year, datumStart.Month, datumStart.Day, 0, 0, 0);
            DateTime reqEn = new DateTime(datumEnd.Year, datumEnd.Month, datumEnd.Day, 0, 0, 0);

            if (reqSt >= reqEn) { logger.Log("Start date can't be bigger than end date!", EventLogEntryType.Warning); MessageBox.Show("Start date can't be bigger or equal End date!"); return; }

            if (reqSt < DateTime.Now) { logger.Log("Date before current date entered!", EventLogEntryType.Warning); MessageBox.Show("You cannot request seeking leave in this date!"); return; }


            SpecialEvent s = new SpecialEvent(model.Id, model.Name, model.Type, reqSt, reqEn, model.Description);

            repo.AddEvent(s);

            logger.Log("Event added!", EventLogEntryType.Information);

            logger.Log("Navigating to admin view!", EventLogEntryType.Information);

            this.navigation.CreateViewModel(() => { return new AdminViewModel(this.navigation, model.Id); });
            this.navigation.Navigate();


        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }


    }
}
