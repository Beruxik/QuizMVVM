﻿using QuizMVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace QuizMVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        // Odpowiedzi
        private string _answerA;
        private string _answerB;
        private string _answerC;
        private string _answerD;
        private string _question;
        private string _score;
        private int _questionNumber = 0;

        private bool _isThisLastQuestion;

        // Timer
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render);
        private int _totalSeconds = 0;
        private string _timerText;

        // Deserializacja danych
        static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        static string fileName = @"..\..\..\quiz-test.json";
        static string jsonString = File.ReadAllText(fileName);
        QuizClass? quizClass = JsonSerializer.Deserialize<QuizClass>(jsonString, options);

        public MainViewModel()
        {
            // Wyświetlenie odpowiedzi
            Question = quizClass?.Questions?[_questionNumber]?.Content?.ToString();
            AnswerA = quizClass?.Questions?[_questionNumber]?.Answers?[0]?.Content?.ToString();
            AnswerB = quizClass?.Questions?[_questionNumber]?.Answers?[1]?.Content?.ToString();
            AnswerC = quizClass?.Questions?[_questionNumber]?.Answers?[2]?.Content?.ToString();
            AnswerD = quizClass?.Questions?[_questionNumber]?.Answers?[3]?.Content?.ToString();

            AnswerButtonCommand = new RelayCommand(AnswerButton);

            NextQuestionCommand = new RelayCommand(NextQuestion, CanGetNextQuestion);
            EndCommand = new RelayCommand(End);

            Score = "Wynik: 0";

            // Timer
            TimerText = "00:00:00";
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }

        

        // Timer
        public string TimerText
        {
            get
            {
                return this._timerText;
            }
            set
            {
                this._timerText = value;
                OnPropertyChanged();
            }
        }
        

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this._totalSeconds += 1;
            TimerText = string.Format("{0:hh\\:mm\\:ss}", TimeSpan.FromSeconds(this._totalSeconds).Duration());
            CommandManager.InvalidateRequerySuggested();
        }

        public string AnswerA
        {
            get { return _answerA; }
            set { _answerA = value; OnPropertyChanged(); }
        }

        public string AnswerB
        {
            get { return _answerB; }
            set { _answerB = value; OnPropertyChanged(); }
        }

        public string AnswerC
        {
            get { return _answerC; }
            set { _answerC = value; OnPropertyChanged(); }
        }

        public string AnswerD
        {
            get { return _answerD; }
            set { _answerD = value; OnPropertyChanged(); }
        }

        public string Question
        {
            get { return _question; }
            set { _question = value; OnPropertyChanged(); }
        }

        public string Score
        {
            get { return _score; }
            set { _score = value; OnPropertyChanged(); }
        }

        public ICommand AnswerButtonCommand { get; set; }

        private void AnswerButton(object obj)
        {
            if(obj as string == "A")
            {
                if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == true)
                {
                    int wynik = Int32.Parse(Regex.Match(Score.ToString(), @"\d+").Value);
                    wynik++;
                    Score = $"Wynik: {wynik.ToString()}";
                }
            }
            if (obj as string == "B")
            {
                if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == true)
                {
                    int wynik = Int32.Parse(Regex.Match(Score.ToString(), @"\d+").Value);
                    wynik++;
                    Score = $"Wynik: {wynik.ToString()}";
                }
            }
            if (obj as string == "C")
            {
                if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == true)
                {
                    int wynik = Int32.Parse(Regex.Match(Score.ToString(), @"\d+").Value);
                    wynik++;
                    Score = $"Wynik: {wynik.ToString()}";
                }
            }
            if (obj as string == "D")
            {
                if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == true)
                {
                    int wynik = Int32.Parse(Regex.Match(Score.ToString(), @"\d+").Value);
                    wynik++;
                    Score = $"Wynik: {wynik.ToString()}";
                }
            } 
        }

        public ICommand NextQuestionCommand { get; set; }

        private void NextQuestion(object obj)
        {
            if (_questionNumber < quizClass?.Questions?.Count - 1)
            {
                _questionNumber++;
                if(_questionNumber >= quizClass?.Questions?.Count - 1)
                    _isThisLastQuestion = true;
                Question = quizClass?.Questions?[_questionNumber]?.Content?.ToString();
                AnswerA = quizClass?.Questions?[_questionNumber]?.Answers?[0]?.Content?.ToString();
                AnswerB = quizClass?.Questions?[_questionNumber]?.Answers?[1]?.Content?.ToString();
                AnswerC = quizClass?.Questions?[_questionNumber]?.Answers?[2]?.Content?.ToString();
                AnswerD = quizClass?.Questions?[_questionNumber]?.Answers?[3]?.Content?.ToString();
            }
        }

        private bool CanGetNextQuestion(object obj) => !_isThisLastQuestion;

        public ICommand EndCommand { get; set; }

        private void End(object obj)
        {
            _dispatcherTimer.Stop();
            MessageBox.Show($"Twój wynik: {Regex.Match(Score.ToString(), @"\d+").Value}\n" +
                $"Rozwiązanie testu zajęło Ci {TimerText.ToString()} czasu.");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
