using TeamTasks.BL.Interfaces;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Services
{
    public class ProjectStatusService : IProjectStatusService
    {
        private readonly IProjectStatusRepository _projectStatusRepository;

        public ProjectStatusService(IProjectStatusRepository projectStatusRepository)
        {
            _projectStatusRepository = projectStatusRepository;
        }

        public async Task<List<ProjectStatus>> GetAll()
        {
            return await _projectStatusRepository.GetAllAsync();
        }

        public async Task<List<ProjectStatus>> GetAllActive()
        {
            var projectStatuses = await _projectStatusRepository.GetAllAsync();

            return projectStatuses.Where(x => x.IsActive == true).ToList();
        }

        public async Task<ProjectStatus> GetById(Guid id)
        {
            return await _projectStatusRepository.GetById(id);
        }

        public async Task<(bool success, string message)> Create(TypeStatusRequestDto projectreq)
        {
            var project = new ProjectStatus
            {
                Name = projectreq.Name,
                IsActive = true
            };

            try
            {
                await _projectStatusRepository.Create(project);
                var resp = await _projectStatusRepository.SaveChangesAsync();

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
            var project = new ProjectStatus
            {
                Name = projectreq.Name,
                IsActive = projectreq.IsActive
            };

            await _projectStatusRepository.Update(project);

            return await _projectStatusRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            await _projectStatusRepository.Delete(id);
            return await _projectStatusRepository.SaveChangesAsync();
        }
    }
}
