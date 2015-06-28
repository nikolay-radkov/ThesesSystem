namespace ThesesSystem.Data.TableGenerator
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Linq;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Models;

    public class RoleGenerator : TableGenerator, ITableGenerator
    {
        private const int MaxCount = 3;
        private const string STUDENT = "Student";
        private const string TEACHER = "Teacher";
        private const string ADMIN = "Admin";
        private const string VERIFIED_USER = "VerifiedUser";
        private const string NOT_VERIFIED_USER = "NotVerifiedUser";
        private const string COMPLETE_USER = "CompleteUser";
        private const string NOT_COMPLETE_USER = "NotCompleteUser";

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
            roleManager.Create(new IdentityRole(VERIFIED_USER));
            roleManager.Create(new IdentityRole(NOT_VERIFIED_USER));
            roleManager.Create(new IdentityRole(COMPLETE_USER));
            roleManager.Create(new IdentityRole(NOT_COMPLETE_USER));

            var userIds = this.Context.Users
                .Where(u => u.Student != null)
                .Select(s => s.Id).ToArray();

            var teacherIds = this.Context.Users
                .Where(u => u.Teacher != null)
                .Select(t => t.Id).ToArray();

            for (int index = 0; index < userIds.Length; index++)
            {
                userManager.AddToRole(userIds[index], STUDENT);
                userManager.AddToRole(userIds[index], VERIFIED_USER);
                userManager.AddToRole(userIds[index], COMPLETE_USER);
            }

            for (int index = 0; index < teacherIds.Length; index++)
            {
                userManager.AddToRole(teacherIds[index], TEACHER);
                userManager.AddToRole(teacherIds[index], VERIFIED_USER);
                userManager.AddToRole(teacherIds[index], COMPLETE_USER);
            }

            this.GenerateAdmin(userManager);
        }

        private void GenerateAdmin(UserManager<User> userManager)
        {
            var passwordHash = new PasswordHasher();
            var user = new User()
            {
                UserName = "admin@thesessystem.com",
                Email = "admin@thesessystem.com",
                FirstName = this.Generator.GenerateString(4, 10),
                MiddleName = this.Generator.GenerateString(4, 10),
                LastName = this.Generator.GenerateString(4, 10),
                EGN = this.Generator.GenerateNumber(10000000, 90000000),
                IsVerified = true,
                PhoneNumber = this.Generator.GenerateNumber(10000000, 90000000).ToString(),
                Teacher = new Teacher()
                {
                    Cabinet = this.Generator.GenerateNumber(1, 300),
                    OfficePhoneNumber = this.Generator.GenerateNumber(2000, 90000).ToString()
                },
                PasswordHash = passwordHash.HashPassword("admin@thesessystem.com"),
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            this.Context.Users.Add(user);
            this.Context.SaveChanges();

            userManager.AddToRole(user.Id, ADMIN);
            userManager.AddToRole(user.Id, TEACHER);
            userManager.AddToRole(user.Id, VERIFIED_USER);
            userManager.AddToRole(user.Id, COMPLETE_USER);
        }
    }
}
