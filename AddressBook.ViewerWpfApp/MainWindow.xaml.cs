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

namespace AddressBook.ViewerWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeList employeeList;
        public MainWindow()
        {
            
            InitializeComponent();
            
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            employeeList = EmployeeList.LoadFromJson(new FileInfo("employees.json"));
            PutItemsIntoComboBox();
        }

        private void PutItemsIntoComboBox()
        {
            foreach (var VARIABLE in employeeList.GetPositions().Distinct())
            {
                FunkciaComboBox.Items.Add(VARIABLE);
            }

            foreach (var VARIABLE in employeeList.GetMainWorkplaces().Distinct())
            {
                PracoviskoComboBox.Items.Add(VARIABLE);
            }
        }

    }
}