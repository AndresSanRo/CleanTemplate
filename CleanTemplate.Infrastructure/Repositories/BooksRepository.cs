using CleanTemplate.Core.Entities;
using CleanTemplate.Core.Interfaces.Infrastructure;
using CleanTemplate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanTemplate.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<BooksRepository> _logger;

        public BooksRepository(DatabaseContext dbContext, ILogger<BooksRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            try
            {
                if (id < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(id));
                }

                return await _dbContext.Books
                                        .AsNoTracking()
                                        .Include(b => b.Author)
                                        .Select(b => new Book
                                        {
                                            Id = b.Id,
                                            Title = b.Title,
                                            AuthorId = b.AuthorId,
                                            Author = new Author
                                            {
                                                Id = b.Author!.Id,
                                                Name = b.Author!.Name
                                            }
                                        })
                                        .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while trying to get the book by id. id value: {id}");
                throw;
            }
        }
    }
}
