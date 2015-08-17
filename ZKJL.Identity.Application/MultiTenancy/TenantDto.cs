using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ZKJL.Identity.Core.MultiTenancy;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Application.MultiTenancy
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string TenancyName { get; set; }
    }
}
