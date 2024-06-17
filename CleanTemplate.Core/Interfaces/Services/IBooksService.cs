using CleanTemplate.Core.Entities;

namespace CleanTemplate.Core.Interfaces.Services
{
    public interface IBooksService
    {
        public Task<Book?> GetBookByIdAsync(int id);
    }
}
