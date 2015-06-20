namespace ThesesSystem.Data.Tests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ThesesSystem.Contracts;
    using ThesesSystem.Data.Repositories.Base;
    using ThesesSystem.Models;

    public class FakeData : IThesesSystemData
    {
        private readonly IThesesSystemDbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public FakeData(IThesesSystemDbContext context)
        {
            this.context = context;
        }
        public IThesesSystemDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IDeletableEntityRepository<Faculty> Faculties
        {
            get { return this.GetDeletableEntityRepository<Faculty>(); }
        }

        public IDeletableEntityRepository<Specialty> Specialties
        {
            get { return this.GetDeletableEntityRepository<Specialty>(); }
        }

        public IDeletableEntityRepository<Student> Students
        {
            get { return this.GetDeletableEntityRepository<Student>(); }
        }

        public IDeletableEntityRepository<Teacher> Teachers
        {
            get { return this.GetDeletableEntityRepository<Teacher>(); }
        }

        public IDeletableEntityRepository<ThesisTheme> ThesisThemes
        {
            get { return this.GetDeletableEntityRepository<ThesisTheme>(); }
        }

        public IDeletableEntityRepository<ThesisTutorial> ThesisTutorials
        {
            get { return this.GetDeletableEntityRepository<ThesisTutorial>(); }
        }

        public IDeletableEntityRepository<Thesis> Theses
        {
            get { return this.GetDeletableEntityRepository<Thesis>(); }
        }

        public IDeletableEntityRepository<Comment> Comments
        {
            get { return this.GetDeletableEntityRepository<Comment>(); }
        }

        public IDeletableEntityRepository<ThesesSystem.Models.Version> Versions
        {
            get { return this.GetDeletableEntityRepository<ThesesSystem.Models.Version>(); }
        }

        public IDeletableEntityRepository<ThesisPart> ThesisParts
        {
            get { return this.GetDeletableEntityRepository<ThesisPart>(); }
        }

        public IDeletableEntityRepository<Evaluation> Evaluations
        {
            get { return this.GetDeletableEntityRepository<Evaluation>(); }
        }

        public IDeletableEntityRepository<Notification> Notifications
        {
            get { return this.GetDeletableEntityRepository<Notification>(); }
        }

        public IDeletableEntityRepository<ThesisLog> ThesisLogs
        {
            get { return this.GetDeletableEntityRepository<ThesisLog>(); }
        }

        public IDeletableEntityRepository<Message> Messages
        {
            get { return this.GetDeletableEntityRepository<Message>(); }
        }


        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
