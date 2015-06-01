namespace ThesesSystem.Data
{
    using ThesesSystem.Contracts;
    using ThesesSystem.Models;

    public interface IThesesSystemData
    {
        IThesesSystemDbContext Context { get; }

        IDeletableEntityRepository<Faculty> Faculties { get; }

        IDeletableEntityRepository<Specialty> Specialties { get; }

        IDeletableEntityRepository<Student> Students { get; }

        IDeletableEntityRepository<Teacher> Teachers { get; }

        IDeletableEntityRepository<ThesisTheme> ThesisThemes { get; }

        IDeletableEntityRepository<ThesisTutorial> ThesisTutorials { get; }

        IDeletableEntityRepository<Thesis> Theses { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<Version> Versions { get; }

        IDeletableEntityRepository<ThesisPart> ThesisParts { get; }

        IDeletableEntityRepository<Evaluation> Evaluations { get; }


        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
