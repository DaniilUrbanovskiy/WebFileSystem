using Microsoft.EntityFrameworkCore;
using WebFileSystem.DataAccess.Domain.Entities;

namespace WebFileSystem.DataAccess
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }
        public DbSet<Folder> Folder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
