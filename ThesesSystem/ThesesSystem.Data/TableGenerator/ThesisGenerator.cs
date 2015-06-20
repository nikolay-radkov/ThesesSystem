namespace ThesesSystem.Data.TableGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ThesesSystem.Common.DataGenerators;
    using ThesesSystem.Models;

    public class ThesisGenerator : TableGenerator, ITableGenerator
    {
        private const int MaxCount = 50;

        public ThesisGenerator(IRandomGenerator generator, IThesesSystemDbContext companyContext)
            : base(generator, companyContext, MaxCount)
        { }

        public override void Generate()
        {
            var userIds = this.Context.Users
                .Where(u => u.Student != null)
                .Select(u => u.Id).ToArray();

            var supervisorIds = this.Context.Users
                .Where(u => u.Teacher != null)
                .Select(u => u.Id).ToArray();

            for (int index = 0; index < this.Count / 2; index++)
            {
                var thesis = new Thesis()
                {
                    FinalEvaluation = (float)this.Generator.GenerateRealNumber(2, 6),
                    FinishedAt = DateTime.Now.AddDays(this.Generator.GenerateNumber(2, 400)),
                    Description = this.Generator.GenerateString(3, 30),
                    StudentId = userIds[index],
                    Title = this.Generator.GenerateString(3, 30),
                    SupervisorId = supervisorIds[index],
                    Pages = this.Generator.GenerateNumber(40, 60),
                    IsComplete = true,
                    Versions = new HashSet<ThesesSystem.Models.Version>()
                    {
                        new ThesesSystem.Models.Version {
                            FileExtension = "rar",
                            FileName = "Дипломна работа.rar",
                            StoragePath = "/uploads/14.6.2015 г. 16.21.15+Дипломна работа.rar"
                        }
                    }
                };

                this.Context.Theses.Add(thesis);
            }

            for (int index = 0; index < this.Count / 2; index++)
            {
                var thesis = new Thesis()
                {
                    StudentId = userIds[this.Count / 2 + index],
                    Title = this.Generator.GenerateString(3, 30),
                    Description = this.Generator.GenerateString(3, 30),
                    SupervisorId = supervisorIds[this.Count / 2 + index],
                    IsComplete = false
                };

                this.Context.Theses.Add(thesis);
            }
        }
    }
}
