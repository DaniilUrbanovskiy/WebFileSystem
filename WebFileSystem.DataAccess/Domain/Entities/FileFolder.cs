using System.ComponentModel.DataAnnotations;

namespace WebFileSystem.DataAccess.Domain.Entities
{
    public class FileFolder
    {
        [Key]
        public int Id { get; set; }
        public int FileId { get; set; }
        public int FolderId { get; set; }
    }
}
