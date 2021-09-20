using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfQuizApp.Commands;
using WpfQuizApp.Models;
using WpfQuizApp.Services;
using WpfQuizApp.Store;

namespace WpfQuizApp.ViewModels
{
    public class ChooseAnswerViewModel : ViewModelBase
    {
        #region Properties
        private readonly UserModel user;
        public string FirstName => user?.FirstName;
        public string LastName => user?.LastName;
        public string ButtonContent { get; set; }
        public ICommand CommandNextQuestion { get; }
        public ObservableCollection<QuizEntity> Quizes { get; } = new ObservableCollection<QuizEntity>();
        #endregion

        public ChooseAnswerViewModel(NavigationStore navigationStore, UserModel data)
        {
            user = data;
            if (navigationStore.CurrentDataStore.UserData.User.FirstName == null)
            {
                navigationStore.CurrentDataStore.UserData.User = user;
            }

            QuizModel _quizModel = navigationStore.CurrentDataStore.Quiz;

            if (_quizModel.CurrentQuiz != _quizModel.Quizes
                .Count(quiz => quiz.Difficulty == user.Difficulty))        // inca raspund la intrebari
            {
                ButtonContent = "Next Question";

                QuizEntity _currentQuiz = _quizModel.Quizes
                    .Where(quiz => quiz.Difficulty == user.Difficulty)
                    .ToList()[index: navigationStore.CurrentDataStore.Quiz.CurrentQuiz++];

                //_currentQuiz.Answers[0].IsSelected = true;
                Quizes.Add(_currentQuiz);

                CommandNextQuestion = new NextCommand(new NavigationService<ChooseAnswerViewModel>(navigationStore,
                    () => new ChooseAnswerViewModel(navigationStore, navigationStore.CurrentDataStore.UserData.User)));
            }
            else
            {
                ButtonContent = "Show results";

                // reset data
                navigationStore.CurrentDataStore.UserData.TotalScore = 0;

                CommandNextQuestion = new DisplayScoreCommand(new NavigationService<DisplayScoreViewModel>(navigationStore,
                    () => new DisplayScoreViewModel(navigationStore)));
            }
        }
    }
}
