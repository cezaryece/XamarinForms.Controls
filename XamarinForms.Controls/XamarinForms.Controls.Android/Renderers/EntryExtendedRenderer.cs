using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Controls.Android.Renderers;
using XamarinForms.Controls.Basic;

[assembly: ExportRenderer(typeof(EntryExtended), typeof(EntryExtendedRenderer))]

namespace XamarinForms.Controls.Android.Renderers
{
	public class EntryExtendedRenderer : EntryRenderer
	{
		public EntryExtendedRenderer(Context context) : base(context) { }

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			if (Control == null) return;
			var textField = (EditText)Control;
			var entry = (EntryExtended)e.NewElement;
			if (!string.IsNullOrEmpty(entry.FontAsset)) textField.Typeface = TrySetFont(entry.FontAsset);
			textField.SetPadding(10, 20, 20, 10);
			var shape = new ShapeDrawable(new RectShape());
			shape.Paint.Color = entry.BorderColor.ToAndroid();
			shape.Paint.StrokeWidth = (float)entry.BorderWidth * 3;
			shape.Paint.SetStyle(Paint.Style.Stroke);
			textField.Background = shape;
			textField.SetBackgroundColor(entry.BackgroundColor.ToAndroid());
			textField.SetTextColor(entry.TextColor.ToAndroid());
			textField.TextSize = (float)entry.FontSize;
			Invalidate();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var entry = (EntryExtended)Element;
			var textField = (EditText)Control;
			switch (e.PropertyName)
			{
				case "TextColor":
					textField.SetTextColor(entry.TextColor.ToAndroid());
					break;
				case "BackgroundColor":
					textField.SetBackgroundColor(entry.BackgroundColor.ToAndroid());
					break;
				case "IsEnabled":
					Control.Enabled = Element.IsEnabled;
					break;
			}

			base.OnElementPropertyChanged(sender, e);
		}

		private Typeface TrySetFont(string fontName)
		{
			Typeface tf;
			try
			{
				tf = Typeface.CreateFromAsset(Context?.Assets, fontName);
				return tf;
			}
			catch (Exception ex)
			{
				Console.Write("not found in assets {0}", ex);
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