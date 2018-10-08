using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SortingTest
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CultureInfo[] AllCultures { get; }

        private CultureInfo _selectedCulture;

        public CultureInfo SelectedCulture
        {
            get { return _selectedCulture; }
            set
            {
                _selectedCulture = value;
                Thread.CurrentThread.CurrentCulture = value;
                SortThem();
            }
        }

        public string[] ComparerNames { get; } =
        {
            "CurrentCulture",
            "CurrentCultureIgnoreCase",
            "InvariantCulture",
            "InvariantCultureIgnoreCase",
            "Ordinal",
            "OrdinalIgnoreCase"
        };

        private string _selectedComparerName;
        public string SelectedComparerName
        {
            get { return _selectedComparerName; }
            set
            {
                _selectedComparerName = value;
                SortThem();
            }
        }

        private string _inputText;
        public string InputText
        {
            get { return _inputText; }
            set { _inputText = value; SortThem(); }
        }

        private string _outputText;
        public string OutputText
        {
            get { return _outputText; }
            set { _outputText = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            AllCultures = GetAllCultures().OrderBy(c => c.DisplayName).ToArray();
            SelectedCulture = Thread.CurrentThread.CurrentCulture;
            SelectedComparerName = ComparerNames.FirstOrDefault();
        }

        private static IEnumerable<CultureInfo> GetAllCultures()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures);
        }

        private void SortThem()
        {
            if (InputText == null) return;

            var comparer = GetComparer();
            if (comparer == null)
            {
                OutputText = "";
                return;
            }

            var lines = InputText.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            var sortedLines = String.Join("\r\n", lines.OrderBy(s => s, comparer));
            OutputText = sortedLines;
        }

        private IComparer<string> GetComparer()
        {
            if (SelectedComparerName == null) return null;
            var prop = typeof(StringComparer).GetProperty(SelectedComparerName, BindingFlags.Public | BindingFlags.Static);
            if (prop == null) return null;
            var comparer = (IComparer<string>)prop.GetValue(null);
            return comparer;
        }

    }
}
