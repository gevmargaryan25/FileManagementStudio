using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.DAL.Entities
{
    public class Folder
    {
        [Key]
        public int FolderId { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Path { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public ICollection<Folder> ChildFolders { get; set; }
        public ICollection<FileEntity> ChildFiles { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
