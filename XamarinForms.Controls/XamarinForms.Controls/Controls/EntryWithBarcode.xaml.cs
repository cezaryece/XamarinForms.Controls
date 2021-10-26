using System;
using System.Diagnostics;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace XamarinForms.Controls.Controls
{
	public delegate void CodeCompletedHandler(object sender, string text);

	// ReSharper disable once RedundantExtendsListEntry
	/// <summary>
	///     Control with Entry for some code and Button for start Barcode scanning process, below optionally label with scanned barcode type
	///     You must have NuGet with ZXing.Net.Mobile.Forms installed and initialized (https://c.telemetria.eu:8443/pages/viewpage.action?pageId=26190598)
	/// </summary>
	public partial class EntryWithBarcode : ContentView
	{
		public event CodeCompletedHandler OnCodeTextCompleted;

		public EntryWithBarcode()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			TypeLabel.IsVisible = ShowType;
			HandleFontSizeChanged(this, null, FontSize);
			HandleScanTextChanged(this, null, ScanText);
			HandleEntryLayoutChanged(this, null, EntryButtonOrientation);
			HandleShowTypeChanged(this, null, ShowType);
			CodeEntry.Completed += (sender, args) => OnCodeTextCompleted?.Invoke(this, Text);
		}

		#region FontAssetProperty
		public static BindableProperty FontAssetProperty = BindableProperty.Create(nameof(FontAsset), typeof(string), typeof(EntryWithBarcode), string.Empty, propertyChanged: HandleFontAssetChanged);

		private static void HandleFontAssetChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as EntryWithBarcode;
			me.CodeEntry.FontAsset = me.TypeLabel.FontAsset = me.ScanButton.FontAsset = (string)newvalue;
		}

		/// <summary>
		///     Font file name located in Asset directory
		/// </summary>
		public string FontAsset { get => (string)GetValue(FontAssetProperty); set => SetValue(FontAssetProperty, value); }
		#endregion

		#region FontNameProperty
		public static BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(EntryWithBarcode), string.Empty, propertyChanged: HandleFontNameChanged);

		private static void HandleFontNameChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as EntryWithBarcode;
			me.CodeEntry.FontName = me.TypeLabel.FontName = me.ScanButton.FontName = (string)newvalue;
		}

		/// <summary>
		///     Used and requierd only by Windows/WindowsPhone apps
		/// </summary>
		public string FontName { get => (string)GetValue(FontNameProperty); set => SetValue(FontNameProperty, value); }
		#endregion

		#region FontSizeProperty
		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(EntryWithBarcode), 14.0, propertyChanged: HandleFontSizeChanged);

		private static void HandleFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as EntryWithBarcode;
			var newSize = (double)newvalue;
			me.CodeEntry.FontSize = newSize;
			me.TypeLabel.FontSize = newSize * 0.75;
			me.ScanButton.FontSize = newSize;
		}

		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }
		#endregion

		#region TextColorProperty
		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(EntryWithBarcode), Color.Default, propertyChanged: HandleTextColorChanged);

		private static void HandleTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as EntryWithBarcode;
			me.CodeEntry.TextColor = me.CodeEntry.BorderColor = me.TypeLabel.TextColor = me.ScanButton.BackgroundColor = (Color)newvalue;
		}

		public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }
		#endregion

		#region TextProperty
		public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryWithBarcode), string.Empty, BindingMode.TwoWay, propertyChanged: HandleTextChanged);

		private static void HandleTextChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as EntryWithBarcode;
			me.CodeEntry.Text = (string)newvalue;
		}

		public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
		#endregion

		#region ScanTextProperty
		public static readonly BindableProperty ScanTextProperty = BindableProperty.Create(nameof(ScanText), typeof(string), typeof(EntryWithBarcode), "Skanuj", propertyChanged: HandleScanTextChanged);

		private static void HandleScanTextChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as EntryWithBarcode;
			me.ScanButton.Text = (string)newvalue;
		}

		public string ScanText { get => (string)GetValue(ScanTextProperty); set => SetValue(ScanTextProperty, value); }
		#endregion

		#region TypeProperty
		public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(string), typeof(EntryWithBarcode), string.Empty, BindingMode.OneWayToSource, propertyChanged: HandleTypeChanged);

		private static void HandleTypeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as EntryWithBarcode;
			me.TypeLabel.Text = (string)newvalue;
		}

		public string Type { get => (string)GetValue(TypeProperty); set => SetValue(TypeProperty, value); }
		#endregion

		#region ShowTypeProperty
		public static BindableProperty ShowTypeProperty = BindableProperty.Create(nameof(ShowType), typeof(bool), typeof(EntryWithBarcode), false, propertyChanged: HandleShowTypeChanged);

		private static void HandleShowTypeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (EntryWithBarcode)bindable;
			me.TypeLabel.IsVisible = (bool)newvalue;
		}

		public bool ShowType { get => (bool)GetValue(ShowTypeProperty); set => SetValue(ShowTypeProperty, value); }
		#endregion

		#region EntryButtonOrientation
		public static BindableProperty EntryButtonOrientationProperty =
			BindableProperty.Create(nameof(EntryButtonOrientation), typeof(StackOrientation), typeof(EntryWithBarcode), StackOrientation.Vertical, propertyChanged: HandleEntryLayoutChanged);

		private static void HandleEntryLayoutChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (EntryWithBarcode)bindable;
			var orientation = (StackOrientation)newvalue;
			me.EntryButtonLayout.Orientation = orientation;

			//me.CodeEntry.HorizontalOptions = (orientation == StackOrientation.Horizontal) ? LayoutOptions.StartAndExpand : LayoutOptions.FillAndExpand;
			me.ScanButton.HorizontalOptions = orientation == StackOrientation.Horizontal ? LayoutOptions.End : LayoutOptions.FillAndExpand;
		}

		public StackOrientation EntryButtonOrientation { get => (StackOrientation)GetValue(EntryButtonOrientationProperty); set => SetValue(EntryButtonOrientationProperty, value); }
		#endregion

		#region EntryBackgroundProperty
		public static BindableProperty EntryBackgroundProperty = BindableProperty.Create(nameof(EntryBackground), typeof(Color), typeof(EntryWithBarcode), Color.Default, propertyChanged: HandleBackgroundChanged);

		private static void HandleBackgroundChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as EntryWithBarcode;
			me.CodeEntry.BackgroundColor = me.ScanButton.BorderColor = (Color)newvalue;
		}

		public Color EntryBackground { get => (Color)GetValue(EntryBackgroundProperty); set => SetValue(EntryBackgroundProperty, value); }
		#endregion

		#region Overrides of Element
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == nameof(IsEnabled))
				CodeEntry.IsEnabled = ScanButton.IsEnabled = IsEnabled;
			else if (propertyName == nameof(BackgroundColor)) ScanButton.TextColor = ScanButton.BorderColor = BackgroundColor;
			if (propertyName == nameof(IsFocused) && !IsFocused) Text = CodeEntry.Text;
		}
		#endregion

		private async void Button_OnClicked(object sender, EventArgs e)
		{
			try
			{
				var scanPage = new ZXingScannerPage();

				scanPage.OnScanResult += result =>
				{
					// Stop scanning
					scanPage.IsScanning = false;

					// Pop the page and show the result
					Device.BeginInvokeOnMainThread(() =>
					{
						Navigation.PopModalAsync();
						Text = result != null ? result.Text : "Brak kodu";
						OnCodeTextCompleted?.Invoke(this, Text);
						SetValue(TypeProperty, result?.BarcodeFormat.ToString());
					});
				};

				// Navigate to our scanner page
				await Navigation.PushModalAsync(scanPage);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				throw;
			}
		}
	}
}