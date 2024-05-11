
using Microsoft.AspNetCore.Identity;

namespace FileManagementStudio.DAL.Entities
{
    public class User : IdentityUser
    {
        public ICollection<FileEntity>? Files { get; set; }

        //public ICollection<Folder> Folders { get; set; }
    }
}
