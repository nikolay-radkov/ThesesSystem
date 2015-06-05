using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Web.Controllers.BaseControllers;
using ThesesSystem.Web.Infrastructure.Constants;

namespace ThesesSystem.Web.Controllers
{
    public class ValidationController : BaseController
    {
        public ValidationController(IThesesSystemData data)
          : base(data)
        {

        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.NOT_COMPLETE_USER)]
        public ActionResult CompleteStudentRegistration()
        {
            // TODO: Redirect to Student registration
            //            var currentUser = UserManager.FindByName(user.UserName);

            //          var roleresult = UserManager.AddToRole(currentUser.Id, GlobalConstants.STUDENT);


            // TODO: create view for completeRegistration
            return View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.NOT_COMPLETE_USER)]
        public ActionResult CompleteTeacherRegistration()
        {
            // TODO: Redirect to Student registration
            //            var currentUser = UserManager.FindByName(user.UserName);

            //          var roleresult = UserManager.AddToRole(currentUser.Id, GlobalConstants.STUDENT);


            // TODO: create view for completeRegistration
            return View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.NOT_VERIFIED_USER)]
        public ActionResult NotVerified()
        {
            // TODO: redirect if is verified
            return View();
        }
    }
}