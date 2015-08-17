using Abp.Authorization.Users;
using ZKJL.Identity.Core.MultiTenancy;

namespace ZKJL.Identity.Core.Users
{
    public class User : AbpUser<Tenant, User>
    {
        public User()
        {
        }

        public User(string name, string userName, string surName, string emailAddress,int? tenantId)
        {
            Name = name;
            UserName = userName;
            Surname = surName;
            EmailAddress = emailAddress;
            TenantId = tenantId;
            Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==";
        }

        public override string ToString()
        {
            return string.Format("[User {0}] {1}", Id, UserName);
        }
    }
}