using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Management.Smo;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Helpers
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser,ApplicationRole>

    {
        public CustomUserClaimsPrincipalFactory(
              UserManager<ApplicationUser> userManager,
              RoleManager<ApplicationRole> roleManager,
              IOptions<IdentityOptions> optionsAccessor)
              : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity =await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("UserFirstName", user.FirstName ?? ""));
            identity.AddClaim(new Claim("UserLastName", user.LastName ?? ""));

            return identity;
        }
    }
}