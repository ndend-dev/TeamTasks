using TeamTasks.DAL.Models;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Interfaces
{
    public interface IDeveloperService
    {
        Task<List<Developer>> GetAll();
        Task<List<Developer>> GetAllActive();
        Task<Developer> GetById(Guid id);
        Task<(bool success, string message)> Create(DeveloperRequestDto developerreq);
        Task<bool> Update(Guid id, DeveloperRequestDto developerreq);
        Task<bool> Delete(Guid id);
    }
}
