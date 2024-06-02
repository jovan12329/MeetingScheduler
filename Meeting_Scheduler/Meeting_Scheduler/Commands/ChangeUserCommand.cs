﻿using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Meeting_Scheduler.Commands
{
    public class ChangeUserCommand:CommandBase
    {



        private readonly ChangeUserViewModel addViewModel;
        private readonly NavigationService navigationService;
        private UserRepository repo = new UserRepository();
        public ChangeUserCommand(ChangeUserViewModel loginViewModel, NavigationService ns)
        {

            this.addViewModel = loginViewModel;
            this.navigationService = ns;


        }

        public override void Execute(object parameter)
        {

            if (String.IsNullOrEmpty(addViewModel.Username)) { MessageBox.Show("Username is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Password)) { MessageBox.Show("Password is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Name)) { MessageBox.Show("Name is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Surname)) { MessageBox.Show("Surname is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Email)) { MessageBox.Show("Email is empty!"); return; }
            if (String.IsNullOrEmpty(addViewModel.Phone)) { MessageBox.Show("Phone is empty!"); return; }


            UserDTO u1 = repo.GetEmployeeById(addViewModel.Id);

            UserDTO u = repo.GetUserByEmail(addViewModel.Email);

            if (u.RowKey != u1.RowKey) { MessageBox.Show($"A user with email {u.Email} already exists"); return; }

            u = new UserDTO(Guid.Parse(u1.RowKey), addViewModel.Username, addViewModel.Password, addViewModel.Name, addViewModel.Surname, Role.EMPLOYEE, addViewModel.Email, addViewModel.Phone);

            repo.UpdateUser(u);

            navigationService.CreateViewModel(() => { return new UsersViewModel(navigationService); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }




    }
}