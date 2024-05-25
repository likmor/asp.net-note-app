using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Cards.Contracts;
using My_Cards.DataAccess;
using My_Cards.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace My_Cards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext _context;

        public NotesController(NotesDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNoteRequest request)
        {
            var note = new Note(request.Title, request.Description);
            await Console.Out.WriteLineAsync(note.Id.ToString());
            await _context.AddAsync(note);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetNotesRequest request)
        {
            var notes = await _context.Notes
                .OrderBy(n => n.CreationTime)
                .Select(n => new NoteDto(n.Id, n.Title, n.Description, n.CreationTime))
                .ToListAsync();

            //var notes = await _context.Notes.ToListAsync();

            return Ok(new GetNotesResponse(notes));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var notes = await _context.Notes.SingleOrDefaultAsync(note => note.Id == id);
            if (notes != null)
            {
                _context.Notes.Remove(notes);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateNoteRequest request)
        {
            var notes = await _context.Notes.SingleOrDefaultAsync(note => note.Id == request.id);
            if (notes != null)
            {
                notes.Title = request.title;
                notes.Description = request.description;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
