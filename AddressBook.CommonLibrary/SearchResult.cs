using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.CommonLibrary
{
    public class SearchResult
    {
       public Employee[] Employees { get; private set; }

       public SearchResult(Employee[] employees)
       {
           Employees = employees;
       }

       
       public void SaveToCsv(FileInfo csvFile, string delimeter = "\t")
       {
           StringBuilder sb = new StringBuilder();
           String[] headers = new string[] { "Name", "MainWorkplace","Workplace", "Room", "Phone", "Email", "Position" };
           sb.AppendLine(String.Join(delimeter, headers));
           foreach (var employee in Employees)
           {
                String[] newline = new string[] { employee.Name, employee.MainWorkplace, employee.Workplace, employee.Room, employee.Phone, employee.Email, employee.Position };
                sb.AppendLine(String.Join(delimeter, newline));
           }

           
           File.AppendAllText(csvFile.FullName, sb.ToString());
           
           
       }
    }
}
