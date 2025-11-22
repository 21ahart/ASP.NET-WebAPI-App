using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public BooksController(ApplicationDbContext db) => _db = db;

    [HttpPost]
    public async Task<ActionResult<Book>> Create([FromBody] Book book)
    {
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Read), new { id = book.Id }, book);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> Read([FromQuery] int? id)
    {
        if (id is null || id == 0)
            return await _db.Books.Take(5).ToListAsync();

        var entity = await _db.Books.FindAsync(id.Value);
        if (entity is null) return NotFound();
        return Ok(new[] { entity });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] Book book)
    {
        if (id != book.Id) return BadRequest("Route id and body id must match.");
        var exists = await _db.Books.AnyAsync(x => x.Id == id);
        if (!exists) return NotFound();

        _db.Entry(book).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await _db.Books.FindAsync(id);
        if (entity is null) return NotFound();

        _db.Books.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
