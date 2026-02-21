using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Interfaces
{
    public interface ITaskServices
    {
        Task<List<DAL.Models.Task>> GetAllAsync();
        Task<DAL.Models.Task> GetById(Guid id);
        Task<(bool success, string message)> Create(TaskRequestDto taskreq);
        Task<bool> Update(Guid id, TaskUpdateRequestDto taskreq);
        Task<bool> Delete(Guid id);
        Task<List<DeveloperWorkloadDto>> GetDeveloperWorkload();
        Task<List<ProjectStatusSumamryDto>> GetProjectStatusSummary();
        Task<List<UpcomingDeadlinesDto>> GetUpcomingDeadlines();
        Task<List<DeveloperDelayRiskPredictionDto>> GetDeveloperDelayRickPrediction();
    }
}
