using System;
using System.Collections.Generic;

namespace XamarinForms.Controls.Clases
{
	public class ComboControlData
	{
		public ComboControlData(string name, List<string> items, int index, string labelTop, string labelBottom)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));
			Name = name;
			LabelBottom = labelBottom ?? string.Empty;
			LabelTop = labelTop ?? string.Empty;
			Index = index;
			Items = items ?? new List<string>();
		}

		public string Name { get; }
		public List<string> Items { get; }
		public int Index { get; }
		public string LabelTop { get; }
		public string LabelBottom { get; }
	}
}