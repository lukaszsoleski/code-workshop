using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMdemo.ViewModels
{
    class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute; 

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null )
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute; 

        }




        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null) CommandManager.RequerySuggested += value;

            }

            remove
            {
                if (_canExecute != null) CommandManager.RequerySuggested -= value; 
            }
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter); 
        }

        public void Execute(object parameter)
        {
            _execute(parameter); 
        }

    }
}
