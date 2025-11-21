using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniWebApi.Data;
using UniWebApi.Models;

namespace UniWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamMembersController : ControllerBase
{
    private readonly AppDbContext _db;
    public TeamMembersController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<ActionResult<TeamMember>> Create([FromBody] TeamMember member)
    {
        _db.TeamMembers.Add(member);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Read), new { id = member.Id }, member);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamMember>>> Read([FromQuery] int? id)
    {
        if (id is null || id == 0)
            return await _db.TeamMembers.Take(5).ToListAsync();

        var entity = await _db.TeamMembers.FindAsync(id.Value);
        if (entity is null) return NotFound();
        return Ok(new[] { entity });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] TeamMember member)
    {
        if (id != member.Id) return BadRequest("Route id and body id must match.");
        var exists = await _db.TeamMembers.AnyAsync(x => x.Id == id);
        if (!exists) return NotFound();

        _db.Entry(member).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await _db.TeamMembers.FindAsync(id);
        if (entity is null) return NotFound();

        _db.TeamMembers.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
