using System.ComponentModel.DataAnnotations;

namespace WebFileSystem.DataAccess.Domain.Entities
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentPath { get; set; }
        public Folder Folder { get; set; }
    }
}