using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace XamarinForms.Controls.Clases
{
	public class SelectableListItem<T> : ListItem<T>, INotifyPropertyChanged
	{
		private bool _selected;

		public bool Selected
		{
			get => _selected;
			set
			{
				if (_selected == value) return;
				_selected = value;
				OnPropertyChanged();
				OnPropertyChanged("TextColor");
			}
		}

		public Color TextColor => Selected ? Color.Aqua : Color.White;

		public SelectableListItem(T item, int id, string name = null, string details = null) : base(item, id, name, details) { Selected = false; }

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
	}
}