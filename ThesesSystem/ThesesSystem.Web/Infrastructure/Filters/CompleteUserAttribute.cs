namespace ThesesSystem.Web.Infrastructure.Filters
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using ThesesSystem.Web.Infrastructure.Constants;

    [AttributeUsageAttribute(AttributeTargets.Class | 
        AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
     public class CompleteUserAttribute : AuthorizeAttribute
    {
        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext 
            filterContext)
        {
            var user = filterContext.HttpContext.User;
          
            //User isn't logged in
            if (!user.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { 
                        controller = "Account", action = "Login" 
                    })
                );
            }
            else if (user.IsInRole(GlobalConstants.NOT_VERIFIED_USER))
            {

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { 
                        controller = "Validation", action = "NotVerified" 
                    })
                );

            }
            else if (user.IsInRole(GlobalConstants.NOT_COMPLETE_USER))
            {
                if (user.IsInRole(GlobalConstants.STUDENT))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { 
                            controller = "Validation", 
                            action = "CompleteStudentRegistration", 
                            id = user.Identity.GetUserId() 
                        })
                    );
                }
                else if (user.IsInRole(GlobalConstants.TEACHER))
                {
                    filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary(new { 
                           controller = "Validation", 
                           action = "CompleteTeacherRegistration", 
                           id = user.Identity.GetUserId() 
                       })
                    );
                }
            }
        }

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthenticatied = httpContext.User.Identity.IsAuthenticated;
            var isVerified = httpContext.User.IsInRole(GlobalConstants.VERIFIED_USER);
            var isCompleted = httpContext.User.IsInRole(GlobalConstants.COMPLETE_USER);

            return isAuthenticatied && isVerified && isCompleted;
        }
    }
}