using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using ZKJL.Identity.Core.Authorization;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Core.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(IRepository<Tenant> tenantRepository)
            : base(tenantRepository)
        {
        }
    }
}
