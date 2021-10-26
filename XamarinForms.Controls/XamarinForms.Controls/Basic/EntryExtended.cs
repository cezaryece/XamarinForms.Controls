using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	public class EntryExtended : Entry
	{
		public static BindableProperty FontAssetProperty = BindableProperty.Create(nameof(FontAsset), typeof(string), typeof(EntryExtended), string.Empty);

		/// <summary>
		///     Font file name located in Asset directory
		/// </summary>
		public string FontAsset { get => (string)GetValue(FontAssetProperty); set => SetValue(FontAssetProperty, value); }

		/// <summary>
		///     Used and requierd only by Windows/WindowsPhone apps
		/// </summary>
		public string FontName { get => (string)GetValue(FontNameProperty); set => SetValue(FontNameProperty, value); }

		public static BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(EntryExtended), string.Empty);

		public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(EntryExtended), 0.0);
		public double BorderWidth { get => (double)GetValue(BorderWidthProperty); set => SetValue(BorderWidthProperty, value); }

		public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(EntryExtended), Color.Transparent);
		public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }

		//public static readonly BindableProperty EntryTextColorProperty = BindableProperty.Create(nameof(EntryTextColor), typeof(Color), typeof(EntryExtended), Color.White);
		//public Color TextColor { get { return (Color)GetValue(EntryTextColorProperty); } set { SetValue(EntryTextColorProperty, value); } }
	}
}