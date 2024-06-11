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

namespace Meeting_Scheduler.ViewModels
{
    public class ApproveAbsenceViewModel:ViewModelBase
    {

        private readonly NavigationUtility _navigationService;
        private readonly AbsenceRepository _appRepository = new AbsenceRepository();

        private ObservableCollection<AbsenceItemViewModel> _seeks;
        private string username;
        public ApproveAbsenceViewModel(NavigationUtility ns, string username)
        {
            this._navigationService = ns;
            this.username = username;
            this._seeks = new ObservableCollection<AbsenceItemViewModel>();

            var l = _appRepository.GetFalseAbsenceLeaves();

            l.ForEach(k => {

                this._seeks.Add(new AbsenceItemViewModel(this._navigationService, k, this.username));

            });

            this._navigationService.CreateViewModel(() => { return new AdminViewModel(this._navigationService, this.username); });
            Back = new NavigationCommand(this._navigationService);
        }


        public ICommand Back { get; set; }



        public IEnumerable<AbsenceItemViewModel> Absences => _seeks;



    }
}
