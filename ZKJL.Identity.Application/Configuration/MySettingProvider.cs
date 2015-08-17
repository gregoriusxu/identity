using System.Collections.Generic;
using Abp.Configuration;

namespace ZKJL.Identity.Application.Configuration
{
    public class MySettingProvider : SettingProvider
    {
        public const string DefaultPageSize = "DefaultPageSize";

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       new SettingDefinition(DefaultPageSize, "10")
                   };
        }
    }
}
