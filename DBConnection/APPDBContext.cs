using Microsoft.EntityFrameworkCore;
using WebApplication1Minimal.DBConnection;
namespace WebApplication1Minimal.DBConnection
{
    public class APPDBContext : DbContext 
    {
        public APPDBContext(DbContextOptions options): base(options) { }
        public DbSet<Model.Task> Tasks { get; set; }

    }
}
