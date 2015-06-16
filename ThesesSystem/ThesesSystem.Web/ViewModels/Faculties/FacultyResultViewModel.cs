namespace ThesesSystem.Web.ViewModels.Faculties
{
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Models;

    public class FacultyResultViewModel : IMapFrom<Faculty>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}