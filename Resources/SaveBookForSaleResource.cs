using System.ComponentModel.DataAnnotations;

namespace BookAPI.Resources
{
    public class SaveBookForSaleResource
    {
        [Required]
        public string BookId {get; set;}
        public string Description { get; set; }
        public string Place { get; set; }
        public double Price {get; set; }
        public bool Actual {get; set;}
    }
}