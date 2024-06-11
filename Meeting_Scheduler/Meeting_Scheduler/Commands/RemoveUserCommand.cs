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
    public class RemoveUserCommand : CommandBase
    {


        private readonly NavigationUtility navigationService;
        private UserViewModel viewModel;
        private readonly UserRepository userRepository = new UserRepository();
        private string admin;
        public RemoveUserCommand(UserViewModel vm, NavigationUtility ns,string a)
        {
            this.viewModel = vm;
            this.navigationService = ns;
            this.admin = a;


        }

        public override void Execute(object parameter)
        {
            User rem = userRepository.GetEmployeeById(viewModel.Id.ToString());
            userRepository.RemoveUser(rem);
            navigationService.CreateViewModel(() => { return new AdminViewModel(navigationService,this.admin); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }



    }
}
