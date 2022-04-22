using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace QuizMVVM.ViewModel
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute != null) CommandManager.RequerySuggested -= value;
            }
        }

        private Action<object> execute;
        private Predicate<object> canExecute;

        public RelayCommand(Action<object> execute) : this(execute, null)
        { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {

            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
