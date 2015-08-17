using Abp.MultiTenancy;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Core.MultiTenancy
{
    public class Tenant : AbpTenant<Tenant, User>
    {
        protected Tenant()
        {

        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}