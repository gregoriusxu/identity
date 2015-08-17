using System;
using System.Collections.Generic;
using System.Security.Claims;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading;
using Abp.Zero.Configuration;
using Microsoft.AspNet.Identity;
using ZKJL.Identity.Core.Authorization;
using ZKJL.Identity.Core.MultiTenancy;

namespace ZKJL.Identity.Core.Users
{
    public class UserManager : AbpUserManager<Tenant, Role, User>
    {
        private readonly UserStore _userStore;

        public UserManager(
            UserStore store,
            RoleManager roleManager,
            IRepository<Tenant> tenantRepository,
            IMultiTenancyConfig multiTenancyConfig,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver)
            : base(
                store,
                roleManager,
                tenantRepository,
                multiTenancyConfig,
                permissionManager,
                unitOfWorkManager,
                settingManager,
                userManagementConfig,
                iocResolver)
        {
            _userStore = store;
            //ClaimsIdentityFactory = new ClaimsFactory();
        }

        public virtual IEnumerable<PermissionGrantInfo> GetAllPermission()
        {
            if (AbpSession.UserId.HasValue)
            {
                var user = AsyncHelper.RunSync(() => _userStore.FindByIdAsync(AbpSession.UserId.Value));
                return AsyncHelper.RunSync((() => _userStore.GetPermissionsAsync(user)));
            }

            return new List<PermissionGrantInfo>();
        }
    }

    public class ClaimsFactory : ClaimsIdentityFactory<User, long>
    {
        public ClaimsFactory()
        {
            UserIdClaimType = Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Subject;
            UserNameClaimType = Thinktecture.IdentityServer.Core.Constants.ClaimTypes.PreferredUserName;
            RoleClaimType = Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role;
        }

        public override async System.Threading.Tasks.Task<ClaimsIdentity> CreateAsync(UserManager<User, long> manager, User user, string authenticationType)
        {
            var ci = await base.CreateAsync(manager, user, authenticationType);
            if (!String.IsNullOrWhiteSpace(user.Name))
            {
                ci.AddClaim(new Claim("given_name", user.Name));
            }
            if (!String.IsNullOrWhiteSpace(user.Surname))
            {
                ci.AddClaim(new Claim("family_name", user.Surname));
            }
            return ci;
        }
    }
    
}