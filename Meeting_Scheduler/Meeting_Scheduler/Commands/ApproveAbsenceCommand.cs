using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Commands
{
    public class ApproveAbsenceCommand:CommandBase
    {
        private readonly AbsenceItemViewModel addViewModel;
        private readonly NavigationUtility navigationService;
        private AbsenceRepository repo = new AbsenceRepository();
        private string user;
        private ILogger logger = new EventViewLogger();
        public ApproveAbsenceCommand(AbsenceItemViewModel loginViewModel, NavigationUtility ns, string user)
        {

            this.addViewModel = loginViewModel;
            this.navigationService = ns;
            this.user = user;


        }

        public override void Execute(object parameter)
        {

            var s = repo.GetById(addViewModel.Id);
            s.Approved = true.ToString();

            logger.Log("Absence approved", System.Diagnostics.EventLogEntryType.Information);
            repo.UpdateAbsence(s);

            logger.Log("Navigating to approve absence view !", System.Diagnostics.EventLogEntryType.Information);
            this.navigationService.CreateViewModel(() => { return new ApproveAbsenceViewModel(this.navigationService, user); });
            navigationService.Navigate();



        }


    }
}
