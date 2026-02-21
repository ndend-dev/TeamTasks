using TeamTasks.DAL.Models;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetAll();
        Task<Project> GetById(Guid id);
        Task<List<DAL.Models.Task>> GetTaskByProject(ProjectTaskRequestDto projectTask);
        Task<(bool success, string message)> Create(ProjectRequestDto projectreq);
        Task<bool> Update(Guid id, ProjectRequestDto projectreq);
        Task<bool> Delete(Guid id);
    }
}
