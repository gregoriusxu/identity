using System.Reflection;
using Abp.Modules;
using Abp.Zero;

namespace ZKJL.Identity.Core
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class IdentityCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
