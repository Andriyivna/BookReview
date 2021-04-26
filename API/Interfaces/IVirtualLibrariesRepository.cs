using API.Entities;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IVirtualLibrariesRepository
    {
        Task<VirtualLibrary> CreateVirtualLibrary(string userId);
        Task<VirtualLibrary> GetVirtualLibraryAsync(string userId);
        Task<VirtualLibraryBook> GetVirtualLibraryBookByIdAsync(int bookId, int libraryId);
        Task<VirtualLibraryBook> AddBookToVirtualLibraryAsync(VirtualLibraryBook book);
        Task RemoveBookFromVirtualLibraryAsync(VirtualLibraryBook book);
        Task<VirtualLibraryBook> EditBookInVirtualLibraryAsync(VirtualLibraryBook book);
    }
}
