using System;
using WpfQuizApp.ViewModels;

namespace WpfQuizApp.Store
{
    public class ParameterNavigationService<TParam, TViewModel>
        where TViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;
        private readonly Func<TParam, TViewModel> createViewModel;

        public ParameterNavigationService(NavigationStore navigationStore, Func<TParam, TViewModel> createViewModel)
        {
            this.navigationStore = navigationStore;
            this.createViewModel = createViewModel;
        }

        public void Goto(TParam param)
        {
            navigationStore.CurrentViewModel = createViewModel(param);
        }
    }
}
