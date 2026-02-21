using TeamTasks.Utils.Dtos;

namespace TeamTasks.DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Models.Task>> GetAllAsync();
        Task<Models.Task> GetById(Guid id);
        //Task Create(Models.Task task);
        void Update(Models.Task task);
        void Delete(Guid id);
        Task<bool> SaveChangesAsync();
        Task<List<DeveloperWorkloadDto>> GetDeveloperWorkload();
        Task<List<ProjectStatusSumamryDto>> GetProjectStatusSummary();
        Task<List<UpcomingDeadlinesDto>> GetUpcomingDeadlines();
        Task<(bool success, string message)> CreateTask(Models.Task task);
        Task<List<DeveloperDelayRiskPredictionDto>> GetDeveloperDelayRickPrediction();

    }
}
