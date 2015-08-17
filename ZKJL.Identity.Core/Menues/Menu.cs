using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Collections.Extensions;
using Abp.Domain.Entities.Auditing;
using Abp.Localization;
using Abp.Runtime.Session;
using Castle.Core.Internal;
using ZKJL.Identity.Core.MultiTenancy;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Core.Menues
{
    [Table("Menues")]
    public class Menu : CreationAuditedEntity<int, User>
    {
        /// <summary>
        /// Tenant of this user.
        /// </summary>
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }

        /// <summary>
        /// Tenant Id of this user.
        /// </summary>
        public  int? TenantId { get; set; }

        /// <summary>
        /// Unique name of the menu item in the application. 
        /// Can be used to find this menu item later.
        /// </summary>
        [Required]
        [StringLength(32)]
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the menu item. Required.
        /// </summary>
        [Required]
        [StringLength(64)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The Display order of the menu. Optional.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Icon of the menu item if exists. Optional.
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Icon { get; set; }

        /// <summary>
        /// The URL to navigate when this menu item is selected. Optional.
        /// </summary>
        [Required]
        [StringLength(512)]
        public string Url { get; set; }

        /// <summary>
        /// A permission name. Only users that has this permission can see this menu item.
        /// Optional.
        /// </summary>
        [StringLength(128)]
        public string RequiredPermissionName { get; set; }

        /// <summary>
        /// This can be set to true if only authenticated users should see this menu item.
        /// </summary>
        public bool RequiresAuthentication { get; set; }

        /// <summary>
        /// Returns true if this menu item has no child <see cref="Items"/>.
        /// </summary>
        public bool IsLeaf
        {
            get { return Items.IsNullOrEmpty(); }
        }

        /// <summary>
        /// Can be used to store a custom object related to this menu item. Optional.
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 上面的父节点
        /// </summary>      
        [ForeignKey("ParentId")]
        public virtual Menu Parent { get; set; }


        /// <summary>
        /// Sub items of this menu item. Optional.
        /// </summary>

        public virtual IList<Menu> Items { get; private set; }

        public Menu()
        {
        }

        /// <summary>
        /// Creates a new <see cref="MenuItemDefinition"/> object.
        /// </summary>
        public Menu(string name, string displayName, int? tenantId, int? parentId = null, string icon = null, string url = null,
            bool requiresAuthentication = false, string requiredPermissionName = null, int order = 0,
            object customData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (displayName == null)
            {
                throw new ArgumentNullException("displayName");
            }

            Name = name;
            DisplayName = displayName;
            Icon = icon;
            Url = url;
            RequiresAuthentication = requiresAuthentication;
            RequiredPermissionName = requiredPermissionName;
            Order = order;
            CustomData = customData;
            TenantId = tenantId;
            ParentId = parentId;

            Items = new List<Menu>();
        }

        /// <summary>
        /// Adds a <see cref="MenuItemDefinition"/> to <see cref="Items"/>.
        /// </summary>
        /// <param name="menuItem"><see cref="MenuItemDefinition"/> to be added</param>
        /// <returns>This <see cref="MenuItemDefinition"/> object</returns>
        public Menu AddItem(Menu menuItem)
        {
            Items.Add(menuItem);
            return this;
        }
    }
}
