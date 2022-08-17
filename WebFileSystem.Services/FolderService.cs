using System;
using System.Threading.Tasks;
using WebFileSystem.DataAccess;
using WebFileSystem.DataAccess.Domain.Entities;

namespace WebFileSystem.Services
{
    public class FolderService
    {
        private readonly UnitOfWork _unitOfWork;
        public FolderService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddFolder(string folderName, int? parentId = null) 
        {
            await _unitOfWork.FolderRepository.Add(new Folder()
            {
                Name = folderName,
                ParentId = parentId
            });
        }

        public async Task RemoveFolder(string folderName) 
        {
            var folder = await _unitOfWork.FolderRepository.GetByName(folderName);

            //var files = await _unitOfWork.FileRepository.Get();

            var childFolders = await _unitOfWork.FolderRepository.GetChilds(folder.Id);


        }
    }
}
