using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ThesesSystem.Models;
using ThesesSystem.Web.Infrastructure.Mapping;

namespace ThesesSystem.Web.ViewModels.Theses
{
    public class DevThesisIndexViewModel : IMapFrom<Thesis>, IHaveCustomMappings
    {
        public int  Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name="ОКС")]
        public ThesesSystem.Models.Oks Oks { get; set; }

        public int CompletedPercent { get; set; }

        public int? DaysDeveloping { get; set; }

        public string StudentName { get; set; }

        public string StudentId { get; set; }

        public string SupervisorName { get; set; }

        public string SupervisorId { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Thesis, DevThesisIndexViewModel>()
              .ForMember(u => u.StudentName, opt => opt.MapFrom(u => u.Student.User.FirstName + " " + u.Student.User.LastName))
              .ForMember(u => u.SupervisorName, opt => opt.MapFrom(u => u.Supervisor.User.FirstName + " " + u.Supervisor.User.LastName))
              .ForMember(u => u.Oks, opt => opt.MapFrom(u => u.Student.Oks))
              .ForMember(u => u.DaysDeveloping, opt => opt.MapFrom(u => DbFunctions.DiffDays(u.CreatedOn, DateTime.Now)))
              .ForMember(u => u.CompletedPercent, opt => opt.MapFrom(u => 
                  u.ThesisParts.Count() == 0 ? 0 : (int)(u.ThesisParts.Where(p => p.Flag == ThesisFlag.Complete).Count() / (double)u.ThesisParts.Count() * 100)));
        }
    }
}