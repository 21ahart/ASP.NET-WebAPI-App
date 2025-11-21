using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

namespace MyWebApi.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BreakfastFood> BreakfastFoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Hobby>().ToTable("Hobbies");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<BreakfastFood>().ToTable("BreakfastFoods");
        }

        public async Task<Student?> RemoveStudentById(int id)
        {
            var student = await Students.FindAsync(id); //check for record
            if (student == null) //return null if pk not matching = 404 from our client as method returns null
            {
                return null;
            }

            Students.Remove(student); //mark entity as deleted

            await SaveChangesAsync(); //actually deletes record

            return student; //return removed entity if del was successful != null for the controller
        }

        public async Task<Student?> UpdateStudentById(int id, Student UpdatedStudent) //similar to above method but for put
        {
            var student = await Students.FindAsync(id); //find record

            if (student == null) //if id does not exist
            {
                return null; //return null
            }
            //Note we will not let request update id as that could cause significant issues. Validation is not extensive, but sufficient
            student.StudentName = UpdatedStudent.StudentName;
            student.Birthdate = UpdatedStudent.Birthdate;
            student.CollegeProgram = UpdatedStudent.CollegeProgram;
            student.YearInProgram = UpdatedStudent.YearInProgram;
           
            Students.Update(student); //mark as updated

            await SaveChangesAsync(); //update record

            return UpdatedStudent; //return success to our controller
        }

        public async Task<Student?> AddStudent(Student student)
        {
            //check student name and program are not the same so there are no duplicates (bare bones - no case sensitivity check or security safeguards)
            var studentExists = await Students.FirstOrDefaultAsync(x => x.StudentName.Equals(student.StudentName) && x.CollegeProgram.Equals(student.CollegeProgram));

            if (studentExists != null) //if match - send a null
            {
                return null;
            }
            if (student.Id != 0) //sending a null here just in case, we really don't want id to change
            {
                return null;
            }

             Students.Add(student); //track addition
             await SaveChangesAsync(); //actually add data
             return student;  
        }
    }

}