namespace WebApplication3.Models.Task
{
    public interface ITask
    {
        Task<IEnumerable<TaskDataModel>?> GetTasksAsync();
        Task<TaskDataModel?> GetTaskByIdAsync(int id);
        Task<TaskDataModel?> GetTaskByNameAsync(string name);
        Task<TaskDataModel?> AddTaskByName(string name);
        Task<bool> EditTaskName(string oldName, string newName);
        Task<bool> EditTaskStatusByName(string name);
    }
}
