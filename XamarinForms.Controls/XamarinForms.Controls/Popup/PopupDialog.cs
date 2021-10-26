using Xamarin.Forms;
using XamarinForms.Controls.Basic;

namespace XamarinForms.Controls.Popup
{
	public class PopupDialog : PopupViewBase
	{
		public enum DialogResult
		{
			Cancel
			, Ok
			, Retry
		}

		public DialogResult Result;

		public LabelExtended HeaderLabel;
		public LabelExtended BodyLabel;
		public LabelExtended CancelLabel;
		public LabelExtended OkLabel;
		public LabelExtended RetryLabel;

		public PopupDialog(FormattedString header, FormattedString body, string cancel, string ok = null, string retry = null)
		{
			HeaderLabel = header == null ? null : new LabelExtended
			{
				FormattedText = header, Style = HeaderStyle
			};
			BodyLabel = new LabelExtended
			{
				FormattedText = body, Style = BodyStyle
			};

			CancelLabel = new LabelExtended
			{
				Text = cancel, HorizontalOptions = LayoutOptions.EndAndExpand, GestureRecognizers =
				{
					new TapGestureRecognizer
					{
						NumberOfTapsRequired = 1, Command = new Command(() => { DialogCloseRequest(DialogResult.Cancel); })
					}
				}
				, Style = FooterStyle, Margin = new Thickness(0, 5, 10, 5)
			};

			OkLabel = string.IsNullOrEmpty(ok) ? null : new LabelExtended
			{
				Text = ok, HorizontalOptions = LayoutOptions.StartAndExpand, GestureRecognizers =
				{
					new TapGestureRecognizer
					{
						NumberOfTapsRequired = 1, Command = new Command(() => { DialogCloseRequest(DialogResult.Ok); })
					}
				}
				, IsVisible = !string.IsNullOrEmpty(ok), Style = FooterStyle, Margin = new Thickness(0, 5, 10, 5)
			};

			RetryLabel = string.IsNullOrEmpty(retry) ? null : new LabelExtended
			{
				Text = retry, HorizontalOptions = LayoutOptions.CenterAndExpand, GestureRecognizers =
				{
					new TapGestureRecognizer
					{
						NumberOfTapsRequired = 1, Command = new Command(() => { DialogCloseRequest(DialogResult.Retry); })
					}
				}
				, IsVisible = !string.IsNullOrEmpty(retry), Style = FooterStyle, Margin = new Thickness(0, 5, 10, 5)
			};

			var buttonPlace = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.End };
			if (OkLabel != null) buttonPlace.Children.Add(OkLabel);
			if (RetryLabel != null) buttonPlace.Children.Add(RetryLabel);
			if (CancelLabel != null) buttonPlace.Children.Add(CancelLabel);

			if (HeaderLabel != null)
				HeaderView = new ContentView
				{
					Content = HeaderLabel
				};

			BodyView = new ContentView
			{
				Content = BodyLabel
			};

			FooterView = new ContentView { Content = buttonPlace };
		}

		private void DialogCloseRequest(DialogResult result)
		{
			Result = result;
			OnCloseRequest(this, true);
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