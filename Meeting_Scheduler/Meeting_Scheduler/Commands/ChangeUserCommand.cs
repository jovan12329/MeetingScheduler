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
using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Services;

namespace Meeting_Scheduler.Commands
{
    public class ChangeUserCommand : CommandBase
    {



        private readonly ChangeUserViewModel addViewModel;
        private readonly NavigationUtility navigationService;
        private UserRepository repo = new UserRepository();
        private string admin;
        private ILogger logger = new EventViewLogger();
        public ChangeUserCommand(ChangeUserViewModel loginViewModel, NavigationUtility ns,string admin)
        {

            this.addViewModel = loginViewModel;
            this.navigationService = ns;
            this.admin= admin;

        }

        public override void Execute(object parameter)
        {

            if (String.IsNullOrEmpty(addViewModel.Username)) { logger.Log("Empty field username!",System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Username is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Name)) { logger.Log("Empty field name!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Name is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Surname)) { logger.Log("Empty field surname!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Surname is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Email)) { logger.Log("Empty field email!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Email is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Phone)) { logger.Log("Empty field phone!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show("Phone is empty!"); return; }


            User u1 = repo.GetEmployeeById(addViewModel.Id);

            User u = repo.GetUserByEmail(addViewModel.Email);

            User u2 = repo.GetByUsername(addViewModel.Username);

            if (u != null && u.RowKey != u1.RowKey) { logger.Log("A user with email exists!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show($"A user with email {u.Email} already exists !"); return; }

            if (u2 != null && u2.RowKey != u1.RowKey) { logger.Log("A user with username exists!", System.Diagnostics.EventLogEntryType.Warning); MessageBox.Show($"A user with username {u2.UserName} already exists !"); return; }

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
            logger.Log("User updated successfully!", System.Diagnostics.EventLogEntryType.Information);

            logger.Log("Navigating to admin view!", System.Diagnostics.EventLogEntryType.Information);

            navigationService.CreateViewModel(() => { return new AdminViewModel(navigationService,this.admin); });
            navigationService.Navigate();



        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }




    }
}
