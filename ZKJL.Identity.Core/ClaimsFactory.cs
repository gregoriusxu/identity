using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Core
{
    public class ClaimsFactory : ClaimsIdentityFactory<User, long>
    {
        public ClaimsFactory()
        {
            this.UserIdClaimType = Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Subject;
            this.UserNameClaimType = Thinktecture.IdentityServer.Core.Constants.ClaimTypes.PreferredUserName;
            this.RoleClaimType = Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role;
        }

        public override async System.Threading.Tasks.Task<System.Security.Claims.ClaimsIdentity> CreateAsync(UserManager<User, long> manager, User user, string authenticationType)
        {
            var ci = await base.CreateAsync(manager, user, authenticationType);
            if (!String.IsNullOrWhiteSpace(user.Surname))
            {
                ci.AddClaim(new Claim("given_name", user.Surname));
            }
            if (!String.IsNullOrWhiteSpace(user.Name))
            {
                ci.AddClaim(new Claim("family_name", user.Name));
            }
            return ci;
        }
    }
}
