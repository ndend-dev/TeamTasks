using Microsoft.EntityFrameworkCore;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;

namespace TeamTasks.DAL.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly AppDbContext _dbContext;

        public PriorityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.Priority>> GetAllAsync()
        {
            return await _dbContext.Priorities.ToListAsync();
        }

        public async Task<Models.Priority> GetById(Guid id)
        {
            return await _dbContext.Priorities.FindAsync(id);
        }

        public async System.Threading.Tasks.Task Create(Models.Priority priority)
        {
            await _dbContext.Priorities.AddAsync(priority);
        }

        public async Task<bool> Update(Models.Priority priority)
        {
            _dbContext.Priorities.Update(priority);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var priority = await _dbContext.Priorities.FindAsync(id);

            if (priority is null) return false;

            _dbContext.Priorities.Remove(priority);

            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

    }
}
