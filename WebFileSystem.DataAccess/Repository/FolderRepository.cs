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

        public async Task<List<Folder>> Get(List<int> folderIds)
        {
            return await _context.Folder.Where(x => folderIds.Contains(x.ParentId)).ToListAsync();
        }
    }
}
