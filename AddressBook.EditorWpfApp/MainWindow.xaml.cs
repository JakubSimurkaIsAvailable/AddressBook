using System.Diagnostics;
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
using AddressBook.CommonLibrary;
using AddressBook.WpfApp;

namespace AddressBook.EditorWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeList el;
        private EmployeeList elnew;

        public EmployeeList El
        {
            get => el;
            set => el = value;
        }
        public Employee? SelectedEmployee { get; set; }
        public int SelectedIndex { get; set; }
        public int MemoryIndex { get; set; }
        private bool dontSelect = false;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            el?.Clear();
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "employees";
            dialog.DefaultExt = ".json";
            dialog.Filter = "JSON files (*.json)|*.json";

            bool? result = dialog.ShowDialog();

            El = EmployeeList.LoadFromJson(new FileInfo(dialog.FileName))!;
            elnew = El;
            foreach (var item in El)
            {
                EmployeesListView.Items.Add(item);
            }

        }

        private void NewClick(object sender, RoutedEventArgs e)
        {
            
                string messageBoxText = "Adresár bol zmenený. Chcete ho uložiť?";
                string caption = "Uložiť adresár";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "employees";
            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV files (*.csv)|*.csv";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                elnew.SaveToJson(new FileInfo(dialog.FileName));
            }
        }

        private void EndClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            new Window1(ref elnew, -1).ShowDialog();
            El = elnew;
            
            
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            SelectedIndex = EmployeesListView.SelectedIndex;
            
            SelectedEmployee = (Employee)EmployeesListView.SelectedItem;
            new Window1(ref elnew, SelectedIndex).ShowDialog();
            El = elnew;


        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            elnew.RemoveAt(SelectedIndex);
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
        
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAddressBook eab = new EmployeeAddressBook();
            eab.El = elnew;
            eab.HideButton();
            eab.ShowDialog();
        }

        private void EmployeesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!dontSelect)
            {
                if (EmployeesListView.SelectedItem != null)
                {
                    EditButton.IsEnabled = true;
                    RemoveButton.IsEnabled = true;
                    EditClickButton.IsEnabled = true;
                    RemoveClickButton.IsEnabled = true;
                    Trace.WriteLine("here");
                }
                else
                {
                    EditButton.IsEnabled = false;
                    RemoveButton.IsEnabled = false;
                    EditClickButton.IsEnabled = false;
                    RemoveClickButton.IsEnabled = false;
                    dontSelect = true;
                    EmployeesListView.UnselectAll();
                }
            }
            else
            {
                dontSelect = false;
            }
            
        }
    }
}