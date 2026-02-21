namespace TeamTasks.DAL.Interfaces
{
    public interface IProjectStatusRepository
    {
        Task<List<Models.ProjectStatus>> GetAllAsync();
        Task<Models.ProjectStatus> GetById(Guid id);
        System.Threading.Tasks.Task Create(Models.ProjectStatus projectStatus);
        Task<bool> Update(Models.ProjectStatus projectStatus);
        Task<bool> Delete(Guid id);
        Task<bool> SaveChangesAsync();

    }
}
