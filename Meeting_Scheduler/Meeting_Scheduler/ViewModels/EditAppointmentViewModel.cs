using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meeting_Scheduler.ViewModels
{
    public class EditAppointmentViewModel:ViewModelBase
    {

        private readonly NavigationUtility _navigationService;
        private UserRepository userRepository = new UserRepository();
        public Appointment _a;

        private string _id;
        private string _name;
        private string _host;
        private string _type;
        private string _date;
        private string _startTime;
        private string _endTime;
        private IEnumerable<string> employee;



        public IEnumerable<string> Employee
        {
            get
            {
                return employee;


            }

        }

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

        public string Host
        {
            get
            {
                return _host;
            }

            set
            {
                _host = value;
                OnPropertyChange(nameof(Host));

            }


        }

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
                OnPropertyChange(nameof(Type));

            }


        }

        public string Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
                OnPropertyChange(nameof(Date));

            }


        }


        public string StartTime
        {

            get
            {
                return _startTime;
            }

            set
            {
                _startTime = value;
                OnPropertyChange(nameof(StartTime));

            }



        }

        public string EndTime
        {

            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
                OnPropertyChange(nameof(EndTime));

            }



        }


        public IEnumerable<string> Users
        {
            get
            {
                if (userRepository.GetByUsername(_id).PartitionKey.Equals("ADMINISTRATOR"))
                {

                    return userRepository.GetEmployees().ToList().Select(k => k.UserName).AsEnumerable();
                }

                return userRepository.GetByUsernameEmployee(_id).Select(k => k.UserName).AsEnumerable();

            }

            set
            {
                employee = value;
                OnPropertyChange(nameof(Users));

            }


        }




        public ICommand Edit { get; }


        public ICommand Cancel { get; }


        public EditAppointmentViewModel(NavigationUtility ns,Appointment a, string id)
        {
            _navigationService = ns;
            _a = a;
            _id = id;

            
            Edit = new EditAppointmentCommand(this, this._navigationService);

            if (userRepository.GetByUsername(_id).PartitionKey.Equals("ADMINISTRATOR"))
            {
                this._navigationService.CreateViewModel(() => { return new AdminViewModel(this._navigationService, Id); });
                Cancel = new NavigationCommand(this._navigationService);
            }
            else
            {

                this._navigationService.CreateViewModel(() => { return new EmployeeViewModel(this._navigationService, Id); });
                Cancel = new NavigationCommand(this._navigationService);


            }

        }


    }
}
