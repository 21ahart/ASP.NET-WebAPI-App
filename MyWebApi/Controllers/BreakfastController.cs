using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BreakfastFoodsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public BreakfastFoodsController(ApplicationDbContext db) => _db = db;

    [HttpPost]
    public async Task<ActionResult<BreakfastFood>> Create([FromBody] BreakfastFood food)
    {
        _db.BreakfastFoods.Add(food);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Read), new { id = food.Id }, food);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BreakfastFood>>> Read([FromQuery] int? id)
    {
        if (id is null || id == 0)
            return await _db.BreakfastFoods.Take(5).ToListAsync();

        var entity = await _db.BreakfastFoods.FindAsync(id.Value);
        if (entity is null) return NotFound();
        return Ok(new[] { entity });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] BreakfastFood food)
    {
        if (id != food.Id) return BadRequest("Route id and body id must match.");
        var exists = await _db.BreakfastFoods.AnyAsync(x => x.Id == id);
        if (!exists) return NotFound();

        _db.Entry(food).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await _db.BreakfastFoods.FindAsync(id);
        if (entity is null) return NotFound();

        _db.BreakfastFoods.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
