namespace ThesesSystem.Web.Areas.Administration.ViewModels.Theses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisViewModel : IMapFrom<Thesis>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Студент")]
        public string StudentName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Thesis, ThesisViewModel>()
                .ForMember(t => t.StudentName, opt => opt.MapFrom(t => t.Student.User.FirstName + " " + t.Student.User.LastName));
        }
    }
}