using Abp.Web.Mvc.Views;
using ZKJL.Identity.Core;

namespace ZKJL.Identity.Web.Views
{
    public abstract class IdentityWebViewPageBase : IdentityWebViewPageBase<dynamic>
    {

    }

    public abstract class IdentityWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected IdentityWebViewPageBase()
        {
            LocalizationSourceName = IdentityConsts.LocalizationSourceName;
        }
    }
}