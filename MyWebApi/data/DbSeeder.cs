using MyWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db, ILogger logger)
    {
        if (!await db.Students.AnyAsync())
        {
            var students = new List<Student>
            {
                new Student { StudentName = "Aiden Hartranft", Birthdate = new DateTime(2003, 4, 27), CollegeProgram = "Software Application Dev", YearInProgram = YearInProgram.Senior },
                new Student { StudentName = "Sebastian Rodriguez Zapata", Birthdate = new DateTime(1999, 10, 15), CollegeProgram  = "Cybersecurity", YearInProgram = YearInProgram.Senior },
                new Student { StudentName = "Kevon Chatman", Birthdate = new DateTime(2002, 2, 12), CollegeProgram = "Cybersecurity", YearInProgram = YearInProgram.Senior },
            };

            db.Students.AddRange(students);
            await db.SaveChangesAsync();
            logger.LogInformation("Seeded {Count} Students.", students.Count);
        }

        if (!await db.Hobbies.AnyAsync())
        {
            var hobbies = new List<Hobby>
            {
                new Hobby { Name = "Programming", YearsPracticed = 5, SkillLevel = "Advanced", Notes = "Web and mobile development" },
                new Hobby { Name = "Photography", YearsPracticed = 3, SkillLevel = "Intermediate", Notes = "Landscape and portrait" },
                new Hobby { Name = "Guitar", YearsPracticed = 2, SkillLevel = "Beginner", Notes = "Learning acoustic" }
            };

            db.Hobbies.AddRange(hobbies);
            await db.SaveChangesAsync();
            logger.LogInformation("Seeded {Count} Hobbies.", hobbies.Count);
        }

        if (!await db.Books.AnyAsync())
        {
            var books = new List<Book>
            {
                new Book { Title = "Clean Code", Author = "Robert C. Martin", YearPublished = 2008, Genre = "Technology" },
                new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", YearPublished = 1999, Genre = "Technology" },
                new Book { Title = "1984", Author = "George Orwell", YearPublished = 1949, Genre = "Fiction" }
            };

            db.Books.AddRange(books);
            await db.SaveChangesAsync();
            logger.LogInformation("Seeded {Count} Books.", books.Count);
        }

        if (!await db.BreakfastFoods.AnyAsync())
        {
            var breakfastFoods = new List<BreakfastFood>
            {
                new BreakfastFood { Name = "Scrambled Eggs", Calories = 200, IsHighProtein = true, FavoritePairing = "Toast" },
                new BreakfastFood { Name = "Oatmeal", Calories = 150, IsHighProtein = false, FavoritePairing = "Berries" },
                new BreakfastFood { Name = "Greek Yogurt", Calories = 120, IsHighProtein = true, FavoritePairing = "Granola" }
            };

            db.BreakfastFoods.AddRange(breakfastFoods);
            await db.SaveChangesAsync();
            logger.LogInformation("Seeded {Count} BreakfastFoods.", breakfastFoods.Count);
        }
    }
}