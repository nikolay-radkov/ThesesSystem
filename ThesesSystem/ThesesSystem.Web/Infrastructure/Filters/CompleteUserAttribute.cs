using System.Security.Principal;
using System.Web.Mvc;
using System.Web;

using ThesesSystem.Web.Infrastructure.Constants;
using System.Web.Routing;
using System;

namespace ThesesSystem.Web.Infrastructure.Filters
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CompleteUserAttribute : AuthorizeAttribute
    {
        //Custom named parameters for annotation
        public string ResourceKey { get; set; }
        public string OperationKey { get; set; }

        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            //User isn't logged in
            if (!user.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Account", action = "Login" })
                );
            }
            else if (user.IsInRole(GlobalConstants.NOT_VERIFIED_USER))
            {

                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Validation", action = "NotVerified" })
                );

            }
            else if (user.IsInRole(GlobalConstants.NOT_COMPLETE_USER))
            {
                if (user.IsInRole(GlobalConstants.STUDENT))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Validation", action = "CompleteStudentRegistration" })
                );
                }
                else if (user.IsInRole(GlobalConstants.TEACHER))
                {
                    filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary(new { controller = "Validation", action = "CompleteTeacherRegistration" })
               );
                }
            }
        }

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthenticatied = httpContext.User.Identity.IsAuthenticated;
            var isVerified = httpContext.User.IsInRole(GlobalConstants.NOT_VERIFIED_USER);
            var isCompleted = httpContext.User.IsInRole(GlobalConstants.NOT_COMPLETE_USER);

            return isAuthenticatied && !isVerified && !isCompleted;
        }
    }
}