using Microsoft.AspNetCore.Identity;

namespace VSHCTwebApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? Competitions { get; set; }
        public string? Ideas { get; set; }
        public string? AboutMe { get; set; }
        public string? AvatarPath { get; set; }
    }

}
