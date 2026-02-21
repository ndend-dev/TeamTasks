using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Interfaces
{
    public interface ITaskStatusService
    {
        Task<List<DAL.Models.TaskStatus>> GetAll();
        Task<List<DAL.Models.TaskStatus>> GetAllActive();
        Task<DAL.Models.TaskStatus> GetById(Guid id);
        Task<(bool success, string message)> Create(TypeStatusRequestDto priorityreq);
        Task<bool> Update(Guid id, TypeStatusRequestDto priorityreq);
        Task<bool> Delete(Guid id);
    }
}
