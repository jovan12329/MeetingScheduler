using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Database.Entities;
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
    public class UpdateUserViewModel : ViewModelBase
    {

        private User _u;
        private readonly NavigationUtility _navigationService;

        private string _id;
        private string _username;
        private string _password;
        private string _name;
        private string _surname;
        private string _email;
        private string _phone;


        public string Id
        {

            get
            {

                return _id;

            }

            set
            {

                _id = value;
                OnPropertyChange(nameof(Id));
            }

        }

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



        public ICommand SaveCommand { get; }


        public ICommand BackCommand { get; }


        public UpdateUserViewModel(NavigationUtility ns, User u)
        {
            _navigationService = ns;
            _u = u;
            SaveCommand = new UpdateUserCommand(this, _navigationService);
            this._id = u.RowKey.ToString();
            this._username = u.UserName;
            this._name = u.Name;
            this._surname = u.Surname;
            this._email = u.Email;
            this._phone = u.Phone;

            this._navigationService.CreateViewModel(() => { return new EmployeeViewModel(this._navigationService, u.UserName); });
            BackCommand = new NavigationCommand(this._navigationService);

        }



    }
}
