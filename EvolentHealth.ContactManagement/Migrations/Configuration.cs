namespace EvolentHealth.ContactManagement.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EvolentHealth.ContactManagement.Models.EvolentHealthContactManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EvolentHealth.ContactManagement.Models.EvolentHealthContactManagementContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Contacts.AddOrUpdate( new Contact[] {
            
              new Contact { FirstName = "Andrew", LastName= "Peters", Email="apeters@gmail.com", PhoneNumber="488-983-3832", IsActive=true },
              new Contact { FirstName = "Brice", LastName= "Lambson", Email="blambson@gmail.com", PhoneNumber="356-434-3832", IsActive=true   },
              new Contact { FirstName = "Rowan", LastName= "Miller", Email="rmiller@gmail.com", PhoneNumber="533-878-2232", IsActive=true  }
            });

        }
    }
}
