using System.Collections.Generic;

namespace BookAPI.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }

        public IList<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public IList<BookForSale> BooksForSale {get;set;} = new List<BookForSale>();
        public IList<BookOrdered> BooksOrdered {get;set;} = new List<BookOrdered>();
    }
}