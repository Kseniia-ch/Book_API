using System.ComponentModel.DataAnnotations;

namespace BookAPI.Resources
{
    public class SaveBookOrderedResource
    {
        [Required]
        public int BookForsaleId {get; set;}

        public bool Sold {get; set;}
    }
}