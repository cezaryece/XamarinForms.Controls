using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class FixedSizeButton : ContentView
	{
		public event EventHandler ClickedHandler;

		public FixedSizeButton()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;

			BoxElement.HeightRequest = HeightRequest;
			BoxElement.WidthRequest = WidthRequest;
			ImageElement.HeightRequest = HeightRequest;
			ImageElement.WidthRequest = WidthRequest;
			ImageElement.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(ImageElement.IsLoading))
				{
					ImageElement.HeightRequest = HeightRequest - BorderWidth - 2;
					ImageElement.WidthRequest = WidthRequest - BorderWidth - 2;
				}
			};
			BoxElement.BackgroundColor = BackgroundColor;
			LabelElement.TextColor = TextColor;
			LabelElement.FontSize = Utils.GetScalledFontSize(FontSize);
			LayoutGrid.BackgroundColor = BorderColor;
		}

		protected void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (!IsEnabled) return;
			if (Command != null && Command.CanExecute(CommandParameter))
				Command.Execute(CommandParameter);
			else
				ClickedHandler?.Invoke(this, EventArgs.Empty);
		}

		#region TextColorProperty
		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FixedSizeButton), Color.Default, propertyChanged: HandleTextColorChanged);

		private static void HandleTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			me.LabelElement.TextColor = me.IsEnabled ? (Color)newvalue : me.DisabledTextColor;
		}

		public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }
		#endregion

		#region TextProperty
		public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FixedSizeButton), string.Empty, propertyChanged: HandleTextChanged);

		private static void HandleTextChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			me.LabelElement.Text = (string)newvalue;
			me.LabelElement.IsVisible = !string.IsNullOrEmpty((string)newvalue);
		}

		public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
		#endregion

		#region FontSizeProperty
		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(FixedSizeButton), 14.0, propertyChanged: HandleFontSizeChanged);

		private static void HandleFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			var size = Utils.GetScalledFontSize((double)newvalue);
			me.LabelElement.FontSize = size;
			me.LabelGrid.Padding = size / 2;

			//if (string.IsNullOrEmpty(me.Text)) return;
			//if (Math.Abs(me.HeightRequest - (double) HeightRequestProperty.DefaultValue) < 0.1)
			//	me.HeightRequest = me.LabelElement.FontSize*3;
		}

		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }
		#endregion

		#region FontAssetProperty
		public static BindableProperty FontAssetProperty = BindableProperty.Create(nameof(FontAsset), typeof(string), typeof(FixedSizeButton), string.Empty, propertyChanged: HandleFontAssetChanged);

		private static void HandleFontAssetChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			me.LabelElement.FontAsset = (string)newvalue;
		}

		/// <summary>
		///     Font file name located in Asset directory
		/// </summary>
		public string FontAsset { get => (string)GetValue(FontAssetProperty); set => SetValue(FontAssetProperty, value); }
		#endregion

		#region FontNameProperty
		public static BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(FixedSizeButton), string.Empty, propertyChanged: HandleFontNameChanged);

		private static void HandleFontNameChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			me.LabelElement.FontName = (string)newvalue;
		}

		/// <summary>
		///     Used and requierd only by Windows/WindowsPhone apps
		/// </summary>
		public string FontName { get => (string)GetValue(FontNameProperty); set => SetValue(FontNameProperty, value); }
		#endregion

		#region CommandProperty
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(FixedSizeButton));
		public Command Command { get => (Command)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }
		#endregion

		#region CommandParameterProperty
		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FixedSizeButton));
		public object CommandParameter { get => GetValue(CommandParameterProperty); set => SetValue(CommandParameterProperty, value); }
		#endregion

		#region ImageProperty
		public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(FixedSizeButton), null, propertyChanged: HandleImageChanged);

		private static void HandleImageChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			var imageElement = me.ImageElement;
			imageElement.Source = (ImageSource)newvalue;
		}

		public ImageSource Image { get => (ImageSource)GetValue(ImageProperty); set => SetValue(ImageProperty, value); }
		#endregion

		#region BorderWidthProperty
		public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(FixedSizeButton), 0.0, propertyChanged: HandleBorderWidthChanged);

		private static void HandleBorderWidthChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			me.LayoutGrid.Padding = (double)newvalue;
		}

		public double BorderWidth { get => (double)GetValue(BorderWidthProperty); set => SetValue(BorderWidthProperty, value); }
		#endregion

		#region BorderColorProperty
		public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FixedSizeButton), Color.Transparent, propertyChanged: HandleBorderColorChanged);

		private static void HandleBorderColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			me.LayoutGrid.BackgroundColor = me.IsEnabled ? (Color)newvalue : me.DisabledBorderColor;
		}

		public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }
		#endregion

		#region DisabledColors
//colors for disabled state
		public static readonly BindableProperty DisabledTextColorProperty = BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(FixedSizeButton), Color.Default, propertyChanged: HandleDisabledTextColorChanged);

		private static void HandleDisabledTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			me.LabelElement.TextColor = me.IsEnabled ? me.TextColor : (Color)newvalue;
		}

		public Color DisabledTextColor { get => (Color)GetValue(DisabledTextColorProperty); set => SetValue(DisabledTextColorProperty, value); }

		public static readonly BindableProperty DisabledBorderColorProperty =
			BindableProperty.Create(nameof(DisabledBorderColor), typeof(Color), typeof(FixedSizeButton), Color.Transparent, propertyChanged: HandleDisabledBorderColorChanged);

		private static void HandleDisabledBorderColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			me.LayoutGrid.BackgroundColor = me.IsEnabled ? me.BorderColor : (Color)newvalue;
		}

		public Color DisabledBorderColor { get => (Color)GetValue(DisabledBorderColorProperty); set => SetValue(DisabledBorderColorProperty, value); }

		public static readonly BindableProperty DisabledBackgroundProperty =
			BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(FixedSizeButton), Color.Default, propertyChanged: HandleDisabledBackgroundColorChanged);

		private static void HandleDisabledBackgroundColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as FixedSizeButton;
			me.BoxElement.BackgroundColor = me.IsEnabled ? me.BackgroundColor : (Color)newvalue;
		}

		public Color DisabledBackgroundColor { get => (Color)GetValue(DisabledBackgroundProperty); set => SetValue(DisabledBackgroundProperty, value); }
		#endregion

		#region Overrides of BindableObject
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == null)
				return;
			if (propertyName == nameof(BackgroundColor))
			{
				BoxElement.BackgroundColor = IsEnabled ? BackgroundColor : DisabledBackgroundColor;
			}
			else if (propertyName == nameof(IsEnabled))
			{
				try
				{
					ImageElement.Opacity = IsEnabled ? 1 : 0.5;
					BoxElement.BackgroundColor = IsEnabled ? BackgroundColor : DisabledBackgroundColor;
					LabelElement.IsEnabled = IsEnabled;
					LabelElement.TextColor = IsEnabled ? TextColor : DisabledTextColor;
					LayoutGrid.BackgroundColor = IsEnabled ? BorderColor : DisabledBorderColor;
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
				}
			}
			else if (propertyName == nameof(HeightRequest) || propertyName == nameof(WidthRequest))
			{
				BoxElement.MinimumHeightRequest = BoxElement.HeightRequest = HeightRequest;
				BoxElement.MinimumWidthRequest = BoxElement.WidthRequest = WidthRequest;
				ImageElement.MinimumHeightRequest = ImageElement.HeightRequest = HeightRequest - BorderWidth - 2;
				ImageElement.MinimumWidthRequest = ImageElement.WidthRequest = WidthRequest - BorderWidth - 2;
			}
		}
		#endregion
	}
}