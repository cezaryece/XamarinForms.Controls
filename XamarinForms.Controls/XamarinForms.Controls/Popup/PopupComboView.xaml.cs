using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XamarinForms.Controls.Basic;

namespace XamarinForms.Controls.Popup
{
	// ReSharper disable once RedundantExtendsListEntry
	public partial class PopupComboView : ContentView
	{
		public event EventHandler<PopupViewBase> ShowHidePopup;
		public event EventHandler<List<string>> SelectedItemsChanged;

		public LabelExtended HeaderLabel;
		public LabelExtended CancelLabel;
		public LabelExtended OkLabel;

		public PopupListView PopupListView { get; }

		public PopupComboView()
		{
			InitializeComponent();
			HeaderLabel = new LabelExtended
			{
				//TextColor = TextColor,
				//Style = HeaderStyle,
				//FontAttributes = FontAttributes.Bold,
				Text = PlaceholderText
			};
			CancelLabel = new LabelExtended
			{
				Text = "Anuluj", HorizontalOptions = LayoutOptions.EndAndExpand, GestureRecognizers =
				{
					new TapGestureRecognizer
					{
						NumberOfTapsRequired = 1, Command = new Command(() => { OnPopupListCloseRequest(PopupListView, false); })
					}
				}

				//TextColor = TextColor,
				//FontAttributes = FontAttributes.Bold,
				//Style = FooterStyle
			};

			OkLabel = new LabelExtended
			{
				Text = "OK", HorizontalOptions = LayoutOptions.StartAndExpand, GestureRecognizers =
				{
					new TapGestureRecognizer
					{
						NumberOfTapsRequired = 1, Command = new Command(() => { OnPopupListCloseRequest(PopupListView, true); })
					}
				}
				, IsVisible = IsEnabled = AllowMultiply, TextColor = TextColor, FontAttributes = FontAttributes.Bold, Style = FooterStyle
			};

			PopupListView = new PopupListView
			{
				HeaderView = new ContentView
				{
					Content = HeaderLabel
				}
				, FooterView = new ContentView
				{
					Content = new StackLayout
					{
						Children =
						{
							OkLabel, CancelLabel
						}
						, Orientation = StackOrientation.Horizontal
					}
				}
				, BorderColor = BorderColor, BorderWidth = BorderWidth, OpacityMargin = 15, OpacityColor = BackgroundColor != Color.Default ? BackgroundColor.MultiplyAlpha(0.7) : Color.Transparent, ContentBackgroudColor = BackgroundColor
				, BodyStyle = BodyStyle, TextColor = TextColor, AllowMultiply = AllowMultiply, BackgroundColor = BackgroundColor, SelectedColor = SelectedColor
			};
			PopupListView.CloseRequest += OnPopupListCloseRequest;
			PopupListView.SelectionChanged += (sender, selected) => { SelectedItems = selected.Select(i => i.Name).ToList(); };

			BackgroundBox.Padding = 5 + BorderWidth;
			BackgroundBox.BackgroundColor = PlaceholderBackgroundColor;

			//PlaceHolder.ShowType = false;
			//PlaceHolder.Style = PlaceholderStyle;
			//PlaceHolder.TextColor = PlaceholderTextColor;
			PlaceHolder.Text = PlaceholderText;
			PlaceHolder.HeightRequest = PlaceHolder.FontSize * (Device.OS == TargetPlatform.Windows ? 3 : 2);
		}

		#region Border
		public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(PopupComboView), 0.0, propertyChanged: HandleBorderWidthChanged);

