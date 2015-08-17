using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Entities.Auditing;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.Core.MenuRole
{
    [Table("MenuRoles")]
    public class MenuRole : CreationAuditedEntity<long>
    {
        /// <summary>
        /// User id.
        /// </summary>
        public virtual long MenuId { get; set; }

        /// <summary>
        /// Role id.
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// </summary>
        public MenuRole()
        {

        }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="roleId">Role id</param>
        public MenuRole(long userId, int roleId)
        {
            MenuId = userId;
            RoleId = roleId;
        }
    }
}
