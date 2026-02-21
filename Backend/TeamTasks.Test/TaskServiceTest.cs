using Moq;
using TeamTasks.BL.Services;
using TeamTasks.DAL.Interfaces;
using TeamTasks.Utils.Dtos;
using Xunit;

namespace TeamTasks.Test
{
    public class TaskServiceTest
    {
        [Fact]
        public async Task GetAllAsyncTest()
        {
            var mock = new Mock<ITaskRepository>();
            var faketask = new List<DAL.Models.Task> { new DAL.Models.Task { TaskId = Guid.NewGuid(), ProjectId = Guid.NewGuid(), Title = "Test",
                    Description = "Descrption Test", AssignedId = Guid.NewGuid(), StatusId = Guid.NewGuid(), EstimatedComplexity = 1, DueDate = DateTime.Now.AddDays(10),
                    CompletionDate = null, CreatedAt = DateTime.Now } };

            mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(faketask);

            var service = new TaskServices(mock.Object);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var mock = new Mock<ITaskRepository>();

            var guid = Guid.NewGuid();

            var faketask = new DAL.Models.Task
            {
                TaskId = guid,
                ProjectId = Guid.NewGuid(),
                Title = "Test",
                Description = "Descrption Test",
                AssignedId = Guid.NewGuid(),
                StatusId = Guid.NewGuid(),
                EstimatedComplexity = 1,
                DueDate = DateTime.Now.AddDays(10),
                CompletionDate = null,
                CreatedAt = DateTime.Now
            };

            mock.Setup(repo => repo.GetById(guid)).ReturnsAsync(faketask);

            var service = new TaskServices(mock.Object);

            var result = await service.GetById(guid);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);
        }

        [Fact]
        public async Task CreateTest()
        {
            var mock = new Mock<ITaskRepository>();

            var guid = Guid.NewGuid();
            var newTask = new TaskRequestDto
            {
                ProjectId = guid,
                Title = "Test",
                Description = "Descrption Test",
                AssignedId = Guid.NewGuid(),
                StatusId = Guid.NewGuid(),
                PriorityId = Guid.NewGuid(),
                EstimatedComplexity = 5,
                DueDate = DateTime.Now.AddDays(10),
                CompletionDate = null
            };


            mock.Setup(repo => repo.CreateTask(It.IsAny<DAL.Models.Task>())).ReturnsAsync((true, "Tarea creada exitosamente."));
            var service = new TaskServices(mock.Object);

            var (success, message) = await service.Create(newTask);

            Assert.True(success);
            Assert.Equal("Tarea creada exitosamente.", message);
        }

        [Fact]
        public async Task UpdateTest()
        {
            var mock = new Mock<ITaskRepository>();

            var guid = Guid.NewGuid();
            var newTask = new TaskUpdateRequestDto
            {
                StatusId = Guid.NewGuid(),
                PriorityId = Guid.NewGuid(),
                EstimatedComplexity = 5
            };


            mock.Setup(repo => repo.Update(It.IsAny<DAL.Models.Task>()));
            mock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            var service = new TaskServices(mock.Object);

            var resp = await service.Update(guid, newTask);

            Assert.True(resp);
        }

        [Fact]
        public async Task DeleteTest()
        {
            var mock = new Mock<ITaskRepository>();

            var guid = Guid.NewGuid();
            
            mock.Setup(repo => repo.Delete(guid));
            mock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);

            var service = new TaskServices(mock.Object);

            var resp = await service.Delete(guid);

            Assert.True(resp);
        }

    }
}


