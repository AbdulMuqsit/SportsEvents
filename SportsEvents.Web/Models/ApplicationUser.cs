using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsEvents.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [System.ComponentModel.DataAnnotations.Url]
        public string Link { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string OrganiztionName { get; set; }
        public string OrganizationDecription { get; set; }
        public byte[] OrganaiztionLogo { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Event> BookmarkedEvents { get; set; }
        public ICollection<Event> RegisteredEvents { get; set; }
        public ICollection<Event> RegisterRequestEvents { get; set; }
        public ICollection<Event> ClickedEvents { get; set; }

        public string ContactDetailsId { get; set; }
        public ContactDetails ContactDetails { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}