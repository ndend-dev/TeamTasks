namespace TeamTasks.DAL.Interfaces
{
    public interface IPriorityRepository
    {
        Task<List<Models.Priority>> GetAllAsync();
        Task<Models.Priority> GetById(Guid id);
        Task Create(Models.Priority priority);
        Task<bool> Update(Models.Priority priority);
        Task<bool> Delete(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
