using Microsoft.EntityFrameworkCore;
using WebApplication3.Models.Task;

namespace WebApplication3.Data
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<TaskDataModel> Tasks { get; set; }
    }
}
