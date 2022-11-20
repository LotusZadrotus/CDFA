using CDFA_back.Models;
using System.Text.Json.Serialization;

namespace CDFA_back.DTO
{
    public record CreateNoteDTO
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public CreateNoteDTO(Note note)
        {
            Title = note.Title;
            Description = note.Description;
        }
        [JsonConstructor]
        public CreateNoteDTO( string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
