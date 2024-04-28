using FileManagementStudio.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileManagementStudio.Server.Context
{
    public class FileManagementStudioDbContext : DbContext
    {
        public FileManagementStudioDbContext(DbContextOptions<FileManagementStudioDbContext> options) : base(options)
        {
        }
        public DbSet<Entities.FileEntity> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
