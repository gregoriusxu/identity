using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ZKJL.Identity.Application.MultiTenancy;
using ZKJL.Identity.Core.MultiTenancy;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Application.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public TenantDto Tenant { get; set; }

        public int? TenantId { get; set; }
    }
}