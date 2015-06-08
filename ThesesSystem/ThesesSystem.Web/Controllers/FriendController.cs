using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Web.Controllers.BaseControllers;
using Microsoft.AspNet.Identity;
using ThesesSystem.Web.ViewModels.Friends;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ThesesSystem.Models;

namespace ThesesSystem.Web.Controllers
{
    public class FriendController : AuthorizeController
    {
        public FriendController(IThesesSystemData data)
            : base(data)
        {

        }

        // GET: Friend
        [HttpGet]
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();

            var friends = this.Data.Users.All()
                                .Where(u => u.Id == userId)
                                .SelectMany(u => u.Friends)
                                .Project()
                                .To<FriendViewModel>()
                                .ToList();

            return View(friends);
        }
    }
}