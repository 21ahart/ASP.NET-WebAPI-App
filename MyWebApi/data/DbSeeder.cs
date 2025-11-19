using MyWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db, ILogger logger)
    {
        if (await db.Students.AnyAsync()) return;

        var students = new List<Student>
        {
            new Student { StudentName = "Aiden Hartranft", Birthdate = new DateTime(2003, 4, 27), CollegeProgram = "Software Application Dev", YearInProgram = YearInProgram.Senior },
            new Student { StudentName = "Maria Gomez", Birthdate = new DateTime(2004, 2, 12), CollegeProgram = "Business Administration", YearInProgram = YearInProgram.Junior },
        };

        db.Students.AddRange(students);
        await db.SaveChangesAsync();
        logger.LogInformation("Seeded {Count} Students.", students.Count);
    }
}