using Microsoft.EntityFrameworkCore;
using WebFileSystem.DataAccess.Domain.Entities;

namespace WebFileSystem.DataAccess
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }
        public DbSet<File> File { get; set; }
        public DbSet<Folder> Folder { get; set; }
        public DbSet<FileFolder> FileFolder { get; set; }
    }
}
