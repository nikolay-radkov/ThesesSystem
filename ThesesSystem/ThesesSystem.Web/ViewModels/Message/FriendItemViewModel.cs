using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThesesSystem.Models;
using ThesesSystem.Web.Infrastructure.Mapping;

namespace ThesesSystem.Web.ViewModels.Message
{
    public class FriendItemViewModel //: IMapFrom<User>, IHaveCustomMappings
    {
        public string FromUserId { get; set; }

        public string FromUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsSeen { get; set; }
    }
}