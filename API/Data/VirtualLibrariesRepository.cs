using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class VirtualLibrariesRepository : IVirtualLibrariesRepository
    {
        private readonly AppDbContext _context;

        public VirtualLibrariesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VirtualLibrary> CreateVirtualLibrary(string userId)
        {
            var virtualLib = new VirtualLibrary
            {
                UserId = userId
            };

            _context.VirtualLibraries.Add(virtualLib);

            await _context.SaveChangesAsync();

            return virtualLib;
        }

        public async Task<VirtualLibraryBook> GetVirtualLibraryBookByIdAsync(int bookId, int libraryId)
        {
            return await _context.VirtualLibraryBooks
                .Include(x => x.Book)
                .ThenInclude(x => x.Genre)
                .Include(x => x.Book)
                .ThenInclude(x => x.Author)
                .Where(x => x.Id == bookId && x.VirtualLibraryId == libraryId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<VirtualLibrary> GetVirtualLibraryAsync(string userId)
        {
            return await _context.VirtualLibraries
                .Include(x => x.Books)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Author)
                .Include(x => x.Books)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Genre)
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<VirtualLibraryBook> AddBookToVirtualLibraryAsync(VirtualLibraryBook book)
        {
            _context.VirtualLibraryBooks.Add(book);

            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<VirtualLibraryBook> EditBookInVirtualLibraryAsync(VirtualLibraryBook book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task RemoveBookFromVirtualLibraryAsync(VirtualLibraryBook book)
        {
            _context.VirtualLibraryBooks.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
