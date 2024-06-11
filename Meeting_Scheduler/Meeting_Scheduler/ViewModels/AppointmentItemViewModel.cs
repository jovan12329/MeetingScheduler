using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meeting_Scheduler.ViewModels
{
    public class AppointmentItemViewModel:ViewModelBase
    {


        public Appointment _a;
        private readonly NavigationUtility _navigationService;

        public string Session { get; set; }
        public string Id => _a.RowKey;

        public string Name => _a.Name;
        public string Start => _a.StartTime.ToLocalTime().ToString();
        public string End => _a.EndTime.ToLocalTime().ToString();


        public ICommand EditAppointment { get; set; }

        public ICommand CancelAppointment { get; set; }

        public AppointmentItemViewModel(NavigationUtility ns, Appointment a, string session)
        {
            _navigationService = ns;
            _a = a;
            Session = session;
            CancelAppointment = new CancelAppointmentCommand(this, this._navigationService, Session);
            EditAppointment = new EditAppointmentNavigationCommand(_navigationService, Session, _a);
        }


    }
}
