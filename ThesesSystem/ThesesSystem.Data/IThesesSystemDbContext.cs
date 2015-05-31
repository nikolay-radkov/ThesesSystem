namespace ThesesSystem.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ThesesSystem.Models;

    public interface IThesesSystemDbContext
    {
        IDbSet<User> Users { get; set; }

       DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
