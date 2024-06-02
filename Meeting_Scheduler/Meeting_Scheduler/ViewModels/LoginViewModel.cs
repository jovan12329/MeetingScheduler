using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Services;
using Meeting_Scheduler.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public LoginViewModel(NavigationService navigationService)
        {

            LoginCommand = new LoginCommand(this, navigationService);
            
        }



    }
}
