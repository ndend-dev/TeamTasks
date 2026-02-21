using Microsoft.EntityFrameworkCore;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;

namespace TeamTasks.DAL.Repositories
{
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private readonly AppDbContext _dbContext;

        public ProjectStatusRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.ProjectStatus>> GetAllAsync()
        {
            return await _dbContext.ProjectStatuses.ToListAsync();
        }

        public async Task<Models.ProjectStatus> GetById(Guid id)
        {
            return await _dbContext.ProjectStatuses.FindAsync(id);
        }

        public async System.Threading.Tasks.Task Create(Models.ProjectStatus projectStatus)
        {
            await _dbContext.ProjectStatuses.AddAsync(projectStatus);
        }

        public async Task<bool> Update(Models.ProjectStatus projectStatus)
        {
            _dbContext.ProjectStatuses.Update(projectStatus);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var projectStatus = await _dbContext.ProjectStatuses.FindAsync(id);

            if (projectStatus is null) return false;

            _dbContext.ProjectStatuses.Remove(projectStatus);

            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}
