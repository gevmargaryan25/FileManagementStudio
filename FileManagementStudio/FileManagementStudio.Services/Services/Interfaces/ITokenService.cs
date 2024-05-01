using FileManagementStudio.EntityFramework.Entities;

namespace FileManagementStudio.Services.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
