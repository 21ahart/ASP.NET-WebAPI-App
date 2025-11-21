using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //route is api/students for now
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public StudentsController(ApplicationDbContext db) => _db = db;

        // GET
        [HttpGet("{id?}")] // ? = can be null
        public async Task<IActionResult> GetStudents(int? id)
        {
            if (!id.HasValue || id.Value == 0) //if id is missing or 0, return first 5
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

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var wasRemoved = await _db.RemoveStudentById(id); //await our method

            if (wasRemoved == null) //if method returns null = no matching id
            {
                return NotFound(id); //not found error
            }

            return NoContent(); //else 204 indicates success, but nothing to return = succesful DELETE
        }

        //PUT
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateById(int id, Student UpdatedStudent)
        {
            var wasUpdated = await _db.UpdateStudentById(id, UpdatedStudent); //await our method like above
            if (wasUpdated == null)
            {
                return NotFound(id); //if method returns null = no matching id
            }

            return Ok(UpdatedStudent); //else 200 for successful update
        }

        //POST
        [HttpPost]
        
        public async Task<IActionResult> PostRequest(Student student)
        {
            if (student.Id != 0) return BadRequest(); //do not want id changes at all
            try
            {
                var addResult = await _db.AddStudent(student); //wait for method

                if (addResult == null) //if returns null
                {
                    return StatusCode(500, "Team already exists."); //there is a duplicate
                }
                return Ok(student); //else it is ok = 200
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected internal error."); //something else goes wrong = generic error
            }
           
        }

    }
}
