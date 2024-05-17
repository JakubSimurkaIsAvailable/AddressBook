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
        //private EmployeeList? el;
        private EmployeeList? elnew;

        public EmployeeList El { get => elnew!; set => elnew = value; }
        public Employee? SelectedEmployee { get; set; }
        public int SelectedIndex { get; set; }
        public int MemoryIndex { get; set; }
        public bool ChangeWasMade { get; set; }


        public MainWindow()
        {

            InitializeComponent();
            El = [];
            EmployeesListView.ItemsSource = El;
            ChangeWasMade = false;
            SearchButton.IsEnabled = false;

        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            El.Clear();
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "employees",
                DefaultExt = ".json",
                Filter = "JSON files (*.json)|*.json"
            };


            dialog.ShowDialog();

            El = EmployeeList.LoadFromJson(new FileInfo(dialog.FileName))!;
            EmployeesListView.ItemsSource = El;
            //foreach (var item in El)
            //{
            //   EmployeesListView.Items.Add(item);
            //}
            ResetLabelCount();
        }

        private void NewClick(object sender, RoutedEventArgs e)
        {
            if (ChangeWasMade)
            {
                string messageBoxText = "Adresár bol zmenený. Chcete ho uložiť?";
                string caption = "Uložiť adresár";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SaveClick(sender, e);
                        
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            ResetLabelCount();
            El.Clear();
            SearchButton.IsEnabled = false;
            ResetLabelCount();

        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "employees",
                DefaultExt = ".json",
                Filter = "JSON files (*.json)|*.json"
            };
            var result = dialog.ShowDialog();
            if (result == true)
            {
                El.SaveToJson(new FileInfo(dialog.FileName));
            }
        }

        private void EndClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            
                var w = new Window1();
                w.ShowDialog();
                if (w.SaveChanges)
                {
                    var temployee = new Employee()
                    {
                        Name = w.ZamestnanecTextBox.Text,
                        Position = w.FunkciaTextBox.Text,
                        Phone = w.TelefonTextBox.Text,
                        Email = w.EmailTextBox.Text,
                        Room = w.MiestnostTextBox.Text,
                        MainWorkplace = w.HlavnePracoviskoTextBox.Text,
                        Workplace = w.PracoviskoTextBox.Text
                    };
                    El.Add(temployee);
                    ResetLabelCount();
                    ChangeWasMade = true;
                }
            
            
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            var w = new Window1();
            var selectedItem = (Employee)EmployeesListView.SelectedItem;
            w.ZamestnanecTextBox.Text = selectedItem.Name;
            w.FunkciaTextBox.Text = selectedItem.Position;
            w.TelefonTextBox.Text = selectedItem.Phone;
            w.EmailTextBox.Text = selectedItem.Email;
            w.MiestnostTextBox.Text = selectedItem.Room;
            w.HlavnePracoviskoTextBox.Text = selectedItem.MainWorkplace;
            w.PracoviskoTextBox.Text = selectedItem.Workplace;
            w.ShowDialog();
            if (w.SaveChanges)
            {
                var selectedIndex = EmployeesListView.SelectedIndex;
                EmployeesListView.SelectedItem = null;
                //elnew.RemoveAt(selectedIndex);
                //EmployeesListView.Items.Remove((Employee)EmployeesListView.SelectedItem);
                El[selectedIndex] = new Employee()
                {
                    Name = w.ZamestnanecTextBox.Text,
                    Position = w.FunkciaTextBox.Text,
                    Phone = w.TelefonTextBox.Text,
                    Email = w.EmailTextBox.Text,
                    Room = w.MiestnostTextBox.Text,
                    MainWorkplace = w.HlavnePracoviskoTextBox.Text,
                    Workplace = w.PracoviskoTextBox.Text
                };

                //Employee temployee = new Employee()
                //{
                //  Name = w.ZamestnanecTextBox.Text,
                //Position = w.FunkciaTextBox.Text,
                //Phone = w.TelefonTextBox.Text,
                //Email = w.EmailTextBox.Text,
                //Room = w.MiestnostTextBox.Text,
                //MainWorkplace = w.HlavnePracoviskoTextBox.Text,
                //Workplace = w.PracoviskoTextBox.Text
                //};
                //EmployeesListView.Items.Add(temployee);
                //elnew.Add(temployee);
                ChangeWasMade = true;
                //dontSelect = true;
                //EmployeesListView.UnselectAll();
            }
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            El.RemoveAt(SelectedIndex);
            ResetLabelCount();
            ChangeWasMade = true;
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            var helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAddressBook eab = new()
            {
                El = El
            };
            eab.HideButton();
            eab.PoziciaComboBox_SelectionLoaded();
            eab.PracoviskoComboBox_SelectionLoaded();
            eab.ShowDialog();

        }

        private void EmployeesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (EmployeesListView.SelectedItem != null)
            {
                EditButton.IsEnabled = true;
                RemoveButton.IsEnabled = true;
                EditClickButton.IsEnabled = true;
                RemoveClickButton.IsEnabled = true;
            }
            else
            {
                EditButton.IsEnabled = false;
                RemoveButton.IsEnabled = false;
                EditClickButton.IsEnabled = false;
                RemoveClickButton.IsEnabled = false;
                EmployeesListView.UnselectAll();
            }
        }

        public void ResetLabelCount()
        {
            CountLabel.Content = El.Count.ToString();
            CheckSearchAvailability();
        }

        public void CheckSearchAvailability()
        {
            if (El.Count == 0)
            {
                SearchButton.IsEnabled = false;
            }
            else
            {
                SearchButton.IsEnabled = true;
            }
        }
    }
}