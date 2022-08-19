using System.Collections.Generic;
using System.Net;
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

        public async Task<string> AddFolder(string folderName, int? parentId = null) 
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return WebServerError.NoName;
            }
            var isExists = await _folderRepository.IsExists(folderName, parentId);
            if (isExists)
            {
                return WebServerError.Exists;
            }
            await _folderRepository.Add(new Folder()
            {
                Name = folderName,
                ParentId = parentId
            });

            return WebServerError.Created;
        }

        public async Task<List<Folder>> GetFolders(int? folderId = null)
        {
            return folderId == null ? await GetRootFolders() : await GetChildFolders(folderId);
        }

        public async Task<string> RemoveByName(string folderName, int? parentId) 
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return WebServerError.NoName;
            }
            var isExists = await _folderRepository.IsExists(folderName, parentId);
            if (!isExists)
            {
                return WebServerError.WrongName;
            }
            var folder = await _folderRepository.GetBy(folderName, parentId);

            await RemoveFolders(folder);

            return WebServerError.Removed;
        }

        private async Task RemoveFolders(Folder folder) 
        {         
            var childFolders = await _folderRepository.GetChildFolders(folder.Id);

            await _folderRepository.Remove(folder);

            foreach (var item in childFolders)
            {
                await RemoveFolders(item);
            }
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
