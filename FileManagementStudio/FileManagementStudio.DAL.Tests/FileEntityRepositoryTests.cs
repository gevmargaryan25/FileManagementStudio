using FileManagementStudio.DAL.Context;
using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Exceptions;
using FileManagementStudio.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.DAL.Tests
{
    public class FileEntityRepositoryTests
    {
        private DbContextOptions<FileManagementStudioDbContext> dbContextOptions;

        public FileEntityRepositoryTests()
        {
            var dbName = $"FileEntity_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<FileManagementStudioDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }


        [Fact]
        public async Task GetByID_ExistingEntity_ReturnsEntity()
        {
            var repository = await CreateRepositoryAsync();

            var file = await repository.GetByID(1);

            Assert.NotNull(file);
            Assert.Equal("File1", file.Name);
            Assert.Equal("path/to/file1", file.Path);
            Assert.Equal(1, file.FileSize);
            Assert.Equal(1, file.FolderId);
            Assert.Equal(1, file.UserId);
        }

        [Fact]
        public async Task GetByID_EntityNotFound_ThrowsEntityNotFoundException()
        {
            var repository = await CreateRepositoryAsync();
            var id = 5;

            var file = repository.GetByID(id);

            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await file);
        }

        [Fact]
        public async Task Get_Filtering_ReturnsFilteredEntities()
        {

            var repository = await CreateRepositoryAsync();
            Expression<Func<FileEntity, bool>> filter = file => file.FileSize > 1;


            var files = await repository.Get(filter);


            Assert.NotNull(files);
            Assert.All(files, file => Assert.True(file.FileSize > 1));
        }


        [Fact]
        public async Task Get_IncludingProperties_ReturnsEntitiesWithIncludedProperties()
        {
            var repository = await CreateRepositoryAsync();
            string includeProperties = "User";

            var files = await repository.Get(includeProperties: includeProperties);


            Assert.NotNull(files);
            Assert.All(files, file => Assert.NotNull(file.User));
        }

        [Fact]
        public async Task Add_Entity_Successfully()
        {
            var repository = await CreateRepositoryAsync();

            var fileEntity = new FileEntity()
            {
                User = new User()
                {
                    Name = "User",
                    Email = "email",
                    PasswordHash = "Hash",
                    Files = new List<FileEntity>(),
                    Folders = new List<Folder>()
                },
                Name = "testFile",
                Path = "path/to/file",
                FileSize = 0,
                FolderId = 0
            };

            await repository.Add(fileEntity);
            await repository.SaveFromRepository();
            var addedEntity = (await repository.Get(e => e.Name == fileEntity.Name)).FirstOrDefault();
            Assert.NotNull(addedEntity); 
            Assert.Equal(fileEntity.Path, addedEntity.Path);
        }



        private async Task<FileEntityRepository> CreateRepositoryAsync()
        {
            FileManagementStudioDbContext context = new FileManagementStudioDbContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new FileEntityRepository(context);
        }

        private async Task PopulateDataAsync(FileManagementStudioDbContext context)
        {
            int index = 1;

            while (index <= 3)
            {
                var user = new User()
                {
                    Name = "User" + index,
                    Email = "email" + index,
                    PasswordHash = "Hash" + index,
                    Files = new List<FileEntity>
                    {
                        new FileEntity
                        {
                            UserId=index,
                            Name = "File"+index,
                            Path = "path/to/file"+index,
                            FileSize = index,
                            FolderId =index
                        }
                    },
                    Folders = new List<Folder>()
                };

                index++;
                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }
    }


}