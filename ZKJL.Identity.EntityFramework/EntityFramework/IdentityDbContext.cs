using System.Data.Common;
using System.Data.Entity;
using Abp.Authorization.Users;
using Abp.Zero.EntityFramework;
using ZKJL.Identity.Core.Authorization;
using ZKJL.Identity.Core.Menues;
using ZKJL.Identity.Core.MenuRole;
using ZKJL.Identity.Core.MultiTenancy;
using ZKJL.Identity.Core.Users;

namespace ZKJL.Identity.EntityFramework.EntityFramework
{
    public class IdentityDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for each Entity...
        public virtual IDbSet<Menu> Menues { get; set; }

        public virtual IDbSet<MenuRole> MenuRoles { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public IdentityDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in IdentityDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of IdentityDbContext since ABP automatically handles it.
         */
        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public IdentityDbContext(DbConnection connection)
            : base(connection, true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Menu>()
            .HasMany(menu => menu.Items)
            .WithOptional(menu => menu.Parent);
        }

    }
}
