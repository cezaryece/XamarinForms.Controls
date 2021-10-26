using System.Diagnostics;
using System.Reflection;
using Xamarin.Forms;
using XamarinForms.Controls;

namespace TestLib
{
	public class App : Application
	{
		public static double CommonFontSize;

		public App()
		{
			CommonFontSize = Utils.GetScalledFontSize(14);
			Current.Resources = new ResourceDictionary();
			Current.Resources.Add(new Style(typeof(Label))
			{
				Setters =
				{
					new Setter { Property = Label.TextColorProperty, Value = Color.Yellow }, new Setter { Property = Label.FontSizeProperty, Value = CommonFontSize }
				}
			});
			Current.Resources.Add(new Style(typeof(ContentPage)) { Setters = { new Setter { Property = VisualElement.BackgroundColorProperty, Value = Color.Green } } });
			Current.Resources.Add(new Style(typeof(Button)) { Setters = { new Setter { Property = Button.FontSizeProperty, Value = CommonFontSize * 0.8 } } });
			Current.Resources.Add("SectionTitle", new Style(typeof(Label))
			{
				Setters =
				{
					new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold }, new Setter { Property = Label.FontSizeProperty, Value = CommonFontSize * 1.5 }
				}
			});

			// The root page of your application
			MainPage = new TestPage();

#if DEBUG
			var assembly = typeof(TestPage).GetTypeInfo().Assembly;
			foreach (var res in assembly.GetManifestResourceNames())
				Debug.WriteLine("found resource: " + res);
#endif
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}