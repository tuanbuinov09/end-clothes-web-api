﻿using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace ClothingWebAPI.Entities
{
    public class MailMessage
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public MailMessage(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("END_CLOTHES", x)));
            Subject = subject;
            Content = content;
        }
    }
}
