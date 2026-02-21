using TeamTasks.BL.Interfaces;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;
using TeamTasks.DAL.Repositories;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.BL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;

        public ProjectService(IProjectRepository projectRepository, ITaskRepository taskRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        public async Task<List<Project>> GetAll()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<Project> GetById(Guid id)
        {
            return await _projectRepository.GetById(id);
        }

        public async Task<List<DAL.Models.Task>> GetTaskByProject(ProjectTaskRequestDto projectTask)
        {
            List<DAL.Models.Task> tasks = await _taskRepository.GetAllAsync();

            tasks = tasks.Where(x => x.ProjectId == projectTask.ProjectId).ToList();

            if (tasks.Any())
            {

                if (projectTask.StatusId != null)
                    tasks = tasks.Where(x => x.StatusId == projectTask.StatusId).ToList();

                if (projectTask.DeveloperId != null)
                    tasks = tasks.Where(x => x.AssignedId == projectTask.DeveloperId).ToList();
            }


            return await System.Threading.Tasks.Task.FromResult(tasks);
        }

        public async Task<(bool success, string message)> Create(ProjectRequestDto projectreq)
        {
            var project = new Project
            {
                Name = projectreq.Name,
                ClientName = projectreq.ClientName,
                StartDate = projectreq.StartDate,
                EndDate = projectreq.EndDate
            };

            try
            {
                await _projectRepository.Create(project);
                var resp = await _projectRepository.SaveChangesAsync();

                if (resp)
                    return (success: resp, message: "Projecto creado exitosamente.");
                else
                    return (success: resp, message: "Error al crear projecto.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Guid id, ProjectRequestDto projectreq)
        {
            var project = new Project
            {
                ProjectId = id,
                ClientName = projectreq.ClientName,
                StartDate = projectreq.StartDate,
                EndDate = projectreq.EndDate,
                StatudId = projectreq.StatusId
            };

            await _projectRepository.Update(project);

            return await _projectRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            await _projectRepository.Delete(id);
            return await _projectRepository.SaveChangesAsync();
        }

    }
}
