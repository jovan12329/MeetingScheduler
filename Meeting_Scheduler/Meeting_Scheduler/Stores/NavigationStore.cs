using Meeting_Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Stores
{
    public class NavigationStore
    {

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurretViewModel
        {

            get => _currentViewModel;
            set
            {

                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }

        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;


    }
}
