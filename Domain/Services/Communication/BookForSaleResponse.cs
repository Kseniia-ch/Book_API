using BookAPI.Domain.Models;

namespace BookAPI.Domain.Services.Communication
{
    public class BookForSaleResponse: BaseResponse
    {
        public BookForSale BookForSale {get; private set;}
        public BookForSaleResponse(bool success, string message, BookForSale bookForSale) : base(success, message)
        {
            BookForSale = bookForSale;
        }

        public BookForSaleResponse(BookForSale bookForSale): this(true, string.Empty, bookForSale){}

        public BookForSaleResponse(string message): this(false, message, null) {}
       
    }
}