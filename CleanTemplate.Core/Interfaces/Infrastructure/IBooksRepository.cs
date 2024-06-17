using CleanTemplate.Core.Entities;

namespace CleanTemplate.Core.Interfaces.Infrastructure
{
    public interface IBooksRepository
    {
        public Task<Book?> GetBookByIdAsync(int id);
    }
}
