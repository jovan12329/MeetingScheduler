using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meeting_Scheduler.ViewModels
{
    public class EmployeeCalendarViewModel:ViewModelBase
    {

        private readonly NavigationUtility _navigationService;
        public string session;

        public ICommand Back { get; set; }


        public EmployeeCalendarViewModel(NavigationUtility ns, string session)
        {
            _navigationService = ns;
            this.session = session;
            Back = new BackEmployeeCalendarCommand(this._navigationService, session);
        }


    }
}
