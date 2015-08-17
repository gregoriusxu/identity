using System.Reflection;
using Abp.Dependency;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Castle.Windsor.Installer;
using ZKJL.Identity.Core;

namespace ZKJL.Identity.EntityFramework
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(IdentityCoreModule))]
    public class IdentityDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
