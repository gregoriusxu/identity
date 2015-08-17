using Abp.Application.Services;
using ZKJL.Identity.Core;

namespace ZKJL.Identity.Application
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class IdentityAppServiceBase : ApplicationService
    {
        protected IdentityAppServiceBase()
        {
            LocalizationSourceName = IdentityConsts.LocalizationSourceName;
        }
    }
}