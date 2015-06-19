namespace ThesesSystem.Data.TableGenerator
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Models;
    using System.Linq;
    using System.Data.Entity.Validation;

    public class UserGenerator : TableGenerator, ITableGenerator
    {
        private const int MaxCount = 100;

        public UserGenerator(IRandomGenerator generator, IThesesSystemDbContext companyContext)
            : base(generator, companyContext, MaxCount)
        { }

        public override void Generate()
        {
            var passwordHash = new PasswordHasher();
            var specialtyIds = this.Context.Specialties.Select(f => f.Id).ToArray();

            for (int index = 0; index < this.Count / 2; index++)
            {
                var user = new User()
                {
                    UserName = "aafa" + index + "@aa" + index + ".com",
                    Email = "aafa" + index + "@aa" + index + ".com",
                    FirstName = this.Generator.GenerateString(4, 10),
                    MiddleName = this.Generator.GenerateString(4, 10),
                    LastName = this.Generator.GenerateString(4, 10),
                    IsVerified = true,
                    EGN = this.Generator.GenerateNumber(10000000, 90000000),
                    PhoneNumber = this.Generator.GenerateNumber(10000000, 90000000).ToString(),
                    
                    Student = new Student()
                    {
                        FacultyNumber = this.Generator.GenerateNumber(10000000, 90000000),
                        Oks = Oks.Bachelor,
                        SpecialtyGroup = this.Generator.GenerateNumber(1, 50),
                        SpecialtyId = specialtyIds[index % (specialtyIds.Length - 1)]
                    },
                    PasswordHash = passwordHash.HashPassword("123456"),
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                this.Context.Users.Add(user);
              
            }

         
            try
            {
                this.Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var s = e.EntityValidationErrors;
            }

            for (int index = 0; index < this.Count / 2; index++)
            {
                var user = new User()
                {
                    UserName = "cc" + index + "@cc" + index + ".com",
                    Email = "cc" + index + "@cc" + index + ".com",
                    FirstName = this.Generator.GenerateString(4, 10),
                    MiddleName = this.Generator.GenerateString(4, 10),
                    LastName = this.Generator.GenerateString(4, 10),
                    IsVerified = true,
                    EGN = this.Generator.GenerateNumber(10000000, 90000000),
                    PhoneNumber = this.Generator.GenerateNumber(10000000, 90000000).ToString(),
                    Teacher = new Teacher()
                    {
                        Cabinet = this.Generator.GenerateNumber(1, 300),
                        OfficePhoneNumber = this.Generator.GenerateNumber(2000, 90000).ToString()
                    },
                    PasswordHash = passwordHash.HashPassword("123456"),
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                this.Context.Users.Add(user);
            }

            this.Context.SaveChanges();
        }
    }
}
