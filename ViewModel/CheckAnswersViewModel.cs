using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace QuizMVVM.ViewModel
{
    internal class CheckAnswersViewModel : MainViewModel, INotifyPropertyChanged
    {
        private Brush _buttonABackgroundCheckAnswers;
        private Brush _buttonBBackgroundCheckAnswers;
        private Brush _buttonCBackgroundCheckAnswers;
        private Brush _buttonDBackgroundCheckAnswers;

        private bool _isThisLastQuestion;
        public CheckAnswersViewModel()
        {
            _questionNumber = 0;
            Title = "Check answers " + Title;
            if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == true && Answers[_questionNumber].Contains("A"))
                ButtonABackgroundCheckAnswers = Brushes.Green;
            else if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == true && !Answers[_questionNumber].Contains("A"))
                ButtonABackgroundCheckAnswers = Brushes.LightGreen;
            else if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == false && Answers[_questionNumber].Contains("A"))
                ButtonABackgroundCheckAnswers = Brushes.Red;

            if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == true && Answers[_questionNumber].Contains("B"))
                ButtonBBackgroundCheckAnswers = Brushes.Green;
            else if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == true && !Answers[_questionNumber].Contains("B"))
                ButtonBBackgroundCheckAnswers = Brushes.LightGreen;
            else if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == false && Answers[_questionNumber].Contains("B"))
                ButtonBBackgroundCheckAnswers = Brushes.Red;

            if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == true && Answers[_questionNumber].Contains("C"))
                ButtonCBackgroundCheckAnswers = Brushes.Green;
            else if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == true && !Answers[_questionNumber].Contains("C"))
                ButtonCBackgroundCheckAnswers = Brushes.LightGreen;
            else if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == false && Answers[_questionNumber].Contains("C"))
                ButtonCBackgroundCheckAnswers = Brushes.Red;

            if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == true && Answers[_questionNumber].Contains("D"))
                ButtonDBackgroundCheckAnswers = Brushes.Green;
            else if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == true && !Answers[_questionNumber].Contains("D"))
                ButtonDBackgroundCheckAnswers = Brushes.LightGreen;
            else if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == false && Answers[_questionNumber].Contains("D"))
                ButtonDBackgroundCheckAnswers = Brushes.Red;

            NextQuestionCheckAnswersCommand = new RelayCommand(NextQuestionCheckAnswers, CanGetNextQuestion);
        }

        public Brush ButtonABackgroundCheckAnswers
        {
            get { return _buttonABackgroundCheckAnswers; }
            set { _buttonABackgroundCheckAnswers = value; OnPropertyChanged(); }
        }

        public Brush ButtonBBackgroundCheckAnswers
        {
            get { return _buttonBBackgroundCheckAnswers; }
            set { _buttonBBackgroundCheckAnswers = value; OnPropertyChanged(); }
        }

        public Brush ButtonCBackgroundCheckAnswers
        {
            get { return _buttonCBackgroundCheckAnswers; }
            set { _buttonCBackgroundCheckAnswers = value; OnPropertyChanged(); }
        }

        public Brush ButtonDBackgroundCheckAnswers
        {
            get { return _buttonDBackgroundCheckAnswers; }
            set { _buttonDBackgroundCheckAnswers = value; OnPropertyChanged(); }
        }

        public ICommand NextQuestionCheckAnswersCommand { get; set; }

        private void NextQuestionCheckAnswers(object obj)
        {
            // Pokazywanie kolejnych odpowiedzi i ustawianie wartości odpowiedzi
            if (_questionNumber < quizClass?.Questions?.Count - 1)
            {
                _questionNumber++;
                if (_questionNumber >= quizClass?.Questions?.Count - 1)
                    _isThisLastQuestion = true;
                Question = quizClass?.Questions?[_questionNumber]?.Content?.ToString();
                AnswerA = quizClass?.Questions?[_questionNumber]?.Answers?[0]?.Content?.ToString();
                AnswerB = quizClass?.Questions?[_questionNumber]?.Answers?[1]?.Content?.ToString();
                AnswerC = quizClass?.Questions?[_questionNumber]?.Answers?[2]?.Content?.ToString();
                AnswerD = quizClass?.Questions?[_questionNumber]?.Answers?[3]?.Content?.ToString();

                ButtonABackgroundCheckAnswers = Brushes.LightGray;
                ButtonBBackgroundCheckAnswers = Brushes.LightGray;
                ButtonCBackgroundCheckAnswers = Brushes.LightGray;
                ButtonDBackgroundCheckAnswers = Brushes.LightGray;

                if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == true && Answers[_questionNumber].Contains("A"))
                    ButtonABackgroundCheckAnswers = Brushes.Green;
                else if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == true && !Answers[_questionNumber].Contains("A"))
                    ButtonABackgroundCheckAnswers = Brushes.LightGreen;
                else if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == false && Answers[_questionNumber].Contains("A"))
                    ButtonABackgroundCheckAnswers = Brushes.Red;

                if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == true && Answers[_questionNumber].Contains("B"))
                    ButtonBBackgroundCheckAnswers = Brushes.Green;
                else if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == true && !Answers[_questionNumber].Contains("B"))
                    ButtonBBackgroundCheckAnswers = Brushes.LightGreen;
                else if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == false && Answers[_questionNumber].Contains("B"))
                    ButtonBBackgroundCheckAnswers = Brushes.Red;

                if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == true && Answers[_questionNumber].Contains("C"))
                    ButtonCBackgroundCheckAnswers = Brushes.Green;
                else if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == true && !Answers[_questionNumber].Contains("C"))
                    ButtonCBackgroundCheckAnswers = Brushes.LightGreen;
                else if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == false && Answers[_questionNumber].Contains("C"))
                    ButtonCBackgroundCheckAnswers = Brushes.Red;

                if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == true && Answers[_questionNumber].Contains("D"))
                    ButtonDBackgroundCheckAnswers = Brushes.Green;
                else if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == true && !Answers[_questionNumber].Contains("D"))
                    ButtonDBackgroundCheckAnswers = Brushes.LightGreen;
                else if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == false && Answers[_questionNumber].Contains("D"))
                    ButtonDBackgroundCheckAnswers = Brushes.Red;
            }
        }

        private bool CanGetNextQuestion(object obj) => !_isThisLastQuestion;
    }
}
