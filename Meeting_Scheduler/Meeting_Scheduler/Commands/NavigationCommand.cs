using Meeting_Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Meeting_Scheduler.Commands
{
    public class NavigationCommand : CommandBase
    {
        private readonly NavigationUtility _navigationService;

        public NavigationCommand(NavigationUtility ns)
        {
            _navigationService = ns;
        }
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();

        }
    }
}
