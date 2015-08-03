namespace NativeSQLQuery
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using SoftUniCRUD;

    public class NativeSQLQuery
    {
        public static void Main()
        {
            //Problem 4.	Native SQL Query
            var contex = new SoftUniEntities();

            var sw = new Stopwatch();
            sw.Start();
            PrintNamesWithNativeQuesry(contex);
            Console.WriteLine("Native: {0}", sw.Elapsed);

            sw.Restart();

            PrintNamesWithLinqQuery(contex);
            Console.WriteLine("Linq: {0}", sw.Elapsed);
        }

        private static void PrintNamesWithLinqQuery(SoftUniEntities contex)
        {
            var employees = contex.Employees
                .Where(e => e.Projects.Any(p => p.StartDate.Year == 2002))
                .Select(e => e.FirstName);
        }

        private static void PrintNamesWithNativeQuesry(SoftUniEntities contex)
        {
            var employees = contex.Database.SqlQuery<string>
                                    ("SELECT e.FirstName" +
                                    "FROM Employees e" +
                                        "JOIN EmployeesProjects ep ON ep.EmployeeID = e.EmployeeID" +
                                        "JOIN Projects p ON ep.ProjectID = p.ProjectID" +
                                    "WHERE YEAR(p.StartDate) = 2002" +
                                    "GROUP BY e.FirstName, p.StartDate");
        }
    }
}
