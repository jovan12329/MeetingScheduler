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
    public class AddUserCommand:CommandBase
    {


        private readonly AddUserViewModel addViewModel;
        private readonly NavigationUtility navigationService;
        private UserRepository repo = new UserRepository();
        private string user;
        private ILogger logger = new EventViewLogger();
        public AddUserCommand(AddUserViewModel loginViewModel, NavigationUtility ns, string user)
        {

            this.addViewModel = loginViewModel;
            this.navigationService = ns;
            this.user = user;


        }

        public override void Execute(object parameter)
        {

            if (String.IsNullOrEmpty(addViewModel.Username)) { logger.Log("Empty field username entered!",System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Username is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Password)) { logger.Log("Empty field password entered!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Password is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Name)) { logger.Log("Empty field name entered!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Name is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Surname)) { logger.Log("Empty field surname entered!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Surname is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Email)) { logger.Log("Empty field email entered!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Email is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Phone)) { logger.Log("Empty field phone entered!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Phone is empty!"); return; }


            User u = repo.GetUserByEmail(addViewModel.Email);
            User u1 = repo.GetByUsername(addViewModel.Username);

            if (u != null) { logger.Log("User with email exists!!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show($"A user with email {u.Email} already exists"); return; }
            if (u1 != null) { logger.Log("User with username exists!!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show($"A user with username {u.UserName} already exists"); return; }

            string password = HashPassword.Hash(addViewModel.Password);
            logger.Log("Password hashed successfully!", System.Diagnostics.EventLogEntryType.Information);

            u = new User(addViewModel.Username, password, addViewModel.Name, addViewModel.Surname, Role.EMPLOYEE, addViewModel.Email, addViewModel.Phone);

            repo.AddUser(u);
            logger.Log("User added successfully!", System.Diagnostics.EventLogEntryType.Information);

            logger.Log("Navigating to admin view model!", System.Diagnostics.EventLogEntryType.Information);

            navigationService.CreateViewModel(() => { return new AdminViewModel(navigationService, user); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }


    }
}
