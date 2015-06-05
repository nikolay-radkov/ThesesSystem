using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Web.Infrastructure.Constants;
using ThesesSystem.Web.Infrastructure.Filters;

namespace ThesesSystem.Web.Controllers.BaseControllers
{
    [CompleteUserAttribute]
    public abstract class AuthorizeController : BaseController
    {
        public AuthorizeController(IThesesSystemData data)
            : base(data)
        {
        }
    }
}