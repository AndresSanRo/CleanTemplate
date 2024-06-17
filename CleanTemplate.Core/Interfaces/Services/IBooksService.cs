using CleanTemplate.Core.Dtos;

namespace CleanTemplate.Core.Interfaces.Services
{
    public interface IBooksService
    {
        public Task<BookDto?> GetBookByIdAsync(int id);
    }
}
