using System.Windows;
using WpfQuizApp.Models;
using WpfQuizApp.Store;
using WpfQuizApp.ViewModels;

namespace WpfQuizApp.Commands
{
    public class StartCommand : CommandBase
    {
        private readonly StartGameViewModel startGameViewModel;
        private readonly ParameterNavigationService<UserModel, ChooseAnswerViewModel> navigationStore;

        public StartCommand(StartGameViewModel startGameView, ParameterNavigationService<UserModel, ChooseAnswerViewModel> navigationStore)
        {
            startGameViewModel = startGameView;
            this.navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _ = MessageBox.Show($"Welcome `{startGameViewModel.FirstName}` !");

            UserModel user = new()
            {
                LastName = startGameViewModel.LastName,
                FirstName = startGameViewModel.FirstName
            };

            //navigationStore.CurrentViewModel = new ChooseAnswerViewModel(navigationStore, user);
            //navigationStore.Goto();
            navigationStore.Goto(user);
        }
    }
}
