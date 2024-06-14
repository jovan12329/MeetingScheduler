using Meeting_Scheduler.Commands;
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
    public class AdminViewModel:ViewModelBase
    {
        private readonly NavigationUtility _navigationService;
        private readonly ObservableCollection<UserViewModel> _users;
        private readonly UserRepository _userRepository = new UserRepository();
        private string adminId;


        public AdminViewModel(NavigationUtility ns, string adminId)
        {
            this._navigationService = ns;
            this.adminId = adminId;
            var employees = _userRepository.GetEmployees().ToList();
            _users = new ObservableCollection<UserViewModel>();
            employees.ForEach((e) => { _users.Add(new UserViewModel(this._navigationService, e,this.adminId));});

            LogOutUser = new LogoutCommand(this._navigationService);

            AddNavigate = new AddNavigateCommand(this._navigationService, this.adminId);

            ScheduleNav = new ScheduleNavigationCommand(this._navigationService, this.adminId);

            ApproveSeekNav = new ApproveSeekNavCommand(this._navigationService,this.adminId);

            ApproveAbsNav = new AbsenceApproveNavigationCommand(this._navigationService,this.adminId);

            EventNav = new EventNavigationCommand(this._navigationService,this.adminId);

            ReportAdmin = new ReportAdminCommand();

            AdminCalendarNav = new AdminCalendarNavCommand(this._navigationService, this.adminId);

        }




        public IEnumerable<UserViewModel> Users => _users;

        public ICommand LogOutUser { get; set; }
        public ICommand AddNavigate { get; set; }

        public ICommand ScheduleNav { get; set; }
        public ICommand ApproveSeekNav { get; set; }
        public ICommand ApproveAbsNav { get; set; }
        public ICommand EventNav { get; set; }
        public ICommand ReportAdmin { get; set; }
        public ICommand AdminCalendarNav { get; set; }
        public string AdminId => adminId;


    }
}
