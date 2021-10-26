using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Controls.Controls
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class TabedContentView : ContentView
	{
		protected List<Button> TabedItems { get; }

		public static BindableProperty TabedViewsProperty = BindableProperty.Create(nameof(TabedViews), typeof(Dictionary<string, ContentView>), typeof(TabedContentView), null, BindingMode.TwoWay, propertyChanging: HandleTabedViewsChange);

		private static void HandleTabedViewsChange(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (TabedContentView)bindable;
			me.TabedItems.Clear();
			me.CaruselLayout.Children.Clear();
			var viewsDict = (Dictionary<string, ContentView>)newvalue;
			if (viewsDict == null || !viewsDict.Any()) return;
			var heightRequest = me.FontSize * (Device.OS == TargetPlatform.Windows ? 4 : 3);
			foreach (var pairOfStringView in viewsDict)
			{
				var button = new Button
				{
					Text = pairOfStringView.Key, BackgroundColor = me.ButtonBackgroundColor, BorderWidth = me.ButtonBorderWidth, BorderRadius = me.BorderRadius, BorderColor = me.ButtonBorderColor, TextColor = me.TextColor
					, FontSize = me.FontSize, HeightRequest = heightRequest
				};
				button.Clicked += (sender, args) =>
				{
					me.CaruselLayout.Children.Clear();
					me.CaruselLayout.Children.Add(pairOfStringView.Value);
					me.CurrentIndex = (int)button.CommandParameter;
				};
				me.TabedItems.Add(button);
				button.CommandParameter = me.TabedItems.Count - 1;
			}

			me.UpdateTabsGrid();
			me.CurrentIndex = 0;
			me.CaruselLayout.Children.Add(viewsDict[me.TabedItems[me.CurrentIndex].Text]);
		}

		public Dictionary<string, ContentView> TabedViews { get => (Dictionary<string, ContentView>)GetValue(TabedViewsProperty); set => SetValue(TabedViewsProperty, value); }

		public static BindableProperty CurrentIndexProperty = BindableProperty.Create(nameof(CurrentIndex), typeof(int), typeof(TabedContentView), -1, BindingMode.TwoWay, propertyChanging: HandleCurrentIndexChanged);

		private static void HandleCurrentIndexChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as TabedContentView;
			if (me.TabedItems == null || !me.TabedItems.Any()) return;
			if ((int)oldvalue >= 0)
			{
				me.TabedItems[(int)oldvalue].TextColor = me.TextColor;
				me.TabedItems[(int)oldvalue].BackgroundColor = me.ButtonBackgroundColor;
			}

			if ((int)newvalue >= 0)
			{
				me.TabedItems[(int)newvalue].TextColor = me.SelectedColor;
				me.TabedItems[(int)newvalue].BackgroundColor = me.ButtonSelectedBackgroundColor;
			}

			var index = Math.Max(0, (int)newvalue);
			me.Scroll.ScrollToAsync(me.TabedItems[index], ScrollToPosition.Center, true);
		}

		public int CurrentIndex { get => (int)GetValue(CurrentIndexProperty); set => SetValue(CurrentIndexProperty, value); }

		public static readonly BindableProperty ButtonBorderWidthProperty = BindableProperty.Create(nameof(ButtonBorderWidth), typeof(double), typeof(TabedContentView), 0.0, propertyChanged: HandleButtonBorderWidthChanged);

		private static void HandleButtonBorderWidthChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as TabedContentView;
			foreach (var button in me.TabedItems) button.BorderWidth = (double)newvalue;
		}

		public double ButtonBorderWidth { get => (double)GetValue(ButtonBorderWidthProperty); set => SetValue(ButtonBorderWidthProperty, value); }

		public static readonly BindableProperty BorderRadiusProperty =
			BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(TabedContentView), Device.OS == TargetPlatform.Android ? 1 : 0, propertyChanged: HandleBorderRadiusChanged);

		private static void HandleBorderRadiusChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as TabedContentView;
			var radius = (int)newvalue;
			if (Device.OS == TargetPlatform.Android && radius == 0) radius = 1;
			foreach (var button in me.TabedItems) button.BorderRadius = radius;
		}

		public int BorderRadius { get => (int)GetValue(BorderRadiusProperty); set => SetValue(BorderRadiusProperty, value); }

		public static readonly BindableProperty ButtonBorderColorProperty = BindableProperty.Create(nameof(ButtonBorderColor), typeof(Color), typeof(TabedContentView), Color.Transparent, propertyChanged: HandleButtonBorderColorChanged);

		private static void HandleButtonBorderColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as TabedContentView;
			foreach (var button in me.TabedItems) button.BorderColor = (Color)newvalue;
		}

		public Color ButtonBorderColor { get => (Color)GetValue(ButtonBorderColorProperty); set => SetValue(ButtonBorderColorProperty, value); }

		public static readonly BindableProperty ButtonBackgroundColorProperty =
			BindableProperty.Create(nameof(ButtonBackgroundColor), typeof(Color), typeof(TabedContentView), Color.Default, propertyChanged: HandleButtonBackgroundColorChanged);

		private static void HandleButtonBackgroundColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as TabedContentView;
			foreach (var button in me.TabedItems) button.BackgroundColor = (Color)newvalue;
		}

		public Color ButtonBackgroundColor { get => (Color)GetValue(ButtonBackgroundColorProperty); set => SetValue(ButtonBackgroundColorProperty, value); }

		public static readonly BindableProperty ButtonSelectedBackgroundColorProperty =
			BindableProperty.Create(nameof(ButtonSelectedBackgroundColor), typeof(Color), typeof(TabedContentView), Color.Default, propertyChanged: HandleSelectedBackgroundColorChanged);

		private static void HandleSelectedBackgroundColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as TabedContentView;
			if (me.CurrentIndex < 0) return;
			me.TabedItems[me.CurrentIndex].BackgroundColor = (Color)newvalue;
		}

		public Color ButtonSelectedBackgroundColor { get => (Color)GetValue(ButtonSelectedBackgroundColorProperty); set => SetValue(ButtonSelectedBackgroundColorProperty, value); }

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TabedContentView), Color.Default, propertyChanged: HandleTextColorChanged);

		private static void HandleTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as TabedContentView;
			foreach (var button in me.TabedItems)
			{
				var index = me.TabedItems.FindIndex(b => b == button);
				button.TextColor = index != me.CurrentIndex ? (Color)newvalue : me.SelectedColor;
			}
		}

		public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

		public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(TabedContentView), Color.Default, propertyChanged: HandleSelectedColorChanged);

		private static void HandleSelectedColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as TabedContentView;
			if (me.CurrentIndex < 0) return;
			me.TabedItems[me.CurrentIndex].TextColor = (Color)newvalue;
		}

		public Color SelectedColor { get => (Color)GetValue(SelectedColorProperty); set => SetValue(SelectedColorProperty, value); }

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(TabedContentView), 14.0, propertyChanged: HandleFontSizeChanged);

		private static void HandleFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as TabedContentView;

			var fontSize = Utils.GetScalledFontSize((double)newvalue);
			var heightRequest = fontSize * (Device.OS == TargetPlatform.Windows ? 3 : 2);

			foreach (var button in me.TabedItems)
			{
				button.FontSize = fontSize;
				button.HeightRequest = heightRequest;
			}
		}

		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

		public TabedContentView()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			TabedItems = new List<Button>();
			UpdateTabsGrid();
		}

		private void UpdateTabsGrid()
		{
			TabsGrid.ColumnDefinitions.Clear();
			foreach (var button in TabedItems)
			{
				var newCol = new ColumnDefinition
				{
					Width = GridLength.Auto
				};
				TabsGrid.ColumnDefinitions.Add(newCol);
				button.SetValue(Grid.ColumnProperty, TabsGrid.ColumnDefinitions.Count - 1);
				TabsGrid.Children.Add(button);
			}

			InvalidateLayout();
		}

		private void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
		{
			//niestety recognizer nie działa, ale zostawiam kod, bo byc może cos się poprawi
			Debug.WriteLine($"OnPanUpdated {e.TotalX} {sender}");
			if (e.TotalX > 100) CurrentIndex--;
			else if (e.TotalX < -100) CurrentIndex++;
			else return;
			var currentSender = sender as ContentView;
			CaruselLayout.Children.Clear();
			if (CurrentIndex < 0) CurrentIndex = TabedItems.Count - 1;
			else if (CurrentIndex == TabedItems.Count) CurrentIndex = 0;

			CaruselLayout.Children.Add(TabedViews[TabedItems[CurrentIndex].Text]);
		}
	}
}