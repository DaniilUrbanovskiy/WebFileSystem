using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFileSystem.DataAccess.Domain.Entities;

namespace WebFileSystem.DataAccess.Repository
{
    public class FileRepository
    {
        private readonly SqlContext _context;
        public FileRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task Add(File file)
        {
            _context.File.Add(file);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(File file)
        {
            _context.File.Remove(file);
            await _context.SaveChangesAsync();
        }

        public async Task<List<File>> Get(List<int> fileIds)
        {
            return await _context.File.Where(x => fileIds.Contains(x.Id)).ToListAsync();
        }
    }
}
