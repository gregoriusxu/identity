using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Abp.Application.Navigation;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Runtime.Session;
using ZKJL.Identity.Application.Menues;
using ZKJL.Identity.Core;
using ZKJL.Identity.Core.Menues;

namespace ZKJL.Identity.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class IdentityNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            //context.Manager.MainMenu
            //    .AddItem(
            //        new MenuItemDefinition(
            //            "Menues",
            //            new LocalizableString("Menues", IdentityConsts.LocalizationSourceName),
            //            url: "#/menues",
            //            icon: "fa fa-question"
            //            ).AddItem(new MenuItemDefinition(
            //            "Menues1",
            //            new LocalizableString("Menues", IdentityConsts.LocalizationSourceName),
            //            url: "#/menues1",
            //            icon: "fa fa-question"
            //            )
            //            )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "Users",
            //            new LocalizableString("Users", IdentityConsts.LocalizationSourceName),
            //            url: "#/users",
            //            icon: "fa fa-users"
            //            )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "Roles",
            //            new LocalizableString("Roles", IdentityConsts.LocalizationSourceName),
            //            url: "#/roles",
            //            icon: "fa fa-roles"
            //            )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "UserRoles",
            //            new LocalizableString("UserRoles", IdentityConsts.LocalizationSourceName),
            //            url: "#/userroles",
            //            icon: "fa fa-userroles"
            //            )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "MenuRoles",
            //            new LocalizableString("MenuRoles", IdentityConsts.LocalizationSourceName),
            //            url: "#/menuroles",
            //            icon: "fa fa-menuroles"
            //            )
            //    );
        }
    }
}
