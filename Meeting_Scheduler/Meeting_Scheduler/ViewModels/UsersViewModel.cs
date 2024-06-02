using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Database.Repositories;
using Meeting_Scheduler.Models;
using Meeting_Scheduler.Models.UserModel;
using Meeting_Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meeting_Scheduler.ViewModels
{
    public class UsersViewModel:ViewModelBase
    {
        private readonly NavigationService _navigationService;
        private readonly ObservableCollection<UserViewModel> _users;
        private readonly UserRepository _userRepository=new UserRepository();    
        public UsersViewModel(NavigationService ns)
        {
            this._navigationService = ns;
            var employees = _userRepository.GetEmployees().ToList();
            _users = new ObservableCollection<UserViewModel>();
            employees.ForEach((e) => { _users.Add(new UserViewModel(this._navigationService,new EmployeeUser(Guid.Parse(e.RowKey), e.UserName, e.Password, e.Name, e.Surname,e.Email,e.Phone)));});
            LogOutUser = new LogOutCommand(this._navigationService);
            AddNavigate = new AddNavigateCommand(this._navigationService);
        }

        public IEnumerable<UserViewModel> Users => _users;

        public ICommand LogOutUser { get; set; }
        public ICommand AddNavigate { get; set; }

    }
}
