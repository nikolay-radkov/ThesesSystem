namespace ThesesSystem.Web.Infrastructure.Helper
{
    using System;
    using System.Linq;
    using ThesesSystem.Data;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.ViewModels.Filters;
    using ThesesSystem.Web.ViewModels.Partials;

    public static class PaginationHelper
    {
        public static PaginationViewModel CreatePagination(string action, string controller,
            IThesesSystemData data, int currentPage, int id = 0, FilterOptions options = null)
        {
            int pagesNumber = 0;
            if (options == null)
            {
                pagesNumber = GetPages(data, controller + action, id);
            }
            else
            {
                pagesNumber = GetPages(data, controller + action, keyWord: options.KeyWord, filterBy: options.FilterBy);
            }

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

        private static int GetPages(IThesesSystemData data, string action, int id = 0, string keyWord = null,
            FilterBy filterBy = FilterBy.All)
        {
            int pageNumber = 0;

            switch (action)
            {
                case "StorageIndex":

                    if  (keyWord == null)
                    {
                        pageNumber = (int)Math.Ceiling((double)data.Theses.All().Where(t => t.IsComplete).Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                        break;
                    }
                    switch (filterBy)
                    {
                        case FilterBy.All:
                            pageNumber = (int)Math.Ceiling((double)data.Theses.All().Where(t => t.IsComplete).Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                            break;
                        case FilterBy.FacultyNumber:
                            long number = 0;
                            if (long.TryParse(keyWord, out number))
                            {
                                pageNumber = (int)Math.Ceiling((double)data.Theses.All()
                                                        .Where(t => t.IsComplete && t.Student.FacultyNumber == number)
                                                        .Count()
                                                        / GlobalConstants.ELEMENTS_PER_PAGE);
                            }
                            else
                            {
                                pageNumber = (int)Math.Ceiling((double)data.Theses.All().Where(t => t.IsComplete).Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                            }
                            break;
                        case FilterBy.EGN:
                            long egn = 0;
                            if (long.TryParse(keyWord, out egn))
                            {
                                pageNumber = (int)Math.Ceiling((double)data.Theses.All()
                                                       .Where(t => t.IsComplete && t.Student.User.EGN == egn)
                                                       .Count()
                                                       / GlobalConstants.ELEMENTS_PER_PAGE);
                            }
                            else
                            {
                                pageNumber = (int)Math.Ceiling((double)data.Theses.All().Where(t => t.IsComplete).Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                            }
                            break;
                        case FilterBy.StudentName:
                            pageNumber = (int)Math.Ceiling((double)data.Theses.All()
                                                  .Where(t => t.IsComplete && (t.Student.User.FirstName.Contains(keyWord)
                                                        || t.Student.User.MiddleName.Contains(keyWord)
                                                        || t.Student.User.LastName.Contains(keyWord)))
                                                  .Count()
                                                  / GlobalConstants.ELEMENTS_PER_PAGE);
                            break;
                        case FilterBy.TeacherName:
                            pageNumber = (int)Math.Ceiling((double)data.Theses.All()
                                                  .Where(t => t.IsComplete && (t.Supervisor.User.FirstName.Contains(keyWord)
                                                        || t.Supervisor.User.MiddleName.Contains(keyWord)
                                                        || t.Supervisor.User.LastName.Contains(keyWord)))
                                                  .Count()
                                                  / GlobalConstants.ELEMENTS_PER_PAGE);
                            break;
                        case FilterBy.Specialty:
                            pageNumber = (int)Math.Ceiling((double)data.Theses.All()
                                                .Where(t => t.IsComplete && (t.Student.Specialty.Title.Contains(keyWord)))
                                                .Count()
                                                / GlobalConstants.ELEMENTS_PER_PAGE);
                            break;
                        case FilterBy.Faculty:
                            pageNumber = (int)Math.Ceiling((double)data.Theses.All()
                                              .Where(t => t.IsComplete && (t.Student.Specialty.Faculty.Title.Contains(keyWord)))
                                              .Count()
                                              / GlobalConstants.ELEMENTS_PER_PAGE);
                            break;
                        case FilterBy.Thesis:
                            pageNumber = (int)Math.Ceiling((double)data.Theses.All()
                                              .Where(t => t.IsComplete && (t.Title.Contains(keyWord)))
                                              .Count()
                                              / GlobalConstants.ELEMENTS_PER_PAGE);
                            break;
                        case FilterBy.Description:
                            pageNumber = (int)Math.Ceiling((double)data.Theses.All()
                                              .Where(t => t.IsComplete && (t.Description.Contains(keyWord)))
                                              .Count()
                                              / GlobalConstants.ELEMENTS_PER_PAGE);
                            break;
                    }
                    break;
                case "UserVerification":
                    pageNumber = (int)Math.Ceiling((double)data.Users.All().Where(u => !u.IsVerified).Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                    break;
                case "IdeaThemes":
                    pageNumber = (int)Math.Ceiling((double)data.ThesisThemes.All().Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                    break;
                case "TutorialIndex":
                    pageNumber = (int)Math.Ceiling((double)data.ThesisTutorials.All().Count() / GlobalConstants.ELEMENTS_PER_PAGE);
                    break;
                case "SpecialtySpecialtyProfile":
                    pageNumber = (int)Math.Ceiling((double)data.Students.All().Where(s => s.SpecialtyId == id).Count() / GlobalConstants.ELEMENTS_PER_PAGE);
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