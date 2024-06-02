using Meeting_Scheduler.Services;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Commands
{
    public class AddNavigateCommand:CommandBase
    {

        private readonly NavigationService navigationService;

        public AddNavigateCommand(NavigationService ns)
        {

            this.navigationService = ns;


        }

        public override void Execute(object parameter)
        {

            navigationService.CreateViewModel(() => { return new AddUserViewModel(navigationService); });
            navigationService.Navigate();

        }

        public override bool CanExecute(object parameter)
        {

            return base.CanExecute(parameter);
        }

    }
}
