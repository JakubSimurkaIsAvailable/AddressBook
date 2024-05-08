using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AddressBook.CommonLibrary;

namespace AddressBook.EditorWpfApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public int SelectedIndex { get; set; }
        public EmployeeList EmployeeList { get; set; }
        public Window1(ref EmployeeList el, int index)
        {
            SelectedIndex = index;
            EmployeeList = el;
            InitializeComponent();
            if (SelectedIndex != -1)
            {
                ZamestnanecTextBox.Text = EmployeeList[SelectedIndex].Name;
                FunkciaTextBox.Text = EmployeeList[SelectedIndex].Position;
                TelefonTextBox.Text = EmployeeList[SelectedIndex].Phone;
                EmailTextBox.Text = EmployeeList[SelectedIndex].Email;
                MiestnostTextBox.Text = EmployeeList[SelectedIndex].Room;
                HlavnePracoviskoTextBox.Text = EmployeeList[SelectedIndex].MainWorkplace;
                PracoviskoTextBox.Text = EmployeeList[SelectedIndex].Workplace;
            }
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex == -1)
            {
                EmployeeList.Add(new Employee
                {
                    Name = ZamestnanecTextBox.Text,
                    Position = FunkciaTextBox.Text,
                    Phone = TelefonTextBox.Text,
                    Email = EmailTextBox.Text,
                    Room = MiestnostTextBox.Text,
                    MainWorkplace = HlavnePracoviskoTextBox.Text,
                    Workplace = PracoviskoTextBox.Text
                });
            }
            else
            {

                 EmployeeList[SelectedIndex] = EmployeeList[SelectedIndex] with
                 { 
                     Name = ZamestnanecTextBox.Text,
                   Position = FunkciaTextBox.Text,
                  Phone = TelefonTextBox.Text,
                  Email = EmailTextBox.Text,
                  Room = MiestnostTextBox.Text,
                  MainWorkplace = HlavnePracoviskoTextBox.Text,
                  Workplace = PracoviskoTextBox.Text
                 };
                

            }
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    
}
