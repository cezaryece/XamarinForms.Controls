using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	/// <summary>
	///     Interaction logic for NumericTextBox.xaml
	/// </summary>

	// ReSharper disable once RedundantExtendsListEntry
	public partial class NumTextBox : ContentView
	{
		public NumTextBox()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			HandleFontSizeChanged(this, null, FontSize);
			HandleValueChanged(this, null, Value);
		}

		public decimal Value
		{
			get => (decimal)GetValue(ValueProperty);
			set
			{
				var val = Rounded(value, DecimalPlace);
				SetValue(ValueProperty, val);
				OnPropertyChanged();
			}
		}

		public static BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(decimal), typeof(NumTextBox), 0m, BindingMode.TwoWay, propertyChanged: HandleValueChanged);

		private static void HandleValueChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (NumTextBox)bindable;
			me.InputEntry.Text = ((decimal)newvalue).ToString("F" + Math.Max(0, me.DecimalPlace));
			me.Validate();
		}

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(NumTextBox), 14.0, propertyChanging: HandleFontSizeChanged);

		private static void HandleFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (NumTextBox)bindable;
			me.InputEntry.FontSize = Utils.GetScalledFontSize((double)newvalue);
		}

		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

		private bool _enabled;

		public bool Enabled
		{
			get => _enabled;
			set
			{
				_enabled = value;
				OnPropertyChanged();
				InputEntry.IsEnabled = value;

				// ReSharper disable once ExplicitCallerInfoArgument
				if (_enabled) OnPropertyChanged(nameof(HasValidationError));
			}
		}

		private bool _hasValidationError;

		public bool HasValidationError
		{
			get => Enabled && _hasValidationError;
			private set
			{
				if (_hasValidationError == value) return;
				_hasValidationError = value;
				InputEntry.TextColor = _hasValidationError ? Color.Red : Color.Black;
				OnPropertyChanged();
			}
		}

		private decimal _max;

		public decimal Maximum
		{
			get => _max;
			set
			{
				_max = value;
				if (Value > _max) Value = _max;
			}
		}

		private decimal _min;

		public decimal Minimum
		{
			get => _min;
			set
			{
				_min = value;
				if (Value < _min) Value = _min;
			}
		}

		private int _decimalPlace;

		public int DecimalPlace
		{
			get => _decimalPlace;
			set
			{
				_decimalPlace = value;
				Validate();
			}
		}

		private void Validate(bool updateValue = true)
		{
			if (string.IsNullOrWhiteSpace(InputEntry.Text))
			{
				HasValidationError = true;
				return;
			}

			decimal inputValue = 0;
			if (TryParse(InputEntry.Text, ref inputValue))
			{
				var val = Rounded(inputValue, DecimalPlace);
				if (val <= Maximum && val >= Minimum)
				{
					HasValidationError = false;
					if (updateValue) SetValue(ValueProperty, val);
					return;
				}
			}

			HasValidationError = true;
		}

		private bool TryParse(string str, ref decimal val)
		{
			if (str == null) throw new ArgumentNullException(nameof(str));
			try
			{
				var sep = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
				str = str.Replace(".", sep).Replace(",", sep);
				val = decimal.Parse(str);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void InputEntry_OnTextChanged(object sender, TextChangedEventArgs e) { Validate(false); }
		private void InputEntry_OnUnfocused(object sender, FocusEventArgs e) { Validate(); }

		private void InputEntry_OnCompleted(object sender, EventArgs e)
		{
			Validate();
			OnPropertyChanged("EditedText");
		}

		public string this[string propertyName]
		{
			get
			{
				var result = string.Empty;
				Validate();
				if (HasValidationError)
					result = "Wartość spoza zakresu";
				return result;
			}
		}

		public string Error => this[string.Empty];

		private decimal Rounded(decimal inputValue, int places) { return places >= 0 ? Math.Round(inputValue, places) : inputValue / (int)Math.Pow(10, places) * (int)Math.Pow(10, places); }
	}
}