using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using ZKJL.Identity.Core.MultiTenancy;

namespace ZKJL.Identity.Application.MultiTenancy
{
    public class MultiTenancyService : ApplicationService, IMultiTenancyService
    {
        private readonly TenantManager _tenantManager;
        private readonly IRepository<Tenant> _tenantRepository;

        public MultiTenancyService(TenantManager tenantManager, IRepository<Tenant> tenantRepository)
        {
            _tenantManager = tenantManager;
            _tenantRepository = tenantRepository;
        }

        public IEnumerable<TenantDto> GetMultiTenancys()
        {
            var tenants = _tenantRepository.GetAllList();
            return tenants.MapTo<IEnumerable<TenantDto>>();
        }
    }
}
