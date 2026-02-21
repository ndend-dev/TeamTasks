namespace TeamTasks.DAL.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Models.Project>> GetAllAsync();
        Task<Models.Project> GetById(Guid id);
        Task Create(Models.Project project);
        Task<bool> Update(Models.Project project);
        Task<bool> Delete(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
