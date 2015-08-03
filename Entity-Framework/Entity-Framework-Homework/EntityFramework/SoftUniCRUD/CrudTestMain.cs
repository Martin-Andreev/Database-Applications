namespace SoftUniCRUD
{
    using System.Linq;
    using SoftUniDatabaseFirst;

    public class CrudTestMain
    {
        public static void Main()
        {
            var contex = new SoftUniEntities();

            //Task 1. Inserts an employee
            //Employee employee = new Employee()
            //{
            //    FirstName = "Krali",
            //    LastName = "Marko",
            //    JobTitle = "Chieftain",
            //    DepartmentID = 5,
            //    HireDate = DateTime.Now,
            //    Salary = 10000
            //};

            //DataAccessObject.Add(employee);

            //var employee = DataAccessObject.FindByKey(contex.Employees.Max(e => e.EmployeeID));


            //3. Change the employee parameters and saves it to the database
            //employee.Salary = 1000000;
            //employee.DepartmentID = 3;

            //DataAccessObject.Modify(employee);

            //Task 4. Deletes an employee
            //DataAccessObject.Delete(employee);


            //Problem 3.	Database Search Queries
            /* Task 1. Find all employees who have projects started in the time period 2001 - 2003 (inclusive). Select each employee's 
             * first name, last name and manager name and each of their projects' name, start date, end date. */

            //var employees =
            //    contex.Employees
            //        .Where(e => e.Projects.Count(
            //                    p => p.StartDate >= new DateTime(2000, 12, 31) && p.StartDate <= new DateTime(2004, 1, 1)) > 0)
            //        .Select(e => new
            //        {
            //            e.FirstName,
            //            e.LastName,
            //            ManagerName = e.Employee1.FirstName + " " + e.Employee1.LastName,
            //            EmployeeProjects =
            //                e.Projects.Select(p => new
            //                {
            //                    p.Name,
            //                    p.StartDate,
            //                    p.EndDate
            //                })
            //        });

            //foreach (var employee in employees)
            //{
            //    Console.WriteLine("{0} {1} - Manager: {2}", employee.FirstName, employee.LastName, employee.ManagerName);
            //    if (employee.EmployeeProjects.Any())
            //    {
            //        Console.WriteLine("-Projects:");
            //        foreach (var project in employee.EmployeeProjects)
            //        {
            //            Console.WriteLine("{0}{1} Started on: {2}. Ended on: {3}",
            //                new string('-', 2),
            //                project.Name,
            //                project.StartDate,
            //                project.EndDate != null ? project.EndDate.ToString() : "Not finished yet");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("--No Projects--");
            //    }

            //    Console.WriteLine();
            //}

            /*Task 2. Find all addresses, ordered by the number of employees who live there (descending), then by town name 
              (ascending). Take only the first 10 addresses and select their address text, town name and employee count.*/

            //var addresses = contex.Addresses
            //    .OrderByDescending(a => a.Employees.Count)
            //    .ThenBy(a => a.Town.Name)
            //    .Take(10)
            //    .Select(a => new
            //    {
            //        a.AddressText,
            //        TownName = a.Town.Name,
            //        EmployeesCount = a.Employees.Count
            //    });

            //foreach (var address in addresses)
            //{
            //    Console.WriteLine("{0}, {1} - {2} {3}", 
            //        address.AddressText, 
            //        address.TownName, 
            //        address.EmployeesCount,
            //        address.EmployeesCount != 1 ? "employees" : "employee");
            //}


            /*Task 3. Get an employee by id (e.g. 147). Select only his/her first name, last name, job title and projects 
              (only their names). The projects should be ordered by name (ascending).*/

            //var employee = contex.Employees
            //    .Where(e => e.EmployeeID == 147)
            //    .Select(e => new
            //    {
            //        e.FirstName,
            //        e.LastName,
            //        e.JobTitle,
            //        Projects = e.Projects
            //            .OrderBy(p => p.Name)
            //            .Select(p => p.Name)
            //    })
            //    .FirstOrDefault();

            //Console.WriteLine("{0} {1} - {2}{3}--Projects: {4}", 
            //    employee.FirstName, 
            //    employee.LastName,
            //    employee.JobTitle,
            //    Environment.NewLine,
            //    string.Join(", ", employee.Projects));


            /*Task 4. Find all departments with more than 5 employees. Order them by employee count (ascending). 
              Select the department name, manager name and first name, last name, hire date and job title of every employee.*/

            //var departments = contex.Departments
            //    .Where(d => d.Employees.Count > 5)
            //    .OrderBy(d => d.Employees.Count)
            //    .Select(d => new
            //    {
            //        DepartmentName = d.Name,
            //        ManagerName = d.Employee.FirstName + " " + d.Employee.LastName,
            //        Employees = d.Employees
            //            .Select(e => new
            //            {
            //                FirstName = e.FirstName,
            //                LastName = e.LastName,
            //                HireDate = e.HireDate,
            //                JobTitle = e.JobTitle
            //            })
            //    });

            //Console.WriteLine("Departments count: " + departments.Count());
            //foreach (var department in departments)
            //{
            //    Console.WriteLine("--{0} - Manager: {1}, Employees: {2}",
            //        department.DepartmentName,
            //        department.ManagerName,
            //        department.Employees.Count());
            //}
        }
    }
}
