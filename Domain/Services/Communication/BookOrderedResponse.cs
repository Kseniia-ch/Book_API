using BookAPI.Domain.Models;

namespace BookAPI.Domain.Services.Communication
{
    public class BookOrderedResponse: BaseResponse
    {
        public BookOrdered BookOrdered {get; private set;}
        public BookOrderedResponse(bool success, string message, BookOrdered bookOrdered) : base(success, message)
        {
            BookOrdered = bookOrdered;
        }

        public BookOrderedResponse(BookOrdered bookOrdered): this(true, string.Empty, bookOrdered){}

        public BookOrderedResponse(string message): this(false, message, null) {}
       
    }
}