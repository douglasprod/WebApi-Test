using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using WebApplication3.Data;
using WebApplication3.Models.Task;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;
        private readonly TaskService _taskService;
        public TaskController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _taskService = new TaskService(_applicationContext);
        }

        [HttpGet("GetTasksList")]
        public async Task<ActionResult<List<TaskDataModel>>> GetTasksList()
        {
            var tasks = await _taskService.GetTasksAsync();
            if (tasks == null) return NoContent();
            var list = tasks.OrderBy(x => x.CreatedDate).ToList();
            return Ok(list);
        }

        [HttpGet("id")]
        public async Task<ActionResult<TaskDataModel>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NoContent();
            return Ok(task);
        }

        [HttpPost("AddTask")]
        public async Task<ActionResult> AddTask(string name)
        {
            if(string.IsNullOrEmpty(name) && name.Length > 255) return BadRequest("incorrect task name");
            var task = await _taskService.AddTaskByName(name);
            if (task == null) return BadRequest("Task not added"); 
            return Ok($"Task added -  Id: {task.Id}, Name: {task.Name}, Time: {task.CreatedDate}");

        }

        [HttpPost("EditTaskByName")]
        public async Task<ActionResult> EditTaskByName(string oldName, string newName)
        {
            var task = await _taskService.GetTaskByNameAsync(oldName);
            if (string.IsNullOrEmpty(oldName) && oldName.Length > 255 && task == null) return BadRequest("incorrect task name");
            var isEdit = await _taskService.EditTaskName(oldName, newName);
            if (!isEdit) return BadRequest("Task name not changed");
            return Ok("Task name changed");
        }

        [HttpPost("EditTaskStatus")]
        public async Task<ActionResult> EditTaskStatus(string name)
        {
            var task = await _taskService.GetTaskByNameAsync(name);
            if (string.IsNullOrEmpty(name) && name.Length > 255 && task == null) return BadRequest("incorrect task name");
            var isEdit = await _taskService.EditTaskStatusByName(name);
            if (!isEdit) return BadRequest("Task status not changed");
            return Ok("Task status changed");
        }
    }
}
