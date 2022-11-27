using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoProject.Models
{
    public class Entities : IdentityDbContext
    {
        public Entities()
        {

        }
        public Entities(DbContextOptions<Entities> options) : base(options)
        {

        }

        public DbSet<TodoList> todoList { get; set; }
        public DbSet<ApplicationUser> appUser { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoList>()
                .HasOne(t => t.User)
                .WithMany(u => u.TodoList);
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = ToDo; Integrated Security = True");
        }
    }
}
