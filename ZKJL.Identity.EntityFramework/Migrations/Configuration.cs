using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZKJL.Identity.EntityFramework.EntityFramework;
using ZKJL.Identity.EntityFramework.Migrations.Data;

namespace ZKJL.Identity.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ZKJL.Identity.EntityFramework";
        }

        protected override void Seed(IdentityDbContext context)
        {
            new InitialDataBuilder().Build(context);
        }
    }
}
