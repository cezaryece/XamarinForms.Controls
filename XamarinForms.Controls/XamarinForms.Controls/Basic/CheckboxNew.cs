using System;
using Xamarin.Forms;

namespace XamarinForms.Controls.Basic
{
	public class CheckboxNew : View
	{
		/// <summary>
		///     The checked changed event.
		/// </summary>
		public event EventHandler<bool> CheckedChanged;

		public CheckboxNew() { HandleCheckedChanged(this, null, Checked); }

		/// <summary>
		///     The checked state property.
		/// </summary>
		public static BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool), typeof(CheckboxNew), false, BindingMode.TwoWay, propertyChanged: HandleCheckedChanged);

		private static void HandleCheckedChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (CheckboxNew)bindable;
			me.CheckedChanged?.Invoke(me, (bool)newvalue);
			me.OnChckedChanged();
		}

		/// <summary>
		///     Gets or sets a value indicating whether the control is checked.
		/// </summary>
		/// <value>The checked state.</value>
		public bool Checked { get => (bool)GetValue(CheckedProperty); set => SetValue(CheckedProperty, value); }

		/// <summary>
		///     The checked text property.
		/// </summary>
		public static BindableProperty CheckedTextProperty = BindableProperty.Create(nameof(CheckedText), typeof(string), typeof(CheckboxNew), string.Empty, propertyChanged: OnTextChanged);

		/// <summary>
		///     Gets or sets a value indicating the checked text.
		/// </summary>
		/// <value>The checked state.</value>
		/// <remarks>
		///     Overwrites the default text property if set when checkbox is checked.
		/// </remarks>
		public string CheckedText { get => (string)GetValue(CheckedTextProperty); set => SetValue(CheckedTextProperty, value); }

		/// <summary>
		///     The unchecked text property.
		/// </summary>
		public static BindableProperty UncheckedTextProperty = BindableProperty.Create(nameof(UncheckedText), typeof(string), typeof(CheckboxNew), string.Empty, propertyChanged: OnTextChanged);

		/// <summary>
		///     Gets or sets a value indicating whether the control is checked.
		/// </summary>
		/// <value>The checked state.</value>
		/// <remarks>
		///     Overwrites the default text property if set when checkbox is checked.
		/// </remarks>
		public string UncheckedText { get => (string)GetValue(UncheckedTextProperty); set => SetValue(UncheckedTextProperty, value); }

		/// <summary>
		///     The default text property.
		/// </summary>
		public static BindableProperty DefaultTextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckboxNew), string.Empty, propertyChanged: OnTextChanged);

		/// <summary>
		///     Gets or sets the text.
		/// </summary>
		public string DefaultText { get => (string)GetValue(DefaultTextProperty); set => SetValue(DefaultTextProperty, value); }

		/// <summary>
		///     The font size property
		/// </summary>
		public static BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CheckboxNew), 14.0, propertyChanged: OnTextChanged);

		/// <summary>
		///     Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, Utils.GetScalledFontSize(value)); }

		/// <summary>
		///     The font name property.
		/// </summary>
		public static BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(CheckboxNew), string.Empty, propertyChanged: OnTextChanged);

		/// <summary>
		///     Gets or sets the name of the font.
		/// </summary>
		/// <value>The name of the font.</value>
		public string FontName { get => (string)GetValue(FontNameProperty); set => SetValue(FontNameProperty, value); }

		private static void OnTextChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (CheckboxNew)bindable;
			if (!me.IsVisible) return;
			me.IsVisible = false;
			me.IsVisible = true;
		}

		private Color _textColor;

		public Color TextColor
		{
			get => _textColor;
			private set
			{
				_textColor = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     Identifies the TextColor bindable property.
		/// </summary>
		/// <remarks />
		public static BindableProperty DefaultTextColorProperty = BindableProperty.Create(nameof(DefaultTextColor), typeof(Color), typeof(CheckboxNew), Color.Default, propertyChanged: HandleCheckedColorChanged);

		public Color DefaultTextColor { get => (Color)GetValue(DefaultTextColorProperty); set => SetValue(DefaultTextColorProperty, value); }

		public static BindableProperty CheckedTextColorProperty = BindableProperty.Create(nameof(CheckedTextColor), typeof(Color), typeof(CheckboxNew), Color.Default, propertyChanged: HandleCheckedColorChanged);
		public Color CheckedTextColor { get => (Color)GetValue(CheckedTextColorProperty); set => SetValue(CheckedTextColorProperty, value); }

		public static BindableProperty UnCheckedTextColorProperty = BindableProperty.Create(nameof(UnCheckedTextColor), typeof(Color), typeof(CheckboxNew), Color.Default, propertyChanged: HandleCheckedColorChanged);
		public Color UnCheckedTextColor { get => (Color)GetValue(UnCheckedTextColorProperty); set => SetValue(UnCheckedTextColorProperty, value); }

		public static BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Color), typeof(CheckboxNew), Color.Default);
		public Color Background { get => (Color)GetValue(BackgroundProperty); set => SetValue(BackgroundProperty, value); }

		private static void HandleCheckedColorChanged(BindableObject bindable, object oldValue, object newValue) { ((CheckboxNew)bindable).OnChckedChanged(); }

		private void OnChckedChanged()
		{
			if (Checked)
				TextColor = CheckedTextColor != Color.Default ? CheckedTextColor : DefaultTextColor;
			else
				TextColor = UnCheckedTextColor != Color.Default ? UnCheckedTextColor : DefaultTextColor;
		}

		/// <summary>
		///     Gets the text.
		/// </summary>
		/// <value>The text.</value>
		private string _text;

		public string Text
		{
			get
			{
				var newtext = Checked ? string.IsNullOrEmpty(CheckedText) ? DefaultText : CheckedText : string.IsNullOrEmpty(UncheckedText) ? DefaultText : UncheckedText;
				if (!string.Equals(_text, newtext, StringComparison.Ordinal))
					_text = newtext;
				return _text;
			}
		}

		//#region Overrides of Element
		//protected override void OnPropertyChanged(string propertyName = null)
		//{
		//	base.OnPropertyChanged(propertyName);
		//	//if (propertyName == nameof(Text) || propertyName == nameof(Checked))
		//	//{
		//	//	OnTextChanged();
		//	//	OnChckedChanged();
		//	//}
		//}
		//#endregion
	}
}