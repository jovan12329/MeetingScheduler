using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Database.Entities;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Meeting_Scheduler.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {

        private readonly NavigationUtility _navigationService;
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly AppointmentRepository _appRepository = new AppointmentRepository();
        private ObservableCollection<AppointmentItemViewModel> _appointments;
        private string username;
        public EmployeeViewModel(NavigationUtility ns, string username)
        {
            this._navigationService = ns;
            this.username = username;
            this._appointments = new ObservableCollection<AppointmentItemViewModel>();

            var l= _appRepository.GetAppointmentsByHost(this.username).ToList();

            l.ForEach(k => {

                this._appointments.Add(new AppointmentItemViewModel(this._navigationService,k,this.username));

            });


            LogOutUser = new LogoutCommand(this._navigationService);
            User u = _userRepository.GetByUsername(username);
            UpdateCommand = new UpdateUserNavigatonCommand(this._navigationService, u);
            SeekLeave = new SeekingNavigationCommand(this._navigationService,this.username);
            Absence = new AbsenceNavigationCommand(this._navigationService,this.username);
            NavSchedCommand = new ScheduleNavigationCommand(this._navigationService, this.username);
            ReportEmployee = new EmployeeReportCommand(this.username);
            EmployeeCalendarNav = new EmployeeCalendarNavCommand(this._navigationService, this.username);
        }


        public ICommand LogOutUser { get; set; }
        public ICommand UpdateCommand { get; set; }

        public ICommand SeekLeave { get; set; }
        public ICommand NavSchedCommand { get; set; }
        public ICommand Absence { get; set; }
        public ICommand ReportEmployee { get; set; }
        public ICommand EmployeeCalendarNav { get; set; }
       

        public IEnumerable<AppointmentItemViewModel> Appointments => _appointments;

    }
}
