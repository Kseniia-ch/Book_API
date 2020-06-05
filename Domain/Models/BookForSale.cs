using System;

namespace BookAPI.Domain.Models
{
    public class BookForSale
    {
        public int Id {get; set;}
        public int BookId {get; set;}
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public double Price {get; set; }

        public DateTime DateAdded {get; set;}
        public bool Actual {get; set;}

        public Book Book {get; set;}
        public User User { get; set; }
        public BookOrdered BookOrdered {get; set;}

    }
}