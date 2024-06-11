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

namespace Meeting_Scheduler.ViewModels
{
    public class ApproveSeekViewModel:ViewModelBase
    {

        private readonly NavigationUtility _navigationService;
        private readonly SeekingLeaveRepository _appRepository = new SeekingLeaveRepository();

        private ObservableCollection<SeekingItemViewModel> _seeks;
        private string username;
        public ApproveSeekViewModel(NavigationUtility ns, string username)
        {
            this._navigationService = ns;
            this.username = username;
            this._seeks = new ObservableCollection<SeekingItemViewModel>();

            var l = _appRepository.GetFalseSeekingLeaves().ToList();

            l.ForEach(k => {

                this._seeks.Add(new SeekingItemViewModel(this._navigationService, k, this.username));

            });

            this._navigationService.CreateViewModel(() => { return new AdminViewModel(this._navigationService,this.username); });
            Back = new NavigationCommand(this._navigationService);
        }


        public ICommand Back { get; set; }
       


        public IEnumerable<SeekingItemViewModel> Seekings => _seeks;






    }
}
