using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFileSystem.DataAccess.Domain.Entities;

namespace WebFileSystem.DataAccess.Repository
{
    public class FolderRepository
    {
        private readonly SqlContext _context;
        public FolderRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task Add(Folder folder)
        {
            _context.Folder.Add(folder);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Folder folder)
        {
            _context.Folder.Remove(folder);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExists(string folderName, int? parentId)
        {
            return await _context.Folder.Where(x => x.Name == folderName && x.ParentId == parentId).AnyAsync();
        }

        public async Task<List<Folder>> GetChildFolders(int? folderId)
        {
            return await _context.Folder.Where(x => x.ParentId == folderId).ToListAsync();
        }

        public async Task<List<Folder>> GetRootFolders()
        {
            return await _context.Folder.Where(x => x.ParentId == null).ToListAsync();
        }

        public async Task<Folder> GetBy(string folderName, int? parentId)
        {
            return await _context.Folder.Where(x => x.Name == folderName && x.ParentId == parentId).FirstOrDefaultAsync();
        }

        public async Task<Folder> GetBy(int? folderId)
        {
            return await _context.Folder.FirstOrDefaultAsync(x => x.Id == folderId);
        }
    }
}
