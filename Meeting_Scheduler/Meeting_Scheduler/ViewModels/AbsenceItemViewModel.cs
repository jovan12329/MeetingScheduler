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
    public class AbsenceItemViewModel:ViewModelBase
    {

        public AbsenceRequest _a;
        private readonly NavigationUtility _navigationService;

        public string Session { get; set; }
        public string Id => _a.RowKey;
        public string Type => _a.AbsenceType;
        public string Reason => _a.Reason;
        public string Username => _a.Username;
        public string Start => _a.StartDate.ToLocalTime().ToString();
        public string End => _a.EndDate.ToLocalTime().ToString();


        public ICommand Approve { get; set; }

        public ICommand Deny { get; set; }

        public AbsenceItemViewModel(NavigationUtility ns, AbsenceRequest a, string session)
        {
            _navigationService = ns;
            _a = a;
            Session = session;
            Deny = new DenyAbsenceCommand(this, _navigationService, Session);
            Approve = new ApproveAbsenceCommand(this, _navigationService, Session);
        }



    }
}
