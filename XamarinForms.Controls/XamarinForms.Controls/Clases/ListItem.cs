namespace XamarinForms.Controls.Clases
{
	public class ListItem<T>
	{
		public T Item { get; protected set; }
		public int Id { get; protected set; }
		public string Name { get; protected set; }
		public string Details { get; protected set; }

		public ListItem(T item, int id, string name = "", string details = "")
		{
			Item = item;
			Id = id;
			Name = name;
			Details = details;
		}
	}
}