using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ThesesSystem.Web.Controllers.BaseControllers;
using ThesesSystem.Data;
using ThesesSystem.Web.ViewModels.Theses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ThesesSystem.Web.ViewModels.Teacher;
using ThesesSystem.Models;

namespace ThesesSystem.Web.Controllers
{
    public class ThesisController : AuthorizeController
    {
        public ThesisController(IThesesSystemData data)
            : base(data)
        {
        }

        // GET: Thesis
        [HttpGet]
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();

            var thesesViewModel = this.Data.Theses.All()
                                    .Where(t => t.StudentId == userId || t.SupervisorId == userId)
                                    .Project()
                                    .To<DevThesisIndexViewModel>()
                                    .ToList();

            return View(thesesViewModel);
        }

        [HttpGet]
        public ActionResult ThesisProfile(int id)
        {   
            // TODO: Add more new parts
            // TODO: delete the thesis
            // TODO: Add reviewer and admin
            var userId = this.User.Identity.GetUserId();

            var thesis = this.Data.Theses.GetById(id);

            if (thesis.SupervisorId == userId || thesis.StudentId == userId)
            {
             
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Storage");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var newThesis = new CreateThesisViewModel();
            var superviosors = this.Data.Teachers.All()
                                  .AsQueryable()
                                  .Project()
                                  .To<SupervisorDropDownListITemViewModel>()
                                  .ToList();


            ViewBag.SupervisorId = new SelectList(superviosors, "Id", "FullName");

            return View(newThesis);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateThesisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var thesis = Mapper.Map<Thesis>(model);
                thesis.StudentId = this.User.Identity.GetUserId();

                this.Data.Theses.Add(thesis);

                this.Data.SaveChanges();

                return RedirectToAction("ThesisProfile", "Thesis", new { id = thesis.Id });
            }

            var superviosors = this.Data.Teachers.All()
                               .AsQueryable()
                               .Project()
                               .To<SupervisorDropDownListITemViewModel>()
                               .ToList();


            ViewBag.SupervisorId = new SelectList(superviosors, "Id", "FullName");

            return View(model);
        }

    }
}