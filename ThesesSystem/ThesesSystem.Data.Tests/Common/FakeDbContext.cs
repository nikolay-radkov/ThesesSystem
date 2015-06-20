namespace ThesesSystem.Data.Tests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Data.TableGenerator;
    using ThesesSystem.Models;

    public class FakeDbContext : IThesesSystemDbContext
    {
        public FakeDbContext()
        {
            GenerateDbSets();
            PopulateData();
        }
        
        private void GenerateDbSets()
        {
            this.Faculties = new FakeDbSet<Faculty>();
            this.Specialties = new FakeDbSet<Specialty>();
            this.Students = new FakeDbSet<Student>();
            this.Teachers = new FakeDbSet<Teacher>();
            this.ThesisThemes = new FakeDbSet<ThesisTheme>();

            this.ThesisTutorials = new FakeDbSet<ThesisTutorial>();
            this.Theses = new FakeDbSet<Thesis>();
            this.Comments = new FakeDbSet<Comment>();
            this.Versions = new FakeDbSet<ThesesSystem.Models.Version>();
            this.ThesisParts = new FakeDbSet<ThesisPart>();
            this.Evaluations = new FakeDbSet<Evaluation>();
            this.Messages = new FakeDbSet<Message>();
            this.Notifications = new FakeDbSet<Notification>();
            this.ThesisLogs = new FakeDbSet<ThesisLog>();
            this.Users = new FakeDbSet<User>();
        }

        private void PopulateData()
        {
            var generator = DefaultRandomGenerator.Instance;

            ICollection<ITableGenerator> populators = new List<ITableGenerator>()
                {
                     new FacultyGenerator(generator, this),
                     new SpecialtyGenerator(generator,this),
                     new UserGenerator(generator, this),
                     new ThesisGenerator(generator, this),
                     new ThesisThemeGenerator(generator, this)
                };

            foreach (var populator in populators)
            {
                populator.Generate();
                populator.Context.SaveChanges();
            }
        }

        
        public IDbSet<Faculty> Faculties { get; set; }

        public IDbSet<Specialty> Specialties { get; set; }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Teacher> Teachers { get; set; }

        public IDbSet<ThesisTheme> ThesisThemes { get; set; }

        public IDbSet<ThesisTutorial> ThesisTutorials { get; set; }

        public IDbSet<Thesis> Theses { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<ThesesSystem.Models.Version> Versions { get; set; }

        public IDbSet<ThesisPart> ThesisParts { get; set; }

        public IDbSet<Evaluation> Evaluations { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        public IDbSet<ThesisLog> ThesisLogs { get; set; }
        
        public IDbSet<User> Users { get; set;}

        public IDbSet<T> Set<T>() where T : class
        {
            foreach (PropertyInfo property in typeof(FakeDbContext).GetProperties())
            {
                if (property.PropertyType == typeof(IDbSet<T>))
                    return property.GetValue(this, null) as IDbSet<T>;
            }
            throw new Exception("Type collection not found");
        }

        public int SaveChanges()
        {
            return 0;
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }


        public DbContext DbContext
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
           
        }
    }
}
