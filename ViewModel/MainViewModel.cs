using QuizMVVM.Model;
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
using System.Windows.Media;
using System.Windows.Threading;

namespace QuizMVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _title;
        // Odpowiedzi
        private string _answerA;
        private string _answerB;
        private string _answerC;
        private string _answerD;
        private string _question;
        private string _score;
        private int _scoreCount = 0;
        private Brush _buttonABackground;
        private Brush _buttonBBackground;
        private Brush _buttonCBackground;
        private Brush _buttonDBackground;
        protected int _questionNumber = 0;
        private bool _isAnswerAChecked;
        private bool _isAnswerBChecked;
        private bool _isAnswerCChecked;
        private bool _isAnswerDChecked;
        static protected List<List<string>> _answers = new List<List<string>>();
        private readonly List<List<string>> _answersCheck = _answers;

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

        string fileString = File.ReadAllText(fileName);

        static string jsonString = File.ReadAllText(fileName);
        protected QuizClass? quizClass = JsonSerializer.Deserialize<QuizClass>(jsonString, options);


        public MainViewModel()
        {
            //Decode.Decoding(fileName, 3);

            Title = quizClass?.Title;
            for (int i = 0; i < quizClass?.Questions?.Count; i++)
                _answers.Add(new List<string>());

            // Wyświetlenie odpowiedzi
            Question = quizClass?.Questions?[_questionNumber]?.Content?.ToString();
            AnswerA = quizClass?.Questions?[_questionNumber]?.Answers?[0]?.Content?.ToString();
            AnswerB = quizClass?.Questions?[_questionNumber]?.Answers?[1]?.Content?.ToString();
            AnswerC = quizClass?.Questions?[_questionNumber]?.Answers?[2]?.Content?.ToString();
            AnswerD = quizClass?.Questions?[_questionNumber]?.Answers?[3]?.Content?.ToString();

            ButtonABackground = Brushes.LightGray;
            ButtonBBackground = Brushes.LightGray;
            ButtonCBackground = Brushes.LightGray;
            ButtonDBackground = Brushes.LightGray;

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

        public string Title 
        { 
            get { return this._title; } 
            set { this._title = value; OnPropertyChanged(); } 
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

        public Brush ButtonABackground
        {
            get { return _buttonABackground; }
            set { _buttonABackground = value; OnPropertyChanged(); }
        }

        public Brush ButtonBBackground
        {
            get { return _buttonBBackground; }
            set { _buttonBBackground = value; OnPropertyChanged(); }
        }

        public Brush ButtonCBackground
        {
            get { return _buttonCBackground; }
            set { _buttonCBackground = value; OnPropertyChanged(); }
        }

        public Brush ButtonDBackground
        {
            get { return _buttonDBackground; }
            set { _buttonDBackground = value; OnPropertyChanged(); }
        }

        public ICommand AnswerButtonCommand { get; set; }

        private void AnswerButton(object obj)
        {
            // Zmiana koloru przycisku przy jego kliknięciu
            if (obj as string == "A")
            {
                if (ButtonABackground == Brushes.LightGray)
                {
                    ButtonABackground = Brushes.Cyan;
                    _isAnswerAChecked = true;
                }
                else
                {
                    ButtonABackground = Brushes.LightGray;
                    _isAnswerAChecked = false;
                }
            }
            if (obj as string == "B")
            {
                if (ButtonBBackground == Brushes.LightGray)
                {
                    ButtonBBackground = Brushes.Cyan;
                    _isAnswerBChecked = true;
                }
                else
                {
                    ButtonBBackground = Brushes.LightGray;
                    _isAnswerBChecked = false;
                }
            }
            if (obj as string == "C")
            {
                if (ButtonCBackground == Brushes.LightGray)
                {
                    ButtonCBackground = Brushes.Cyan;
                    _isAnswerCChecked = true;
                }
                else
                {
                    ButtonCBackground = Brushes.LightGray;
                    _isAnswerCChecked = false;
                }
            }
            if (obj as string == "D")
            {
                if (ButtonDBackground == Brushes.LightGray)
                {
                    ButtonDBackground = Brushes.Cyan;
                    _isAnswerDChecked = true;
                }
                else
                {
                    ButtonDBackground = Brushes.LightGray;
                    _isAnswerDChecked = false;
                }
            }
        }

        public ICommand NextQuestionCommand { get; set; }

        private void NextQuestion(object obj)
        {
            // Sprawdzanie poprawności zaznaczonych odpowiedzi
            if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == true && _isAnswerAChecked == true) // A
                _scoreCount++;
            if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == false && _isAnswerAChecked == true)
                _scoreCount--;

            if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == true && _isAnswerBChecked == true) // B
                _scoreCount++;
            if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == false && _isAnswerBChecked == true)
                _scoreCount--;

            if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == true && _isAnswerCChecked == true) // C
                _scoreCount++;
            if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == false && _isAnswerCChecked == true)
                _scoreCount--;

            if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == true && _isAnswerDChecked == true) // D
                _scoreCount++;
            if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == false && _isAnswerDChecked == true)
                _scoreCount--;

            Score = $"Wynik: {_scoreCount.ToString()}";

            // Wpisywanie zaznaczonych odpowiedzi do listy
            if (_isAnswerAChecked == true)
                _answers[_questionNumber].Add("A");
            if (_isAnswerBChecked == true)
                _answers[_questionNumber].Add("B");
            if (_isAnswerCChecked == true)
                _answers[_questionNumber].Add("C");
            if (_isAnswerDChecked == true)
                _answers[_questionNumber].Add("D");

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
                _isAnswerAChecked = false;
                _isAnswerBChecked = false;
                _isAnswerCChecked = false;
                _isAnswerDChecked = false;
                ButtonABackground = Brushes.LightGray;
                ButtonBBackground = Brushes.LightGray;
                ButtonCBackground = Brushes.LightGray;
                ButtonDBackground = Brushes.LightGray;
            }
        }

        private bool CanGetNextQuestion(object obj) => !_isThisLastQuestion;

        public ICommand EndCommand { get; set; }

        private void End(object obj)
        {
            // Sprawdzanie poprawności zaznaczonych odpowiedzi
            if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == true && _isAnswerAChecked == true) // A
                _scoreCount++;
            if (quizClass?.Questions?[_questionNumber]?.Answers?[0]?.IsCorrect == false && _isAnswerAChecked == true)
                _scoreCount--;

            if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == true && _isAnswerBChecked == true) // B
                _scoreCount++;
            if (quizClass?.Questions?[_questionNumber]?.Answers?[1]?.IsCorrect == false && _isAnswerBChecked == true)
                _scoreCount--;

            if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == true && _isAnswerCChecked == true) // C
                _scoreCount++;
            if (quizClass?.Questions?[_questionNumber]?.Answers?[2]?.IsCorrect == false && _isAnswerCChecked == true)
                _scoreCount--;

            if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == true && _isAnswerDChecked == true) // D
                _scoreCount++;
            if (quizClass?.Questions?[_questionNumber]?.Answers?[3]?.IsCorrect == false && _isAnswerDChecked == true)
                _scoreCount--;

            Score = $"Wynik: {_scoreCount.ToString()}";

            // Wpisywanie zaznaczonych odpowiedzi do listy
            if (_isAnswerAChecked == true)
                _answers[_questionNumber].Add("A");
            if (_isAnswerBChecked == true)
                _answers[_questionNumber].Add("B");
            if (_isAnswerCChecked == true)
                _answers[_questionNumber].Add("C");
            if (_isAnswerDChecked == true)
                _answers[_questionNumber].Add("D");

            // Zatrzymanie timera i wyświetlenie wyniku
            _dispatcherTimer.Stop();
            MessageBox.Show($"Twój wynik: {_scoreCount}\n" +
                $"Rozwiązanie testu zajęło Ci {TimerText.ToString()} czasu.");

            CheckAnswers checkAnswers = new CheckAnswers();
            checkAnswers.ShowDialog();
            CloseAction();
        }

        public List<List<string>> Answers
        {
            get { return _answersCheck; }
        }

        public Action CloseAction { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
