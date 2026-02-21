namespace TeamTasks.DAL.Interfaces
{
    public interface ITaskStatusRepository
    {
        Task<List<Models.TaskStatus>> GetAllAsync();
        Task<Models.TaskStatus> GetById(Guid id);
        System.Threading.Tasks.Task Create(Models.TaskStatus taskStatus);
        Task<bool> Update(Models.TaskStatus taskStatus);
        Task<bool> Delete(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
