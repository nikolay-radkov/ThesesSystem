using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThesesSystem.Web.Infrastructure.Mapping;

namespace ThesesSystem.Web.ViewModels.Message
{
    public class ChatViewModel
    {
        public string FromUserId { get; set; }

        public string ToUserId { get; set; }

        public string FromUser { get; set; }
    }
}