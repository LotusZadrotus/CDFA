using CDFA_back.Models;
using Microsoft.EntityFrameworkCore;

namespace CDFA_back.DAL.impl
{
    public class UserRepository: IUserRepository
    {
        private readonly ProjectContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ProjectContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> GetUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user is null)
                {
                    throw new InvalidOperationException("There is no user with such id");
                }
                return user;
            }catch(Exception e)
            {
                _logger.LogError($"Error in method GetUserAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a RepositoryException 
            }
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
                if (user is null)
                {
                    throw new InvalidOperationException("There is no user with such name");
                }
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method GetUserAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a RepositoryException 
            }
        }

        public async Task<User> RegistrateUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
               
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method RegistrateUserAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a RepositoryException 
            }
        }
    }
}
