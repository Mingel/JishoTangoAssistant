using System;
using System.Windows.Input;

namespace JishoTangoAssistant.Services.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object?> executeAction;
        private readonly Func<object?, bool> canExecuteAction;

        public DelegateCommand(Action<object?> executeAction, Func<object?, bool> canExecuteAction)
        {
            this.executeAction = executeAction;
            this.canExecuteAction = canExecuteAction;
        }

        public void Execute(object? parameter) => executeAction(parameter);

        public bool CanExecute(object? parameter) => canExecuteAction.Invoke(parameter);

        public event EventHandler? CanExecuteChanged;

        public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
