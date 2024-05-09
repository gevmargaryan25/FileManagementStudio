using FileManagementStudio.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileManagementStudio.DAL.Context
{
    public class FileManagementStudioDbContext : IdentityDbContext<User>
    {
        public FileManagementStudioDbContext(DbContextOptions<FileManagementStudioDbContext> options) : base(options)
        { }

        public DbSet<Entities.FileEntity> Files { get; set; }
        //public DbSet<Folder> Folders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

}
