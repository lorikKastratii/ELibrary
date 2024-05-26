using AutoMapper;
using Elibrary.Books.Domain.Entity;
using Elibrary.Books.Domain.Interfaces;
using ELibrary.Books.Application.Dtos;
using ELibrary.Books.Application.Interfaces;

namespace ELibrary.Books.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public List<BookDto> GetBooks()
        {
            var books = _bookRepository.GetBooks();

            var bookDtos = _mapper.Map<List<BookDto>>(books);

            return bookDtos;
        }

        public async Task<bool> CreateBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);

            var result = await _bookRepository.CreateBookAsync(book);

            if (result)
            {
                return true;
            }

            return false;
        }
    }
}
