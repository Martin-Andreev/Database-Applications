namespace SoftUniDatabaseFirst
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using SoftUniCRUD;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;

    public static class DataAccessObject
    {
        private static SoftUniEntities context = new SoftUniEntities();

        public static void Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public static Employee FindByKey(object key)
        {
            return context.Employees.Find(key);
        }

        public static void Modify(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void Delete(Employee employee)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();


            //var projects = employee.Projects;
            //var departments = employee.Departments;
            //var managedEmployees = employee.Employees1;

            //var managedDepartments = Context.Departments
            //                        .Where(d => d.ManagerID == employee.ManagerID);

            //foreach (var managedDepartment in managedDepartments)
            //{
            //    managedDepartment.ManagerID = 0;
            //}

            //foreach (var managedEmployee in managedEmployees)
            //{
            //    managedEmployee.ManagerID = null;
            //}

            //foreach (var project in projects)
            //{
            //    project.Employees.Remove(employee);
            //}

            //foreach (var department in departments)
            //{
            //    department.Employees1.Remove(employee);
            //}

            //Context.Employees.Remove(employee);
            //Context.SaveChanges();
        }
    }
}
