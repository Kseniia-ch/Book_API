using System.ComponentModel.DataAnnotations;

namespace BookAPI.Resources
{
    public class SavePublisherResource
    {
        [Required]
        public string Name {get; set;}
        [Required]
        public string City {get; set;}
    }
}