namespace ThesesSystem.Data.TableGenerator
{
    using System.Collections.Generic;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Models;
    using System.Linq;

    public class SpecialtyGenerator : TableGenerator, ITableGenerator
    {
        private const int MaxCount = 5;

        public SpecialtyGenerator(IRandomGenerator generator, IThesesSystemDbContext companyContext)
            : base(generator, companyContext, MaxCount)
        { }

        public override void Generate()
        {
            var facultyIds = this.Context.Faculties.Select(e => e.Id).ToArray();

            for (int i = 0; i < facultyIds.Length; i++)
            {
                var facultyId = facultyIds[i];

                for (int index = 0; index < this.Count; index++)
                {
                    var title = this.Generator.GenerateString(2, 20);
                    
                    var specialty = new Specialty()
                    {
                        Title = title,
                        FacultyId = facultyId
                    };

                    this.Context.Specialties.Add(specialty);
                }
            }

            this.Context.SaveChanges();
        }

    }
}
