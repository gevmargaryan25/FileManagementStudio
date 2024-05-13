using AutoMapper;
using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Exceptions;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Services.Services.Extensions;
using FileManagementStudio.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FileManagementStudio.Services.Services
{
    public class FIleService : IFileService<FileEntity>
    {
        private readonly IFileEntityRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public FIleService(IFileEntityRepository repository, IMapper mapper, UserManager<User> userManager)
        {
            _fileRepository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<FileEntity>> GetFilesByUser(string userEmail)
        {
            var user = await _userManager.GetUserAsync(userEmail);
            return user.Files;
        }

        public async Task AddAsync(IFormFile entity, string email)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FileEntity fileEntity = _mapper.Map<FileEntity>(entity);
            var list = _userManager.Users.ToList();
            var user = await _userManager.GetUserAsync(email);
            if (user == null)
            {
                throw new NullReferenceException(nameof(user));
            }
            if (fileEntity.Users == null) fileEntity.Users = new List<User>() { user };
            else fileEntity.Users.Add(user);
            try
            {
                await _fileRepository.AddAsync(fileEntity);
                await _fileRepository.SaveFromRepositoryAsync();
            }
            catch (EntityNotFoundException ex)
            {
                throw;
            }
            catch (RepositoryException ex)
            {
                throw;
            }
        }

        public Task AddRangeAsync(IEnumerable<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FileEntity>> GetEntitiesAsync()
        {
            try
            {
                return await _fileRepository.GetAsync(includeProperties: "User");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<FileEntity> GetEntityAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(string entityName, string email)
        {
            try
            {
                var user = await _userManager.GetUserAsync(email);
                FileEntity fileEntity = user.Files.FirstOrDefault(f => f.Name == entityName);
                _fileRepository.Remove(fileEntity);
                await _fileRepository.SaveFromRepositoryAsync();
            }
            catch (EntityNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
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

        public async Task ShareFile(string originEmail, string destEmail, string fileName)
        {
            try
            {

                var list = _userManager.Users.ToList();
                var destUser = await _userManager.GetUserAsync(destEmail);
                if (destUser == null)
                    throw new RepositoryException();
                var originUser = await _userManager.GetUserAsync(originEmail);
                var originUserId = originUser.Id;
                var file = originUser.Files.FirstOrDefault(f => f.Name == fileName);
                destUser.Files.Add(file);
                await _fileRepository.SaveFromRepositoryAsync();
            }
            catch (Exception e)
            {

            }
        }
    }
}
