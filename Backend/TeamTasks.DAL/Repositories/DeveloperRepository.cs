using Microsoft.EntityFrameworkCore;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;

namespace TeamTasks.DAL.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly AppDbContext _dbContext;
        public DeveloperRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Models.Developer>> GetAllAsync()
        {
            return await _dbContext.Developers.ToListAsync();
        }

        public async Task<Models.Developer> GetById(Guid id)
        {
            return await _dbContext.Developers.FindAsync(id);
        }

        public async System.Threading.Tasks.Task Create(Models.Developer developer)
        {
            await _dbContext.Developers.AddAsync(developer);
        }

        public async Task<bool> Update(Models.Developer developer)
        {
            _dbContext.Developers.Update(developer);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var developer = await _dbContext.Developers.FindAsync(id);

            if (developer is null) return false;

            _dbContext.Developers.Remove(developer);

            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }


    }
}
