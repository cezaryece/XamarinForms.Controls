using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	/// <summary>
	///     Interaction logic for DecimaUpDown.xaml
	/// </summary>

	// ReSharper disable once RedundantExtendsListEntry
	public partial class DecimalUpDown : ContentView
	{
		private string _unitsString;

		public DecimalUpDown()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			HandleControlHeightChanged(this, 0.0, ControlHeight);
			HandleFontSizeChanged(this, null, FontSize);

			//HandleTextColorChanged(this, null, TextColor);
			HandleValueChanged(this, null, Value);
			HandleUnitsChanged(this, null, Units);
			HandleTopLabelChanged(this, null, TopLabelText);
			Up.BorderRadius = Down.BorderRadius = 90;
		}

		public void SetDataDouble(string label, double value, double minimum, double maximum, string units = "", int precision = 0, double increment = 1)
		{
			TopLabelText = label;
			DecimalPlace = precision;
			Maximum = maximum;
			Minimum = minimum;
			Increment = increment;
			Units = units;
			Value = value;
		}

		public void SetDataDecimal(string label, decimal value, decimal minimum, decimal maximum, string units = "", int precision = 0, decimal increment = 1)
		{
			TopLabelText = label;
			DecimalPlace = precision;
			Maximum = (double)maximum;
			Minimum = (double)minimum;
			Increment = (double)increment;
			Units = units;
			Value = (double)value;
		}

		public static readonly BindableProperty ControlHeightProperty = BindableProperty.Create(nameof(ControlHeight), typeof(double), typeof(DecimalUpDown), 0.0, BindingMode.TwoWay, propertyChanged: HandleControlHeightChanged);

		private static void HandleControlHeightChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (DecimalUpDown)bindable;
			var value = (double)newvalue;
			if (Math.Abs(value) < 0.001 && Math.Abs((double)oldvalue) < 0.001) value = me.FontSize * 2;
			me.ControlLayout.HeightRequest = value;
			me.InputEntry.HeightRequest = me.InputEntry.MinimumHeightRequest = value;
			me.Down.WidthRequest = me.Down.MinimumWidthRequest = me.Down.HeightRequest = value;
			me.Up.WidthRequest = me.Up.MinimumWidthRequest = me.Up.HeightRequest = value;
			me.Up.FontSize = me.Down.FontSize = value * 0.6;
			if (me.IsVisible)
			{
				me.IsVisible = false;
				me.IsVisible = true;
			}
		}

		public double ControlHeight { get => (double)GetValue(ControlHeightProperty); set => SetValue(ControlHeightProperty, value); }

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(DecimalUpDown), Color.White, BindingMode.OneWay, propertyChanged: HandleTextColorChanged);

		private static void HandleTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as DecimalUpDown;
			me.TopLabel.TextColor = me.MinMaxLabel.TextColor = me.InputEntry.BackgroundColor = (Color)newvalue;
			me.InputEntry.TextColor = Utils.InvertColor((Color)newvalue);
		}

		public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

		public static readonly BindableProperty UnitsProperty = BindableProperty.Create(nameof(Units), typeof(string), typeof(DecimalUpDown), string.Empty, BindingMode.OneWay, propertyChanged: HandleUnitsChanged);

		private static void HandleUnitsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as DecimalUpDown;
			var value = newvalue as string;
			me._unitsString = string.IsNullOrEmpty(value) ? string.Empty : " [" + value + "]";
			me.MinMaxLabelUpdate();
		}

		public string Units { get => (string)GetValue(UnitsProperty); set => SetValue(UnitsProperty, value); }

		public static readonly BindableProperty TopLabelTextProperty = BindableProperty.Create(nameof(TopLabelText), typeof(string), typeof(DecimalUpDown), string.Empty, BindingMode.OneWay, propertyChanged: HandleTopLabelChanged);

		private static void HandleTopLabelChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as DecimalUpDown;
			me.TopLabel.Text = (string)newvalue;
			me.TopLabel.IsVisible = !string.IsNullOrEmpty((string)newvalue);
		}

		public string TopLabelText { get => (string)GetValue(TopLabelTextProperty); set => SetValue(TopLabelTextProperty, value); }

		public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(DecimalUpDown), double.MaxValue, BindingMode.TwoWay, propertyChanged: HandleMinMaxChanged);
		public double Maximum { get => (double)GetValue(MaximumProperty); set => SetValue(MaximumProperty, Math.Round(value, Math.Max(0, DecimalPlace))); }

		public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(DecimalUpDown), double.MinValue, BindingMode.TwoWay, propertyChanged: HandleMinMaxChanged);
		public double Minimum { get => (double)GetValue(MinimumProperty); set => SetValue(MinimumProperty, Math.Round(value, Math.Max(0, DecimalPlace))); }

		private static void HandleMinMaxChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			((DecimalUpDown)bindable).Validate();
			((DecimalUpDown)bindable).MinMaxLabelUpdate();
		}

		public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(DecimalUpDown), 0.0, BindingMode.TwoWay, propertyChanged: HandleValueChanged);

		private static void HandleValueChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as DecimalUpDown;
			me.InputEntry.Text = ((double)newvalue).ToString("F" + Math.Max(0, ((DecimalUpDown)bindable).DecimalPlace));
			me.Validate();
		}

		public double Value { get => (double)GetValue(ValueProperty); set => SetValue(ValueProperty, Math.Round(value, Math.Max(0, DecimalPlace))); }

		public static readonly BindableProperty DecimalPlaceProperty = BindableProperty.Create(nameof(DecimalPlace), typeof(int), typeof(DecimalUpDown), 0, BindingMode.TwoWay, propertyChanged: DecimalChanged);
		private static void DecimalChanged(BindableObject bindable, object oldvalue, object newvalue) { ((DecimalUpDown)bindable).Validate(); }
		public int DecimalPlace { get => (int)GetValue(DecimalPlaceProperty); set => SetValue(DecimalPlaceProperty, value); }

		public static readonly BindableProperty IncrementProperty = BindableProperty.Create(nameof(Increment), typeof(double), typeof(DecimalUpDown), 1.0);
		public double Increment { get => (double)GetValue(IncrementProperty); set => SetValue(IncrementProperty, value); }

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(DecimalUpDown), 14.0, BindingMode.TwoWay, propertyChanged: HandleFontSizeChanged);

		private static void HandleFontSizeChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (DecimalUpDown)bindable;
			var newSize = Utils.GetScalledFontSize((double)newvalue);
			me.TopLabel.FontSize = me.InputEntry.FontSize = newSize;
			me.MinMaxLabel.FontSize = newSize * 0.8;
			HandleControlHeightChanged(me, me.ControlHeight, me.ControlHeight);
		}

		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

		public static BindableProperty ShowMinMaxProperty = BindableProperty.Create(nameof(ShowMinMax), typeof(bool), typeof(DecimalUpDown), true, propertyChanged: HandleShowMinMaxChanged);

		private static void HandleShowMinMaxChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (DecimalUpDown)bindable;
			me.MinMaxLabel.IsVisible = (bool)newvalue;
		}

		public bool ShowMinMax { get => (bool)GetValue(ShowMinMaxProperty); set => SetValue(ShowMinMaxProperty, value); }

		private void Validate(bool updateValue = false)
		{
			if (string.IsNullOrWhiteSpace(InputEntry.Text))
			{
				HasValidationError = true;
			}
			else
			{
				decimal inputValue = 0;
				if (TryParse(InputEntry.Text, ref inputValue))
				{
					var val = (double)Rounded(inputValue, DecimalPlace);
					Up.IsEnabled = val < Maximum;
					Down.IsEnabled = val > Minimum;
					if (val <= Maximum && val >= Minimum)
					{
						HasValidationError = false;
						OnPropertyChanged();
						if (updateValue) SetValue(ValueProperty, val);
						return;
					}
				}

				HasValidationError = true;
			}

			OnPropertyChanged();
		}

		private bool _hasValidationError;

		public bool HasValidationError
		{
			get => IsEnabled && _hasValidationError;
			private set
			{
				if (_hasValidationError == value) return;
				_hasValidationError = value;
				InputEntry.TextColor = _hasValidationError ? Color.Red : Color.Black;
				OnPropertyChanged();
			}
		}

		private decimal Rounded(decimal inputValue, int places) { return places >= 0 ? Math.Round(inputValue, places) : inputValue / (int)Math.Pow(10, -places) * (int)Math.Pow(10, -places); }

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

		//private void InputEntry_OnTextChanged(object sender, TextChangedEventArgs e) { Validate(updateValue: true); }
		private void InputEntry_OnUnfocused(object sender, FocusEventArgs e) { Validate(true); }
		private void InputEntry_OnCompleted(object sender, EventArgs e) { Validate(true); }

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

		private void Up_Click(object sender, EventArgs eventArgs)
		{
			if (Value + Increment < Minimum)
				Value = Minimum;
			else if (Value + Increment < Maximum)
				Value += Increment;
			else
				Value = Maximum;
		}

		private void Down_Click(object sender, EventArgs eventArgs)
		{
			if (Value - Increment > Maximum)
				Value = Maximum;
			else if (Value - Increment > Minimum)
				Value -= Increment;
			else
				Value = Minimum;
		}

		#region Overrides of BindableObject
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == null) return;
			if (propertyName == "IsEnabled")
			{
				Up.IsEnabled = Down.IsEnabled = IsEnabled;
				InputEntry.IsEnabled = IsEnabled;

				//InputEntry.BackgroundColor = IsEnabled ? TextColor : Color.Transparent;
				//Up.IsVisible = Down.IsVisible = IsEnabled;
			}
		}

		private void MinMaxLabelUpdate()
		{
			var val = $"Min: {Minimum} | Max: {Maximum}{_unitsString}";
			MinMaxLabel.Text = val;
			MinMaxLabel.IsVisible = !string.IsNullOrEmpty(val);
		}
		#endregion
	}
}