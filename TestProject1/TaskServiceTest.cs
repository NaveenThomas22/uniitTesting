using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using uniitTesting.Data;
using uniitTesting.Model;
using uniitTesting.Services;

namespace TestProject1
{
    public class TaskServiceTest
    {
        private readonly AppDbContext _context;
        private readonly TaskService _service;

        public TaskServiceTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new AppDbContext(options);
            _service = new TaskService(_context);

            _context.Tasks.Add(new TaskItems { Id = 1, Title = "Test Task", IsCompleted = false });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetTaskItems_ReturnsList_WhenDataExists()
        {
            // Arrange
            var result = await _service.GetTaskItems();

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
            Assert.Equal("Test Task", result[0].Title);
        }

        [Fact]
        public async Task Update_ReturnsFalse_WhenTaskDoesNotExist()
        {
            // Arrange
            var notExistentTask = new TaskItems { Id = 999, Title = "Non_existent", IsCompleted = true };

            // Act
            var result = await _service.Update(notExistentTask);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetTaskItems_ReturnsEmptyList_WhenNoDataExists()
        {
            // Arrange
            _context.Tasks.RemoveRange(_context.Tasks);
            _context.SaveChanges();

            // Act
            var result = await _service.GetTaskItems();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task Create_ReturnsTrue_WhenTaskAdded()
        {
            // Arrange
            var newTask = new TaskItems { Id = 2, Title = "New Task", IsCompleted = false };

            // Act
            var result = await _service.Create(newTask);

            // Assert
            Assert.True(result);
            var tasks = await _context.Tasks.ToListAsync();
            Assert.Equal(2, tasks.Count);
            Assert.Contains(tasks, t => t.Title == "New Task");
        }

        [Fact]
        public async Task Delete_ReturnsTrue_WhenTaskExists()
        {
            // Act
            var result = await _service.Delete(1);

            // Assert
            Assert.True(result);
            var tasks = await _context.Tasks.ToListAsync();
            Assert.Empty(tasks);
        }

        [Fact]
        public async Task Delete_ReturnsFalse_WhenTaskDoesNotExist()
        {
            // Act
            var result = await _service.Delete(633);

            // Assert
            Assert.False(result);
        }
    }
}
