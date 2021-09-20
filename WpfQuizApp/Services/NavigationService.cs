using System;
using WpfQuizApp.Store;
using WpfQuizApp.ViewModels;

namespace WpfQuizApp.Services
{
    public class NavigationService<TViewModel>
        where TViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;
        private readonly Func<TViewModel> createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            this.navigationStore = navigationStore;
            this.createViewModel = createViewModel;
        }

        public void Goto()
        {
            navigationStore.CurrentViewModel = createViewModel();
        }
    }
}
