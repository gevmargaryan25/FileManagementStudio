
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FileManagementStudio.DAL.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(60)]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Email format is not valid"), MaxLength(60)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<FileEntity> Files { get; set; }

        public ICollection<Folder> Folders { get; set; }
    }
}
