using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppInHollandMTF.DataLayer;
using WebAppInHollandMTF.Domain;
using WebAppInHollandMTF.DTO;
using WebAppInHollandMTF.Services;

namespace WebAppInHollandMTF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly NoteDBContext _notesContext;

        public NotesController(IBlobStorageService blobStorageService, NoteDBContext notescontext)
        {
            _blobStorageService = blobStorageService;
            _notesContext = notescontext;
        }

        // GET: api/<NotesController>
        [HttpGet("GetAllNotes")]
        public async Task<IEnumerable<Note>> GetNotes()
        {
            return await _notesContext.Notes.OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        [HttpPost("CreateNote")]
        public async Task<IActionResult> Create([FromForm] CreateNoteDTO dto)
        {
            string? attachmentUrl = null;

            if (dto.Attachment is not null)
            {
                attachmentUrl = await _blobStorageService.UploadFileAsync(dto.Attachment);
            }


            Note note = new Note
            {
                Title = dto.Title,
                Content = dto.Content,
                AttachmentUrl = attachmentUrl,
                CreatedAt = DateTime.UtcNow
            };

            _notesContext.Notes.Add(note);
            await _notesContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }


        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _notesContext.Notes.FindAsync(id);
            if (note == null) return NotFound();

            _notesContext.Notes.Remove(note);
            await _notesContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _notesContext.Notes.FindAsync(id);
            if (note == null) return NotFound();

            return note;
        }
    }
}
