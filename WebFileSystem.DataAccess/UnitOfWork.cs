using WebFileSystem.DataAccess.Repository;

namespace WebFileSystem.DataAccess
{
    public class UnitOfWork
    {
        public UnitOfWork(FileRepository fileRepository, FolderRepository folderRepository)
        {
            FileRepository = fileRepository;
            FolderRepository = folderRepository;
        }

        public FileRepository FileRepository { get; set; }
        public FolderRepository FolderRepository { get; set; }
    }
}
