using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Application.Navigation;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Localization.Sources.Xml;
using Abp.Modules;
using Abp.Runtime.Session;
using ZKJL.Identity.Application;
using ZKJL.Identity.Core;
using ZKJL.Identity.Core.Menues;
using ZKJL.Identity.EntityFramework;
using ZKJL.Identity.WebApi;

namespace ZKJL.Identity.Web
{
    [DependsOn(typeof(IdentityDataModule), typeof(IdentityApplicationModule), typeof(IdentityWebApiModule))]
    public class IdentityWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "famfamfam-flag-cn"));

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new XmlLocalizationSource(
                    IdentityConsts.LocalizationSourceName,
                    HttpContext.Current.Server.MapPath("~/Localization/ZKJL.Identity.Web")
                    )
                );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<IdentityNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
