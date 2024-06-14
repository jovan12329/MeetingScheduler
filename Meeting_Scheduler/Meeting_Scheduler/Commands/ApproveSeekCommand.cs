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
    public class ApproveSeekCommand : CommandBase
    {

        private readonly SeekingItemViewModel addViewModel;
        private readonly NavigationUtility navigationService;
        private SeekingLeaveRepository repo = new SeekingLeaveRepository();
        private string user;
        private ILogger logger = new EventViewLogger();
        public ApproveSeekCommand(SeekingItemViewModel loginViewModel, NavigationUtility ns, string user)
        {

            this.addViewModel = loginViewModel;
            this.navigationService = ns;
            this.user = user;


        }

        public override void Execute(object parameter)
        {

            var s = repo.GetById(addViewModel.Id);
            s.Approved = true.ToString();

            logger.Log("Seek approved!",System.Diagnostics.EventLogEntryType.Information);
            
            repo.UpdateSeek(s);

            logger.Log("Navigating to approve seek view!", System.Diagnostics.EventLogEntryType.Information);
            this.navigationService.CreateViewModel(() => { return new ApproveSeekViewModel(this.navigationService, user); });
            navigationService.Navigate();



        }
    }
}
