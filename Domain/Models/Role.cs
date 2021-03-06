using System.Collections.Generic;

namespace BookAPI.Domain.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<UserRole> UserRoles {get;set;} = new List<UserRole>();
    }
}