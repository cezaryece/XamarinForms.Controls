using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.Controls.Popup
{
	public abstract class PopupContentPageBase : ContentPage, IDisposable
	{
		public bool IsDisposed { get; private set; }

		public Grid PopupMainGrid { get; private set; }

		protected PopupViewBase _popup;

		public void ShowPopup(PopupViewBase popup)
		{
			RemovePopup();
			_popup = popup;
			_popup.CloseRequest += _popup_CloseRequest;
			PopupMainGrid.Children.Add(_popup);
		}

		private void _popup_CloseRequest(object sender, bool e) { RemovePopup(); }

		protected override void OnAppearing()
		{
			if (PopupMainGrid == null)
			{
				PopupMainGrid = new Grid();
				PopupMainGrid.Children.Add(Content);
				Content = new ContentView { Content = PopupMainGrid };
			}

			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			RemovePopup();
			base.OnDisappearing();
		}

		protected override bool OnBackButtonPressed()
		{
			if (_popup != null)
			{
				RemovePopup();
				return true;
			}

			return base.OnBackButtonPressed();
		}

		protected void RemovePopup()
		{
			if (_popup != null)
			{
				if (PopupMainGrid != null && PopupMainGrid.Children.Contains(_popup))
					PopupMainGrid.Children.Remove(_popup);
				_popup.CloseRequest -= _popup_CloseRequest;
				_popup = null;
			}
		}

		public void PopupPage_OnShowHidePopup(object sender, PopupViewBase popup)
		{
			if (popup == _popup)
				RemovePopup();
			else
				ShowPopup(popup);
		}

		public Task<PopupDialog.DialogResult> ShowDialogPopupAsync(PopupDialog dialog)
		{
			RemovePopup();
			_popup = dialog;
			var dialogCompleted = new TaskCompletionSource<PopupDialog.DialogResult>();
			_popup.CloseRequest += (sender, b) =>
			{
				dialogCompleted.SetResult(dialog.Result);
				RemovePopup();
			};
			return dialogCompleted.Task;
		}

		public void Dispose()
		{
			if (IsDisposed) throw new ObjectDisposedException(GetType().FullName);

			IsDisposed = true;
			RemovePopup();
			OnDispose();
		}

		public virtual void OnDispose() { }
	}
}