using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Models;
using Meeting_Scheduler.Models.UserModel;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Commands
{
    public class ChangeNavigateCommand:CommandBase
    {


        private readonly NavigationService navigationService;
        private readonly UserViewModel viewModel;
        private readonly UserRepository userRepository=new UserRepository();

        public ChangeNavigateCommand(NavigationService ns, UserViewModel vm)
        {

            this.navigationService = ns;
            this.viewModel = vm;


        }

        public override void Execute(object parameter)
        {
            UserDTO u = userRepository.GetEmployeeById(viewModel.Id.ToString());
            navigationService.CreateViewModel(() => { return new ChangeUserViewModel(navigationService,new EmployeeUser(Guid.Parse(u.RowKey),u.UserName,u.Password,u.Name,u.Surname,u.Email,u.Phone));});
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }



    }
}
