using Microsoft.AspNetCore.Identity;

namespace GiftOfTheGivers.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool? IsVolunteer { get; set; }
    }
}
