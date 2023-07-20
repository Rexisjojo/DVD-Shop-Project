using Microsoft.AspNetCore.Identity;

namespace DVD_Shop.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        
        public string? ProfilePicture { get; set; }
    }
}
