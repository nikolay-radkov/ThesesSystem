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
using ThesesSystem.Models;

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

            this.UpdateHistory(userId, friendId);
            var messages = GetConversation(friendId, userId);
            var friendsList = GetHistory(userId);

            var chatViewModel = new ChatViewModel
            {
                Messages = messages,
                FriendsList = friendsList
            };

            ViewData["FromUserName"] = this.Data.Users.All().Where(u => u.Id == userId).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault();
            ViewData["ToUserName"] = this.Data.Users.All().Where(u => u.Id == friendId).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault();

            ViewData["ToUserId"] = friendId;
            ViewData["FromUserId"] = userId;

            return View(chatViewModel);
        }

        [NonAction]
        private void UpdateHistory(string userId, string friendId)
        {
            ICollection<Message> history = this.Data.Messages.All()
                               .Where(m => m.FromUserId == friendId && m.ToUserId == userId)
                               .ToList();

            foreach (var item in history)
            {
                if(!item.IsSeen)
                {
                    item.IsSeen = true;
                }
            }

            this.Data.SaveChanges();
        }

        [NonAction]
        private List<MessageViewModel> GetConversation(string friendId, string userId)
        {
            var messages = this.Data.Messages.All()
                               .Where(m => (m.FromUserId == friendId && m.ToUserId == userId) || (m.FromUserId == userId && m.ToUserId == friendId))
                               .AsQueryable()
                               .Project()
                               .To<MessageViewModel>()
                               .ToList();
            return messages;
        }

        [NonAction]
        private ICollection<FriendItemViewModel> GetHistory(string userId)
        {
            ICollection<FriendItemViewModel> friendsList = this.Data.Messages.All()
                                .Where(u => u.FromUserId == userId || u.ToUserId == userId)
                                .OrderByDescending(u => u.CreatedOn)
                                .Select(m => new FriendItemViewModel
                                {
                                    FromUserId = m.FromUserId == userId ? m.ToUserId : m.FromUserId,
                                    FromUserName = m.FromUserId == userId ?
                                                    m.ToUser.FirstName + " " + m.ToUser.LastName :
                                                    m.FromUser.FirstName + " " + m.FromUser.LastName,
                                    IsSeen = m.IsSeen,
                                    CreatedOn = m.CreatedOn
                                }).ToList();


            friendsList = friendsList.GroupBy(f => new { f.FromUserId, f.FromUserName })
                            .Select(y => new FriendItemViewModel()
                            {
                                FromUserId = y.Key.FromUserId,
                                FromUserName = y.Key.FromUserName,
                                IsSeen = y.Select(s => s.IsSeen).FirstOrDefault(),
                                CreatedOn = y.Select(d => d.CreatedOn).FirstOrDefault()
                            }
                            )
                            .ToList();
            return friendsList;
        }
    }
}