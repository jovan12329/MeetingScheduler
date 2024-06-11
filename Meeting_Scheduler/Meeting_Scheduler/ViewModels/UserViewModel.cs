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
    public class UserViewModel : ViewModelBase
    {
        private User _u;
        private readonly NavigationUtility _navigationService;

        public string Admin { get; set; }
        public string Id => _u.RowKey;
        public string Name => _u.Name;
        public string Surname => _u.Surname;

        public string Email => _u.Email;
        public string Phone => _u.Phone;

        public ICommand ChangeUser { get; set; }

        public ICommand RemoveUser { get; set; }

        public UserViewModel(NavigationUtility ns, User u,string admin)
        {
            _navigationService = ns;
            _u = u;
            Admin = admin;
            RemoveUser = new RemoveUserCommand(this, this._navigationService,Admin);
            ChangeUser = new ChangeNavigateCommand(_navigationService, this,Admin);
        }

    }
}
