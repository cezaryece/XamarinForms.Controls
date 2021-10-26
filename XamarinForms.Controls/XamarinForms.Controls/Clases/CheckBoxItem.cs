using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace XamarinForms.Controls.Clases
{
	public class CheckBoxItem : INotifyPropertyChanged
	{
		private object _tagObject;
		private string _keyString;
		private bool _isChecked;

		public object TagObject
		{
			get => _tagObject;
			set
			{
				_tagObject = value;
				OnPropertyChanged();
			}
		}

		public string KeyString
		{
			get => _keyString;
			set
			{
				if (string.Equals(_keyString, value)) return;
				_keyString = value;
				OnPropertyChanged();
			}
		}

		public bool IsChecked
		{
			get => _isChecked;
			set
			{
				if (_isChecked == value) return;
				_isChecked = value;
				OnPropertyChanged();
			}
		}

		public CheckBoxItem(string key, object tag, bool check = false)
		{
			_keyString = key;
			_tagObject = tag;
			_isChecked = check;
		}

		public CheckBoxItem() { }

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
	}
}