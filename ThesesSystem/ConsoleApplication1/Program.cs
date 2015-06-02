namespace ConsoleApplication1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Data.TableGenerator;

    public class Program
    {
        public static void Main(string[] args)
        {
            var generator = DefaultRandomGenerator.Instance;
            var context = new ThesesSystem.Data.ThesesSystemDbContext();


            ICollection<ITableGenerator> populators = new List<ITableGenerator>()
            {
               // new FacultyGenerator(generator, context),
              // new SpecialtyGenerator(generator,context),
            // new UserGenerator(generator, context),
              //  new ThesisGenerator(generator, context),
               // new RoleGenerator(generator, context),
              // new ThesisThemeGenerator(generator, context)
            };

            foreach (var populator in populators)
            {
                populator.Generate();
                populator.Context.SaveChanges();
            }
        }
    }
}
