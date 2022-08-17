using System.ComponentModel.DataAnnotations;

namespace WebFileSystem.DataAccess.Domain.Entities
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
