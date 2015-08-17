using System.Linq;
using Abp.Application.Navigation;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Localization;
using EntityFramework.DynamicFilters;
using ZKJL.Identity.Core;
using ZKJL.Identity.Core.Authorization;
using ZKJL.Identity.Core.Menues;
using ZKJL.Identity.Core.MultiTenancy;
using ZKJL.Identity.Core.Users;
using ZKJL.Identity.EntityFramework.EntityFramework;

namespace ZKJL.Identity.EntityFramework.Migrations.Data
{
    public class InitialDataBuilder
    {
        public void Build(IdentityDbContext context)
        {
            context.DisableAllFilters();
            CreateUserAndRoles(context);
        }

        private void CreateUserAndRoles(IdentityDbContext context)
        {
            //Admin role for tenancy owner

            var adminRoleForTenancyOwner = context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == "Admin");
            if (adminRoleForTenancyOwner == null)
            {
                adminRoleForTenancyOwner = context.Roles.Add(new Role(null, "Admin", "Admin"));
                context.SaveChanges();

                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForTenancyOwner.Id, Name = "CanCreateUsers", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForTenancyOwner.Id, Name = "CanEditUsers", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForTenancyOwner.Id, Name = "CanDeleteUsers", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForTenancyOwner.Id, Name = "CanDeleteRoles", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForTenancyOwner.Id, Name = "CanCreateMenues", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForTenancyOwner.Id, Name = "CanEditMenues", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForTenancyOwner.Id, Name = "CanDeleteMenues", IsGranted = true });
                context.SaveChanges();
            }

            //Admin user for tenancy owner

            var adminUserForTenancyOwner = context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == "admin");
            if (adminUserForTenancyOwner == null)
            {
                adminUserForTenancyOwner = context.Users.Add(
                    new User
                    {
                        TenantId = null,
                        UserName = "admin",
                        Name = "System",
                        Surname = "Administrator",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });

                context.SaveChanges();

                context.UserRoles.Add(new UserRole(adminUserForTenancyOwner.Id, adminRoleForTenancyOwner.Id));

                context.SaveChanges();
            }

            //Default tenant

            var defaultTenant = context.Tenants.FirstOrDefault(t => t.TenancyName == "Default");
            if (defaultTenant == null)
            {
                defaultTenant = context.Tenants.Add(new Tenant("Default", "Default"));
                context.SaveChanges();
            }

            //Admin role for 'Default' tenant

            var adminRoleForDefaultTenant = context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == "Admin");
            if (adminRoleForDefaultTenant == null)
            {
                adminRoleForDefaultTenant = context.Roles.Add(new Role(defaultTenant.Id, "Admin", "Admin"));
                context.SaveChanges();

                //Permission definitions for Admin of 'Default' tenant
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanCreateUsers", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanEditUsers", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanDeleteUsers", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanDeleteRoles", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanCreateMenues", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanEditMenues", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanDeleteMenues", IsGranted = true });
                context.SaveChanges();
            }

            //User role for 'Default' tenant

            var userRoleForDefaultTenant = context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == "User");
            if (userRoleForDefaultTenant == null)
            {
                userRoleForDefaultTenant = context.Roles.Add(new Role(defaultTenant.Id, "User", "User"));
                context.SaveChanges();

                //Permission definitions for User of 'Default' tenant
                //context.Permissions.Add(new RolePermissionSetting { RoleId = userRoleForDefaultTenant.Id, Name = "CanCreateMenues", IsGranted = true });
                //context.SaveChanges();
            }

            //Admin for 'Default' tenant

            var adminUserForDefaultTenant = context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "admin");
            if (adminUserForDefaultTenant == null)
            {
                adminUserForDefaultTenant = context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "admin",
                        Name = "System",
                        Surname = "Administrator",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                context.SaveChanges();

                context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, adminRoleForDefaultTenant.Id));
                context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();

            }

            //User 'Emre' for 'Default' tenant

            var emreUserForDefaultTenant = context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "emre");
            if (emreUserForDefaultTenant == null)
            {
                emreUserForDefaultTenant = context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "emre",
                        Name = "Yunus Emre",
                        Surname = "Kalkan",
                        EmailAddress = "emre@aspnetboilerplate.com",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                context.SaveChanges();

                context.UserRoles.Add(new UserRole(emreUserForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();
            }

            //Menu
            context.Menues.Add(new Menu(
                        "Menues",
                        "Menues",
                        defaultTenant.Id,
                        url: "#/menues",
                        icon: "fa fa-file",
                        requiredPermissionName: "Menues"
                        )
                        );
            context.Menues.Add(new Menu(
                "Users",
                "Users",
                defaultTenant.Id,
                url: "#/users",
                icon: "fa fa-user",
                requiredPermissionName: "Users"
                )
                );
            context.Menues.Add(new Menu(
                "Roles",
                "Roles",
                defaultTenant.Id,
                url: "#/roles",
                icon: "fa fa-group",
                requiredPermissionName: "Roles"
                )
                );
            context.Menues.Add(new Menu(
                    "UserRoles",
                    "UserRoles",
                    defaultTenant.Id,
                    url: "#/userroles",
                    icon: "fa fa-userplus",
                    requiredPermissionName: "UserRoles"
                    )
                );
            context.Menues.Add(new Menu(
                    "MenuRoles",
                    "MenuRoles",
                    defaultTenant.Id,
                    url: "#/menuroles",
                    icon: "fa fa-gears",
                    requiredPermissionName: "MenuRoles"
                    ));
            context.SaveChanges();
        }
    }
}