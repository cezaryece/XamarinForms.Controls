using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using XamarinForms.Controls.Basic;

namespace XamarinForms.Controls.Popup
{
	public class PopupMenuView : PopupViewBase
	{
		private List<LabelExtended> _labels;

		public static BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<PopupMenuItem>), typeof(PopupMenuView), null, propertyChanged: HandleItemsChanged);

		private static void HandleItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupMenuView;
			Debug.Assert(me != null, "me != null");
			me._labels = new List<LabelExtended>();
			var stackLayout = new StackLayout { Spacing = 20 };

			foreach (var menuItem in me.Items)
			{
				var newLabel = new LabelExtended
				{
					Text = menuItem.Name, GestureRecognizers =
					{
						new TapGestureRecognizer
						{
							NumberOfTapsRequired = 1, Command = new Command(() => me.RunCommand(menuItem.Command))
						}
					}
					, Style = me.BodyStyle
				};
				me._labels.Add(newLabel);
				stackLayout.Children.Add(newLabel);
			}

			me.BodyView = new ContentView { Content = stackLayout };
		}

		public List<PopupMenuItem> Items { get => (List<PopupMenuItem>)GetValue(ItemsProperty); set => SetValue(ItemsProperty, value); }

		public static readonly BindableProperty BodyStyleProperty = BindableProperty.Create(nameof(BodyStyle), typeof(Style), typeof(PopupMenuView), null, propertyChanged: HandleBodyStyleChanged);

		private static void HandleBodyStyleChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupMenuView;
			foreach (var label in me._labels) label.Style = (Style)newvalue;
		}

		public Style BodyStyle { get => (Style)GetValue(BodyStyleProperty); set => SetValue(BodyStyleProperty, value); }

		public void AddMenuItem(PopupMenuItem item, int pos = -1)
		{
			if (pos == -1) pos = Items.Count;
			Items.Insert(pos, item);
			Items = Items.ToList();
		}

		public void RemoveMenuItem(string name)
		{
			var pos = Items.FindIndex(item => item.Name == name);
			if (pos == -1) return;
			Items.RemoveAt(pos);
			Items = Items.ToList();
		}

		private void RunCommand(Action action)
		{
			OnCloseRequest(this, false);
			action.Invoke();
		}
	}
}