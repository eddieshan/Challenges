using System;
using System.Windows.Input;

namespace Drills.TennisMatch.Desktop.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action methodToExecute) : this(methodToExecute, null)
        {
        }

        public RelayCommand(Action methodToExecute, Func<bool> canExecuteAction)
        {
            _action = methodToExecute;
            _canExecute = canExecuteAction;
        }

        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute();

        public void Execute(object parameter) => _action();
    }

    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<T> _action = null;
        private readonly Predicate<T> _canExecute = null;

        public RelayCommand(Action<T> execute): this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _action = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null? true : _canExecute((T)parameter);

        public void Execute(object parameter) => _action((T)parameter);
    }
}
