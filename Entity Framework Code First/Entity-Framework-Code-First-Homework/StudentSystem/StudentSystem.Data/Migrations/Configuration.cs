namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "StudentSystem.Data.StudentSystemContex";
        }

        protected override void Seed(StudentSystemContex context)
        {
            if (!context.Students.Any())
            {
                AddResourcesToDatabase(context);

                AddStudentsToDatabase(context);

                AddCoursesToDatabase(context);

                AddHomeworksToDatabase(context);

                AddCoursesHomeworks(context);

                AddCoursesResources(context);

                AddStudentsCourses(context);
            }
        }

        private static void AddHomeworksCourses(StudentSystemContex context)
        {
            Random random = new Random();
            int coursesCount = context.Courses.Count();

            foreach (var homework in context.Homeworks.Local)
            {
                int courseId = random.Next(1, coursesCount);

                var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                homework.Course = course;
            }
          
            context.SaveChanges();
        }

        private static void AddHomeworksStudents(StudentSystemContex context)
        {
            Random random = new Random();
            int studentsCount = context.Students.Count();

            foreach (var homework in context.Homeworks.Local)
            {
                int studentId = random.Next(1, studentsCount);

                var student = context.Students.FirstOrDefault(s => s.Id == studentId);
                
                homework.Student = student;
            }

            context.SaveChanges();
        }

        private static void AddCoursesResources(StudentSystemContex context)
        {
            Random random = new Random();

            for (int i = 1; i < 21; i++)
            {
                int resourceId = random.Next(1, 7);
                int courseId = random.Next(1, 16);

                var resource = context.Resources.FirstOrDefault(r => r.Id == resourceId);
                var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                resource.Course = course;
            }

            context.SaveChanges();
        }

        private static void AddCoursesHomeworks(StudentSystemContex context)
        {
            Random random = new Random();

            for (int i = 1; i < 21; i++)
            {
                int homeworkId = random.Next(1, 9);
                int courseId = random.Next(1, 16);

                var homework = context.Homeworks.FirstOrDefault(h => h.Id == homeworkId);
                var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                homework.Course = course;
            }

            context.SaveChanges();
        }

        private static void AddStudentsCourses(StudentSystemContex context)
        {
            Random random = new Random();

            for (int i = 1; i < 31; i++)
            {
                int studentId = random.Next(1, 17);
                int courseId = random.Next(1, 16);

                var student = context.Students.FirstOrDefault(s => s.Id == studentId);
                var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                student.Courses.Add(course);
                course.Students.Add(student);
            }

            context.SaveChanges();
        }

        private static void AddStudentsToDatabase(StudentSystemContex context)
        {
            using (var reader = new StreamReader("students.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();

                while (line != null)
                {
                    var data = line.Split(new[] { '-' }, 4);
                    var name = data[0];
                    var phoneNumber = data[1];
                    var registrationDate = DateTime.ParseExact(data[2], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var birthDay = DateTime.ParseExact(data[3], "d/M/yyyy", CultureInfo.InvariantCulture);

                    context.Students.Add(new Student()
                    {
                        Name = name,
                        PhoneNumber = phoneNumber,
                        RegistrationDate = registrationDate,
                        BirthDay = birthDay,
                    });

                    line = reader.ReadLine();
                }

                context.SaveChanges();
            }
        }

        private static void AddCoursesToDatabase(StudentSystemContex context)
        {
            using (var reader = new StreamReader("courses.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();

                while (line != null)
                {
                    var data = line.Split(new[] { '-' }, 5);
                    var name = data[0];
                    var description = data[1];
                    var endDate = DateTime.ParseExact(data[2], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var startDate = DateTime.ParseExact(data[3], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var price = decimal.Parse(data[4]);

                    context.Courses.Add(new Course()
                    {
                        Name = name,
                        Description = description,
                        StartDate = startDate,
                        EndDate = endDate,
                        Price = price
                    });

                    line = reader.ReadLine();
                }

                context.SaveChanges();
            }
        }

        private static void AddHomeworksToDatabase(StudentSystemContex context)
        {
            using (var reader = new StreamReader("homeworks.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();

                while (line != null)
                {
                    var data = line.Split(new[] { '-' }, 3);
                    var content = data[0];
                    var contentType = (ContentType)int.Parse(data[1]);
                    var submissionDate = DateTime.ParseExact(data[2], "d/M/yyyy", CultureInfo.InvariantCulture);

                    context.Homeworks.Add(new Homework()
                    {
                        Content = content,
                        ContentType = contentType,
                        SubmissionDate = submissionDate
                    });

                    line = reader.ReadLine();
                }

                AddHomeworksStudents(context);
                AddHomeworksCourses(context);

                context.SaveChanges();
            }
        }

        private static void AddResourcesToDatabase(StudentSystemContex context)
        {
            using (var reader = new StreamReader("resources.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();

                while (line != null)
                {
                    var data = line.Split(new[] { '-' }, 3);
                    var name = data[0];
                    var resourceType = (ResourceType)int.Parse(data[1]);
                    var url = data[2];

                    context.Resources.Add(new Resource()
                    {
                        Name = name,
                        ResourceType = resourceType,
                        Url = url
                    });

                    line = reader.ReadLine();
                }

                context.SaveChanges();
            }
        }
    }
}
