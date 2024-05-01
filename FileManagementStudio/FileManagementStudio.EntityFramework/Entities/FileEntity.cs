using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.EntityFramework.Entities
{
    public enum FileType
    {
        pdf,
        jpeg,
        png,
        mp4,
        txt
    }
    public class FileEntity
    {
        [Key]
        public int FileId { get; set; }

        [Required]
        public string Path { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public FileType FileType { get; set; }

        public double FileSize { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("FolderId")]
        public int FolderId { get; set; }
        public Folder Folder { get; set; }
    }
}
