using TeamTasks.DAL.Models;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Interfaces
{
    public interface IProjectStatusService
    {
        Task<List<ProjectStatus>> GetAll();
        Task<List<ProjectStatus>> GetAllActive();
        Task<ProjectStatus> GetById(Guid id);
        Task<(bool success, string message)> Create(TypeStatusRequestDto projectreq);
        Task<bool> Update(Guid id, TypeStatusRequestDto projectreq);
        Task<bool> Delete(Guid id);
    }
}
