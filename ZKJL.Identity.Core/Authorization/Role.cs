using Abp.Authorization.Roles;
using ZKJL.Identity.Core.MultiTenancy;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Core.Authorization
{
    public class Role : AbpRole<Tenant, User>
    {
        public Role()
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}