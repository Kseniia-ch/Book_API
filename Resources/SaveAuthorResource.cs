using System.ComponentModel.DataAnnotations;

namespace BookAPI.Resources
{
    public class SaveAuthorResource
    {
        [Required]
        public string LastName {get; set;}
        [Required]
        public string FirstName {get; set;}
        public string Patronymic {get; set;}
    }
}