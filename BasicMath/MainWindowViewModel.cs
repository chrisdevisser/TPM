using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BasicMath {
	public enum OperationKinds {
		Plus,
		Minus
	}

	internal class MainWindowViewModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		private int _firstNumber;
		public int FirstNumber {
			get {return _firstNumber;}
			set {
				if (_firstNumber != value) {
					_firstNumber = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(Result));
					UpdateOperation();
				}
			}
		}

		private int _secondNumber;
		public int SecondNumber {
			get {return _secondNumber;}
			set {
				if (_secondNumber != value) {
					_secondNumber = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(Result));
					UpdateOperation();
				}
			}
		}

		private OperationKinds _operationKind;
		public OperationKinds OperationKind {
			get {return _operationKind;}
			set {
				if (_operationKind != value) {
					_operationKind = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(Result));
					UpdateOperation();
				}
			}
		}

		public int Result {
			get {
				if (_operationKind == OperationKinds.Plus) {
					return FirstNumber + SecondNumber;
				} else if (_operationKind == OperationKinds.Minus) {
					return FirstNumber - SecondNumber;
				} else {
					return 0;
				}
			}
		}

		private string _operation;
		public string Operation {
			get {
				return _operation;
			}
			set {
                if (_operation == value) {return;}
				_operation = value;

				var formula = value.Split('=')[0];

				if (formula.Contains('+') && AssignOperands(formula.Split('+'))) {
					OperationKind = OperationKinds.Plus;
				} else if (formula.Contains('-') && AssignOperands(formula.Split('-'))) {
					OperationKind = OperationKinds.Minus;
				}

				OnPropertyChanged();
			}
		}

		private bool AssignOperands(string[] operands) {
			if (operands.Length != 2) {return false;}

			if (operands.Any(op => op.Length > 2)) {
				return false;
			}

			var intOperands = new int[2];
			if (!(int.TryParse(operands[0], out intOperands[0]) && int.TryParse(operands[1], out intOperands[1]))) {
				return false;
			}

			FirstNumber = intOperands[0];
			SecondNumber = intOperands[1];
			return true;
		}

		private void UpdateOperation() {
			Operation = $"{FirstNumber}{ToSymbol(OperationKind)}{SecondNumber}={Result}";
		}

		private static string ToSymbol(OperationKinds kind) {
			if (kind == OperationKinds.Plus) {
				return "+";
			} else if (kind == OperationKinds.Minus) {
				return "-";
			} else {
				return "?";
			}
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
