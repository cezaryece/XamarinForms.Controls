using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace XamarinForms.Controls.Data.Mvvm
{
	public class ViewModelBase : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private bool _isDisposed;
		private bool _isBusy;

		public bool IsBusy
		{
			get => _isBusy;
			set
			{
				_isBusy = value;
				Device.BeginInvokeOnMainThread(() =>
				{
					RaisePropertyChanged("IsBusy");
					OnBusyChange();
				});
			}
		}

		protected virtual void OnBusyChange() { }

		[NotifyPropertyChangedInvocator]
		public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

		protected virtual void RaisePropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

		public virtual void Cleanup() { Dispose(); }

		public void Dispose()
		{
			if (!_isDisposed)
			{
				PropertyChanged = null;
				_isDisposed = true;
			}
		}
	}
}