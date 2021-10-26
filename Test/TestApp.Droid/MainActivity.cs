using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using TestLib;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Controls;
using ZXing.Net.Mobile.Forms.Android;
using Platform = ZXing.Net.Mobile.Forms.Android.Platform;

[assembly: UsesPermission(Manifest.Permission.Flashlight)]

namespace XamarinControlsApp.Droid
{
	[Activity(Label = "XamarinControls", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			if (Resources?.DisplayMetrics != null) Utils.ScaleFactor = 2 / Resources.DisplayMetrics.ScaledDensity;
			base.OnCreate(bundle);

			Forms.Init(this, bundle);
			Platform.Init();
			var ap = new App();
			LoadApplication(ap);
#if __ANDROID__

			// Initialize the scanner first so it can track the current context
			//MobileBarcodeScanner.Initialize (Application);
			Xamarin.Essentials.Platform.Init(Application);
#endif
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults) { PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults); }
	}
}