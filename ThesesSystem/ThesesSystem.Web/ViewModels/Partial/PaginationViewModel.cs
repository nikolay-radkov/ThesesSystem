namespace ThesesSystem.Web.ViewModels.Partial
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }

        public bool HasPrevious { get; set; }

        public bool HasNext { get; set; }

        public int PagesNumber { get; set; }
    }
}