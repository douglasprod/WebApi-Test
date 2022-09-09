using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models.Task
{
    public class TaskDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
