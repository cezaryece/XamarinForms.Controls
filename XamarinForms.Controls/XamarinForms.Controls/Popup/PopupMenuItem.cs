using System;

namespace XamarinForms.Controls.Popup
{
	public class PopupMenuItem
	{
		public string Name { get; }
		public Action Command { get; }

		public PopupMenuItem(string name, Action command)
		{
			Name = name;
			Command = command;
		}

		public void Run() { Command?.Invoke(); }
	}
}