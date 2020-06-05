using System.Collections.Generic;

namespace BookAPI.Domain.Models
{
    public class Genre
    {   
        public int Id {get; set;}
        public string Name {get; set;}
        public IList<Book> Books {get; set;} = new List<Book>();
    }
}