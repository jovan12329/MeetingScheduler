using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Models;
using Meeting_Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meeting_Scheduler.ViewModels
{
    public class ChangeUserViewModel:ViewModelBase
    {

        private User _u;
        private readonly NavigationService _navigationService;

        private string _id;
        private string _username;
        private string _password;
        private string _name;
        private string _surname;
        private string _email;
        private string _phone;


        public string Id { 
            
            get {

                return _u.Id.ToString();
            
            } 
            
            set {

                _id = value;
                OnPropertyChange(nameof(Id));
            }
        
        }

        public string Username
        {
            get
            {
                return _u.UserName;
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
                return _u.Password;
            }

            set
            {
                _password = value;
                OnPropertyChange(nameof(Password));

            }


        }


        public string Name
        {
            get
            {
                return _u.Name;
            }

            set
            {
                _name = value;
                OnPropertyChange(nameof(Name));

            }


        }


        public string Surname
        {
            get
            {
                return _u.Surname;
            }

            set
            {
                _surname = value;
                OnPropertyChange(nameof(Surname));

            }


        }

        public string Email
        {
            get
            {
                return _u.Email;
            }

            set
            {
                _email = value;
                OnPropertyChange(nameof(Email));

            }


        }

        public string Phone
        {
            get
            {
                return _u.Phone;
            }

            set
            {
                _phone = value;
                OnPropertyChange(nameof(Phone));

            }


        }



        public ICommand SaveCommand { get; }
        

        public ChangeUserViewModel(NavigationService ns, User u)
        {
            _navigationService = ns;
            _u = u;
            SaveCommand = new ChangeUserCommand(this,_navigationService);
            
        }


    }
}
