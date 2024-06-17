using CleanTemplate.Core.Entities;
using CleanTemplate.Core.Interfaces.Infrastructure;
using CleanTemplate.Core.Interfaces.Services;

namespace CleanTemplate.Core.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _booksRepository.GetBookByIdAsync(id);
        }
    }
}
