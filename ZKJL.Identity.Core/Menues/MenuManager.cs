using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Dependency;
using Abp.Localization;

namespace ZKJL.Identity.Core.Menues
{
    public class MenuManager : ITransientDependency
    {
        public void SetMenu(MenuDefinition mainMenu, IEnumerable<MenuDto> menues)
        {
            foreach (var menu in menues)
            {
                var menuitem = new MenuItemDefinition(
                    menu.Name,
                    new LocalizableString(menu.DisplayName, IdentityConsts.LocalizationSourceName),
                    url: menu.Url,
                    icon: menu.Icon
                    );
                mainMenu.AddItem(menuitem);

                if (menu.Items.Count > 0)
                {
                    SetMenu(menuitem, menu.Items);
                }
            }
        }

        public void SetMenu(MenuItemDefinition mainMenu, IEnumerable<MenuDto> menues)
        {
            foreach (var menu in menues)
            {
                var menuitem = new MenuItemDefinition(
                    menu.Name,
                    new LocalizableString(menu.DisplayName, IdentityConsts.LocalizationSourceName),
                    url: menu.Url,
                    icon: menu.Icon
                    );
                mainMenu.AddItem(menuitem);

                if (menu.Items.Count > 0)
                {
                    SetMenu(menuitem, menu.Items);
                }
            }
        }
    }
}
