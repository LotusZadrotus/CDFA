using CDFA_back.Models;

namespace CDFA_back.DAL
{
    public interface INoteRepository
    {
        public Task<IEnumerable<Note>> GetNotesAsync(int page, int count, int user_id);
        public Task<IEnumerable<Note>> GetArchivedNotesAsync(int page, int count, int user_id);
        public Task<Note> CreateNoteAsync(Note note);
        public Task<bool> CompleteNoteAsync(int id, int user_id);
        public Task<bool> ArchiveNoteAsync(int id, int user_id);
    }
}
