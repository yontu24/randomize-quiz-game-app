using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml;
using WpfQuizApp.Models;
using WpfQuizApp.Store;

namespace WpfQuizApp.Services
{
    public sealed class ParseQuizService
    {
        private readonly XmlNode root;
        private const string DOCUMENT_LOCATION = "C:\\Users\\Alexandra\\source\\repos\\WpfQuizApp\\WpfQuizApp\\Quiz.xml";
        private const string ROOT_ELEMENT = "/Quizes";

        #region SingletonPattern
        private ParseQuizService()
        {
            XmlDocument doc = new();
            doc.Load(DOCUMENT_LOCATION);
            root = doc.DocumentElement.SelectSingleNode(ROOT_ELEMENT);
        }

        private static ParseQuizService instance;
        public static ParseQuizService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ParseQuizService();
                }
                return instance;
            }
        }
        #endregion

        public void ParseQuiz(DataStore dataStore)
        {
            dataStore.Quiz.Quizes = new();
            foreach (XmlNode node in root.ChildNodes)
            {
                QuizEntity quiz = new();
                quiz.Difficulty = node.Attributes["difficulty"].InnerText;
                if (quiz.Difficulty == null)
                {
                    quiz.Difficulty = "Easy";
                }

                quiz.Question = node.ChildNodes[0].InnerText;
                if (node.ChildNodes[0].Attributes["type"]?.InnerText == "single")
                {
                    quiz.Type = "Radio";
                }
                else if (node.ChildNodes[0].Attributes["type"]?.InnerText == "multiple")
                {
                    quiz.Type = "Check";
                }

                quiz.Answers = new();
                foreach (XmlNode answerNode in node.ChildNodes[1])
                {
                    AnswerEntity answerEntity = new()
                    {
                        Answer = answerNode.InnerText,
                        IsSelected = false,
                        IsCorrect = answerNode.Attributes["correct"]?.InnerText == "true"
                    };

                    quiz.Answers.Add(answerEntity);
                }

                dataStore.Quiz.Quizes.Add(quiz);
            }
        }

        // https://stackoverflow.com/questions/273313/randomize-a-listt
        public static void Shuffle(ObservableCollection<QuizEntity> list)
        {
            RNGCryptoServiceProvider provider = new();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do
                {
                    provider.GetBytes(box);
                }
                while (!(box[0] < n * (byte.MaxValue / n)));

                int k = box[0] % n;
                n--;

                QuizEntity value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
