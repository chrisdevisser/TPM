using System;
using System.Globalization;
using System.Windows.Data;

namespace BasicMath {
	[ValueConversion(typeof(int), typeof(string))]
	internal class IntToStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return value.Equals(0) ? string.Empty : value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			int result;
			if (int.TryParse((string)value, out result)) {
				return result;
			} else {
				return 0;
			}
		}
	}
}