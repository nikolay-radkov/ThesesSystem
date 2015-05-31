namespace ThesesSystem.Data
{
    using ThesesSystem.Contracts;
    using ThesesSystem.Models;

    public interface IThesesSystemData
    {
        IThesesSystemDbContext Context { get; }

        IDeletableEntityRepository<Faculty> Faculties { get; }

        IDeletableEntityRepository<Specialty> Specialties { get; }
        
        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
