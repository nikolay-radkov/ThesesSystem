﻿namespace ThesesSystem.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using ThesesSystem.Data;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.ViewModels.Theses;
    using ThesesSystem.Web.Infrastructure.Helper;
    using ThesesSystem.Models;

    [Authorize]
    public class StorageController : BaseController
    {
        public StorageController(IThesesSystemData data)
            : base(data)
        {
        }

        // GET: Storage
        [HttpGet]
        public ActionResult Index(int? page)
        {
            //TODO: create profile for Thesis, Student, Teacher 
            //TODO: implement searching and filtering
           
            int currentPage = page ?? 0;

            var theses = this.Data.Theses
                                    .All()
                                    .Where(u => u.IsComplete)
                                    .OrderBy(t => t.FinishedAt)
                                    .Skip(GlobalConstants.ELEMENTS_PER_PAGE * currentPage)
                                    .Take(GlobalConstants.ELEMENTS_PER_PAGE)
                                    .AsQueryable()
                                    .Project()
                                    .To<ThesisViewModel>()
                                    .ToList();

            ViewData["Pagination"] = PaginationHelper.CreatePagination("Index", "Storage", this.Data, currentPage);

            return View(theses);
        }

        [HttpGet]
        public ActionResult ThesisProfile(int id)
        {
            // TODO: Implement downloading

            var thesis = this.Data.Theses.GetById(id);

            var thesisViewModel = Mapper.Map<ThesisProfileViewModel>(thesis);

            return View(thesisViewModel);
        }
    }
}