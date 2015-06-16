namespace ThesesSystem.Web.Infrastructure.Helper
{
    using System;
    using System.Linq;
    using ThesesSystem.Data;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.ViewModels.Partials;

    public static class PaginationHelper
    {
        public static PaginationViewModel CreatePagination(string action, string controller, IThesesSystemData data, int currentPage)
        {
            var pagesNumber = GetPages(data, controller + action);

            var hasNextPage = currentPage != (pagesNumber - 1) && pagesNumber != 0;
            var hasPreviousPage = currentPage != 0;

            var pagination = new PaginationViewModel
            {
                CurrentPage = currentPage,
                PagesNumber = pagesNumber,
                HasNext = hasNextPage,
                HasPrevious = hasPreviousPage,
                Action = action,
                Controller = controller
            };

            return pagination;
        }

        private static int GetPages(IThesesSystemData data, string action)
        {
            int pageNumber = 0;

            switch (action)
            {
                case "StorageIndex":
                    pageNumber = (int)Math.Ceiling((double)data.Theses.All().Where(t => t.IsComplete).Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                    break;
                case "UserVerification" :
                    pageNumber = (int)Math.Ceiling((double)data.Users.All().Where(u => !u.IsVerified).Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                    break;
                case "IdeaThemes":
                    pageNumber = (int)Math.Ceiling((double)data.ThesisThemes.All().Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                    break;
                case "TutorialIndex":
                    pageNumber = (int)Math.Ceiling((double)data.ThesisTutorials.All().Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                    break;
                //TODO: Other paging
                default:
                    pageNumber = 0;
                    break;
            }

            return pageNumber;
        }
    }
}