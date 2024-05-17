using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AddressBook.CommonLibrary;

namespace AddressBook.WpfApp
{
    /// <summary>
    /// Interaction logic for EmployeeAddressBook.xaml
    /// </summary>
    public partial class EmployeeAddressBook : Window
    {
        public SearchResult SearchResult { get; set; }
        public int SearchCount { get; set; }
        //private EmployeeList _el;
        public EmployeeList El { get; set; }
        public EmployeeAddressBook()
        {
            SearchResult = new SearchResult(new Employee[0]);
            El = new EmployeeList();
            SearchCount = 0;
            InitializeComponent();
            CountLabel.Content = SearchCount.ToString();
        }

        public void HideButton()
        {
            OtvoritButton.Visibility = Visibility.Collapsed;
        }

        public void PoziciaComboBox_SelectionLoaded()
        {
            var pos = El.GetPositions().Distinct();
            foreach (var VARIABLE in pos)
            {
                poziciaComboBox.Items.Add(VARIABLE);
            }
        }

        public void PracoviskoComboBox_SelectionLoaded()
        {
            var prac = El.GetMainWorkplaces().Distinct();
            foreach (var VARIABLE in prac)
            {
                pracoviskoComboBox.Items.Add(VARIABLE);
            }
        }

        private void PracoviskoComboBox_Clear()
        {
            pracoviskoComboBox.Items.Clear();
        }

        private void PoziciaComboBox_Clear()
        {
            poziciaComboBox.Items.Clear();
        }

        private void OtvoritButton_Click(object sender, RoutedEventArgs e)
        {
            El?.Clear();
            PracoviskoComboBox_Clear();
            PoziciaComboBox_Clear();
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "employees";
            dialog.DefaultExt = ".json";
            dialog.Filter = "JSON files (*.json)|*.json";

            dialog.ShowDialog();

            El = EmployeeList.LoadFromJson(new FileInfo(dialog.FileName))!;
            //el[0] = el[0] with { Name = "Janko Hrasko" };
            PoziciaComboBox_SelectionLoaded();
            PracoviskoComboBox_SelectionLoaded();
        }

        private void VyhladatButton_Click(object sender, RoutedEventArgs e)
        {
            SearchResult = El.Search(pracoviskoComboBox.Text, poziciaComboBox.Text, menoTextBox.Text);
            SearchResultListView.ItemsSource = SearchResult.Employees;
            ResetSearchCount();


        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            menoTextBox.Clear();
            PoziciaComboBox_Clear();
            PracoviskoComboBox_Clear();
            SearchResultListView.ItemsSource = null;
            SearchCount = 0;
            CountLabel.Content = SearchCount.ToString();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "employees";
            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV files (*.csv)|*.csv";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                SearchResult.SaveToCsv(new FileInfo(dialog.FileName));
            }
        }

        private void ResetSearchCount()
        {
            SearchCount = SearchResult.Employees.Length;
            CountLabel.Content = SearchCount.ToString();
        }
    }
}