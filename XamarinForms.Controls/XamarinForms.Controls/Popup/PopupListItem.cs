namespace XamarinForms.Controls.Popup
{
	public class PopupListItem
	{
		public string Name { get; }
		public bool Selected { get; set; }
		public object Tag { get; set; }

		public PopupListItem(string name, bool selected = false)
		{
			Name = name;
			Selected = selected;
		}
	}
}