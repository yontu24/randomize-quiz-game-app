using System;
using WpfQuizApp.ViewModels;

namespace WpfQuizApp.Store
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        #region Properties
        private ViewModelBase _currentViewModel;
        private DataStore _dataStore;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public DataStore CurrentDataStore
        {
            get => _dataStore;
            set
            {
                _dataStore = value;
                OnCurrentViewModelChanged();
            }
        }
        #endregion

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
