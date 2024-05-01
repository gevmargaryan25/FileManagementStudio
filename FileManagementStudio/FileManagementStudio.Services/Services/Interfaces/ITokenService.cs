using FileManagementStudio.DAL.Entities;

namespace FileManagementStudio.Services.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
