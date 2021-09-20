using System.Linq;
using System.Windows.Input;
using WpfQuizApp.Commands;
using WpfQuizApp.Models;
using WpfQuizApp.Services;
using WpfQuizApp.Store;

namespace WpfQuizApp.ViewModels
{
    public class DisplayScoreViewModel : ViewModelBase
    {
        #region Properties
        private readonly UserDataModel userData;
        public string Result1 => userData.User?.FirstName + " " + userData.User?.LastName;
        public string Result2 => "\nyour score is '" + userData.TotalScore + "'";
        public ICommand CommandTryAgain { get; }
        #endregion

        public DisplayScoreViewModel(NavigationStore navigationStore)
        {
            userData = navigationStore.CurrentDataStore.UserData;

            CommandTryAgain = new NavigateCommand<StartGameViewModel>(new NavigationService<StartGameViewModel>(navigationStore,
                () => new StartGameViewModel(navigationStore)));

            foreach (QuizEntity quizEntity in navigationStore.CurrentDataStore.Quiz.Quizes)
            {
                bool hasAllAnswers = quizEntity.Answers.Count(answer => answer.IsCorrect && answer.IsSelected) ==
                    quizEntity.Answers.Count(answer => answer.IsCorrect);

                if (hasAllAnswers)
                {
                    userData.TotalScore++;
                }

                // reset data
                quizEntity.Answers = new(quizEntity.Answers.Select(answer =>
                    {
                        answer.IsSelected = false;
                        return answer;
                    }).ToList());
            }

            // reset data
            navigationStore.CurrentDataStore.Quiz.CurrentQuiz = 0;
            navigationStore.CurrentDataStore.UserData.User.Difficulty = "empty";
        }
    }
}
