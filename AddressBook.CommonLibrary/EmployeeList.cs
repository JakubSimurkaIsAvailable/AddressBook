﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AddressBook.CommonLibrary
{
    public class EmployeeList : ObservableCollection<Employee>
    {
        private static EmployeeList _employeeList = new EmployeeList();
        //private static EmployeeList? _employeeList;
        public static EmployeeList? LoadFromJson(FileInfo jsonFile)
        {
            string text = File.ReadAllText(jsonFile.FullName);
            var employees = JsonSerializer.Deserialize<List<Employee>>(text);
            if (employees != null)
            {
                foreach (var employee in employees)
                {
                    _employeeList.Add(employee);
                }
            }
            else
            {
                return null;
            }
            return _employeeList;
        }

        public void SaveToJson(FileInfo jsonFile)
        {
            var jsonString = JsonSerializer.Serialize(_employeeList);
            File.WriteAllText(jsonFile.FullName, jsonString);
        }

        public IEnumerable<string> GetPositions()
        {
            IEnumerable<string> query = _employeeList.Select(e => e.Position).OrderBy(p => p);
            return query;
        }

        public IEnumerable<string> GetMainWorkplaces()
        {
            IEnumerable<string> query = _employeeList.Select(e => e.MainWorkplace).OrderBy(p => p)!;
            return query;
        }

        public SearchResult Search(string? mainWorkplace = null, string? position = null, string? name = null)
        {
            Employee[] employees = _employeeList.Where(e =>
                (mainWorkplace == null || e.MainWorkplace.ToLower().Contains(mainWorkplace.ToLower())) && 
                (position == null || e.Position.ToLower().Contains(position.ToLower())) && 
                (name == null || e.Name.ToLower().Contains(name.ToLower()))).ToArray();
            return new SearchResult(employees);
        }
    }
}
