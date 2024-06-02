using Meeting_Scheduler.Stores;
using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Services
{
    public class NavigationService
    {
        private NavigationStore _navigationStore;
        private Func<ViewModelBase> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void CreateViewModel(Func<ViewModelBase> createViewModel) 
        {

            _createViewModel = createViewModel;

        }

        public void Navigate()
        {

            _navigationStore.CurretViewModel = _createViewModel();

        }

    }
}
