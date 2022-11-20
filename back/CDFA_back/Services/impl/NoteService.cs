using CDFA_back.DAL;
using CDFA_back.DTO;
using CDFA_back.Models;

namespace CDFA_back.Services.impl
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _notes;
        private readonly IUserRepository _users;
        private readonly ILogger<NoteService> _logger;
        public NoteService(INoteRepository notes, ILogger<NoteService> logger, IUserRepository user)
        {
            _notes = notes;
            _logger = logger;
            _users = user;
        }
    
        public async Task<bool> CompleteNoteAsync(int note_id, string user_name)
        {
            try
            {
                var logged_user = await _users.GetUserByNameAsync(user_name);
                if (logged_user is null)
                {
                    throw new InvalidOperationException($"User with such name not presented in system. Possibly you are using old token.");
                }
                var toReturn = await _notes.CompleteNoteAsync(note_id, logged_user.Id);
                return toReturn;
            }
            catch (Exception e)
            {
                if(e is InvalidOperationException)
                {
                    throw new InvalidOperationException(e.Message);
                }
                _logger.LogError($"Error in method CompleteNoteAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a ServiceException 
            }
        }
        public async Task<bool> ArchiveNoteAsync(int note_id, string user_name)
        {
            try
            {
                var logged_user = await _users.GetUserByNameAsync(user_name);
                if (logged_user is null)
                {
                    throw new InvalidOperationException($"User with such name not presented in system. Possibly you are using old token.");
                }
                var toReturn = await _notes.ArchiveNoteAsync(note_id, logged_user.Id);
                return toReturn;
            }
            catch (Exception e)
            {
                if (e is InvalidOperationException)
                {
                    throw new InvalidOperationException(e.Message);
                }
                _logger.LogError($"Error in method ArchiveNoteAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a ServiceException 
            }
        }
        public async Task<NoteDTO> CreateNoteAsync(CreateNoteDTO note, string user_name)
        {
            try
            {
                var logged_user = await _users.GetUserByNameAsync(user_name);
                if (logged_user is null)
                {
                    throw new InvalidOperationException($"User with such name not presented in system. Possibly you are using old token.");
                }
                var toCreate = new Note(note.Title, note.Description, logged_user.Id);
                toCreate = await _notes.CreateNoteAsync(toCreate);
                return new NoteDTO(toCreate);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method CompleteNoteAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a ServiceException 
            }
        }

        public async Task<IEnumerable<NoteDTO>> GetArchivedNotesAsync(int page, int count, string user_name)
        {
            try
            {
                var logged_user = await _users.GetUserByNameAsync(user_name);
                if (logged_user is null)
                {
                    throw new InvalidOperationException($"User with such name not presented in system. Possibly you are using old token.");
                }
                var toReturn = await _notes.GetArchivedNotesAsync(page, count, logged_user.Id);
                return toReturn.Select(x => new NoteDTO(x));
            }
            catch (Exception e)
            {
                if (e is InvalidOperationException)
                {
                    throw new InvalidOperationException(e.Message);
                }
                _logger.LogError($"Error in method GetNotesAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a ServiceException 
            }
        }

        public async Task<IEnumerable<NoteDTO>> GetNotesAsync(int page, int count, string user_name)
        {
            try
            {
                var logged_user = await _users.GetUserByNameAsync(user_name);
                if(logged_user is null)
                {
                    throw new InvalidOperationException($"User with such name not presented in system. Possibly you are using old token.");
                }
                var toReturn = await _notes.GetNotesAsync(page, count, logged_user.Id);
                return toReturn.Select(x=>new NoteDTO(x));
            }
            catch (Exception e)
            {
                if (e is InvalidOperationException)
                {
                    throw new InvalidOperationException(e.Message);
                }
                _logger.LogError($"Error in method GetNotesAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a ServiceException 
            }
        }
    }
}
