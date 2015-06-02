namespace ThesesSystem.Web.ViewModels.Theses
{
    using System;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisViewModel : IMapFrom<Thesis>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string StudentName { get; set; }

        public string SupervisorName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Thesis, ThesisViewModel>()
               .ForMember(u => u.StudentName, opt => opt.MapFrom(u => u.Student.User.FirstName + " " + u.Student.User.LastName))
               .ForMember(u => u.SupervisorName, opt => opt.MapFrom(u => u.Supervisor.User.FirstName + " " + u.Supervisor.User.LastName));
        }
    }
}