using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using ZKJL.Identity.Core.MultiTenancy;

namespace ZKJL.Identity.Application.MultiTenancy
{
    public interface IMultiTenancyService : IApplicationService
    {
        IEnumerable<TenantDto> GetMultiTenancys();
    }
}
