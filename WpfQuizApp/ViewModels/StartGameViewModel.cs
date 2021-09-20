using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfQuizApp.Commands;
using WpfQuizApp.Models;
using WpfQuizApp.Store;

namespace WpfQuizApp.ViewModels
{
    public class StartGameViewModel : ViewModelBase
    {
        #region Properties
        public ICommand CommandStartGame { get; }
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public ObservableCollection<AnswerEntity> Difficulty { get; set; } = new()
        {
            new AnswerEntity()
            {
                Answer = "Easy",
                IsSelected = true
            },
            new AnswerEntity()
            {
                Answer = "Medium"
            },
            new AnswerEntity()
            {
                Answer = "Hard"
            }
        };
        #endregion

        public StartGameViewModel(NavigationStore navigationStore)
        {
            ParameterNavigationService<UserModel, ChooseAnswerViewModel> parameter = new(navigationStore, (param) =>
                {
                    param.Difficulty = Difficulty.First(difficulty => difficulty.IsSelected).Answer;
                    return new ChooseAnswerViewModel(navigationStore, param);
                }
            );

            CommandStartGame = new StartCommand(this, parameter);
        }
    }
}
