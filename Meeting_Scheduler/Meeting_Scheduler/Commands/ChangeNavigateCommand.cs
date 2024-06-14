using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Meeting_Scheduler.Commands
{
    public class ChangeNavigateCommand : CommandBase
    {


        private readonly NavigationUtility navigationService;
        private readonly UserViewModel viewModel;
        private readonly UserRepository userRepository = new UserRepository();
        private string admin;
        private ILogger logger = new EventViewLogger();
        public ChangeNavigateCommand(NavigationUtility ns, UserViewModel vm,string admin)
        {

            this.navigationService = ns;
            this.viewModel = vm;
            this.admin = admin;


        }

        public override void Execute(object parameter)
        {
            User u = userRepository.GetEmployeeById(viewModel.Id.ToString());
            logger.Log("Navigating to the change user view!",System.Diagnostics.EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new ChangeUserViewModel(navigationService, u,this.admin); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }



    }
}
