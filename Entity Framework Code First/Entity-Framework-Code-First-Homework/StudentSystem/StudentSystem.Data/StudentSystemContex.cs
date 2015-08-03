namespace StudentSystem.Data
{
    using System.Data.Entity;
    using Models;

    public class StudentSystemContex : DbContext
    {
        public StudentSystemContex()
            : base("name=StudentSystemContex")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany<Course>(s => s.Courses)
                .WithMany(c => c.Students)
                .Map(cs =>
                {
                    cs.MapLeftKey("StudentId");
                    cs.MapRightKey("CourseId");
                    cs.ToTable("StudentsCourses");
                });
        }

        public IDbSet<Student> Students { get; set; }
        
        public IDbSet<Course> Courses { get; set; }
        
        public IDbSet<Resource> Resources { get; set; }
        
        public IDbSet<Homework> Homeworks { get; set; }
    }
}