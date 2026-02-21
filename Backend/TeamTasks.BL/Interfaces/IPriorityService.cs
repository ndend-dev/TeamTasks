using TeamTasks.DAL.Models;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Interfaces
{
    public interface IPriorityService
    {
        Task<List<Priority>> GetAll();
        Task<List<Priority>> GetAllActive();
        Task<Priority> GetById(Guid id);
        Task<(bool success, string message)> Create(TypeStatusRequestDto priorityreq);
        Task<bool> Update(Guid id, TypeStatusRequestDto priorityreq);
        Task<bool> Delete(Guid id);
    }
}
