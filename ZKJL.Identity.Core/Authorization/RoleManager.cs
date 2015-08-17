using System.Collections.Generic;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Uow;
using Abp.Threading;
using Abp.Zero.Configuration;
using ZKJL.Identity.Core.MultiTenancy;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Core.Authorization
{
    public class RoleManager : AbpRoleManager<Tenant, Role, User>
    {
        private RoleStore _roleStore;
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                store,
                permissionManager,
                roleManagementConfig,
                unitOfWorkManager)
        {
            _roleStore = store;
        }

        public IList<PermissionGrantInfo> GetPermissions()
        {
            var permissiongrantinfolist = new List<PermissionGrantInfo>();
            if (AbpSession.UserId.HasValue)
            {
                var roles = _roleStore.GetRoleByUserId(AbpSession.UserId.Value);
                foreach (var role in roles)
                {
                    permissiongrantinfolist.AddRange(AsyncHelper.RunSync(() => AbpStore.GetPermissionsAsync(role)));
                }
            }

            return permissiongrantinfolist;
        }
    }
}