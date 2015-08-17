using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;

namespace ZKJL.Identity.Core.Menues
{
    [AutoMapFrom(typeof(Menu))]
    public class MenuDto : CreationAuditedEntityDto
    {
        /// <summary>
        /// Unique name of the menu item in the application. 
        /// Can be used to find this menu item later.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the menu item. Required.
        /// </summary>
        public string DisplayName { get; set; }

        public int? TenantId { get; set; }

        public int? ParentId { get; set; }

        public MenuDto Parent { get; set; }

        [JsonIgnore]
        public IList<MenuDto> Items { get; set; }

        public bool IsLeaf { get; set; }

        /// <summary>
        /// The Display order of the menu. Optional.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Icon of the menu item if exists. Optional.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// The URL to navigate when this menu item is selected. Optional.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A permission name. Only users that has this permission can see this menu item.
        /// Optional.
        /// </summary>
        public string RequiredPermissionName { get; set; }

        /// <summary>
        /// This can be set to true if only authenticated users should see this menu item.
        /// </summary>
        public bool RequiresAuthentication { get; set; }

        public string CreatorUserName { get; set; }
    }
}