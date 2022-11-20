using CDFA_back.DTO;

namespace CDFA_back.Services
{
    public interface INoteService
    {
        public Task<IEnumerable<NoteDTO>> GetNotesAsync(int page, int count, string user_name);
        public Task<IEnumerable<NoteDTO>> GetArchivedNotesAsync(int page, int count, string user_name);
        public Task<NoteDTO> CreateNoteAsync(CreateNoteDTO note, string user_name);
        public Task<bool> CompleteNoteAsync(int note_id, string user_name);
        public Task<bool> ArchiveNoteAsync(int note_id, string user_name);
    }
}
