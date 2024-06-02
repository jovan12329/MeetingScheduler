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
    public class UserViewModel:ViewModelBase
    {
        private User _u;
        private readonly NavigationService _navigationService; 
        
        public Guid Id =>_u.Id;
        public string Name => _u.Name;
        public string Surname => _u.Surname;

        public string Email => _u.Email;
        public string Phone => _u.Phone;

        public ICommand ChangeUser { get; set; }

        public ICommand RemoveUser { get; set; }

        public UserViewModel(NavigationService ns,User u)
        {
            _navigationService = ns;
            _u = u;
            RemoveUser = new RemoveUserCommand(this,this._navigationService);
            ChangeUser = new ChangeNavigateCommand(_navigationService,this);
        }

    }
}
