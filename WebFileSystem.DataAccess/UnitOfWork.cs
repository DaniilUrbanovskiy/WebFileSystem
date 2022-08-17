using WebFileSystem.DataAccess.Repository;

namespace WebFileSystem.DataAccess
{
    public class UnitOfWork
    {
        public UnitOfWork(FileRepository fileRepository, FolderRepository folderRepository, FileFolderRepository fileFolderRepository)
        {
            FileRepository = fileRepository;
            FolderRepository = folderRepository;
            FileFolderRepository = fileFolderRepository;
        }

        public FileRepository FileRepository { get; set; }
        public FolderRepository FolderRepository { get; set; }
        public FileFolderRepository FileFolderRepository { get; set; }
    }
}
