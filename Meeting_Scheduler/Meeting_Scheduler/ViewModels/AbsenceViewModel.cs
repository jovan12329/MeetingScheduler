using Meeting_Scheduler.Commands;
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
    public class AbsenceViewModel:ViewModelBase
    {

        private readonly NavigationUtility _navigationService;
        

        private string _id;
        private string _reason;
        private string _type;
        private string _startDate;
        private string _endDate;
        private string _description;



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


        public string Reason
        {
            get
            {
                return _reason;
            }

            set
            {
                _reason = value;
                OnPropertyChange(nameof(Reason));

            }


        }

        public string StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                _startDate = value;
                OnPropertyChange(nameof(StartDate));

            }


        }

        public string EndDate
        {
            get
            {
                return _endDate;
            }

            set
            {
                _endDate = value;
                OnPropertyChange(nameof(EndDate));

            }


        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                OnPropertyChange(nameof(Description));

            }


        }






        public ICommand SendRequest { get; }


        public ICommand Cancel { get; }


        public AbsenceViewModel(NavigationUtility ns, string id)
        {
            _navigationService = ns;
            _id = id;
            SendRequest = new RequestAbsenceCommand(this, this._navigationService);

            

            this._navigationService.CreateViewModel(() => { return new EmployeeViewModel(this._navigationService, Id); });
            Cancel = new NavigationCommand(this._navigationService);


            

        }



    }
}
