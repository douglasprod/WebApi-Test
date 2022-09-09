using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication3.Data;

namespace WebApplication3.Models.Task
{
    public class TaskService : ITask
    {
        private readonly ApplicationContext _applicationContext;
        public TaskService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IEnumerable<TaskDataModel>?> GetTasksAsync()
        {
            var tasks = await _applicationContext.Tasks.ToListAsync();
            if (tasks.Count <= 0) return null; 
            return tasks;
        }
        
        public async Task<TaskDataModel?> GetTaskByIdAsync(int id)
        {
            var task = await _applicationContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null) return null;
            return task;
        }

        public async Task<TaskDataModel?> GetTaskByNameAsync(string name)
        {
            var task = await _applicationContext.Tasks.FirstOrDefaultAsync(x => x.Name == name);
            if (task == null) return null;
            return task;
        }

        public async Task<TaskDataModel?> AddTaskByName(string name)
        {
            var task = await _applicationContext.Tasks.AddAsync(new TaskDataModel
            {
                Name = name,
                CreatedDate = DateTime.UtcNow
            });
            await _applicationContext.SaveChangesAsync();
            if (task.Entity == null) return null;
            return task.Entity;
        }

        public async Task<bool> EditTaskName(string oldName, string newName)
        {
            var task = await _applicationContext.Tasks.FirstOrDefaultAsync(x => x.Name == oldName);
            if (task == null) return false;
            task.Name = newName;
            await _applicationContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditTaskStatusByName(string name)
        {
            var task = await _applicationContext.Tasks.FirstOrDefaultAsync(x => x.Name == name);
            if (task == null) return false;
            task.IsCompleted = true;
            task.EndDate = DateTime.UtcNow;
            await _applicationContext.SaveChangesAsync();
            return true;
        }
    }
}
