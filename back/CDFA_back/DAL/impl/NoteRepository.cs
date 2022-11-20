using CDFA_back.Models;
using Microsoft.EntityFrameworkCore;

namespace CDFA_back.DAL.impl
{
    public class NoteRepository: INoteRepository
    {
        private readonly ProjectContext _context;
        private readonly ILogger<NoteRepository> _logger;
        public NoteRepository(ProjectContext context, ILogger<NoteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CompleteNoteAsync(int id, int user_id)
        {
            try
            {
                var toComplete =  await _context.Notes.FirstOrDefaultAsync(x=>x.Id==id);
                if(toComplete == null)
                {
                    throw new InvalidOperationException($"There is no note with such Id: {id}");
                }
                if(toComplete.Owner != user_id)
                {
                    throw new InvalidOperationException($"You don't have rights");
                }
                toComplete.IsCompleted = true;
                _context.Entry<Note>(toComplete).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method CompleteNoteAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a RepositoryException 
            }
        }
        public async Task<bool> ArchiveNoteAsync(int id, int user_id)
        {
            try
            {
                var toArchive = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
                if (toArchive == null)
                {
                    throw new InvalidOperationException($"There is no note with such Id: {id}");
                }
                if (toArchive.Owner != user_id)
                {
                    throw new InvalidOperationException($"You don't have rights");
                }
                toArchive.IsArchived = true;
                _context.Entry<Note>(toArchive).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method CompleteNoteAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a RepositoryException 
            }
        }
        public async Task<Note> CreateNoteAsync(Note note)
        {
            try
            {
                await _context.Notes.AddAsync(note);
                await _context.SaveChangesAsync();

                return note;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method CreateNoteAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a RepositoryException 
            }
        }

        public async Task<IEnumerable<Note>> GetArchivedNotesAsync(int page, int count, int user_id)
        {
            try
            {
                var notes = await _context.Notes.Where(x => (x.Owner == user_id && x.IsArchived)).Skip(count * (page - 1)).Take(count).ToArrayAsync();
                if (notes is null)
                {
                    throw new InvalidOperationException("There is no notes on this page");
                }
                return notes;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method GetNotesAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a RepositoryException 
            }
        }

        public async Task<IEnumerable<Note>> GetNotesAsync(int page, int count, int user_id)
        {
            try
            {
                var notes = await _context.Notes.Where(x=>(x.Owner == user_id && !x.IsArchived)).Skip(count*(page-1)).Take(count).ToArrayAsync();
                if (notes is null)
                {
                    throw new InvalidOperationException("There is no notes on this page");
                }
                return notes;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method GetNotesAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a RepositoryException 
            }
        }
    }
}
