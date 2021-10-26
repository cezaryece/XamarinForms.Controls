using System;
using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Controls.Android.Renderers;
using XamarinForms.Controls.Basic;
using CheckBox = Android.Widget.CheckBox;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(CheckboxNew), typeof(CheckboxNewRenderer))]

namespace XamarinForms.Controls.Android.Renderers
{
	public class CheckboxNewRenderer : ViewRenderer<CheckboxNew, CheckBox>
	{
		private ColorStateList _defaultTextColor;

		public CheckboxNewRenderer(Context context) : base(context) { }

		/// <summary>
		///     Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<CheckboxNew> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				var checkBox = new CheckBox(Context);
				checkBox.CheckedChange += CheckBoxCheckedChange;

				_defaultTextColor = checkBox.TextColors;
				SetNativeControl(checkBox);
			}

			Control.Text = e.NewElement.Text;
			Control.Checked = e.NewElement.Checked;
			Control.Enabled = e.NewElement.IsEnabled;
			UpdateTextColor();

			if (e.NewElement.FontSize > 0) Control.TextSize = (float)e.NewElement.FontSize;

			if (!string.IsNullOrEmpty(e.NewElement.FontName)) Control.Typeface = TrySetFont(e.NewElement.FontName);
		}

		/// <summary>
		///     Handles the <see cref="E:ElementPropertyChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			switch (e.PropertyName)
			{
				case "EntryBackground":
					int[][] states =
					{
						new[] { global::Android.Resource.Attribute.StateEnabled }, // enabled
						new[] { global::Android.Resource.Attribute.StateEnabled }
						, // disabled
						new[] { global::Android.Resource.Attribute.StateChecked }
						, // unchecked
						new[] { global::Android.Resource.Attribute.StatePressed } // pressed
					};

					var checkBoxColor = Element.TextColor.ToAndroid();

					// Using ColorStateList to change the border color of the checkbox
					int[] colors =
					{
						checkBoxColor, checkBoxColor, checkBoxColor, checkBoxColor
					};
					Control.ButtonTintList = new ColorStateList(states, colors);
					break;
				case "Checked":
					Control.Text = Element.Text;
					Control.Checked = Element.Checked;
					break;
				case "IsEnabled":
					Control.Enabled = Element.IsEnabled;
					UpdateTextColor();
					break;
				case "DefaultTextColor":
				case "CheckedTextColor":
				case "UnCheckedTextColor":
				case "TextColor":
					UpdateTextColor();
					break;
				case "FontName":
					if (!string.IsNullOrEmpty(Element.FontName)) Control.Typeface = TrySetFont(Element.FontName);
					break;
				case "FontSize":
					if (Element.FontSize > 0) Control.TextSize = (float)Element.FontSize;
					break;
				case "CheckedText":
				case "UncheckedText":
				case "DefaultText":
				case "Text":
					Control.Text = Element.Text;
					break;
				case "IsVisible":
					Control.Visibility = Element.IsVisible ? ViewStates.Visible : ViewStates.Gone;
					break;
				case "Width":
				case "Height":
				case "Tag":
				case "Renderer":
				case "X":
				case "Y":
					break;
				default:
					System.Diagnostics.Debug.WriteLine($"CheckboxNew property change for {e.PropertyName} has not been implemented.");
					break;
			}
		}

		/// <summary>
		///     CheckBoxes the checked change.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="CompoundButton.CheckedChangeEventArgs" /> instance containing the event data.</param>
		private void CheckBoxCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e) { Element.Checked = e.IsChecked; }

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

		/// <summary>
		///     Updates the color of the text
		/// </summary>
		private void UpdateTextColor()
		{
			if (Control == null || Element == null)
				return;

			if (Element.TextColor == Color.Default)
				Control.SetTextColor(_defaultTextColor);
			else
				Control.SetTextColor(Element.TextColor.MultiplyAlpha(Element.IsEnabled ? 1 : 0.5).ToAndroid());
			SetButtonTint();
		}

		private void SetButtonTint()
		{
			int[][] states =
			{
				new[] { global::Android.Resource.Attribute.StateEnabled }, // enabled
				new[] { global::Android.Resource.Attribute.StateEnabled }
				, // disabled
				new[] { global::Android.Resource.Attribute.StateChecked }
				, // unchecked
				new[] { global::Android.Resource.Attribute.StatePressed } // pressed
			};

			var checkBoxColor = Control.CurrentTextColor;

			// Using ColorStateList to change the border color of the checkbox
			int[] colors =
			{
				checkBoxColor, checkBoxColor, checkBoxColor, checkBoxColor
			};
			Control.ButtonTintList = new ColorStateList(states, colors);
		}
	}
}