using System.ComponentModel.DataAnnotations;

namespace FileManagementStudio.DAL.Entities
{

    public class FileEntity
    {
        [Key]
        public int FileId { get; set; }

        //public string Path { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FileType { get; set; }

        public double FileSize { get; set; }

        public List<User> Users { get; set; }

      /*  [ForeignKey("FolderId")]
        public int? FolderId { get; set; }
        public Folder? Folder { get; set; }*/
    }
}
