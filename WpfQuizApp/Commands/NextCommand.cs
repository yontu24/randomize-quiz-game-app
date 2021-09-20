using WpfQuizApp.Services;
using WpfQuizApp.ViewModels;

namespace WpfQuizApp.Commands
{
    public class NextCommand : CommandBase
    {
        private readonly NavigationService<ChooseAnswerViewModel> navigationService;
        public NextCommand(NavigationService<ChooseAnswerViewModel> navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            //TheGrid.RowDefinitions.Add(new RowDefinition());
            navigationService.Goto();
        }
    }
}
