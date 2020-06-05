using System.Collections.Generic;

namespace BookAPI.Domain.Models
{
    public class Book
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public int? GenreId { get; set; }
        public int? PublisherId { get; set; }

        public int Year {get; set;}
        public int Pages {get; set;}


        public Genre Genre {get; set;}
        public Publisher Publisher {get; set;}
        public IList<AuthorBook> Authors {get;set;} = new List<AuthorBook>();

        public IList<BookForSale> BooksForSale {get;set;} = new List<BookForSale>();
    }
}