using Meeting_Scheduler.Services;
using Meeting_Scheduler.Stores;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Meeting_Scheduler.Commands
{
    public class NavigationCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public NavigationCommand(NavigationService ns)
        {
            _navigationService = ns;
        }
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
            
        }
    }
}
