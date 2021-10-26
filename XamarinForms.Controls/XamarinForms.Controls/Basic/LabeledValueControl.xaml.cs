using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class LabeledValueControl : ContentView
	{
		public LabeledValueControl()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			InputEntry.IsEnabled = IsEnabled;
		}

		public static readonly BindableProperty TopLabelTextProperty = BindableProperty.Create(nameof(TopLabelText), typeof(string), typeof(LabeledValueControl), null, propertyChanging: HandleTopLabelTextChanged);
		private static void HandleTopLabelTextChanged(BindableObject bindable, object oldvalue, object newvalue) { ((LabeledValueControl)bindable).TopLabel.Text = (string)newvalue; }
		public string TopLabelText { get => (string)GetValue(TopLabelTextProperty); set => SetValue(TopLabelTextProperty, value); }

		public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(LabeledValueControl), null, propertyChanging: HandleValueChanged);
		private static void HandleValueChanged(BindableObject bindable, object oldvalue, object newvalue) { ((LabeledValueControl)bindable).InputEntry.Text = (string)newvalue; }
		public string Value { get => (string)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(LabeledValueControl), 14.0, propertyChanging: HandleFontSizeChanged);

		private static void HandleFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (LabeledValueControl)bindable;
			me.TopLabel.FontSize = me.InputEntry.FontSize = Utils.GetScalledFontSize((double)newvalue);
		}

		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

		public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(LabeledValueControl), Keyboard.Default, propertyChanging: HandleKeyboardChanged);
		private static void HandleKeyboardChanged(BindableObject bindable, object oldvalue, object newvalue) { ((LabeledValueControl)bindable).InputEntry.Keyboard = (Keyboard)newvalue; }
		public Keyboard Keyboard { get => (Keyboard)GetValue(FontSizeProperty); set => SetValue(KeyboardProperty, value); }

		#region Overrides of BindableObject
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName != null && propertyName == "IsEnabled") InputEntry.IsEnabled = IsEnabled;
		}
		#endregion
	}
}