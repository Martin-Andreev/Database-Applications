namespace ConcurrentDatabaseChanges
{
    using System.Linq;
    using SoftUniCRUD;

    public class ConcurrentDatabaseChanges 
    {
        public static void Main()
        {
            var contex1 = new SoftUniEntities();
            var contex2 = new SoftUniEntities();

            var employee1 = contex1.Employees.FirstOrDefault(e => e.EmployeeID == 5);
            var employee2 = contex2.Employees.FirstOrDefault(e => e.EmployeeID == 5);

            employee1.FirstName = "B1";
            employee2.FirstName = "B2";

            contex1.SaveChanges();
            contex2.SaveChanges();
        }
    }
}
