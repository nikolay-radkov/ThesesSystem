namespace ThesesSystem.Data.TableGenerator
{
    using System.Collections.Generic;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Models;
    using System.Linq;

    public class ThesisThemeGenerator : TableGenerator, ITableGenerator
    {
       private const int MaxCount = 5;

       public ThesisThemeGenerator(IRandomGenerator generator, IThesesSystemDbContext companyContext)
            : base(generator, companyContext, MaxCount)
        { }

        public override void Generate()
        {
            var teachersIds = this.Context.Teachers.Select(t => t.Id).ToArray();

            for (int i = 0; i < teachersIds.Length; i++)
            {
                var teachersId = teachersIds[i];

                for (int index = 0; index < this.Count; index++)
                {
                    var title = this.Generator.GenerateString(2, 20);

                    var thesisTheme = new ThesisTheme()
                    {
                        Title = title,
                        TeacherId = teachersId,
                        IsTaken = false
                    };

                    this.Context.ThesisThemes.Add(thesisTheme);
                }
            }

        }

    }
}
