using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route ("api/[controller]")] //route is api/students for now
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public StudentsController(ApplicationDbContext db) => _db = db;

        // GET
        [HttpGet("{id?}")] // ? = can be null
        public async Task<IActionResult> GetStudents(int? id)
        {
            if(!id.HasValue ||  id.Value == 0) //if id is missing or 0, return first 5
            {
                var firstFive = await _db.Students
                    .OrderBy(s => s.Id)
                    .Take(5)
                    .ToListAsync();

                return Ok(firstFive);
            }

            var student = await _db.Students.FindAsync(id.Value);
            if (student == null) //find by key, if not found = 404, else 200 w/ student data
            {
                return NotFound();
            }

            return Ok(student);
        }

    }
}
