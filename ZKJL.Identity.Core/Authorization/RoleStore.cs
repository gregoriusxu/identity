using System.Collections.Generic;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Threading;
using ZKJL.Identity.Core.MultiTenancy;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Core.Authorization
{
    public class RoleStore : AbpRoleStore<Tenant, Role, User>
    {
        private IRepository<Role> _roleRepository;
        private IRepository<UserRole, long> _userRoleRepository;

        public RoleStore(
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public IEnumerable<Role> GetRoleByUserId(long id)
        {
            var rolelist = new List<Role>();

            var userroles = AsyncHelper.RunSync(() => _userRoleRepository.GetAllListAsync(p => p.UserId == id));
            foreach (var userrole in userroles)
            {
                yield return AsyncHelper.RunSync(() => _roleRepository.GetAsync(userrole.RoleId)); ;
            }
        }
    }
}