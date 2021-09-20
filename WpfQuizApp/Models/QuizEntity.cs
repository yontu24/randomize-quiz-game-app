using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfQuizApp.Models
{
    public class QuizEntity
    {
        public ObservableCollection<AnswerEntity> Answers { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string Difficulty { get; set; }
    }
}
