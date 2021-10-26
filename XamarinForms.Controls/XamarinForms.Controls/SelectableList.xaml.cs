using System;
using System.Linq;
using Xamarin.Forms;
using XamarinForms.Controls.Clases;
using XamarinForms.Controls.Data;

namespace XamarinForms.Controls
{
	/// <summary>
	///     Interaction logic for CheckboxList.xaml
	/// </summary>

	// ReSharper disable once RedundantExtendsListEntry
	public partial class SelectableList : ContentView
	{
		public ListView List { get; }
		public StackLayout ListLayout { get; }
		public SelectableListViewModel ViewModel => (SelectableListViewModel)BindingContext;

		public SelectableList()
		{
			BindingContext = new SelectableListViewModel();
			ListLayout = this.FindByName<StackLayout>("ListLayoutName");
			List = this.FindByName<ListView>("ListName");
			List.ItemTapped += (s, args) =>
			{
				var listItem = ViewModel.Items.FirstOrDefault(i => i.Id == ((ListItem<object>)args.Item).Id);
				if (listItem == null) throw new Exception("Cannot find tapped item");
				listItem.Selected = !listItem.Selected;
				List.SelectedItem = null;
				ViewModel.OnPropertyChanged(nameof(ViewModel.SelectedItems));
			};
		}
	}
}