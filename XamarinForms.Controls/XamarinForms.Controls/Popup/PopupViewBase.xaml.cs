using System;
using Xamarin.Forms;

namespace XamarinForms.Controls.Popup
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class PopupViewBase : ContentView
	{
		public event EventHandler<bool> CloseRequest;

		public static readonly BindableProperty OpacityColorProperty = BindableProperty.Create(nameof(OpacityColor), typeof(Color), typeof(PopupViewBase), Color.Default, propertyChanged: HandleOpacityColorChanged);
		private static void HandleOpacityColorChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupViewBase).OpacityGrid.BackgroundColor = (Color)newvalue; }
		public Color OpacityColor { get => (Color)GetValue(OpacityColorProperty); set => SetValue(OpacityColorProperty, value); }

		public static readonly BindableProperty ContentBackgroudColorProperty = BindableProperty.Create(nameof(ContentBackgroudColor), typeof(Color), typeof(PopupViewBase), Color.Default, propertyChanged: HandleBackgroundColorChanged);

		private static void HandleBackgroundColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupViewBase;
			me.BackgroundBox.BackgroundColor = (Color)newvalue;
			if (me.OpacityColor == Color.Default)
				me.OpacityGrid.BackgroundColor = ((Color)newvalue).MultiplyAlpha(0.75);
		}

		public Color ContentBackgroudColor { get => (Color)GetValue(ContentBackgroudColorProperty); set => SetValue(ContentBackgroudColorProperty, value); }

		public static readonly BindableProperty OpacityMarginProperty = BindableProperty.Create(nameof(OpacityMargin), typeof(double), typeof(PopupViewBase), 10.0, propertyChanged: HandleFontSizeChanged);

		private static void HandleFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var opacityGrid = (bindable as PopupViewBase).OpacityGrid;
			opacityGrid.RowDefinitions[0].Height = opacityGrid.RowDefinitions[2].Height = (double)newvalue;
			opacityGrid.ColumnDefinitions[0].Width = opacityGrid.ColumnDefinitions[2].Width = (double)newvalue;
		}

		public double OpacityMargin { get => (double)GetValue(OpacityMarginProperty); set => SetValue(OpacityMarginProperty, value); }

		public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(PopupViewBase), 1.0, propertyChanged: HandleBorderWidthChanged);
		private static void HandleBorderWidthChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupViewBase).MainGrid.Padding = (double)newvalue; }
		public double BorderWidth { get => (double)GetValue(BorderWidthProperty); set => SetValue(BorderWidthProperty, value); }

		public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(PopupViewBase), Color.Accent, propertyChanged: HandleBorderColorChanged);
		private static void HandleBorderColorChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupViewBase).MainGrid.BackgroundColor = (Color)newvalue; }
		public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }

		public static readonly BindableProperty HeaderViewProperty = BindableProperty.Create(nameof(HeaderView), typeof(ContentView), typeof(PopupViewBase), null, propertyChanged: HandleHeaderChanged);

		private static void HandleHeaderChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			(bindable as PopupViewBase).Header.Content = ((ContentView)newvalue).Content;
			(bindable as PopupViewBase).ForceLayout();
		}

		public ContentView HeaderView { get => (ContentView)GetValue(HeaderViewProperty); set => SetValue(HeaderViewProperty, value); }

		public static readonly BindableProperty BodyViewProperty = BindableProperty.Create(nameof(BodyView), typeof(ContentView), typeof(PopupViewBase), null, propertyChanged: HandleBodyChanged);
		private static void HandleBodyChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupViewBase).Body.Content = ((ContentView)newvalue).Content; }
		public ContentView BodyView { get => (ContentView)GetValue(BodyViewProperty); set => SetValue(BodyViewProperty, value); }

		public static readonly BindableProperty FooterViewProperty = BindableProperty.Create(nameof(FooterView), typeof(ContentView), typeof(PopupViewBase), null, propertyChanged: HandleFooterChanged);
		private static void HandleFooterChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupViewBase).Footer.Content = ((ContentView)newvalue).Content; }
		public ContentView FooterView { get => (ContentView)GetValue(FooterViewProperty); set => SetValue(FooterViewProperty, value); }

		public PopupViewBase()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			HorizontalOptions = LayoutOptions.FillAndExpand;
			VerticalOptions = LayoutOptions.FillAndExpand;

			OpacityGrid.BackgroundColor = OpacityColor;
			BackgroundBox.BackgroundColor = ContentBackgroudColor;
			OpacityGrid.RowDefinitions[0].Height = OpacityGrid.RowDefinitions[2].Height = OpacityMargin;
			OpacityGrid.ColumnDefinitions[0].Width = OpacityGrid.ColumnDefinitions[2].Width = OpacityMargin;
			MainGrid.Padding = BorderWidth;
			MainGrid.BackgroundColor = BorderColor;
		}

		public void OnCloseRequest(object sender, bool hasResults) { CloseRequest?.Invoke(this, hasResults); }
	}
}