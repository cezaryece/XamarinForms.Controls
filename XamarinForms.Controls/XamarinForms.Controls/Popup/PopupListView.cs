using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using XamarinForms.Controls.Basic;
using XamarinForms.Controls.Clases;

namespace XamarinForms.Controls.Popup
{
	public class PopupListView : PopupViewBase
	{
		public event EventHandler<List<PopupListItem>> SelectionChanged;

		private List<LabelExtended> _labels;

		public static BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<PopupListItem>), typeof(PopupListView), new List<PopupListItem>(), propertyChanged: HandleItemsChanged);

		private static void HandleItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupListView;
			Debug.Assert(me != null, "me != null");
			me._labels = new List<LabelExtended>();
			var stackLayout = new StackLayout { Spacing = 20 };
			var items = newvalue != null ? (List<PopupListItem>)newvalue : new List<PopupListItem>();
			foreach (var listItem in items)
			{
				var newLabel = new LabelExtended
				{
					Text = listItem.Name, GestureRecognizers =
					{
						new TapGestureRecognizer
						{
							NumberOfTapsRequired = 1, Command = new Command(() => me.ToggleSelectItem(listItem))
						}
					}
					, TextColor = me.TextColor
				};
				newLabel.SetDynamicResource(StyleProperty, nameof(BodyStyle));
				me._labels.Add(newLabel);
				stackLayout.Children.Add(newLabel);
			}

			me.BodyView = new ContentView { Content = stackLayout };
			var selected = items.Where(i => i.Selected).ToList();
			if (selected.Any())
				me.SelectedItems = me.AllowMultiply ? selected : new List<PopupListItem> { selected.FirstOrDefault() };
			else me.SelectedItems = new List<PopupListItem>();
		}

		public List<PopupListItem> Items { get => (List<PopupListItem>)GetValue(ItemsProperty); set => SetValue(ItemsProperty, value); }

		public static readonly BindableProperty BodyStyleProperty = BindableProperty.Create(nameof(BodyStyle), typeof(Style), typeof(PopupListView), null, propertyChanged: HandleBodyStyleChanged);

		private static void HandleBodyStyleChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupListView;
			if (me.Items == null) return;
			foreach (var item in me.Items) me.ResetLabel(item);
		}

		public Style BodyStyle { get => (Style)GetValue(BodyStyleProperty); set => SetValue(BodyStyleProperty, value); }

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(PopupListView), Color.Default, propertyChanged: HandleTextColorChanged);

		private static void HandleTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupListView;
			foreach (var item in me.Items) me.ResetLabel(item);
		}

		public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

		public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(PopupListView), Color.Default, propertyChanged: HandleSelectedColorChanged);

		private static void HandleSelectedColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupListView;
			foreach (var item in me.Items) me.ResetLabel(item);
		}

		public Color SelectedColor { get => (Color)GetValue(SelectedColorProperty); set => SetValue(SelectedColorProperty, value); }

		public static BindableProperty SelectedItemsProperty =
			BindableProperty.Create(nameof(SelectedItems), typeof(List<PopupListItem>), typeof(PopupListView), new List<PopupListItem>(), BindingMode.TwoWay, propertyChanging: HandleSelectedItemsChanged);

		private static void HandleSelectedItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupListView)bindable;
			var selected = (List<PopupListItem>)newvalue;
			if (selected != null && selected.Count > 1 && !me.AllowMultiply)
				me.SelectedItems = new List<PopupListItem>
				{
					selected[0]
				};
			if (me.Items == null) return;
			foreach (var item in me.Items)
			{
				item.Selected = selected != null && selected.Contains(item);
				me.ResetLabel(item);
			}
		}

		public List<PopupListItem> SelectedItems { get => (List<PopupListItem>)GetValue(SelectedItemsProperty); set => SetValue(SelectedItemsProperty, value); }

		public static BindableProperty CloseOnSelectionProperty = BindableProperty.Create(nameof(CloseOnSelection), typeof(bool), typeof(PopupListView), true);
		public bool CloseOnSelection { get => (bool)GetValue(CloseOnSelectionProperty); set => SetValue(CloseOnSelectionProperty, value); }

		public static BindableProperty AllowMultiplyProperty = BindableProperty.Create(nameof(AllowMultiply), typeof(bool), typeof(PopupListView), false, propertyChanged: OnAllowMultiplyChanged);

		private static void OnAllowMultiplyChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupListView)bindable;
			if (!me.SelectedItems.Any()) return;
			me.SelectedItems = new List<PopupListItem> { me.SelectedItems[0] };
		}

		public bool AllowMultiply { get => (bool)GetValue(AllowMultiplyProperty); set => SetValue(AllowMultiplyProperty, value); }

		private void ToggleSelectItem(PopupListItem item)
		{
			if (!AllowMultiply)
			{
				if (!CloseOnSelection && SelectedItems.Any() && item == SelectedItems[0])
				{
					item.Selected = !item.Selected;
				}
				else
				{
					if (SelectedItems.Any())
						SelectedItems[0].Selected = false;
					item.Selected = true;
					SelectedItems = new List<PopupListItem> { item };
				}

				SelectionChanged?.Invoke(this, SelectedItems);
				if (CloseOnSelection)
					OnCloseRequest(this, true);
				return;
			}

			var selected = SelectedItems.ToList();
			if (item.Selected)
			{
				item.Selected = false;
				selected.Remove(item);
			}
			else
			{
				item.Selected = true;
				selected.Add(item);
			}

			SelectedItems = selected;
			SelectionChanged?.Invoke(this, SelectedItems);
		}

		private void ResetLabel(PopupListItem item)
		{
			try
			{
				var label = _labels.First(l => l.Text == item.Name);
				var unselectedColor = Color.Default;
				label.Style = BodyStyle;
				if (TextColor != Color.Default)
					unselectedColor = TextColor;
				else if (BodyStyle != null) unselectedColor = StyleSetterFinder<Color>.Get("TextColor", BodyStyle);
				label.TextColor = item.Selected ? SelectedColor : unselectedColor;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}
	}
}