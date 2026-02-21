using Microsoft.EntityFrameworkCore;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;

namespace TeamTasks.DAL.Repositories
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskStatusRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.TaskStatus>> GetAllAsync()
        {
            return await _dbContext.TaskStatuses.ToListAsync();
        }

        public async Task<Models.TaskStatus> GetById(Guid id)
        {
            return await _dbContext.TaskStatuses.FindAsync(id);
        }

        public async System.Threading.Tasks.Task Create(Models.TaskStatus taskStatus)
        {
            await _dbContext.TaskStatuses.AddAsync(taskStatus);
        }

        public async Task<bool> Update(Models.TaskStatus taskStatus)
        {
            _dbContext.TaskStatuses.Update(taskStatus);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var taskStatus = await _dbContext.TaskStatuses.FindAsync(id);

            if (taskStatus is null) return false;

            _dbContext.TaskStatuses.Remove(taskStatus);

            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}
