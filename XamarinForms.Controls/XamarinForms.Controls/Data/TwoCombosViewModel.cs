using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using XamarinForms.Controls.Clases;

namespace XamarinForms.Controls.Data
{
	/// <summary>
	///     This class contains properties that a View can data bind to.
	///     <para>
	///         See http://www.galasoft.ch/mvvm
	///     </para>
	/// </summary>
	public class TwoCombosViewModel : INotifyPropertyChanged
	{
		private ComboControlData _combo1Data;

		public ComboControlData Combo1Data
		{
			get => _combo1Data;
			private set
			{
				_combo1Data = value;
				OnPropertyChanged();
			}
		}

		private ComboControlData _combo2Data;

		public ComboControlData Combo2Data
		{
			get => _combo2Data;
			private set
			{
				_combo2Data = value;
				OnPropertyChanged();
			}
		}

		private int _combo1Index;

		public int Combo1Index
		{
			get => _combo1Index;
			set
			{
				//if (_combo1Index == value) return;
				_combo1Index = value;
				OnPropertyChanged();
			}
		}

		private int _combo2Index;

		public int Combo2Index
		{
			get => _combo2Index;
			set
			{
				//if (_combo2Index == value) return;
				_combo2Index = value;
				OnPropertyChanged();
			}
		}

		public TwoCombosViewModel() { _combo1Index = _combo2Index = -1; }

		/// <summary>
		///     Initializes a new instance of the TwoCombosViewModel class.
		/// </summary>
		public void SetCombosData(ComboControlData combo1Data, ComboControlData combo2Data)
		{
			if (combo1Data == null) throw new ArgumentNullException(nameof(combo1Data));
			if (combo2Data == null) throw new ArgumentNullException(nameof(combo2Data));
			Combo1Data = combo1Data;
			Combo2Data = combo2Data;
			Combo1Index = combo1Data.Index;
			Combo2Index = combo2Data.Index;
		}

		public int[] GetCombosValues() { return new[] { Combo1Index, Combo2Index }; }

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
	}
}