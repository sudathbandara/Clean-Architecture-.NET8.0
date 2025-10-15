using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskItem>> GetAll() => await _taskService.GetAllTasksAsync();

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create([FromBody] string title)
    {
        var task = await _taskService.CreateTaskAsync(title);
        return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        await _taskService.MarkTaskCompleteAsync(id);
        return NoContent();
    }
}
