using System;
using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Controls.Android.Renderers;
using XamarinForms.Controls.Basic;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(RoundedButtonRenderer))]

namespace XamarinForms.Controls.Android.Renderers
{
	public class RoundedButtonRenderer : ButtonRenderer
	{
		private GradientDrawable _normal, _pressed;

		public RoundedButtonRenderer(Context context) : base(context) { }

		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control == null) return;
			var button = (RoundedButton)e.NewElement;

			button.SizeChanged += (s, args) =>
			{
				var radius = (float)Math.Min(button.Width, button.Height);

				// Create a drawable for the button's normal state
				_normal = new GradientDrawable();

				if (Math.Abs(button.BackgroundColor.R - -1.0) < Utils.Tolerance && Math.Abs(button.BackgroundColor.G - -1.0) < Utils.Tolerance && Math.Abs(button.BackgroundColor.B - -1.0) < Utils.Tolerance)
					_normal.SetColor(Color.ParseColor("#ff2c2e2f"));
				else
					_normal.SetColor(button.BackgroundColor.ToAndroid());

				_normal.SetCornerRadius(radius);

				// Create a drawable for the button's pressed state
				_pressed = new GradientDrawable();
				var highlight = Context?.ObtainStyledAttributes(new[] { global::Android.Resource.Attribute.ColorActivatedHighlight }).GetColor(0, Color.Gray);
				if (highlight != null) _pressed.SetColor((int)highlight);
				_pressed.SetCornerRadius(radius);

				// Add the drawables to a state list and assign the state list to the button
				var sld = new StateListDrawable();
				sld.AddState(new[] { global::Android.Resource.Attribute.StatePressed }, _pressed);
				sld.AddState(new int[] { }, _normal);
				Control.SetBackground(sld);
				Control.SetPadding(0, 0, 0, 0);
			};
		}
	}
}