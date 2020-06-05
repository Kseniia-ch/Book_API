using System.Collections.Generic;

namespace BookAPI.Resources
{
    public class BookResource
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public string City { get; set; }
        public int Year {get; set;}
        public int Pages {get; set;}
        public AuthorResource[] Authors {get;set;}
        public BookForSaleResource[] BooksForSale {get;set;}
    }
}