		private static void HandleBorderWidthChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupComboView)bindable;
			me.BorderGrid.Padding = (double)newvalue;
			me.PopupListView.BorderWidth = (double)newvalue;
			me.BackgroundBox.Padding = 5 + (double)newvalue;
		}

		public double BorderWidth { get => (double)GetValue(BorderWidthProperty); set => SetValue(BorderWidthProperty, value); }

		public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(PopupComboView), Color.Default, propertyChanged: HandleBorderColorChanged);

		private static void HandleBorderColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupComboView)bindable;
			me.BorderGrid.BackgroundColor = (Color)newvalue;
			me.PopupListView.BorderColor = (Color)newvalue;
		}

		public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }
		#endregion

		public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(PopupComboView), Color.Default, propertyChanged: HandleBackgroundColorChanged);

		private static void HandleBackgroundColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupComboView)bindable;
			var newColor = (Color)newvalue;
			me.PopupListView.ContentBackgroudColor = newColor;
			me.PopupListView.OpacityColor = newColor != Color.Transparent && newColor != Color.Default && newColor != Color.Accent ? newColor.MultiplyAlpha(0.7) : Color.Transparent;
		}

		public new Color BackgroundColor { get => (Color)GetValue(BackgroundColorProperty); set => SetValue(BackgroundColorProperty, value); }

		#region PlaceHolder
		public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(PopupComboView), "Wybierz...", propertyChanging: HandlePlaceholderTextChanged);

		private static void HandlePlaceholderTextChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupComboView)bindable;
			me.SetPlaceholderText(me.SelectedItems);
			me.HeaderLabel.Text = (string)newvalue;
		}

		public string PlaceholderText { get => (string)GetValue(PlaceholderTextProperty); set => SetValue(PlaceholderTextProperty, value); }

		public static readonly BindableProperty PlaceholderDisabledTextProperty =
			BindableProperty.Create(nameof(PlaceholderDisabledText), typeof(string), typeof(PopupComboView), string.Empty, propertyChanging: HandlePlaceholderDisabledTextChanged);

		private static void HandlePlaceholderDisabledTextChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupComboView)bindable;
			me.SetPlaceholderText(me.SelectedItems);
		}

		public string PlaceholderDisabledText { get => (string)GetValue(PlaceholderDisabledTextProperty); set => SetValue(PlaceholderDisabledTextProperty, value); }

		public static readonly BindableProperty PlaceholderBackgroundColorProperty =
			BindableProperty.Create(nameof(PlaceholderBackgroundColor), typeof(Color), typeof(PopupComboView), Color.Default, propertyChanged: HandlePlaceholdeBackgroundColorChanged);

		private static void HandlePlaceholdeBackgroundColorChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupComboView).BackgroundBox.BackgroundColor = (Color)newvalue; }
		public Color PlaceholderBackgroundColor { get => (Color)GetValue(PlaceholderBackgroundColorProperty); set => SetValue(PlaceholderBackgroundColorProperty, value); }

		public static readonly BindableProperty PlaceholderTextColorProperty = BindableProperty.Create(nameof(PlaceholderTextColor), typeof(Color), typeof(PopupComboView), Color.Default, propertyChanged: HandlePlaceholdeTextColorChanged);
		private static void HandlePlaceholdeTextColorChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupComboView).PlaceHolder.TextColor = (Color)newvalue; }
		public Color PlaceholderTextColor { get => (Color)GetValue(PlaceholderTextColorProperty); set => SetValue(PlaceholderTextColorProperty, value); }
		#endregion

		public static BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<string>), typeof(PopupComboView), new List<string>(), propertyChanging: HandleItemsChanged);

		private static void HandleItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupComboView)bindable;
			var newItems = (List<string>)newvalue;
			me.PopupListView.Items = newItems?.Select(i => new PopupListItem(i, me.SelectedItems != null && me.SelectedItems.Contains(i))).ToList();
			me.IsEnabled = newItems != null && newItems.Any();
		}

		public List<string> Items { get => (List<string>)GetValue(ItemsProperty); set => SetValue(ItemsProperty, value); }

		public static BindableProperty AllowMultiplyProperty = BindableProperty.Create(nameof(AllowMultiply), typeof(bool), typeof(PopupComboView), false, propertyChanged: OnAllowMultiplyChanged);

		private static void OnAllowMultiplyChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupComboView)bindable;
			me.PopupListView.AllowMultiply = (bool)newvalue;
			me.OkLabel.IsVisible = me.OkLabel.IsEnabled = me.AllowMultiply;
		}

		public bool AllowMultiply { get => (bool)GetValue(AllowMultiplyProperty); set => SetValue(AllowMultiplyProperty, value); }

		public static readonly BindableProperty PlaceholderStyleProperty = BindableProperty.Create(nameof(PlaceholderStyle), typeof(Style), typeof(PopupComboView), null, propertyChanged: HandlePlaceholderChanged);
		private static void HandlePlaceholderChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupComboView).PlaceHolder.Style = (Style)newvalue; }
		public Style PlaceholderStyle { get => (Style)GetValue(PlaceholderStyleProperty); set => SetValue(PlaceholderStyleProperty, value); }

		public static readonly BindableProperty BodyStyleProperty = BindableProperty.Create(nameof(BodyStyle), typeof(Style), typeof(PopupComboView), null, propertyChanged: HandleBodyStyleChanged);

		private static void HandleBodyStyleChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupComboView;
			me.PopupListView.BodyStyle = (Style)newvalue;
		}

		public Style BodyStyle { get => (Style)GetValue(BodyStyleProperty); set => SetValue(BodyStyleProperty, value); }

		public static readonly BindableProperty HeaderStyleProperty = BindableProperty.Create(nameof(HeaderStyle), typeof(Style), typeof(PopupComboView), null, propertyChanged: HandleHeaderStyleChanged);
		private static void HandleHeaderStyleChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupComboView).HeaderLabel.Style = (Style)newvalue; }
		public Style HeaderStyle { get => (Style)GetValue(HeaderStyleProperty); set => SetValue(HeaderStyleProperty, value); }

		public static readonly BindableProperty FooterStyleProperty = BindableProperty.Create(nameof(FooterStyle), typeof(Style), typeof(PopupComboView), null, propertyChanged: HandleFooterStyleChanged);

		private static void HandleFooterStyleChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			(bindable as PopupComboView).CancelLabel.Style = (Style)newvalue;
			(bindable as PopupComboView).OkLabel.Style = (Style)newvalue;
		}

		public Style FooterStyle { get => (Style)GetValue(FooterStyleProperty); set => SetValue(FooterStyleProperty, value); }

		public static readonly BindableProperty HeaderColorProperty = BindableProperty.Create(nameof(HeaderColor), typeof(Color), typeof(PopupComboView), Color.Default, propertyChanged: HandleHeaderColorChanged);

		private static void HandleHeaderColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupComboView;
			me.HeaderLabel.TextColor = (Color)newvalue;
		}

		public Color HeaderColor { get => (Color)GetValue(HeaderColorProperty); set => SetValue(HeaderColorProperty, value); }

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(PopupComboView), Color.Default, propertyChanged: HandleTextColorChanged);

		private static void HandleTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupComboView;
			me.PopupListView.TextColor = (Color)newvalue;
			if (me.HeaderColor == Color.Default) me.HeaderColor = (Color)newvalue;
			if (me.FooterColor == Color.Default) me.FooterColor = (Color)newvalue;
		}

		public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

		public static readonly BindableProperty FooterColorProperty = BindableProperty.Create(nameof(FooterColor), typeof(Color), typeof(PopupComboView), Color.Default, propertyChanged: HandleFooterColorChanged);

		private static void HandleFooterColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupComboView;
			me.OkLabel.TextColor = me.CancelLabel.TextColor = (Color)newvalue;
		}

		public Color FooterColor { get => (Color)GetValue(FooterColorProperty); set => SetValue(FooterColorProperty, value); }

		public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(PopupComboView), Color.Default, propertyChanged: HandleSelectedColorChanged);

		private static void HandleSelectedColorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = bindable as PopupComboView;
			me.PopupListView.SelectedColor = (Color)newvalue;
		}

		public Color SelectedColor { get => (Color)GetValue(SelectedColorProperty); set => SetValue(SelectedColorProperty, value); }

		public static BindableProperty SelectedItemsProperty =
			BindableProperty.Create(nameof(SelectedItems), typeof(List<string>), typeof(PopupComboView), new List<string>(), BindingMode.TwoWay, propertyChanging: HandleSelectedItemsChanged);

		private static void HandleSelectedItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupComboView)bindable;
			var selected = (List<string>)newvalue;
			if (selected != null && selected.Count > 1 && !me.AllowMultiply)
				selected = new List<string> { selected[0] };
			var popuplistSelected = new List<PopupListItem>();
			if (me.PopupListView.Items.Any() && selected != null && selected.Any())
			{
				popuplistSelected.AddRange(selected.Select(item => me.PopupListView.Items.First(i => i.Name == item)));
				me.SelectedItem = selected[0];
			}
			else
			{
				me.SelectedItem = null;
			}

			me.PopupListView.SelectedItems = popuplistSelected;
			me.SetPlaceholderText(selected);
			me.SelectedItemsChanged?.Invoke(me, selected);
		}

		public List<string> SelectedItems { get => (List<string>)GetValue(SelectedItemsProperty); set => SetValue(SelectedItemsProperty, value); }

		public static BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(string), typeof(PopupComboView), string.Empty, BindingMode.TwoWay, propertyChanging: HandleSelectedItemChanged);

		private static void HandleSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var me = (PopupComboView)bindable;
			var itemString = (string)newvalue;
			me.SelectedItems = string.IsNullOrEmpty(itemString) ? new List<string>() : new List<string> { itemString };
		}

		public string SelectedItem { get => (string)GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }

		private void Label_OnTapped(object sender, EventArgs e)
		{
			if (IsEnabled)
				ShowHidePopup?.Invoke(this, PopupListView);
		}

		private void OnPopupListCloseRequest(object sender, bool hasResult)
		{
			if (hasResult)
			{
				SelectedItems = PopupListView.SelectedItems.Select(si => si.Name).ToList();
				SetPlaceholderText(SelectedItems);
			}

			ShowHidePopup?.Invoke(this, PopupListView);
		}

		private void SetPlaceholderText(List<string> selected)
		{
			if (selected == null || selected.Count == 0)
			{
				if (IsEnabled || string.IsNullOrEmpty(PlaceholderDisabledText))
					PlaceHolder.Text = PlaceholderText;
				else
					PlaceHolder.Text = PlaceholderDisabledText;
			}
			else if (selected.Count == 1)
			{
				PlaceHolder.Text = selected[0];
			}
			else
			{
				PlaceHolder.Text = $"Wybrano: {selected.Count}";
			}
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			if (propertyName == nameof(IsEnabled))
			{
				Opacity = IsEnabled ? 1 : 0.5;
				SetPlaceholderText(SelectedItems);
			}

			base.OnPropertyChanged(propertyName);
		}
	}
}