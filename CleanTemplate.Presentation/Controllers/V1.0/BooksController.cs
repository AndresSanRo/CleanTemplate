using Asp.Versioning;
using CleanTemplate.Core.Dtos;
using CleanTemplate.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanTemplate.Presentation.Controllers.V1._0
{
    [ApiController]
    [Route("api/v{version:apiVersion}/books")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService) 
        {
            _booksService = booksService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType<BookDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        { 
            if (id <= 0) 
            {
               throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0.");
            }

            var book = await _booksService.GetBookByIdAsync(id);

            if (book is null) 
            {
                return NotFound();
            }

            return Ok(book);
        }
    }
}
