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
    public class AdminCalendarViewModel:ViewModelBase
    {

        
        private readonly NavigationUtility _navigationService;
        public string session;

        public ICommand Back { get; set; }
        

        public AdminCalendarViewModel(NavigationUtility ns, string session)
        {
            _navigationService = ns;
            this.session = session;
            Back = new BackAdminCalendarCommand(this._navigationService, session);
        }



    }
}
