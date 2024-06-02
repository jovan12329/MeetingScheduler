using Meeting_Scheduler.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChange(nameof(CurrentViewModel));
        }

        public ViewModelBase CurrentViewModel => _navigationStore.CurretViewModel;

        

    }
}
