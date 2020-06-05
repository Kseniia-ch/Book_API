using System;

namespace BookAPI.Resources
{
    public class BookForSaleResource
    {
        public int Id {get; set;}
        public string BookId {get; set;}
        public string Description { get; set; }
        public string Place { get; set; }
        public double Price {get; set; }

        public DateTime DateAdded {get; set;}
        public bool Actual {get; set;}
        public bool Sold {get; set;}
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
 
    }
}