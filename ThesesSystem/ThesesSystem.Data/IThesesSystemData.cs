namespace ThesesSystem.Data
{
    using ThesesSystem.Contracts;
    using ThesesSystem.Models;

    public interface IThesesSystemData
    {
        IThesesSystemDbContext Context { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<Evaluation> Evaluations { get; }

        IDeletableEntityRepository<Faculty> Faculties { get; }

        IDeletableEntityRepository<Message> Messages { get; }

        IDeletableEntityRepository<Specialty> Specialties { get; }

        IDeletableEntityRepository<Student> Students { get; }

        IDeletableEntityRepository<Teacher> Teachers { get; }

        IDeletableEntityRepository<Thesis> Theses { get; }

        IDeletableEntityRepository<ThesisPart> ThesisParts { get; }

        IDeletableEntityRepository<ThesisTheme> ThesisThemes { get; }

        IDeletableEntityRepository<ThesisTutorial> ThesisTutorials { get; }

        IDeletableEntityRepository<Version> Versions { get; }

        IDeletableEntityRepository<Notification> Notifications { get; }

        IDeletableEntityRepository<ThesisLog> ThesisLogs { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
