namespace ThesesSystem.Web.ViewModels.Specialties
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class FacultySpecialtyViewModel :IMapFrom<Specialty>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}