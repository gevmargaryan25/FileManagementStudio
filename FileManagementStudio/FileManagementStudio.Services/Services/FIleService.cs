using AutoMapper;
using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Exceptions;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FileManagementStudio.Services.Services
{
    public class FIleService : IFileService<FileEntity>
    {
        private readonly IFileEntityRepository _fileRepository;
        //private readonly IFolderRepository _folderRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public FIleService(IFileEntityRepository repository, IMapper mapper, UserManager<User> userManager)
        {
            _fileRepository = repository;
            _mapper = mapper;
            //_folderRepository= folderRepository;
            _userManager = userManager;
        }
        public async Task AddAsync(IFormFile entity, string userId)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FileEntity fileEntity = _mapper.Map<FileEntity>(entity);
            var user = _userManager.Users
                .Include(u => u.Files)
                .FirstOrDefault(u => u.Id == userId);
            //var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new NullReferenceException(nameof(user));
            }
            fileEntity.UserId = userId;
            fileEntity.User = user;
            try
            { 
                await _fileRepository.AddAsync(fileEntity);
                await _fileRepository.SaveFromRepositoryAsync();
            }
            catch (EntityNotFoundException ex)
            {
                throw;
            }
            catch(RepositoryException ex)
            {
                throw;
            }
        }

        public Task AddRangeAsync(IEnumerable<IFormFile> files)
        {
            throw new NotImplementedException();
        }

      /*  public Task Delete(FileEntity entity)
        {
            throw new NotImplementedException();
        }*/

        public async Task<IEnumerable<FileEntity>> GetEntitiesAsync()
        {
            try
            {
                return await _fileRepository.GetAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task<FileEntity> GetEntityAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(string entityName, string userId)
        {
            try
            {
                FileEntity fileEntity = await _fileRepository.GetByFileNameAsync(entityName, userId);
                _fileRepository.Remove(fileEntity);
            }
           catch(EntityNotFoundException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task Remove(object id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<FileEntity> files)
        {
            throw new NotImplementedException();
        }

        public Task Update(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
