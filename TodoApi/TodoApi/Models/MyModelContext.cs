using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class MyModelContext : DbContext
    {
        public MyModelContext(DbContextOptions<MyModelContext> options)
            : base(options)
        {
        }

        public DbSet<MyModel> MyModels { get; set; } = null!;
    }
}
