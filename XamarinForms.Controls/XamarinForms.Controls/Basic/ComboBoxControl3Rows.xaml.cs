using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	/// <summary>
	///     Interaction logic for ComboControl.xaml
	/// </summary>

	// ReSharper disable once RedundantExtendsListEntry
	public partial class ComboBoxControl3Rows : ContentView
	{
		public ComboBoxControl3Rows()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			PickerElement.IsEnabled = IsEnabled;
			PickerElement.BackgroundColor = BackgroundColor;
			HandleTopFontSizeChanged(this, null, TopFontSize);
			HandleBottomFontSizeChanged(this, null, BottomFontSize);
			BottomLabel.IsVisible = !string.IsNullOrEmpty(BottomLabelText);
		}

		public static BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<string>), typeof(ComboBoxControl3Rows), new List<string>(), propertyChanged: HandleItemsChanged);

		private static void HandleItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			if ((Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.Windows) && newvalue != null && ((List<string>)newvalue).Count < 6)
				for (var i = 0; i < 6 - ((List<string>)newvalue).Count; i++)
					((List<string>)newvalue).Add(string.Empty);
			var me = (ComboBoxControl3Rows)bindable;
			me.PickerElement.Items.Clear();
			if (newvalue != null && ((List<string>)newvalue).Any())
				foreach (var s in (List<string>)newvalue)
					me.PickerElement.Items.Add(s);
			me.SelectedIndex = -1;
		}

		public List<string> Items { get => (List<string>)GetValue(ItemsProperty); set => SetValue(ItemsProperty, value); }

		public static BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(string), typeof(ComboBoxControl3Rows), null, BindingMode.TwoWay, propertyChanging: HandleSelectedItemChanged);

		private static void HandleSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (ComboBoxControl3Rows)bindable;
			me.PickerElement.SelectedIndex = newvalue != null ? me.PickerElement.Items.IndexOf((string)newvalue) : -1;
			me.ForceLayout();
			me.OnPropertyChanged(nameof(SelectedIndex));
		}

		public string SelectedItem { get => (string)GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

		public static BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(ComboBoxControl3Rows), -1, BindingMode.TwoWay, propertyChanging: HandleSelectedIndexChanged);

		private static void HandleSelectedIndexChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (ComboBoxControl3Rows)bindable;
			var value = (int)newvalue;
			if (value >= 0 && string.IsNullOrEmpty(me.Items[value]))
				value = -1;
			me.SelectedItem = value > -1 ? me.Items[value] : null;
			me.PickerElement.SelectedIndex = value;
			me.OnPropertyChanged(nameof(SelectedItem));
		}

		public int SelectedIndex { get => (int)GetValue(SelectedIndexProperty); set => SetValue(SelectedIndexProperty, value); }

		public static readonly BindableProperty TopLabelTextProperty = BindableProperty.Create(nameof(TopLabelText), typeof(string), typeof(ComboBoxControl3Rows), null, propertyChanging: HandleTopLabelTextChanged);
		private static void HandleTopLabelTextChanged(BindableObject bindable, object oldvalue, object newvalue) { ((ComboBoxControl3Rows)bindable).TopLabel.Text = (string)newvalue; }
		public string TopLabelText { get => (string)GetValue(TopLabelTextProperty); set => SetValue(TopLabelTextProperty, value); }

		public static readonly BindableProperty BottomLabelTextProperty = BindableProperty.Create(nameof(BottomLabelText), typeof(string), typeof(ComboBoxControl3Rows), null, propertyChanging: HandleBottomLabelTextChanged);

		private static void HandleBottomLabelTextChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			((ComboBoxControl3Rows)bindable).BottomLabel.Text = (string)newvalue;
			((ComboBoxControl3Rows)bindable).BottomLabel.IsVisible = !string.IsNullOrEmpty((string)newvalue);
		}

		public string BottomLabelText { get => (string)GetValue(BottomLabelTextProperty); set => SetValue(BottomLabelTextProperty, value); }

		public static readonly BindableProperty TopFontSizeProperty = BindableProperty.Create(nameof(TopFontSize), typeof(double), typeof(ComboBoxControl3Rows), 16.0, propertyChanging: HandleTopFontSizeChanged);
		private static void HandleTopFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue) { ((ComboBoxControl3Rows)bindable).TopLabel.FontSize = Utils.GetScalledFontSize((double)newvalue); }
		public double TopFontSize { get => (double)GetValue(TopFontSizeProperty); set => SetValue(TopFontSizeProperty, value); }

		public static readonly BindableProperty BottomFontSizeProperty = BindableProperty.Create(nameof(BottomFontSize), typeof(double), typeof(ComboBoxControl3Rows), 12.0, propertyChanging: HandleBottomFontSizeChanged);
		private static void HandleBottomFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue) { ((ComboBoxControl3Rows)bindable).BottomLabel.FontSize = Utils.GetScalledFontSize((double)newvalue); }
		public double BottomFontSize { get => (double)GetValue(BottomFontSizeProperty); set => SetValue(BottomFontSizeProperty, value); }

		private void PickerName_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedIndex = PickerElement.SelectedIndex;
			if (Device.OS == TargetPlatform.Android) return;
			Unfocus();
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