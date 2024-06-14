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
    public class DenySeekCommand : CommandBase
    {
        private readonly SeekingItemViewModel addViewModel;
        private readonly NavigationUtility navigationService;
        private SeekingLeaveRepository repo = new SeekingLeaveRepository();
        private string user;
        private ILogger logger = new EventViewLogger();
        public DenySeekCommand(SeekingItemViewModel loginViewModel, NavigationUtility ns, string user)
        {

            this.addViewModel = loginViewModel;
            this.navigationService = ns;
            this.user = user;


        }

        public override void Execute(object parameter)
        {

            var s = repo.GetById(addViewModel.Id);
            repo.RemoveSeek(s);

            logger.Log("Seek successfully denied !", System.Diagnostics.EventLogEntryType.Information);

            logger.Log("Navigating to the approve seek view!", System.Diagnostics.EventLogEntryType.Information);

            this.navigationService.CreateViewModel(() => { return new ApproveSeekViewModel(this.navigationService, user); });
            navigationService.Navigate();



        }
    }
}
