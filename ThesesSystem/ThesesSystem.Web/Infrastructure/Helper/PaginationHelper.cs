﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThesesSystem.Contracts;
using ThesesSystem.Data;
using ThesesSystem.Web.Infrastructure.Constants;
using ThesesSystem.Web.ViewModels.Partial;

namespace ThesesSystem.Web.Infrastructure.Helper
{
    public static class PaginationHelper
    {
        public static PaginationViewModel CreatePagination(string action, string controller, IThesesSystemData data, int currentPage)
        {
            var pagesNumber = GetPages(data, controller + action);

            var hasNextPage = currentPage != (pagesNumber - 1);
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
                //TODO: Other paging
                default:
                    pageNumber = 0;
                    break;
            }

            return pageNumber;
        }
    }
}