using BookAPI.Domain.Models;

namespace BookAPI.Domain.Services.Communication
{
    public class BookResponse: BaseResponse
    {
        public Book Book {get; private set;}
        public BookResponse(bool success, string message, Book book) : base(success, message)
        {
            Book = book;
        }

        public BookResponse(Book book): this(true, string.Empty, book){}

        public BookResponse(string message): this(false, message, null) {}
       
    }
}