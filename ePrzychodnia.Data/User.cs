using ePrzychodnia.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace ePrzychodnia.Data
{
    public class User:IdentityUser
    {
     
        public Role Role { get; set; }

        public bool IsActive { get; set; }
        
    }
}