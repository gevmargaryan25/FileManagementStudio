using FileManagementStudio.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FileManagementStudio.Services.Services.Extensions
{
    public static class Ext
    {
        public static async Task<User?> GetUserAsync(this UserManager<User> userManager, string email)
        {
            return await userManager.Users
                    .Include(u => u.Files)
                    .FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());
        }
    }
}
