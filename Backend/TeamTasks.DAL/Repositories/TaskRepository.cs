using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TeamTasks.DAL.Interfaces;
using TeamTasks.DAL.Models;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.Task>> GetAllAsync()
        {
            return await _dbContext.Tasks.Include(x => x.Assigned).Include(x => x.Status).Include(x => x.Priority).ToListAsync();
        }

        public async Task<Models.Task> GetById(Guid id)
        {
            return await _dbContext.Tasks.FindAsync(id);
        }

        public async System.Threading.Tasks.Task Create(Models.Task task)
        {
            await _dbContext.Tasks.AddAsync(task);
        }

        public async void Update(Models.Task task)
        {
            var status = await _dbContext.TaskStatuses.FindAsync(task.StatusId);

            if (status.Name == "Completed")
            {
                task.CompletionDate = DateTime.Now;
            }

            _dbContext.Tasks.Update(task);
        }

        public async void Delete(Guid id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);

            _dbContext.Tasks.Remove(task);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }


        //Metodos de consulta de los SP
        public async Task<List<DeveloperWorkloadDto>> GetDeveloperWorkload()
        {
            return await _dbContext.DeveloperWorkloads.FromSqlRaw("EXEC Core.sp_get_developer_workload")
                .ToListAsync();
        }

        public async Task<List<ProjectStatusSumamryDto>> GetProjectStatusSummary()
        {
            return await _dbContext.ProjectStatusSumamries.FromSqlRaw("EXEC Core.sp_get_project_status_sumamry")
                .ToListAsync();
        }

        public async Task<List<UpcomingDeadlinesDto>> GetUpcomingDeadlines()
        {
            return await _dbContext.UpcomingDeadlines.FromSqlRaw("EXEC Core.sp_get_upcoming_deadlines")
                .ToListAsync();
        }

        public async Task<(bool success, string message)> CreateTask(Models.Task task)
        {
            try
            {
                await CreateTaskWithSP(task);

                return (true, "Tarea creada exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        private async Task<int> CreateTaskWithSP(Models.Task task)
        {

            var statusIdVal = _dbContext.TaskStatuses.Where(x => x.Name == "ToDo").FirstOrDefault().StatusId;
               

            var projectId = new SqlParameter("@ProjectId", task.ProjectId);
            var title = new SqlParameter("@Title", task.Title);
            var description = new SqlParameter("@Description", task.Description);
            var developerId = new SqlParameter("@DeveloperId", task.AssignedId);
            var statusId = new SqlParameter("@StatusId", statusIdVal);
            var priorityId = new SqlParameter("@PriorityId", task.PriorityId);
            var estimatedComplexity = new SqlParameter("@EstimatedComplexity", task.EstimatedComplexity);
            var dueDate = new SqlParameter("@DueDate", task.DueDate);


            try
            {
                return await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC Core.sp_insert_task @ProjectId, @Title, @Description, @DeveloperId, @StatusId, @PriorityId, @EstimatedComplexity, @DueDate",
                    projectId, title, description, developerId, statusId, priorityId, estimatedComplexity, dueDate);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DeveloperDelayRiskPredictionDto>> GetDeveloperDelayRickPrediction()
        {
            return await _dbContext.DeveloperDelayRiskPredictions.FromSqlRaw("EXEC Core.sp_developer_delay_risk_prediction")
                .ToListAsync();
        }

    }
}
