using System.Diagnostics;
using System.Windows;
using WpfQuizApp.Models;
using WpfQuizApp.Services;
using WpfQuizApp.Store;
using WpfQuizApp.ViewModels;

namespace WpfQuizApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new();
            DataStore dataStore = new()
            {
                UserData = new UserDataModel()
                {
                    TotalScore = 0,
                    User = new UserModel()
                    {
                        FirstName = null,
                        LastName = null
                    }
                },
                Quiz = new QuizModel()
                {
                    CurrentQuiz = 0,
                    Quizes = new()
                }
            };

            ParseQuizService parseQuizService = ParseQuizService.Instance;
            parseQuizService.ParseQuiz(dataStore);
            ParseQuizService.Shuffle(dataStore.Quiz.Quizes);

            /*
            foreach (QuizEntity quizEntity in dataStore.Quiz.Quizes)
            {
                Debug.WriteLine("QUESTION: " + quizEntity.Question);
                Debug.WriteLine("ANSWERS:" + quizEntity.Answers.Count);
                Debug.WriteLine("Type:" + quizEntity.Type);

                foreach (AnswerEntity answer in quizEntity.Answers)
                {
                    Debug.WriteLine("\t" + answer.Answer + (answer.IsCorrect ? "CORRECT" : ""));
                }
            }
            */

            navigationStore.CurrentViewModel = new StartGameViewModel(navigationStore);
            navigationStore.CurrentDataStore = dataStore;

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
