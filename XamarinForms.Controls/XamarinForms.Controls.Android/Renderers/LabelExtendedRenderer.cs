using System;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Controls.Android.Renderers;
using XamarinForms.Controls.Basic;

[assembly: ExportRenderer(typeof(LabelExtended), typeof(LabelExtendedRenderer))]

namespace XamarinForms.Controls.Android.Renderers
{
	public class LabelExtendedRenderer : LabelRenderer
	{
		public LabelExtendedRenderer(Context context) : base(context) { }

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);
			if (Control == null) return;

			var label = (LabelExtended)e.NewElement; // for example
			if (!string.IsNullOrEmpty(label.FontAsset)) Control.Typeface = TrySetFont(label.FontAsset);
		}

		private Typeface TrySetFont(string fontName)
		{
			Typeface tf;
			try
			{
				tf = Typeface.CreateFromAsset(Context?.Assets, fontName);
				Console.Write("typeface created from asset {0}", fontName);
				return tf;
			}
			catch (Exception ex)
			{
				Console.Write("not found {0} in assets {1}", fontName, ex);
				try
				{
					tf = Typeface.CreateFromFile(fontName);
					return tf;
				}
				catch (Exception ex1)
				{
					Console.Write(ex1);
					return Typeface.Default;
				}
			}
		}
	}
}