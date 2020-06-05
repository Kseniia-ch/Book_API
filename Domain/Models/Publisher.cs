using System.Collections.Generic;

namespace BookAPI.Domain.Models
{
    public class Publisher
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string City {get; set;}
        public IList<Book> Books {get; set;} = new List<Book>();
    }
}