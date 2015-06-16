namespace ThesesSystem.Web.ViewModels.Search
{
    using System.Collections.Generic;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Web.ViewModels.Faculties;
    using ThesesSystem.Web.ViewModels.Specialties;
    using ThesesSystem.Web.ViewModels.Students;
    using ThesesSystem.Web.ViewModels.Teachers;
    using ThesesSystem.Web.ViewModels.Theses;

    public class SearchResultViewModel : IMapFrom<SearchViewModel>
    {
        public SearchResultViewModel()
        {
            this.Students = new HashSet<StudentResultViewModel>();
            this.Teachers = new HashSet<TeacherResultViewModel>();
            this.Archives = new HashSet<ArchiveResultViewModel>();
            this.Faculties = new HashSet<FacultyResultViewModel>();
            this.Specialties = new HashSet<SpecialtyResultViewModel>();
            this.Themes = new HashSet<ThemeResultViewModel>();
        }

        public int ResultsCount { get; set; }

        public string KeyWord { get; set; }

        public SearchType SearchType { get; set; }

        public ICollection<StudentResultViewModel> Students { get; set; }

        public ICollection<TeacherResultViewModel> Teachers { get; set; }

        public ICollection<FacultyResultViewModel> Faculties { get; set; }

        public ICollection<SpecialtyResultViewModel> Specialties { get; set; }

        public ICollection<ArchiveResultViewModel> Archives { get; set; }

        public ICollection<ThemeResultViewModel> Themes { get; set; }
    }
}