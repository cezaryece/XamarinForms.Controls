using System;
using System.Windows.Input;

namespace XamarinForms.Controls.Data.Mvvm
{
	public sealed class RelayCommand : ICommand
	{
		private readonly Action<object> _executeParam;
		private readonly Func<object, bool> _canExecuteParam;

		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public RelayCommand(Action<object> execute) //: base(execute)
		{
			_executeParam = execute;
			OnCanExecuteChanged();
		}

		public RelayCommand(Action execute) //: base(execute)
		{
			_execute = execute;
			OnCanExecuteChanged();
		}

		public RelayCommand(Action<object> execute, Func<object, bool> canExecute) // : base(execute, canExecute)
		{
			_executeParam = execute;
			_canExecuteParam = canExecute;
			OnCanExecuteChanged();
		}

		public RelayCommand(Action execute, Func<bool> canExecute) //: base(execute, canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
			OnCanExecuteChanged();
		}

		public bool CanExecute(object parameter) { return (_canExecuteParam?.Invoke(parameter) ?? true) && (_canExecute?.Invoke() ?? true); }

		public void Execute(object parameter)
		{
			_executeParam?.Invoke(parameter);
			_execute?.Invoke();
		}

		public event EventHandler CanExecuteChanged;

		private void OnCanExecuteChanged() { CanExecuteChanged?.Invoke(this, EventArgs.Empty); }
	}
}