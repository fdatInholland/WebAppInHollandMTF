using Microsoft.EntityFrameworkCore;
using WebAppInHollandMTF.Domain;

namespace WebAppInHollandMTF.DataLayer
{
    public class NoteDBContext : DbContext
    {
        public NoteDBContext(DbContextOptions<NoteDBContext> options) : base(options) { }
        public DbSet<Note> Notes => Set<Note>();
    
    }
}
