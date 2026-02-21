using TeamTasks.BL.Interfaces;
using TeamTasks.DAL.Interfaces;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Services
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly ITaskStatusRepository _taskStatusRepository;

        public TaskStatusService(ITaskStatusRepository taskStatusRepository)
        {
            _taskStatusRepository = taskStatusRepository;
        }

        public async Task<List<DAL.Models.TaskStatus>> GetAll()
        {
            return await _taskStatusRepository.GetAllAsync();
        }

        public async Task<List<DAL.Models.TaskStatus>> GetAllActive()
        {
            var taskStatuses = await _taskStatusRepository.GetAllAsync();

            return taskStatuses.Where(x => x.IsActive == true).ToList();
        }

        public async Task<DAL.Models.TaskStatus> GetById(Guid id)
        {
            return await _taskStatusRepository.GetById(id);
        }

        public async Task<(bool success, string message)> Create(TypeStatusRequestDto projectreq)
        {
            var project = new DAL.Models.TaskStatus
            {
                Name = projectreq.Name,
                IsActive = true
            };

            try
            {
                await _taskStatusRepository.Create(project);
                var resp = await _taskStatusRepository.SaveChangesAsync();

                if (resp)
                    return (success: resp, message: "ProjectStatus creado exitosamente.");
                else
                    return (success: resp, message: "Error al crear ProjectStatus.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Guid id, TypeStatusRequestDto projectreq)
        {
            var project = new DAL.Models.TaskStatus
            {
                Name = projectreq.Name,
                IsActive = projectreq.IsActive
            };

            await _taskStatusRepository.Update(project);

            return await _taskStatusRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            await _taskStatusRepository.Delete(id);
            return await _taskStatusRepository.SaveChangesAsync();
        }
    }
}
