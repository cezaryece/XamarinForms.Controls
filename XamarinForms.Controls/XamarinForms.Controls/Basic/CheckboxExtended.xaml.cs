using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinForms.Controls.Clases;

namespace XamarinForms.Controls.Basic
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class CheckboxExtended : ContentView
	{
		public CheckboxExtended()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;

			var t = new TapGestureRecognizer();
			t.Tapped += OnTapped;
			GridLayout.GestureRecognizers.Add(t);

			BoxLabel.FontAsset = "FontAwesome.ttf";
			BoxLabel.Text = "\uF0C8"; //fa - square  · Unicode: f0c8 · Created: v2.0 · Categories: Web Application Icons, Form Control Icons
			CheckLabel.FontAsset = "FontAwesome.ttf";
			CheckLabel.Text = "\uF096"; //fa-square-o  · Unicode: f096 · Created: v2.0 · Categories: Web Application Icons, Form Control Icons
			HandleCheckedChanged(this, null, Checked);
		}

		/// <summary>
		///     The checked changed event.
		/// </summary>
		public event EventHandler<bool> CheckedChanged;

		public static BindableProperty TagProperty = BindableProperty.Create(nameof(Tag), typeof(object), typeof(CheckboxExtended));
		public object Tag { get => GetValue(TagProperty); set => SetValue(TagProperty, value); }

		/// <summary>
		///     The checked state property.
		/// </summary>
		public static BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool), typeof(CheckboxExtended), false, BindingMode.TwoWay, propertyChanged: HandleCheckedChanged);

		private static void HandleCheckedChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (CheckboxExtended)bindable;
			var checkBoxItem = me.Tag as CheckBoxItem;
			if (checkBoxItem != null && checkBoxItem.IsChecked != (bool)newvalue)
				checkBoxItem.IsChecked = (bool)newvalue;
			me.CheckedChanged?.Invoke(me, (bool)newvalue);
			me.OnCheckedChangeUpdate();
		}

		/// <summary>
		///     Gets or sets a value indicating whether the control is checked.
		/// </summary>
		/// <value>The checked state.</value>
		public bool Checked { get => (bool)GetValue(CheckedProperty); set => SetValue(CheckedProperty, value); }

		/// <summary>
		///     The checked text property.
		/// </summary>
		public static BindableProperty CheckedTextProperty = BindableProperty.Create(nameof(CheckedText), typeof(string), typeof(CheckboxExtended), string.Empty, propertyChanged: OnTextChanged);

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
		public static BindableProperty UncheckedTextProperty = BindableProperty.Create(nameof(UncheckedText), typeof(string), typeof(CheckboxExtended), string.Empty, propertyChanged: OnTextChanged);

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
		public static BindableProperty DefaultTextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckboxExtended), string.Empty, propertyChanged: OnTextChanged);

		/// <summary>
		///     Gets or sets the text.
		/// </summary>
		public string DefaultText { get => (string)GetValue(DefaultTextProperty); set => SetValue(DefaultTextProperty, value); }

		/// <summary>
		///     The font size property
		/// </summary>
		public static BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CheckboxExtended), 14.0, propertyChanged: OnTextChanged);

		/// <summary>
		///     Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, Utils.GetScalledFontSize(value)); }

		/// <summary>
		///     The font name property.
		/// </summary>
		public static BindableProperty FontAssetProperty = BindableProperty.Create(nameof(FontAsset), typeof(string), typeof(CheckboxExtended), string.Empty, propertyChanged: OnTextChanged);

		/// <summary>
		///     Gets or sets the name of the font.
		/// </summary>
		/// <value>The name of the font.</value>
		public string FontAsset { get => (string)GetValue(FontAssetProperty); set => SetValue(FontAssetProperty, value); }

		public static BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(CheckboxExtended), string.Empty, propertyChanged: OnTextChanged);

		/// <summary>
		///     Used and requierd only by Windows/WindowsPhone apps
		/// </summary>
		public string FontName { get => (string)GetValue(FontNameProperty); set => SetValue(FontNameProperty, value); }

		private static void OnTextChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (CheckboxExtended)bindable;
			if (me.Checked) me.TextLabel.Text = string.IsNullOrEmpty(me.CheckedText) ? me.DefaultText : me.CheckedText;
			else me.TextLabel.Text = string.IsNullOrEmpty(me.UncheckedText) ? me.DefaultText : me.UncheckedText;
			me.TextLabel.FontSize = me.FontSize;
			me.TextLabel.FontAsset = me.FontAsset;
			me.TextLabel.FontName = me.FontName;
			me.BoxLabel.FontSize = me.CheckLabel.FontSize = me.FontSize > 20 ? me.FontSize : me.FontSize + (20 - me.FontSize) / 1.5;

			//me.CheckGrid.Padding = new Thickness(0, me.BoxLabel.FontSize / 6, 0, 0);
			//me.CheckGrid.WidthRequest = me.FontSize * 1.1;
			me.CheckGrid.Margin = new Thickness(0, 0, me.FontSize > 10 ? me.FontSize / 2 : 5.0, 0);
		}

		public void OnTextChanged() { OnTextChanged(this, null, null); }

		/// <summary>
		///     Identifies the TextColor bindable property.
		/// </summary>
		/// <remarks />
		public static BindableProperty DefaultTextColorProperty = BindableProperty.Create(nameof(DefaultTextColor), typeof(Color), typeof(CheckboxExtended), Color.Default, propertyChanged: HandleCheckedColorChanged);

		public Color DefaultTextColor { get => (Color)GetValue(DefaultTextColorProperty); set => SetValue(DefaultTextColorProperty, value); }

		public static BindableProperty CheckedTextColorProperty = BindableProperty.Create(nameof(CheckedTextColor), typeof(Color), typeof(CheckboxExtended), Color.Default, propertyChanged: HandleCheckedColorChanged);
		public Color CheckedTextColor { get => (Color)GetValue(CheckedTextColorProperty); set => SetValue(CheckedTextColorProperty, value); }

		public static BindableProperty UnCheckedTextColorProperty = BindableProperty.Create(nameof(UnCheckedTextColor), typeof(Color), typeof(CheckboxExtended), Color.Default, propertyChanged: HandleCheckedColorChanged);
		public Color UnCheckedTextColor { get => (Color)GetValue(UnCheckedTextColorProperty); set => SetValue(UnCheckedTextColorProperty, value); }

		public static BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Color), typeof(CheckboxExtended), Color.Transparent, propertyChanged: HandleBackgroundChanged);
		private static void HandleBackgroundChanged(BindableObject bindable, object oldvalue, object newvalue) { ((CheckboxExtended)bindable).SetCurrentColors(); }
		public Color Background { get => (Color)GetValue(BackgroundProperty); set => SetValue(BackgroundProperty, value); }

		private static void HandleCheckedColorChanged(BindableObject bindable, object oldValue, object newValue) { ((CheckboxExtended)bindable).OnCheckedChangeUpdate(); }

		private void OnCheckedChangeUpdate()
		{
			SetCurrentColors();
			if (Checked)
				TextLabel.Text = string.IsNullOrEmpty(CheckedText) ? DefaultText : CheckedText;
			else
				TextLabel.Text = string.IsNullOrEmpty(UncheckedText) ? DefaultText : UncheckedText;
			CheckLabel.Text = Checked ? "\uF046" : "\uF096"; // fa-check-square-o  · Unicode: f046 · Created: v1.0 · Categories: Web Application Icons, Form Control Icons
		}

		public void SetCheckBoxItemTag(CheckBoxItem item)
		{
			Tag = item;
			DefaultText = item.KeyString;
			Checked = item.IsChecked;
			item.PropertyChanged += (sender, args) =>
			{
				DefaultText = item.KeyString;
				Checked = item.IsChecked;
			};
		}

		/// <summary>
		///     Gets the text.
		/// </summary>
		/// <value>The text.</value>
		private string _text = "";

		public string Text
		{
			get
			{
				var newtext = Checked ? string.IsNullOrEmpty(CheckedText) ? DefaultText : CheckedText : string.IsNullOrEmpty(UncheckedText) ? DefaultText : UncheckedText;

				// ReSharper disable once RedundantCheckBeforeAssignment
				if (_text != newtext)
					_text = newtext;
				return _text;
			}
		}

		protected void OnTapped(object sender, EventArgs args)
		{
			if (IsEnabled)
				Checked = !Checked;
		}

		protected void CheckboxExtended_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(IsEnabled)) SetCurrentColors();
		}

		protected void SetCurrentColors()
		{
			var currentColor = Checked ? CheckedTextColor != Color.Default ? CheckedTextColor : DefaultTextColor : UnCheckedTextColor != Color.Default ? UnCheckedTextColor : DefaultTextColor;
			CheckLabel.IsVisible = Checked;
			BoxLabel.IsVisible = !Checked;
			if (!IsEnabled && currentColor != Color.Default) currentColor = currentColor.MultiplyAlpha(0.5);
			TextLabel.TextColor = CheckLabel.TextColor = BoxLabel.TextColor = currentColor;
			BoxLabel.BackgroundColor = CheckLabel.BackgroundColor = Background;
		}

		private void Switch_OnToggled(object sender, ToggledEventArgs e) { Checked = e.Value; }
	}
}