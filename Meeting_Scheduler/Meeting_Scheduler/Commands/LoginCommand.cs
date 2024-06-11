using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Enums;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.Database.Entities;

namespace Meeting_Scheduler.Commands
{
    public class LoginCommand:CommandBase
    {

        private readonly LoginViewModel loginViewModel;
        private readonly NavigationUtility navigationService;
        private UserRepository repo = new UserRepository();
        public LoginCommand(LoginViewModel loginViewModel, NavigationUtility ns)
        {

            this.loginViewModel = loginViewModel;
            this.navigationService = ns;


        }

        public override void Execute(object parameter)
        {
            if (String.IsNullOrEmpty(loginViewModel.Username)) { MessageBox.Show("You must fill the username!"); return; }
            if (String.IsNullOrEmpty(loginViewModel.Password)) { MessageBox.Show("You must fill the password!"); return; }

            User dt = repo.GetByUsername(loginViewModel.Username);


            if (dt == null)
            {

                MessageBox.Show("Wrong credentials!"); return;
            }

            if (!HashPassword.Verify(loginViewModel.Password, dt.Password))
            {
                MessageBox.Show("Wrong credentials!"); return;

            }


            if (((Role)(Enum.Parse(typeof(Role), dt.PartitionKey))) == Role.ADMINISTRATOR)
            {
                navigationService.CreateViewModel(() => { return new AdminViewModel(navigationService, dt.UserName); });
                navigationService.Navigate();

            }

            if (((Role)(Enum.Parse(typeof(Role), dt.PartitionKey))) == Role.EMPLOYEE)
            {
                navigationService.CreateViewModel(() => { return new EmployeeViewModel(navigationService, loginViewModel.Username); });
                navigationService.Navigate();
            }


        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }



    }
}
