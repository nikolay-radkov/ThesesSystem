namespace ThesesSystem.Data.TableGenerator
{
    using System.Collections.Generic;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Models;

    public class FacultyGenerator : TableGenerator, ITableGenerator
    {
        private const int MaxCount = 5;

        public FacultyGenerator(IRandomGenerator generator, IThesesSystemDbContext companyContext)
            : base(generator, companyContext, MaxCount)
        { }

        public override void Generate()
        {
            for (int index = 0; index < this.Count; index++)
            {
                var title = this.Generator.GenerateString(2, 20);

                var faculty = new Faculty()
                {
                    Title = title
                };

                this.Context.Faculties.Add(faculty);
            }
        }
    }
}
