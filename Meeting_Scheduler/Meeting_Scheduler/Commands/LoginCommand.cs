using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.Stores;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Meeting_Scheduler.Commands
{
    public class LoginCommand : CommandBase
    {

        private readonly LoginViewModel loginViewModel;
        private readonly NavigationService navigationService;
        private UserRepository repo = new UserRepository();
        public LoginCommand(LoginViewModel loginViewModel,NavigationService ns) {

            this.loginViewModel = loginViewModel;
            this.navigationService = ns;
        
        
        }

        public override void Execute(object parameter)
        {
            if (String.IsNullOrEmpty(loginViewModel.Username)) { MessageBox.Show("You must fill the username!");return; }
            if (String.IsNullOrEmpty(loginViewModel.Password)) { MessageBox.Show("You must fill the password!");return; }

            UserDTO dt=repo.GetUser(loginViewModel.Username, loginViewModel.Password);

            if (dt == null) {

                MessageBox.Show("Wrong credentials!"); return;
            }

            if (((Role)(Enum.Parse(typeof(Role),dt.PartitionKey))) == Role.ADMINISTRATOR) 
            {
                navigationService.CreateViewModel(() => { return new UsersViewModel(navigationService); });
                navigationService.Navigate();

            }

            if (((Role)(Enum.Parse(typeof(Role), dt.PartitionKey))) == Role.EMPLOYEE) 
            {
                navigationService.CreateViewModel(() => { return new LoginViewModel(navigationService); });
                navigationService.Navigate();
            }

            
            

            
        }

        public override bool CanExecute(object parameter) 
        {
            
            return base.CanExecute(parameter);
        }  

    }
}
