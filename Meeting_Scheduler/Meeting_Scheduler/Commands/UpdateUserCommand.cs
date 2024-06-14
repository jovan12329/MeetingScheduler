using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Repositories;
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
    public class UpdateUserCommand : CommandBase
    {


        private readonly UpdateUserViewModel addViewModel;
        private readonly NavigationUtility navigationService;
        private UserRepository repo = new UserRepository();
        private ILogger logger = new FileLogger(typeof(UpdateUserCommand));
        public UpdateUserCommand(UpdateUserViewModel loginViewModel, NavigationUtility ns)
        {

            this.addViewModel = loginViewModel;
            this.navigationService = ns;


        }

        public override void Execute(object parameter)
        {

            if (String.IsNullOrEmpty(addViewModel.Username)) { logger.Log("Username field empty!",System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Username is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Name)) { logger.Log("Name field empty!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Name is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Surname)) { logger.Log("Surname field empty!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Surname is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Email)) { logger.Log("Email field empty!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Email is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Phone)) { logger.Log("Phone field empty!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Phone is empty!"); return; }


            User u1 = repo.GetEmployeeById(addViewModel.Id);

            User u = repo.GetUserByEmail(addViewModel.Email);

            User u2 = repo.GetByUsername(addViewModel.Username);

            if (u != null && u.RowKey != u1.RowKey) { logger.Log("Email already exists!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show($"A user with email {u.Email} already exists !"); return; }

            if (u2 != null && u2.RowKey != u1.RowKey) { logger.Log("Username already exists!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show($"A user with username {u2.UserName} already exists !"); return; }

            if (String.IsNullOrEmpty(addViewModel.Password))
            {

                u1.UserName = addViewModel.Username;
                u1.Name = addViewModel.Name;
                u1.Surname = addViewModel.Surname;
                u1.Email = addViewModel.Email;
                u1.Phone = addViewModel.Phone;

            }

            else
            {
                u1.UserName = addViewModel.Username;
                u1.Password = HashPassword.Hash(addViewModel.Password);
                u1.Name = addViewModel.Name;
                u1.Surname = addViewModel.Surname;
                u1.Email = addViewModel.Email;
                u1.Phone = addViewModel.Phone;


            }

            repo.UpdateUser(u1);
            logger.Log("User successfully updated!", System.Diagnostics.EventLogEntryType.Information);

            logger.Log("Navigating to the employee view!", System.Diagnostics.EventLogEntryType.Information);
            navigationService.CreateViewModel(() => { return new EmployeeViewModel(navigationService, u1.UserName); });
            navigationService.Navigate();



        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }



    }
}
