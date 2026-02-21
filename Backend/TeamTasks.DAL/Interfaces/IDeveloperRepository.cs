using TeamTasks.DAL.Models;

namespace TeamTasks.DAL.Interfaces
{
    public interface IDeveloperRepository
    {
        Task<List<Models.Developer>> GetAllAsync();
        Task<Models.Developer> GetById(Guid id);
        System.Threading.Tasks.Task Create(Models.Developer developer);
        Task<bool> Update(Models.Developer developer);
        Task<bool> Delete(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
