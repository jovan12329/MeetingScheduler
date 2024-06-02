using Meeting_Scheduler.Database.Entities;
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
    public class RemoveUserCommand:CommandBase
    {


        private readonly NavigationService navigationService;
        private UserViewModel viewModel;
        private readonly UserRepository userRepository=new UserRepository();
        public RemoveUserCommand(UserViewModel vm,NavigationService ns)
        {
            this.viewModel = vm;
            this.navigationService = ns;


        }

        public override void Execute(object parameter)
        {
            UserDTO rem = userRepository.GetEmployeeById(viewModel.Id.ToString());
            userRepository.RemoveUser(rem);
            navigationService.CreateViewModel(() => { return new UsersViewModel(navigationService); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }



    }
}
