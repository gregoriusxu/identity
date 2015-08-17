using Abp.Authorization;
using ZKJL.Identity.Core.MultiTenancy;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Core.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}