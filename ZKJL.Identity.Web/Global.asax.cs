using System;
using System.Data.Entity;
using Abp.Dependency;
using Abp.Web;
using Castle.Facilities.Logging;
using ZKJL.Identity.EntityFramework.EntityFramework;

namespace ZKJL.Identity.Web
{
    public class MvcApplication : AbpWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer<IdentityDbContext>(null);
            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            base.Application_Start(sender, e);
        }
    }
}
