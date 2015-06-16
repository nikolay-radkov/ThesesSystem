using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Web.Controllers.BaseControllers;
using ThesesSystem.Web.ViewModels.Faculties;
using ThesesSystem.Web.ViewModels.Search;
using ThesesSystem.Web.ViewModels.Specialties;
using ThesesSystem.Web.ViewModels.Students;
using ThesesSystem.Web.ViewModels.Teachers;
using ThesesSystem.Web.ViewModels.Theses;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ThesesSystem.Web.Controllers
{
    public class SearchController : AuthorizeController
    {
        public SearchController(IThesesSystemData data)
            : base(data)
        {

        }

        private ICollection<StudentResultViewModel> GetStudents(string keyWord)
        {
            var students = this.Data.Students.All()
                            .Where(s => s.User.FirstName.Contains(keyWord)
                                || s.User.MiddleName.Contains(keyWord)
                                || s.User.LastName.Contains(keyWord)
                                || s.User.Email.Contains(keyWord))
                            .Project()
                            .To<StudentResultViewModel>()
                            .ToList();

            return students;
        }

        private ICollection<ThemeResultViewModel> GetThemes(string keyWord)
        {
            var themes = this.Data.ThesisThemes.All()
                             .Where(t => t.Title.Contains(keyWord))
                             .Project()
                             .To<ThemeResultViewModel>()
                             .ToList();

            return themes;
        }

        private ICollection<ArchiveResultViewModel> GetArchives(string keyWord)
        {
            var archives = this.Data.Theses.All()
                             .Where(t => t.IsComplete && (t.Title.Contains(keyWord) || t.Description.Contains(keyWord)))
                             .Project()
                             .To<ArchiveResultViewModel>()
                             .ToList();

            return archives;
        }

        private ICollection<SpecialtyResultViewModel> GetSpecialties(string keyWord)
        {
            var specialties = this.Data.Specialties.All()
                             .Where(s => s.Title.Contains(keyWord))
                             .Project()
                             .To<SpecialtyResultViewModel>()
                             .ToList();

            return specialties;
        }

        private ICollection<FacultyResultViewModel> GetFaculties(string keyWord)
        {
            var faculties = this.Data.Faculties.All()
                           .Where(f => f.Title.Contains(keyWord))
                           .Project()
                           .To<FacultyResultViewModel>()
                           .ToList();

            return faculties;
        }

        private ICollection<TeacherResultViewModel> GetTeachers(string keyWord)
        {
            var teachers = this.Data.Teachers.All()
                           .Where(t => t.User.FirstName.Contains(keyWord)
                               || t.User.MiddleName.Contains(keyWord)
                               || t.User.LastName.Contains(keyWord)
                               || t.User.Email.Contains(keyWord))
                           .Project()
                           .To<TeacherResultViewModel>()
                           .ToList();

            return teachers;
        }

        // GET: Search
        public ActionResult Index(SearchViewModel model)
        {
            var resultViewModel = Mapper.Map<SearchResultViewModel>(model);

            switch (model.SearchType)
            {
                case SearchType.Everywhere:
                    resultViewModel.Students = this.GetStudents(model.KeyWord);
                    resultViewModel.Teachers = this.GetTeachers(model.KeyWord);
                    resultViewModel.Faculties = this.GetFaculties(model.KeyWord);
                    resultViewModel.Specialties = this.GetSpecialties(model.KeyWord);
                    resultViewModel.Archives = this.GetArchives(model.KeyWord);
                    resultViewModel.Themes = this.GetThemes(model.KeyWord);
                    break;
                case SearchType.Students:
                    resultViewModel.Students = this.GetStudents(model.KeyWord);
                    break;
                case SearchType.Teachers:
                    resultViewModel.Teachers = this.GetTeachers(model.KeyWord);
                    break;
                case SearchType.Faculties:
                    resultViewModel.Faculties = this.GetFaculties(model.KeyWord);
                    break;
                case SearchType.Specialties:
                    resultViewModel.Specialties = this.GetSpecialties(model.KeyWord);
                    break;
                case SearchType.Archives:
                    resultViewModel.Archives = this.GetArchives(model.KeyWord);
                    break;
                case SearchType.Themes:
                    resultViewModel.Themes = this.GetThemes(model.KeyWord);
                    break;
            }

            resultViewModel.ResultsCount = resultViewModel.Archives.Count
                                        + resultViewModel.Faculties.Count
                                        + resultViewModel.Specialties.Count
                                        + resultViewModel.Students.Count
                                        + resultViewModel.Teachers.Count
                                        + resultViewModel.Themes.Count;

            return View(resultViewModel);
        }
    }
}