using Abp.Web.Mvc.Controllers;
using ZKJL.Identity.Core;

namespace ZKJL.Identity.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class IdentityControllerBase : AbpController
    {
        protected IdentityControllerBase()
        {
            LocalizationSourceName = IdentityConsts.LocalizationSourceName;
        }
    }
}