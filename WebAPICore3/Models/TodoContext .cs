using Microsoft.EntityFrameworkCore;


namespace WebAPICore3.Models
{
    public class TodoContext:DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {        
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
