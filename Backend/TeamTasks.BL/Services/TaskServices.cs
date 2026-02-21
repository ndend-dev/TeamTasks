using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using TeamTasks.BL.Interfaces;
using TeamTasks.DAL.Interfaces;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly ITaskRepository _taskRepository;

        public TaskServices(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<DAL.Models.Task>> GetAllAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<DAL.Models.Task> GetById(Guid id)
        {
            return await _taskRepository.GetById(id);
        }

        public async Task<(bool success, string message)> Create(TaskRequestDto taskreq)
        {

            try
            {
                var task = new DAL.Models.Task
                {
                    ProjectId = taskreq.ProjectId,
                    Title = taskreq.Title,
                    Description = taskreq.Description,
                    AssignedId = taskreq.AssignedId,
                    PriorityId = taskreq.PriorityId,
                    EstimatedComplexity = taskreq.EstimatedComplexity,
                    DueDate = taskreq.DueDate,
                    CompletionDate = taskreq.CompletionDate
                };

                return await _taskRepository.CreateTask(task);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Guid id, TaskUpdateRequestDto taskreq)
        {

            var task = new DAL.Models.Task
            {
                TaskId = id,
                StatusId = taskreq.StatusId,
                PriorityId = taskreq.PriorityId,
                EstimatedComplexity = taskreq.EstimatedComplexity,
            };

            _taskRepository.Update(task);
            return await _taskRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            _taskRepository.Delete(id);
            return await _taskRepository.SaveChangesAsync();
        }

        public async Task<List<DeveloperWorkloadDto>> GetDeveloperWorkload()
        {
            return await _taskRepository.GetDeveloperWorkload();
        }

        public async Task<List<ProjectStatusSumamryDto>> GetProjectStatusSummary()
        {
            return await _taskRepository.GetProjectStatusSummary();
        }

        public async Task<List<UpcomingDeadlinesDto>> GetUpcomingDeadlines()
        {
            return await _taskRepository.GetUpcomingDeadlines();
        }

        public async Task<List<DeveloperDelayRiskPredictionDto>> GetDeveloperDelayRickPrediction()
        {
            return await _taskRepository.GetDeveloperDelayRickPrediction();
        }
    }
}
