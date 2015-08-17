using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Zero;
using ZKJL.Identity.Application.Authorization;
using ZKJL.Identity.Application.Configuration;

namespace ZKJL.Identity.Application
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(AbpAutoMapperModule))]
    public class IdentityApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Configuration.Authorization.Providers.Add<IdentityAuthorizationProvider>();
            Configuration.Settings.Providers.Add<MySettingProvider>();
        }
    }
}
