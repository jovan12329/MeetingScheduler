using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Commands
{
    public class AbsenceApproveNavigationCommand:CommandBase
    {


        private readonly NavigationUtility navigationService;
        private string id;
        public AbsenceApproveNavigationCommand(NavigationUtility ns, string id)
        {

            this.navigationService = ns;
            this.id = id;


        }

        public override void Execute(object parameter)
        {

            navigationService.CreateViewModel(() => { return new ApproveAbsenceViewModel(navigationService, this.id); });
            navigationService.Navigate();

        }



    }
}
