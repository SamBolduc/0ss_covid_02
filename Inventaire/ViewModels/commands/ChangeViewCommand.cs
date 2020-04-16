using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BillingManagement.UI.ViewModels.commands
{
    public class ChangeViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<string> _actions;

        public ChangeViewCommand(Action<string> actions)
        {
            _actions = actions;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _actions?.Invoke(parameter as string);
        }
    }
}
