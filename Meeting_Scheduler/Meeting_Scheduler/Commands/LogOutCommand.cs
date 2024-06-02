using Meeting_Scheduler.Database.Entities;
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
    public class LogOutCommand:CommandBase
    {
        
        private readonly NavigationService navigationService;
        
        public LogOutCommand(NavigationService ns)
        {

            this.navigationService = ns;


        }

        public override void Execute(object parameter)
        {
            
                navigationService.CreateViewModel(() => { return new LoginViewModel(navigationService); });
                navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }


    }
}
