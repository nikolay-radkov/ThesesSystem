namespace ThesesSystem.Web.ViewModels.Evaluation
{
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Models;
    using System.ComponentModel.DataAnnotations;
    using System;

    public class ReviewViewModel : IMapFrom<Evaluation>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Оценка")]
        public float Mark { get; set; }

        [Display(Name = "Добавена на:")]
        public DateTime CreatedOn { get; set; }

        public string FilePath { get; set; }

        [Display(Name = "Файл:")]
        public string FileName { get; set; }

        public string FileExtension { get; set; }

        [Display(Name = "Рецензент:")]
        public string ReviewerName { get; set; }

        public string ReviewerId{ get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Evaluation, ReviewViewModel>()
               .ForMember(u => u.ReviewerName, opt => opt.MapFrom(u => u.Reviewer.User.FirstName + " " + u.Reviewer.User.LastName));
        }
    }
}