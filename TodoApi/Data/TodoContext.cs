using Microsoft.EntityFrameworkCore;
using TodoApi.Entities;

namespace TodoApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> opt): base(opt)
        {

        }

        public DbSet<Todo> Todos { get; set; }
    }
}
