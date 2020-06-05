using BookAPI.Domain.Models;

namespace BookAPI.Domain.Services.Communication
{
    public class AuthorResponse: BaseResponse
    {
        public Author Author {get; private set;}
        public AuthorResponse(bool success, string message, Author author) : base(success, message)
        {
            Author = author;
        }

        public AuthorResponse(Author author): this(true, string.Empty, author){}

        public AuthorResponse(string message): this(false, message, null) {}
       
    }
}