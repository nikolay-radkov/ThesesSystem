namespace ThesesSystem.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ThesesSystem.Models;

    public interface IThesesSystemDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Faculty> Faculties { get; set; }

        IDbSet<Specialty> Specialties { get; set; }

        IDbSet<Student> Students { get; set; }

        IDbSet<Teacher> Teachers { get; set; }

        IDbSet<ThesisTheme> ThesisThemes { get; set; }

        IDbSet<ThesisTutorial> ThesisTutorials { get; set; }

        IDbSet<Thesis> Theses { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Version> Versions { get; set; }

        IDbSet<ThesisPart> ThesisParts { get; set; }

        IDbSet<Evaluation> Evaluations { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
