using System.Collections.Generic;
using System.Threading.Tasks;
using WebFileSystem.DataAccess.Domain.Entities;
using WebFileSystem.DataAccess.Repository;

namespace WebFileSystem.Services
{
    public class FolderService
    {
        private readonly FolderRepository _folderRepository;
        public FolderService(FolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        public async Task AddFolder(string folderName, int? parentId = null) 
        {
            await _folderRepository.Add(new Folder()
            {
                Name = folderName,
                ParentId = parentId
            });
        }

        public async Task<List<Folder>> GetFolders(int? folderId = null)
        {
            return folderId == null ? await GetRootFolders() : await GetChildFolders(folderId);
        }

        public async Task RemoveByName(string folderName) 
        {
            var folder = await _folderRepository.GetBy(folderName);

            await RemoveFolders(folder);
        }

        private async Task RemoveFolders(Folder folder) 
        {         
            var childFolders = await _folderRepository.GetChildFolders(folder.Id);

            await _folderRepository.Remove(folder);

            foreach (var item in childFolders)
            {
                await RemoveFolders(item);
            }
            return;//TODO: return?
        }

        private async Task<List<Folder>> GetRootFolders()
        {
            return await _folderRepository.GetRootFolders();
        }

        private async Task<List<Folder>> GetChildFolders(int? folderId = null)
        {
            return await _folderRepository.GetChildFolders(folderId);
        }
    }
}
