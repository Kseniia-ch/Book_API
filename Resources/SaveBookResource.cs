using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Resources
{
    public class SaveBookResource
    {
        [Required]
        public string Title {get; set;}
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public string City { get; set; }
        public int Year {get; set;}
        public int Pages {get; set;}
        public List<SaveAuthorResource> Authors {get;set;}
    }
}