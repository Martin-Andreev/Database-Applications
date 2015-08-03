using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SofUni_Database_First
{
    public class DatabaseTestMain
    {
        public static void Main()
        {
            var contex = new SoftUniEntities();

            //TASK 2
            //- Employees with Salary Over 50 000

            //var employeeNames = contex.Employees.Where(e => e.Salary > 50000).Select(e => e.FirstName);
            //foreach (var employee in employeeNames)
            //{
            //    Console.WriteLine(employee);
            //}


            //TASK 3
            //Extract all employees from the Research and Development department. Order them by salary (in ascending order), 
            //then by first name (in descending order). Select only their first name, last name, department name and salary.

            //var employees =
            //    contex.Employees
            //        .Where(e => e.Department.Name == "Research and Development")
            //        .OrderBy(e => e.Salary)
            //        .ThenByDescending(e => e.FirstName)
            //        .Select(e => new
            //        {
            //            e.FirstName,
            //            e.LastName,
            //            e.Department.Name,
            //            e.Salary
            //        });

            //foreach (var employee in employees)
            //{
            //    Console.WriteLine(
            //        "{0} {1} from {2} - ${3:F2}",
            //        employee.FirstName, 
            //        employee.LastName, 
            //        employee.Name,
            //        employee.Salary);
            //}


            //TASK 4
            //Adding a New Address and Updating Employee

            //var adresses = new Address()
            //{
            //    AddressText = "Vitoshka 15",
            //    TownID = 4
            //};

            //Employee employee = contex.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            //employee.Address = adresses;
            //contex.SaveChanges();

            //var employeeNakov = contex.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            //Console.WriteLine(employeeNakov.Address.AddressText);


            //TASK 5
            //Deleting Project by Id

            var project = contex.Projects.Find(2);
            var employees = contex.Employees.Where(e => e.Projects.Contains(project));
            foreach (var employee in employees)
            {
                employee.Projects.Remove(project);
            }

            contex.Projects.Remove(project);

            contex.SaveChanges();

        }
    }
}
