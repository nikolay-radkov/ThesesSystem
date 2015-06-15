using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThesesSystem.Models;
using ThesesSystem.Web.Infrastructure.Mapping;

namespace ThesesSystem.Web.ViewModels.Comments
{
    public class CommentProfileViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentProfileViewModel>()
                .ForMember(c => c.FullName, opt => opt.MapFrom(c => c.User.FirstName + " " + c.User.LastName));
        }
    }
}