using Microsoft.AspNetCore.Identity;

namespace ePrzychodnia.Data
{
    public class User:IdentityUser
    {
       public string Login { get; set; }
        public string Password { get; set; }
        
    }
}