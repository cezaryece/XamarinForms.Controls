using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinForms.Controls.Clases;
using XamarinForms.Controls.Data;

namespace XamarinForms.Controls.Basic
{
	/// <summary>
	///     Interaction logic for TwoCombosControl.xaml
	/// </summary>

	// ReSharper disable once RedundantExtendsListEntry
	public partial class TwoCombosControl : ContentView, IDisposable
	{
		public TwoCombosViewModel ViewModel => (TwoCombosViewModel)BindingContext;

		public TwoCombosControl()
		{
			InitializeComponent();
			Resources = Application.Current.Resources;
			ViewModel.PropertyChanged += OnPropertyChanged;
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(ViewModel.Combo1Data):
					SetCombo(C1Combo, ViewModel.Combo1Data);
					break;
				case nameof(ViewModel.Combo2Data):
					SetCombo(C2Combo, ViewModel.Combo2Data);
					break;
			}
		}

		private void SetCombo(ComboBoxControl3Rows comboItem, ComboControlData data)
		{
			comboItem.Items = data.Items;
			comboItem.TopLabelText = data.LabelTop;
			comboItem.BottomLabelText = data.LabelBottom;
			comboItem.IsEnabled = IsEnabled;

			//comboItem.SelectedIndex = data.Index;
		}

		public void Dispose() { ViewModel.PropertyChanged -= OnPropertyChanged; }
	}
}