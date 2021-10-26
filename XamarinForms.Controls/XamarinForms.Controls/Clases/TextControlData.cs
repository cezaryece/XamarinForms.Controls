using System;

namespace XamarinForms.Controls.Clases
{
	public class TextControlData
	{
		public string LabelTop { get; }
		public string LabelBottom { get; }
		public string Text;

		public TextControlData(string value, string labelTop = null, string labelBottom = null)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));
			LabelBottom = labelBottom ?? string.Empty;
			LabelTop = labelTop ?? string.Empty;
			Text = value;
		}
	}
}