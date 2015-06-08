using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Web.Controllers.BaseControllers;
using ThesesSystem.Web.ViewModels.Message;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;

namespace ThesesSystem.Web.Controllers
{
    public class MessageController : AuthorizeController
    {
        public MessageController(IThesesSystemData data)
            : base(data)
        {

        }

        // GET: Message
        public ActionResult Index(string friendId)
        {
            var userId = this.User.Identity.GetUserId();

            var messages = this.Data.Messages.All()
                               .Where(m => (m.FromUserId == friendId && m.ToUserId == userId) || (m.FromUserId == userId && m.ToUserId == friendId))
                               .AsQueryable()
                               .Project()
                               .To<MessageViewModel>()
                               .ToList();

            var userId = this.User.Identity.GetUserId();

            ViewData["FromUserName"] = this.Data.Users.All().Where(u => u.Id == userId).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault();
            ViewData["ToUserId"] = friendId;
            ViewData["FromUserId"] = userId;

            return View(messages);
        }
    }
}