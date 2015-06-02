namespace ThesesSystem.Data.TableGenerator
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Models;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RoleGenerator : TableGenerator, ITableGenerator
    {
        private const int MaxCount = 3;
        private const string STUDENT = "Student";
        private const string TEACHER = "Teacher";
        private const string ADMIN = "Admin";

        public RoleGenerator(IRandomGenerator generator, IThesesSystemDbContext companyContext)
            : base(generator, companyContext, MaxCount)
        { }

        public override void Generate()
        {
            var userManager = new UserManager<User>(new UserStore<User>(this.Context.DbContext));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.Context.DbContext));

            roleManager.Create(new IdentityRole(STUDENT));
            roleManager.Create(new IdentityRole(TEACHER));
            roleManager.Create(new IdentityRole(ADMIN));


            var userIds = this.Context.Users
                .Where(u => u.Student != null)
                .Select(s => s.Id).ToArray();

            var teacherIds = this.Context.Users
                .Where(u => u.Teacher != null)
                .Select(t => t.Id).ToArray();

            for (int index = 0; index < userIds.Length; index++)
            {
                var roleresult = userManager.AddToRole(userIds[index], STUDENT);
            }

            for (int index = 0; index < teacherIds.Length; index++)
            {
                var roleresult = userManager.AddToRole(teacherIds[index], TEACHER);
            }

            this.Context.SaveChanges();
        }
    }
}
