using System.Windows;

namespace SortingTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow()
        {
            DataContext = _vm = new MainViewModel();
            InitializeComponent();
            Loaded += (sender, args) => { InputText.Focus(); };
        }
    }
}
