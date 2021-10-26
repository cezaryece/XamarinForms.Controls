using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	/// <summary>
	///     Interaction logic for ComboBoxControl.xaml
	/// </summary>

	// ReSharper disable once RedundantExtendsListEntry
	public partial class ComboBoxControl : ContentView
	{
		public ComboBoxControl()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			PickerElement.IsEnabled = IsEnabled;
			PickerElement.BackgroundColor = BackgroundColor;
		}

		public static BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<string>), typeof(ComboBoxControl), new List<string>(), propertyChanging: HandleItemsChanged);

		private static void HandleItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			if ((Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.Windows) && newvalue != null && ((List<string>)newvalue).Count < 6)
			{
				//dla WindowsPhone dopełnienie do 6 elementów aby wyswietlić w pełnym oknie
				var toAdd = 6 - ((List<string>)newvalue).Count;
				for (var i = 0; i < toAdd; i++)
					((List<string>)newvalue).Add(string.Empty);
			}

			var me = (ComboBoxControl)bindable;
			me.PickerElement.Items.Clear();
			if (newvalue != null && ((List<string>)newvalue).Any())
				foreach (var s in (List<string>)newvalue)
					me.PickerElement.Items.Add(s);
			me.SelectedIndex = -1;
		}

		public List<string> Items { get => (List<string>)GetValue(ItemsProperty); set => SetValue(ItemsProperty, value); }

		public static BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(string), typeof(ComboBoxControl), null, BindingMode.TwoWay, propertyChanging: HandleSelectedItemChanged);

		private static void HandleSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (ComboBoxControl)bindable;
			me.PickerElement.SelectedIndex = newvalue != null ? me.PickerElement.Items.IndexOf((string)newvalue) : -1;
			me.OnPropertyChanged(nameof(SelectedIndex));
		}

		public string SelectedItem { get => (string)GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

		public static BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(ComboBoxControl), -1, BindingMode.TwoWay, propertyChanging: HandleSelectedIndexChanged);

		private static void HandleSelectedIndexChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (ComboBoxControl)bindable;
			var value = (int)newvalue;
			if (me.Items == null || value >= 0 && string.IsNullOrEmpty(me.Items[value]))
				value = -1;
			me.SelectedItem = value > -1 ? me.Items?[value] : null;
			if (me.PickerElement.SelectedIndex == value) return;
			me.PickerElement.SelectedIndex = value;
		}

		public int SelectedIndex { get => (int)GetValue(SelectedIndexProperty); set => SetValue(SelectedIndexProperty, value); }

		private void PickerName_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedIndex == PickerElement.SelectedIndex) return;
			SelectedIndex = PickerElement.SelectedIndex;
		}

		#region Overrides of BindableObject
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == null) return;
			if (propertyName == nameof(IsEnabled))
				PickerElement.IsEnabled = IsEnabled;
			else if (propertyName == nameof(BackgroundColor))
				PickerElement.BackgroundColor = BackgroundColor;
		}
		#endregion
	}
}