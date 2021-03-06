﻿namespace ThesesSystem.Web.ViewModels.Faculties
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class FacultyDropDownListItemViewModel : IMapFrom<Faculty>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}