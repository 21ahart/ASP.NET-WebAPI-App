using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniWebApi.Data;
using UniWebApi.Models;

namespace UniWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HobbiesController : ControllerBase
{
    private readonly AppDbContext _db;
    public HobbiesController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<ActionResult<Hobby>> Create([FromBody] Hobby hobby)
    {
        _db.Hobbies.Add(hobby);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Read), new { id = hobby.Id }, hobby);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hobby>>> Read([FromQuery] int? id)
    {
        if (id is null || id == 0)
            return await _db.Hobbies.Take(5).ToListAsync();

        var entity = await _db.Hobbies.FindAsync(id.Value);
        if (entity is null) return NotFound();
        return Ok(new[] { entity });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] Hobby hobby)
    {
        if (id != hobby.Id) return BadRequest("Route id and body id must match.");
        var exists = await _db.Hobbies.AnyAsync(x => x.Id == id);
        if (!exists) return NotFound();

        _db.Entry(hobby).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await _db.Hobbies.FindAsync(id);
        if (entity is null) return NotFound();

        _db.Hobbies.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
