using WpfQuizApp.Services;
using WpfQuizApp.ViewModels;

namespace WpfQuizApp.Commands
{
    public class DisplayScoreCommand : CommandBase
    {
        private readonly NavigationService<DisplayScoreViewModel> navigationService;
        public DisplayScoreCommand(NavigationService<DisplayScoreViewModel> navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            navigationService.Goto();
        }
    }
}
