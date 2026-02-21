using Microsoft.EntityFrameworkCore;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;

namespace TeamTasks.DAL.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _dbContext;


        public ProjectRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.Project>> GetAllAsync()
        {
            return await _dbContext.Projects.Include(x => x.Statud).ToListAsync();
        }

        public async Task<Models.Project> GetById(Guid id)
        {
            return await _dbContext.Projects.FindAsync(id);
        }

        public async System.Threading.Tasks.Task Create(Models.Project project)
        {
            var status = _dbContext.ProjectStatuses.Where(x => x.Name == "Planned").FirstOrDefault();

            if (status != null)
                project.StatudId = status.StatusId;

            await _dbContext.Projects.AddAsync(project);
        }

        public async Task<bool> Update(Models.Project project)
        {
            _dbContext.Projects.Update(project);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var project = await _dbContext.Projects.FindAsync(id);

            if (project is null) return false;

            _dbContext.Projects.Remove(project);

            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}
