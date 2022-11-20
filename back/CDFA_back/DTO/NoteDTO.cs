using CDFA_back.Models;

namespace CDFA_back.DTO
{
    public record NoteDTO
    {
        public bool IsCompleted { get; init; }
        public bool IsArchived { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public NoteDTO(Note note)
        {
            IsCompleted = note.IsCompleted;
            Title = note.Title;
            Description = note.Description;
            IsArchived = note.IsArchived;
        }
    }
}
