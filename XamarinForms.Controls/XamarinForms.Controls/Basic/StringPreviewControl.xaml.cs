using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class StringPreviewControl : ContentView
	{
		public static BindableProperty OrientationProperty =
			BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(StringPreviewControl), StackOrientation.Vertical, BindingMode.TwoWay, propertyChanging: HandleOrientationChange);

		public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(StringPreviewControl), string.Empty, BindingMode.TwoWay, propertyChanging: HandleLabelTextChange);

		public static BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(StringPreviewControl), string.Empty, BindingMode.TwoWay
			, propertyChanging: (bindable, value, newValue) => ((StringPreviewControl)bindable).StringValue.Text = (string)newValue);

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(StringPreviewControl), 14.0, BindingMode.TwoWay, propertyChanging: (bindable, value, newValue) =>
		{
			var me = (StringPreviewControl)bindable;
			me.StringLabel.FontSize = me.StringValue.FontSize = Utils.GetScalledFontSize((double)newValue);
		});

		public static readonly BindableProperty BorderSizeProperty = BindableProperty.Create(nameof(BorderSize), typeof(double), typeof(StringPreviewControl), 0.0, BindingMode.TwoWay, propertyChanging: HandleBorderSizeChange);

		private static void HandleBorderSizeChange(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (StringPreviewControl)bindable;
			var val = (double)newvalue;
			me.BoxBottomElement.IsVisible = val > 0;
			me.BoxBottomElement.HeightRequest = val;
		}

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(StringPreviewControl), Color.White, BindingMode.TwoWay, propertyChanging: (bindable, value, newValue) =>
		{
			var me = (StringPreviewControl)bindable;
			var color = (Color)newValue;
			me.StringLabel.TextColor = me.StringValue.TextColor = color;
			me.BoxBottomElement.BackgroundColor = color;
		});

		public StringPreviewControl() { Init(); }

		public StringPreviewControl(string label, string value, StackOrientation orientation = StackOrientation.Horizontal, double fontsize = 0)
		{
			Init();
			LabelText = label;
			Value = value;
			Orientation = orientation;
			if (fontsize > 0)
				FontSize = Utils.GetScalledFontSize(fontsize);
		}

		public StackOrientation Orientation { get => (StackOrientation)GetValue(OrientationProperty); set => SetValue(OrientationProperty, value); }
		public string LabelText { get => (string)GetValue(LabelTextProperty); set => SetValue(LabelTextProperty, value); }
		public string Value { get => (string)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }
		public double BorderSize { get => (double)GetValue(BorderSizeProperty); set => SetValue(BorderSizeProperty, value); }

		public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

		private void Init()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			StringLabel.TextColor = StringValue.TextColor = TextColor;
			BoxBottomElement.BackgroundColor = TextColor;
			BorderSize = 1.0;
		}

		private static void HandleOrientationChange(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (StringPreviewControl)bindable;
			me.MainStack.Orientation = (StackOrientation)newvalue;
			me.StringValue.HorizontalTextAlignment = (StackOrientation)newvalue == StackOrientation.Horizontal ? TextAlignment.End : TextAlignment.Start;
		}

		private static void HandleLabelTextChange(BindableObject bindable, object oldvalue, object newvalue) { ((StringPreviewControl)bindable).StringLabel.Text = (string)newvalue; }
	}
}