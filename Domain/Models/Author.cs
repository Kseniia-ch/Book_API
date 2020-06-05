using System.Collections.Generic;

namespace BookAPI.Domain.Models
{
    public class Author
    {
        public int Id {get; set;}
        public string LastName {get; set;}
        public string FirstName {get; set;}
        public string Patronymic {get; set;}
        public IList<AuthorBook> Books {get;set;} = new List<AuthorBook>();
    }
}