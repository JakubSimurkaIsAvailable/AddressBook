using System.CommandLine;
using AddressBook.CommonLibrary;

namespace AddressBook.ViewerConsoleApp
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="position"></param>
        /// <param name="mainWorkplace"></param>
        /// <param name="name"></param>
        static void Main(string input = "", string output = "", string position = "",
            string mainWorkplace = "", string name = "")
        {
            //name = "bach";
            var inputFile = new FileInfo(input);
            


            //output = new FileInfo("output.csv");
            if (input != "")
            {
                EmployeeList? employeeList = EmployeeList.LoadFromJson(new FileInfo(inputFile.FullName));
                if (employeeList != null)
                {
                    SearchResult searchResult = employeeList.Search(mainWorkplace, position, name);

                    if (output != "")
                    {
                        SaveToCsv(searchResult, output);
                    }


                    for (int i = 0; i < searchResult.Employees.Length; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {searchResult.Employees[i].Name}");
                        Console.WriteLine($"Pracovisko: {searchResult.Employees[i].Workplace}");
                        Console.WriteLine($"Miestnost: {searchResult.Employees[i].Room}");
                        Console.WriteLine($"Telefon: {searchResult.Employees[i].Phone}");
                        Console.WriteLine($"Email: {searchResult.Employees[i].Email}");
                        Console.WriteLine($"Funkcia: {searchResult.Employees[i].Position}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    throw new ArgumentException("Input file is not empty");
                }
            }
            else
            {
                throw new ArgumentException("Input file is required");
            }
            


        }

        static void SaveToCsv(SearchResult searchResult, string output)
        {
            var outputFile = new FileInfo(output);
            searchResult.SaveToCsv(outputFile);
        }
    }
}
