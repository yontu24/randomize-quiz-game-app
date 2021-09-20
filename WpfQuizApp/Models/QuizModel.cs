using System.Collections.ObjectModel;

namespace WpfQuizApp.Models
{
    public class QuizModel
    {
        public ObservableCollection<QuizEntity> Quizes { get; set; }
        public int CurrentQuiz { get; set; }
    }
}
