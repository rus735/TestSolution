using Microsoft.EntityFrameworkCore;
using DataManager.Models.EF;

namespace DataManager.Models.Base
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        
        public TodoContext() { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}