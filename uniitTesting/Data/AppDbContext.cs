using Microsoft.EntityFrameworkCore;
using uniitTesting.Model;

namespace uniitTesting.Data
{
    public class AppDbContext : DbContext
    {

       public DbSet<TaskItems> Tasks { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }



    }
}
