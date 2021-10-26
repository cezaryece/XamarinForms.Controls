using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using AT.XamarinControls.Annotations;
using AT.XamarinControls.Clases;
using TestLib.Annotations;
using Xamarin.Forms;

namespace TestLib
{
	public class TestLibViewModel : INotifyPropertyChanged
	{
		private decimal _value;
		private string _title;
		private string _title2;

		public decimal DecimalValue
		{
			get{ return _value;}
			set
			{
				_value = value;
				OnPropertyChanged();
				//Debug.WriteLine(value);
			}
		}
		public int DecimalPlace => 1;
		public string Units => "s";
		public decimal Min => -9999m;
		public decimal Max => 9999m;

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				OnPropertyChanged();
			}
		}

		public string Title2
		{
			get { return _title2; }
			set
			{
				_title2 = value;
				OnPropertyChanged();
			}
		}

		private List<CheckBoxItem> _eventsItems;
		public List<CheckBoxItem> EventsItems { get { return _eventsItems; } set { _eventsItems = value; OnPropertyChanged();} }

		public void SetEventsItems()
		{
			var eventsItems = new List<CheckBoxItem>();
			var selected = new List<string>();
			for (int i = 0; i < ListItems.Count; i++)
			{
				var newCheckBoxItem = new CheckBoxItem($"{ListItems[i].Name} {ListItems[i].Time}", ListItems[i]);
				eventsItems.Add(newCheckBoxItem);
				if (Math.Abs(Math.IEEERemainder(i,2)) < 0.01) selected.Add(ListItems[i].Name);

				//ListItems[i].PropertyChanged += (sender, args) =>
				//{
				//	var listitem = sender as DateTimeItem;

				//	var checkBoxItem = _eventsItems.FirstOrDefault(ei => ((DateTimeItem) ei.TagObject).Name == listitem.Name);
				//	if (checkBoxItem == null) return;
				//	//if (args.PropertyName == nameof(DateTimeItem.InCorrectState))
				//	//{
				//	//	checkBoxItem.IsChecked = listitem.InCorrectState;
				//	//}
				//	//else if (args.PropertyName == nameof(DateTimeItem.Time))
				//	//{
				//	//	checkBoxItem.KeyString = $"item {listitem.Time}";
				//	//}
				//};
			}
			EventsItems = eventsItems;
		}

		private List<object> _selectedEvents;
		public List<object> SelectedEvents
		{
			get { return _selectedEvents; }
			set
			{
				_selectedEvents = value;
				OnPropertyChanged();
			}
		}

		public decimal Increment => 100m;
		public List<string> ComboItems { get { return new List<string> {"pierwszy", "drugi", "trzeci", "czwarty", "piąty"}; } }

		private string _selected;
		public string Selected
		{
			get { return _selected; }
			set
			{
				_selected = value;
				OnPropertyChanged();
			}
		}

		private bool _enabled;
		public bool Enabled
		{
			get { return _enabled; }
			set
			{
				_enabled = value;
				OnPropertyChanged();
			}
		}

		private Dictionary<string, ContentView> _tabedViews;
		public Dictionary<string, ContentView> TabedViews { get { return _tabedViews; } set { _tabedViews = value; OnPropertyChanged();} }

		private List<CheckBoxItem> _checkItems;
		public List<CheckBoxItem> CheckItems
		{
			get { return _checkItems; }
			set
			{
				_checkItems = value;
				OnPropertyChanged();
				SelectedCheckItems = new List<object>();
			}
		}

		private List<object> _selectedCheckItems;
		public List<object> SelectedCheckItems
		{
			get { return _selectedCheckItems; }
			set
			{
				_selectedCheckItems = value;
				OnPropertyChanged();
				Debug.WriteLine("Selected " + _selectedCheckItems.Count);
			}
		}

		private List<DateTimeItem> _listItems;
		public List<DateTimeItem> ListItems
		{
			get { return _listItems; }
			set { _listItems = value; OnPropertyChanged(); }
		}

		private string _codeText;
		public string CodeText { get { return _codeText; } set { _codeText = value; OnPropertyChanged(nameof(CodeText));} }

		private string _codeType;
		public string CodeType { get { return _codeType; } set { _codeType = value; OnPropertyChanged(nameof(CodeType)); } }


		public TestLibViewModel()
		{
			Title = "title";
			Title2 = "title2";
			DecimalValue = 300;
			Selected = "drugi";
			Enabled = true;
			CheckItems = ComboItems.Select(cb => new CheckBoxItem(cb, cb)).ToList();
			CodeText = "skanuj...";

			TabedViews = new Dictionary<string, ContentView>
			{
				{"pierwszy", new ContentView { Content = new Label { Text = "label1"}} },
				{"drugi", new ContentView { Content = new Label { Text = "label2"}} },
				{"trzeci", new ContentView { Content = new Label { Text = "label3"}} },
				{"czwarty", new ContentView { Content = new Label { Text = "label4"}} },
				{"piąty", new ContentView { Content = new Label { Text = "label5"}} },
				{"szósty", new ContentView { Content = new Label { Text = "label6"}} },
				{"7", new ContentView { Content = new Label { Text = "7"}} },
				{"8", new ContentView { Content = new Label { Text = "8"}} },
				{"9", new ContentView { Content = new Label { Text = "9"}} },
				{"10", new ContentView { Content = new Label { Text = "10"}} },
				{"11", new ContentView { Content = new Label { Text = "11"}} },
			};
			//SelectedEvents = new List<object> { EventsItems[0], EventsItems[2]};

			ListItems = new List<DateTimeItem>
			{
				new DateTimeItem(3, "item1"),
				new DateTimeItem(5, "item2"),
				new DateTimeItem(7, "item3")
			};
			SetEventsItems();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
	}

	public class DateTimeItem : INotifyPropertyChanged
	{
		private readonly int _interval;
		private DateTime _time;
		public DateTime Time
		{
			get { return _time; }
			set
			{
				_time = value;
				OnPropertyChanged();
				InCorrectState = (_time.Second % _interval == 0);
			}
		}

		private bool _inCorrectState;
		public bool InCorrectState
		{
			get { return _inCorrectState; }
			set
			{
				if (_inCorrectState == value) return;
				_inCorrectState = value;
				OnPropertyChanged();
				BackGround = _inCorrectState ? Color.Green : Color.Maroon;
				TextColor = _inCorrectState ? Color.Yellow : Color.Silver;
			}
		}

		private Color _backGround;
		public Color BackGround { get { return _backGround; } set { _backGround = value; OnPropertyChanged();} }
		private Color _textColor;
		public Color TextColor { get { return _textColor; } set { _textColor = value; OnPropertyChanged(); } }

		private string _name;
		public string Name { get { return _name; } set { _name = value; OnPropertyChanged();} }

		public DateTimeItem(int interval, string name = null)
		{
			_interval = interval;
			_name = name;
			Device.StartTimer(new TimeSpan(0,0,1), SetTime);
		}

		private bool SetTime() { Time = DateTime.Now; return true; }

		public event PropertyChangedEventHandler PropertyChanged;
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
	}
}
