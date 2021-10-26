using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using AT.XamarinControls.Basic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace TestLib
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	// ReSharper disable once RedundantExtendsListEntry
	public partial class TestPage : ContentPage
	{
		public TestLibViewModel ViewModel => BindingContext as TestLibViewModel;
		public TestPage()
		{
			InitializeComponent();
			ViewModel.Enabled = IsEnabled;
			//Image1.Source = ImageSource.FromResource("TestLib.Images.inkasent-logo.png");
		}

		public void On_Clicked(object sender, EventArgs e)
		{
			if (ViewModel.EventsItems.Any())
				ViewModel.EventsItems.Clear();
			else
				ViewModel.SetEventsItems();
		}

		private async void Button_OnClicked(object sender, EventArgs e)
		{
			try
			{
				//var scanner = new ZXing.Mobile.MobileBarcodeScanner();

				//var result = await scanner.Scan();
				//ViewModel.CodeText = result != null ? result.Text : "Brak kodu";
				//ViewModel.CodeType = result?.BarcodeFormat.ToString();
				var scanPage = new ZXingScannerPage();

				scanPage.OnScanResult += result => {
					// Stop scanning
					scanPage.IsScanning = false;

					// Pop the page and show the result
					Device.BeginInvokeOnMainThread(() => {
						Navigation.PopModalAsync();
						ViewModel.CodeText = result != null ? result.Text : "Brak kodu";
						ViewModel.CodeType = result?.BarcodeFormat.ToString();
						DisplayAlert("Scanned Barcode", result?.Text, "OK");
					});
				};

				// Navigate to our scanner page
				await Navigation.PushModalAsync(scanPage);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				throw;
			}
		}
	}
}
