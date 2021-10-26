using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using XamarinForms.Controls.Clases;

namespace XamarinForms.Controls.Data
{
	/// <summary>
	///     This class contains properties that a View can data bind to.
	///     <para>
	///         See http://www.galasoft.ch/mvvm
	///     </para>
	/// </summary>
	public class SelectableListViewModel : INotifyPropertyChanged
	{
		private List<SelectableListItem<object>> _items;

		public List<SelectableListItem<object>> Items
		{
			get => _items;
			set
			{
				_items = value;
				OnPropertyChanged();
			}
		}

		private string _header;

		public string Header
		{
			get => _header;
			set
			{
				_header = value;
				OnPropertyChanged();
			}
		}

		public object SelectedItems => GetSelected();

		public List<object> GetSelected() { return Items.Where(c => c.Selected).Select(ch => ch.Item).ToList(); }

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

		public void SetSelectableListViewModel(string header, List<SelectableListItem<object>> items)
		{
			Header = header;
			Items = items;
		}
	}
}