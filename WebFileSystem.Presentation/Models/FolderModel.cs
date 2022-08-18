using System.Collections.Generic;
using WebFileSystem.DataAccess.Domain.Entities;

namespace WebFileSystem.Presentation.Models
{
    public class FolderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public static List<FolderModel> Mapper(List<Folder> folders) 
        {
            var folderModels = new List<FolderModel>();
            foreach (var item in folders) 
            {
                folderModels.Add(new FolderModel()
                {
                    Name = item.Name,
                    ParentId = item.ParentId,
                    Id = item.Id
                });
            }
            return folderModels;
        }
    }
}
