using TeamTasks.BL.Interfaces;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IPriorityRepository _priorityRepository;

        public PriorityService(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public async Task<List<Priority>> GetAll()
        {
            return await _priorityRepository.GetAllAsync();
        }

        public async Task<List<Priority>> GetAllActive()
        {
            var priorities = await _priorityRepository.GetAllAsync();

            return priorities.Where(x => x.IsActive == true).ToList();
        }

        public async Task<Priority> GetById(Guid id)
        {
            return await _priorityRepository.GetById(id);
        }

        public async Task<(bool success, string message)> Create(TypeStatusRequestDto priorityreq)
        {
            var priority = new Priority
            {
                Name = priorityreq.Name,
                IsActive = true
            };

            try
            {
                await _priorityRepository.Create(priority);
                var resp = await _priorityRepository.SaveChangesAsync();

                if (resp)
                    return (success: resp, message: "Prioridad creado exitosamente.");
                else
                    return (success: resp, message: "Error al crear prioridad.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Guid id, TypeStatusRequestDto priorityreq)
        {
            var priority = new Priority
            {
                Name = priorityreq.Name,
                IsActive = priorityreq.IsActive
            };

            await _priorityRepository.Update(priority);

            return await _priorityRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            await _priorityRepository.Delete(id);
            return await _priorityRepository.SaveChangesAsync();
        }
    }
}
