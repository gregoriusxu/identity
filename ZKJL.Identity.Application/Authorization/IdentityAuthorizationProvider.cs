using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Localization;
using Microsoft.AspNet.Identity;
using ZKJL.Identity.Core.Authorization;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Application.Authorization
{
    public class IdentityAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //TODO: Localize (Change FixedLocalizableString to LocalizableString)
            //context.CreatePermission("CanEditMenues", new FixedLocalizableString("Can create questions"));
            //context.CreatePermission("CanCreateMenues", new FixedLocalizableString("Can create questions"));
            //context.CreatePermission("CanDeleteMenues", new FixedLocalizableString("Can delete questions"));
            //context.CreatePermission("CanCreateUsers", new FixedLocalizableString("Can create questions"));
            //context.CreatePermission("CanEditUsers", new FixedLocalizableString("Can create questions"));
            //context.CreatePermission("CanDeleteUsers", new FixedLocalizableString("Can create questions"));         
        }

        private object[] SetParameter(ParameterInfo[] paremeters, PermissionGrantInfo permission)
        {
            object[] objlist = new object[paremeters.Length];
            for (int i = 0; i < paremeters.Length; i++)
            {
                objlist[i] = paremeters[i].DefaultValue;
            }
            objlist[0] = permission.Name;

            return objlist;
        }

        public void SetPermissions()
        {
            var rolepermissionslist = IocManager.Instance.Resolve<RoleManager>().GetPermissions();
            var permissionmanager = IocManager.Instance.Resolve<IPermissionManager>();
            if (permissionmanager.GetAllPermissions().Count == 0)
            {
                MethodInfo oMethod = permissionmanager.GetType().GetMethod("CreatePermission", BindingFlags.Instance | BindingFlags.Public);
                var paremeters = oMethod.GetParameters();

                var usermanager = IocManager.Instance.Resolve<UserManager>();
                var userpermissionlist = usermanager.GetAllPermission();
                foreach (var permission in userpermissionlist)
                {
                    oMethod.Invoke(permissionmanager, SetParameter(paremeters, permission));
                }

                foreach (var permission in rolepermissionslist)
                {
                    oMethod.Invoke(permissionmanager, SetParameter(paremeters, permission));
                }
            }

        }
    }
}
