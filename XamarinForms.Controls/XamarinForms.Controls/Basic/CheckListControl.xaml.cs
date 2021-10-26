using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using XamarinForms.Controls.Clases;

namespace XamarinForms.Controls.Basic
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class CheckListControl : ContentView
	{
		protected List<CheckboxExtended> CheckBoxItems { get; set; } = new List<CheckboxExtended>();
		public event EventHandler SelectedChanged;

		public CheckListControl()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;

			HandleFontSizeChanged(this, null, FontSize);
			HandleTextColorChanged(this, null, TextColor);
			HandleCheckedColorChanged(this, null, CheckedTextColor);
			HandleUnCheckedColorChanged(this, null, UnCheckedTextColor);
			UpdateSelectorsGrid();
		}

		public static BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<CheckBoxItem>), typeof(CheckListControl), null, BindingMode.OneWay, propertyChanged: HandleItemsChanged);

		private static void HandleItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			Debug.WriteLine("HandleItemsChanged");
			var me = (CheckListControl)bindable;
			me.CheckBoxItems = new List<CheckboxExtended>();
			var newList = newvalue as List<CheckBoxItem>;
			if (newList == null || !newList.Any())
			{
				me.SelectedItems = new List<object>();
				return;
			}

			foreach (var checkBoxItem in newList)
			{
				var checkbox = new CheckboxExtended();
				if (me.CheckBoxStyle != null)
				{
					checkbox.Style = me.CheckBoxStyle;
				}
				else
				{
					checkbox.FontSize = me.FontSize;
					checkbox.DefaultTextColor = me.TextColor;
				}

				;
				checkbox.SetCheckBoxItemTag(checkBoxItem);
				checkbox.CheckedChanged += (sender, args) =>
				{
					Debug.WriteLine("checkbox.CheckedChanged");
					me.SelectedItems = new List<object>(me.Items.Where(i => i.IsChecked).Select(i => i.TagObject));
				};
				me.CheckBoxItems.Add(checkbox);
			}

			me.UpdateSelectorsGrid();
		}

		public List<CheckBoxItem> Items { get => (List<CheckBoxItem>)GetValue(ItemsProperty); set => SetValue(ItemsProperty, value); }

		public static BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(List<object>), typeof(CheckListControl), null, BindingMode.TwoWay, propertyChanged: HandleSelectedItemsChanged);

		private static void HandleSelectedItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (CheckListControl)bindable;
			var selected = (List<object>)newvalue;
			if (me.Items == null || !me.Items.Any()) return;
			foreach (var item in me.Items) item.IsChecked = selected != null && selected.Contains(item.TagObject);
			me.SelectedChanged?.Invoke(me, null);
		}

		public List<object> SelectedItems { get => (List<object>)GetValue(SelectedItemsProperty); set => SetValue(SelectedItemsProperty, value); }

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckListControl), Color.Default, BindingMode.TwoWay, propertyChanging: HandleTextColorChanged);

		private static void HandleTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (CheckListControl)bindable;
			foreach (var selector in me.CheckBoxItems)
				selector.DefaultTextColor = (Color)newvalue;
		}

		public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

		public static readonly BindableProperty CheckedTextColorProperty =
			BindableProperty.Create(nameof(CheckedTextColor), typeof(Color), typeof(CheckListControl), Color.Default, BindingMode.TwoWay, propertyChanged: HandleCheckedColorChanged);

		private static void HandleCheckedColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (CheckListControl)bindable;
			foreach (var selector in me.CheckBoxItems)
				selector.CheckedTextColor = (Color)newvalue;
		}

		public Color CheckedTextColor { get => (Color)GetValue(CheckedTextColorProperty); set => SetValue(CheckedTextColorProperty, value); }

		public static readonly BindableProperty UnCheckedTextColorProperty =
			BindableProperty.Create(nameof(UnCheckedTextColor), typeof(Color), typeof(CheckListControl), Color.Default, BindingMode.TwoWay, propertyChanged: HandleUnCheckedColorChanged);

		private static void HandleUnCheckedColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (CheckListControl)bindable;
			foreach (var selector in me.CheckBoxItems)
				selector.UnCheckedTextColor = (Color)newvalue;
		}

		public Color UnCheckedTextColor { get => (Color)GetValue(UnCheckedTextColorProperty); set => SetValue(UnCheckedTextColorProperty, value); }

		public static BindableProperty ItemsInRowProperty = BindableProperty.Create(nameof(ItemsInRow), typeof(int), typeof(CheckListControl), 1, BindingMode.TwoWay, propertyChanging: HandleItemsInRowChanged);
		private static void HandleItemsInRowChanged(BindableObject bindable, object oldvalue, object newvalue) { ((CheckListControl)bindable).UpdateSelectorsGrid(); }
		public int ItemsInRow { get => (int)GetValue(ItemsInRowProperty); set => SetValue(ItemsInRowProperty, value); }

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CheckListControl), 14.0, BindingMode.TwoWay, propertyChanging: HandleFontSizeChanged);

		private static void HandleFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (CheckListControl)bindable;
			foreach (var checkBoxItem in me.CheckBoxItems)
				checkBoxItem.FontSize = Utils.GetScalledFontSize((double)newvalue);
		}

		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

		public static readonly BindableProperty CheckBoxStyleProperty = BindableProperty.Create(nameof(CheckBoxStyle), typeof(Style), typeof(CheckListControl), null, BindingMode.OneWay, propertyChanged: HandleCheckBoxStyleChanged);

		private static void HandleCheckBoxStyleChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as CheckListControl;
			if (me.Items == null) return;
			foreach (var checkBoxItem in me.CheckBoxItems)
			{
				checkBoxItem.Style = (Style)newvalue;
				checkBoxItem.OnTextChanged();
			}
		}

		public Style CheckBoxStyle { get => (Style)GetValue(CheckBoxStyleProperty); set => SetValue(CheckBoxStyleProperty, value); }

		private void UpdateSelectorsGrid()
		{
			SelectorsGrid.Children.Clear();
			SelectorsGrid.IsVisible = false;
			if (CheckBoxItems.Count > 0)
			{
				var index = 0;
				var rowLayout = new StackLayout
				{
					Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand
				};
				for (var i = 0; i < CheckBoxItems.Count; i++)
				{
					var checkBox = CheckBoxItems[i];
					Debug.WriteLine("UpdateSelectorsGrid add Checkbox " + (checkBox.Checked ? "checked" : "Unchecked"));
					rowLayout.Children.Add(checkBox);
					index++;
					if (ItemsInRow > 0 && index == ItemsInRow)
					{
						SelectorsGrid.Children.Add(rowLayout);
						index = 0;
						rowLayout = new StackLayout
						{
							Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand
						};
					}
				}

				SelectorsGrid.Children.Add(rowLayout);
				SelectorsGrid.IsVisible = true;
			}

			ForceLayout();
		}

		#region Overrides of BindableObject
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == null) return;
			if (propertyName != "IsEnabled") return;
			foreach (var selector in CheckBoxItems)
				selector.IsEnabled = IsEnabled;
		}
		#endregion
	}
}