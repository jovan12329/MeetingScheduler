using Meeting_Scheduler.Commands;
using Meeting_Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meeting_Scheduler.ViewModels
{
    public class EventViewModel:ViewModelBase
    {


        private readonly NavigationUtility _navigationService;


        private string _id;
        private string _name;
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






        public ICommand AddEventS { get; }


        public ICommand Cancel { get; }


        public EventViewModel(NavigationUtility ns, string id)
        {
            _navigationService = ns;
            _id = id;
            AddEventS = new AddEventCommand(this, this._navigationService);

            this._navigationService.CreateViewModel(() => { return new AdminViewModel(this._navigationService, Id); });
            Cancel = new NavigationCommand(this._navigationService);




        }


    }
}
