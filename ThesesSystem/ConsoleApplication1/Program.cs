namespace ConsoleApplication1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Data.TableGenerator;
    using System.Web;

    public class Program
    {


        private const string App_key = "g2hfafbhxjfb2e6";
        private const string App_secret = "mg5fjhl35rg23io";
        public static string Token { get; set; }
        public static string TokenSecret { get; set; }

        public static void Main(string[] args)
        {
           


            //var generator = DefaultRandomGenerator.Instance;
            //var context = new ThesesSystem.Data.ThesesSystemDbContext();


            //ICollection<ITableGenerator> populators = new List<ITableGenerator>()
            //{
            //   // new FacultyGenerator(generator, context),
            //  // new SpecialtyGenerator(generator,context),
            //// new UserGenerator(generator, context),
            //  //  new ThesisGenerator(generator, context),
            //   // new RoleGenerator(generator, context),
            //  // new ThesisThemeGenerator(generator, context)
            //};

            //foreach (var populator in populators)
            //{
            //    populator.Generate();
            //    populator.Context.SaveChanges();
            //}

        }
    }
}
