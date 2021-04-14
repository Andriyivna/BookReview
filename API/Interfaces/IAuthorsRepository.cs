using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IAuthorsRepository
    {
        Task<Author> GetAuthorByIdAsync(int id);
        Task<IReadOnlyList<Author>> GetAuthorsAsync();
        Task<Author> AddAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);
    }
}
