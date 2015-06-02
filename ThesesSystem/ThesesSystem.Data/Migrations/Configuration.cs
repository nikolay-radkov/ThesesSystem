namespace ThesesSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Data.TableGenerator;

    public sealed class Configuration : DbMigrationsConfiguration<ThesesSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ThesesSystemDbContext context)
        {
            var generator = DefaultRandomGenerator.Instance;
       
            context.Configuration.AutoDetectChangesEnabled = false;


            ICollection<ITableGenerator> populators = new List<ITableGenerator>()
            {
                // new FacultyGenerator(generator, context),
                // new SpecialtyGenerator(generator,context),
                // new UserGenerator(generator, context),
                //  new ThesisGenerator(generator, context),
                // new RoleGenerator(generator, context),
                // new ThesisThemeGenerator(generator, context)
            };

            foreach (var populator in populators)
            {
                populator.Generate();
                populator.Context.SaveChanges();
            }

            context.Configuration.AutoDetectChangesEnabled = true;
        }
    }
}
