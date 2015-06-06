using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThesesSystem.Models;
using ThesesSystem.Web.Infrastructure.Mapping;

namespace ThesesSystem.Web.ViewModels.Faculties
{
    public class FacultyDropDownListItemViewModel : IMapFrom<Faculty>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}