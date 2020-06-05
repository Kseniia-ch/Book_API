using System;

namespace BookAPI.Domain.Models
{
    public class BookOrdered
    {
        public int Id {get; set;}
        public int BookForSaleId {get; set;}
        public int UserId { get; set; }

        public DateTime DateAdded {get; set;}
        public bool Sold {get; set;}

        public BookForSale BookForSale {get; set;}
        public User User { get; set; }
    }
}