using Xamarin.Forms;
using XamarinForms.Controls.Basic;

namespace XamarinForms.Controls.Popup
{
	public class PopupTextView : PopupViewBase
	{
		public LabelExtended HeaderLabel;
		public LabelExtended BodyLabel;
		public LabelExtended FooterLabel;

		public PopupTextView(FormattedString header, FormattedString body, string closeButton)
		{
			HeaderLabel = new LabelExtended
			{
				FormattedText = header, Style = HeaderStyle
			};
			BodyLabel = new LabelExtended
			{
				FormattedText = body, Style = BodyStyle
			};
			FooterLabel = new LabelExtended
			{
				Text = closeButton, HorizontalOptions = LayoutOptions.EndAndExpand, GestureRecognizers =
				{
					new TapGestureRecognizer
					{
						NumberOfTapsRequired = 1, Command = new Command(() => { OnCloseRequest(this, false); })
					}
				}
				, Style = FooterStyle
			};

			HeaderView = new ContentView
			{
				Content = HeaderLabel
			};
			BodyView = new ContentView
			{
				Content = BodyLabel
			};
			FooterView = new ContentView { Content = FooterLabel };
		}

		public static readonly BindableProperty HeaderStyleProperty = BindableProperty.Create(nameof(HeaderStyle), typeof(Style), typeof(PopupTextView), null, propertyChanged: HandleHeaderStyleChanged);
		private static void HandleHeaderStyleChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupTextView).HeaderLabel.Style = (Style)newvalue; }
		public Style HeaderStyle { get => (Style)GetValue(HeaderStyleProperty); set => SetValue(HeaderStyleProperty, value); }

		public static readonly BindableProperty BodyStyleProperty = BindableProperty.Create(nameof(BodyStyle), typeof(Style), typeof(PopupTextView), null, propertyChanged: HandleBodyStyleChanged);
		private static void HandleBodyStyleChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupTextView).BodyLabel.Style = (Style)newvalue; }
		public Style BodyStyle { get => (Style)GetValue(BodyStyleProperty); set => SetValue(BodyStyleProperty, value); }

		public static readonly BindableProperty FooterStyleProperty = BindableProperty.Create(nameof(FooterStyle), typeof(Style), typeof(PopupTextView), null, propertyChanged: HandleFooterStyleChanged);
		private static void HandleFooterStyleChanged(BindableObject bindable, object oldvalue, object newvalue) { (bindable as PopupTextView).FooterLabel.Style = (Style)newvalue; }
		public Style FooterStyle { get => (Style)GetValue(FooterStyleProperty); set => SetValue(FooterStyleProperty, value); }
	}
}