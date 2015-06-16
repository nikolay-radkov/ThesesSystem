namespace ThesesSystem.Web.ViewModels.Specialties
{
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Models;

    public class SpecialtyResultViewModel : IMapFrom<Specialty>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}