using System.Collections.Generic;
using System.Linq;
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
                return Result.NoName;
            }
            var isExists = await _folderRepository.IsExists(folderName, parentId);
            if (isExists)
            {
                return Result.Exists;
            }
            await _folderRepository.Add(new Folder()
            {
                Name = folderName,
                ParentId = parentId
            });

            return Result.Created;
        }

        public async Task<List<Folder>> GetFolders(int? folderId = null)
        {
            return folderId == null ? await GetRootFolders() : await GetChildFolders(folderId);
        }

        public async Task<string> RemoveByName(string folderName, int? parentId) 
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return Result.NoName;
            }
            var isExists = await _folderRepository.IsExists(folderName, parentId);
            if (!isExists)
            {
                return Result.WrongName;
            }
            var folder = await _folderRepository.GetBy(folderName, parentId);

            await RemoveFolders(folder);

            return Result.Removed;
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

        private async Task AddFolders(string foldersPath, int? parentId)
        {
            if (string.IsNullOrEmpty(foldersPath))
            {
                return;
            }
            
            var folders = foldersPath.Split("/").Where(x => x != "").ToList();

            var result = await AddFolder(folders[0], parentId);

            var folder = await _folderRepository.GetBy(folders[0], parentId);

            folders.RemoveAt(0);

            await AddFolders(string.Join('/', folders), folder.Id);
        }

        public async Task<string> CreateStructure(string[] folders, int? parentId)
        {
            for (int i = 0; i < folders.Length; i++)
            {
                await AddFolders(folders[i], parentId);
            }
            return Result.Created;
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
