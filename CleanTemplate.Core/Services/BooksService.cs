using AutoMapper;
using CleanTemplate.Core.Dtos;
using CleanTemplate.Core.Interfaces.Infrastructure;
using CleanTemplate.Core.Interfaces.Services;

namespace CleanTemplate.Core.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public BooksService(IBooksRepository booksRepository, IMapper mapper)
        {
            _booksRepository = booksRepository;
            _mapper = mapper;
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var response = await _booksRepository.GetBookByIdAsync(id);
            return _mapper.Map<BookDto>(response);
        }
    }
}
