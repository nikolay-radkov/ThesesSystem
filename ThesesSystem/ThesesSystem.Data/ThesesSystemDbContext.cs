namespace ThesesSystem.Data
{
    using System.Linq;
    using System.Data.Entity;
    //using ThesesSystem.Data.Migrations;
    using ThesesSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ThesesSystem.Contracts.CodeFirstConventions;
    using ThesesSystem.Contracts;
    using System;
    using ThesesSystem.Data.Migrations;

    public class ThesesSystemDbContext : IdentityDbContext<User>, IThesesSystemDbContext
    {
        public ThesesSystemDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ThesesSystemDbContext>());
             // TODO: Change it
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ThesesSystemDbContext, Configuration>());
        }

        public ThesesSystemDbContext()
            : this("DefaultConnection")
        {
        }

        // TODO: Add tables db sets

        public virtual IDbSet<Faculty> Faculties { get; set; }

        public virtual IDbSet<Specialty> Specialties { get; set; }

        public static ThesesSystemDbContext Create()
        {
            return new ThesesSystemDbContext();
        }


        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            base.OnModelCreating(modelBuilder); // Without this call EntityFramework won't be able to configure the identity model
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
