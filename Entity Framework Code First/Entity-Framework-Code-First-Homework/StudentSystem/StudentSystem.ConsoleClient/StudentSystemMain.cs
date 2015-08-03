namespace StudentSystem.ConsoleClient
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Data;
    using Data.Migrations;

    public class StudentSystemMain
    {
        public static void Main()
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<StudentSystemContex, Configuration>();
            Database.SetInitializer(migrationStrategy);
            var contex = new StudentSystemContex();

            //ListAllStudents(contex);

            //ListAllCourses(contex);

            //ListCoursesByCount(contex);

            //ListAllCoursesFilteredByDate(contex);

            CheckStudetsAllCoursesPrice(contex);
        }

        private static void CheckStudetsAllCoursesPrice(StudentSystemContex contex)
        {
            /*5.	For each student, calculate the number of courses she’s enrolled in, the total price of these courses and the 
                    average price per course for the student. Select the student name, number of courses, total price and average
                    price. Order the results by total price (descending), then by number of courses (descending) and then by the 
                    student’s name (ascending).*/


            var students = contex.Students
                .Include(s => s.Courses)
                .Where(s => s.Courses.Any())
                .OrderByDescending(s => s.Courses.Sum(c => c.Price))
                .ThenByDescending(s => s.Courses.Count)
                .ThenBy(s => s.Name)
                .Select(s => new
                {
                    StudentName = s.Name,
                    CoursesCount = s.Courses.Count,
                    TotalCoursesPrice = s.Courses.Sum(c => c.Price),
                    AverageCoursesPrice = s.Courses.Average(c => c.Price)
                });

            foreach (var student in students)
            {
                Console.WriteLine("{0} -> Courses count: {1}. Total price: {2:F}. Average price: {3:F}{4}",
                    student.StudentName,
                    student.CoursesCount,
                    student.TotalCoursesPrice,
                    student.AverageCoursesPrice,
                    Environment.NewLine);
            }
        }

        private static void ListAllCoursesFilteredByDate(StudentSystemContex contex)
        {
            /*4.	List all courses which were active on a given date (choose the date depending on the data seeded to ensure there 
                   are results), and for each course count the number of students enrolled. Select the course name, start and end 
                   date, course duration (difference between end and start date) and number of students enrolled. Order the 
                   results by the number of students enrolled (in descending order), then by duration (descending).*/

            DateTime wantedDate = new DateTime(2014, 12, 10);
            var courses = contex.Courses
                .Include(c => c.Students)
                .Where(c => c.StartDate < wantedDate && c.EndDate > wantedDate)
                .OrderByDescending(c => c.Students.Count)
                .ThenByDescending(c => DbFunctions.DiffDays(c.StartDate, c.EndDate))
                .Select(c => new
                {
                    CourseName = c.Name,
                    CourseStartDate = c.StartDate,
                    CourseEndDate = c.EndDate,
                    CourseDuration = DbFunctions.DiffDays(c.StartDate, c.EndDate),
                    StudentsEnrolled = c.Students.Count
                });

            foreach (var course in courses)
            {
                Console.WriteLine(
                    "-{0}:{5} Started on: {1}{5} Ended on: {2}{5} Days duration: {3}{5} Students enrolled: {4}{5}",
                    course.CourseName,
                    course.CourseStartDate.Date,
                    course.CourseEndDate.Date,
                    course.CourseDuration,
                    course.StudentsEnrolled,
                    Environment.NewLine);
            }
        }

        private static void ListCoursesByCount(StudentSystemContex contex)
        {
            /*3.	List all courses with more than 5 resources. Order them by resources count (descending), then by start date 
                   (descending). Select only the course name and the resource count.*/

            var courses = contex.Courses
                .Include(c => c.Resources)
                .Where(c => c.Resources.Count > 0)
                .OrderByDescending(c => c.Resources.Count)
                .ThenBy(c => c.StartDate)
                .Select(s => new
                {
                    CourseName = s.Name,
                    ResourcesCount = s.Resources.Count
                });

            foreach (var course in courses)
            {
                Console.WriteLine("{0} -> {1}{2}", course.CourseName, course.ResourcesCount, Environment.NewLine);
            }
        }

        private static void ListAllCourses(StudentSystemContex contex)
        {
            /*2. List all courses with their corresponding resources. Select the course name and description and everything for 
                each resource. Order the courses by start date (ascending), then by end date (descending).*/

            var courses = contex.Courses
                .Include(c => c.Resources)
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    CourseName = c.Name,
                    CourseDescription = c.Description,
                    Resourses = c.Resources
                });

            foreach (var course in courses)
            {
                Console.WriteLine("-{0} -> {1}{2} {3}:",
                    course.CourseName,
                    course.CourseDescription,
                    Environment.NewLine,
                    "Resourses");
                if (course.Resourses.Any())
                {
                    foreach (var resourse in course.Resourses)
                    {
                        Console.WriteLine(" * {0} (type: {1}). Url: {2}", resourse.Name, resourse.ResourceType, resourse.Url);
                    }
                }
                else
                {
                    Console.WriteLine(" Sorry, no resources for this course.");
                }

                Console.WriteLine();
            }
        }

        private static void ListAllStudents(StudentSystemContex contex)
        {
            //1. Lists all students and their homework submissions. Select only their names and for each homework - 
            //   content and content-type.

            var students = contex.Students
                .Include(s => s.Homeworks)
                .Select(sc => new
                {
                    StudentName = sc.Name,
                    Homeworks = sc.Homeworks.Select(h => new
                    {
                        HomeworkContent = h.Content,
                        HomeworkContentType = h.ContentType
                    })
                });

            foreach (var student in students)
            {
                Console.WriteLine("--{0}. Homeworks:", student.StudentName);
                if (student.Homeworks.Any())
                {
                    foreach (var homework in student.Homeworks)
                    {
                        Console.WriteLine("  {0} -> {1}", homework.HomeworkContent, homework.HomeworkContentType);
                    }
                }
                else
                {
                    Console.WriteLine("  The student has no homeworks.");
                }

                Console.WriteLine();
            }
        }
    }
}
