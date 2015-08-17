using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;
using ZKJL.Identity.Application;

namespace ZKJL.Identity.WebApi
{
    [DependsOn(typeof(AbpWebApiModule), typeof(IdentityApplicationModule))]
    public class IdentityWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(IdentityApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
