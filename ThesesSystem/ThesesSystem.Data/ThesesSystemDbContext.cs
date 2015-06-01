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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ThesesSystemDbContext, Configuration>());
        }

        public ThesesSystemDbContext()
            : this("DefaultConnection")
        {
        }

        // TODO: Add tables db sets

        public virtual IDbSet<Faculty> Faculties { get; set; }

        public virtual IDbSet<Specialty> Specialties { get; set; }

        public virtual IDbSet<Student> Students { get; set; }

        public virtual IDbSet<Teacher> Teachers { get; set; }

        public virtual IDbSet<ThesisTheme> ThesisThemes { get; set; }

        public virtual IDbSet<ThesisTutorial> ThesisTutorials { get; set; }

        public virtual IDbSet<Thesis> Theses { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<ThesesSystem.Models.Version> Versions { get; set; }

        public virtual IDbSet<ThesisPart> ThesisParts { get; set; }

        public virtual IDbSet<Evaluation> Evaluations { get; set; }


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
            // Many-to-Many relationship with external table
            modelBuilder.Entity<Message>()
                   .HasRequired(m => m.FromUser)
                   .WithMany(u => u.UserMessages)
                   .HasForeignKey(m => m.FromUserId)
                   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                        .HasRequired(m => m.ToUser)
                        .WithMany(u => u.ToUserMessages)
                        .HasForeignKey(m => m.ToUserId)
                        .WillCascadeOnDelete(false);

            // Many-to-Many relationship without external table
            modelBuilder.Entity<User>()
               .HasMany(u => u.Friends)
               .WithMany()
               .Map(m =>
               {
                   m.MapLeftKey("Id");
                   m.MapRightKey("FriendId");
                   m.ToTable("UserFriends");
               });

            // One-to-One relationships
            modelBuilder.Entity<Student>()
                .HasRequired(s => s.User)
                .WithOptional(u => u.Student);

            modelBuilder.Entity<Teacher>()
               .HasRequired(s => s.User)
               .WithOptional(u => u.Teacher);

            modelBuilder.Entity<Evaluation>()
             .HasRequired(e => e.Thesis)
             .WithOptional(t => t.Evaluation);

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
