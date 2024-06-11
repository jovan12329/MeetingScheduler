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
    public class AddUserViewModel : ViewModelBase
    {

        private NavigationUtility _navigtionService;

        private string id;
        private string _username;
        private string _password;
        private string _name;
        private string _surname;
        private string _email;
        private string _phone;

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


        public string Name
        {
            get
            {
                return _name;
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
                return _surname;
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
                return _email;
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
                return _phone;
            }

            set
            {
                _phone = value;
                OnPropertyChange(nameof(Phone));

            }


        }


        public ICommand AddCommand { get; set; }

        public ICommand BackFrom { get; set; }

        public AddUserViewModel(NavigationUtility navigationService, string id)
        {
            this.id = id;
            this._navigtionService = navigationService;
            AddCommand = new AddUserCommand(this, this._navigtionService, this.id);
            this._navigtionService.CreateViewModel(() => { return new AdminViewModel(this._navigtionService, id); });
            BackFrom = new NavigationCommand(this._navigtionService);
        }




    }
}
