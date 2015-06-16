using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThesesSystem.Web.ViewModels.Filters
{
    public class FilterOptions
    {
        public string KeyWord { get; set; }

        public FilterBy FilterBy { get; set; }

        public SortBy SortBy { get; set; }

        public SortType SortType { get; set; }
    }
}