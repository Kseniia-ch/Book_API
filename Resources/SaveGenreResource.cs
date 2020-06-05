using System.ComponentModel.DataAnnotations;

namespace BookAPI.Resources
{
    public class SaveGenreResource
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}