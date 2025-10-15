using TaskManager.Domain.Entities;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync() => await _repository.GetAllAsync();

        public async Task<TaskItem> CreateTaskAsync(string title)
        {
            var task = new TaskItem(title);
            await _repository.AddAsync(task);
            await _repository.SaveChangesAsync();
            return task;
        }

        public async Task MarkTaskCompleteAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                throw new Exception("Task not found.");

            task.MarkComplete();
            await _repository.SaveChangesAsync();
        }
    }
}
