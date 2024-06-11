using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Meeting_Scheduler.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {

        private string _username;
        private string _password;

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChange(nameof(Username));

            }


        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChange(nameof(Password));

            }


        }



        public ICommand LoginCommand { get; }

        public LoginViewModel(NavigationUtility navigationService)
        {

            LoginCommand = new LoginCommand(this, navigationService);

        }


    }
}
