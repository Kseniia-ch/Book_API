using System;
using BookAPI.Domain.Models;

namespace BookAPI.Resources
{
    public class BookOrderedResource
    {
        public int Id {get; set;}
        public int BookForsaleId {get; set;}
        public DateTime DateAdded {get; set;}
        public bool Sold {get; set;}
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public BookForSaleResource BookForsale { get; set; }
    }
}