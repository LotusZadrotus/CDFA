using Microsoft.EntityFrameworkCore;

namespace CDFA_back.Models
{
    public class ProjectContext: DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {

        }
    }
}
