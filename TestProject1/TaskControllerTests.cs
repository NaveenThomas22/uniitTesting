using Microsoft.AspNetCore.Mvc;
using Moq;
using uniitTesting.Controllers;
using uniitTesting.Model;
using uniitTesting.Services;

public class TaskControllerTests
{
    private readonly Mock<ITaskServices> _mockService;
    private readonly TaskController _controller;

    public TaskControllerTests()
    {
        _mockService = new Mock<ITaskServices>();
        _controller = new TaskController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WhenTasksExist()
    {
        // Arrange
        var tasks = new List<TaskItems>
        {
            new TaskItems { Id = 1, Title = "Task 1", IsCompleted = false }
        };
        _mockService.Setup(s => s.GetTaskItems()).ReturnsAsync(tasks);

        // Act
        var result = await _controller.GetAllAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTasks = Assert.IsType<List<TaskItems>>(okResult.Value);
        Assert.Single(returnTasks);
        Assert.Equal("Task 1", returnTasks[0].Title); // Add this to ensure the task data is correct
    }

    [Fact]
    public async Task GetAllAsync_ReturnsBadRequest_WhenNoTasksExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetTaskItems()).ReturnsAsync(new List<TaskItems>());

        // Act
        var result = await _controller.GetAllAsync();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("No items found", badRequestResult.Value); // Fixed case and removed extra space
    }

    [Fact]
    public async Task CreateData_ReturnsOk_WhenTaskIsCreated()
    {
        // Arrange
        var newTask = new TaskItems { Id = 1, Title = "New Task", IsCompleted = false };
        _mockService.Setup(s => s.Create(newTask)).ReturnsAsync(true);

        // Act
        var result = await _controller.CreateData(newTask);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("TaskCreated successfuly", okResult.Value);
    }

    [Fact]
    public async Task CreateData_ReturnsBadRequest_WhenCreateFails()
    {
        // Arrange
        var newTask = new TaskItems { Id = 1, Title = "New Task", IsCompleted = false };
        _mockService.Setup(s => s.Create(newTask)).ReturnsAsync(false);

        // Act
        var result = await _controller.CreateData(newTask);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Can't create the task", badRequestResult.Value); // Fixed capitalization
    }

    [Fact]
    public async Task UpdateData_ReturnsOk_WhenTaskIsUpdated()
    {
        // Arrange
        var updatedTask = new TaskItems { Id = 1, Title = "Updated Task", IsCompleted = true };
        _mockService.Setup(s => s.Update(updatedTask)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateData(updatedTask);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Product Updated Successfully", okResult.Value); // Fixed typo
    }

    [Fact]
    public async Task UpdateData_ReturnsBadRequest_WhenUpdateFails()
    {
        // Arrange
        var updatedTask = new TaskItems { Id = 1, Title = "Updated Task", IsCompleted = true };
        _mockService.Setup(s => s.Update(updatedTask)).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateData(updatedTask);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Update Failed", badRequestResult.Value); // Fixed capitalization
    }

    [Fact]
    public async Task DeleteData_ReturnsOk_WhenTaskIsDeleted()
    {
        // Arrange
        _mockService.Setup(s => s.Delete(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteData(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Product Deleted Successfully", okResult.Value); // Fixed typo
    }

    [Fact]
    public async Task DeleteData_ReturnsBadRequest_WhenDeleteFails()
    {
        // Arrange
        _mockService.Setup(s => s.Delete(1)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteData(1);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Can't delete the product", badRequestResult.Value);
    }
}