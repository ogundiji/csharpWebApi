﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiFundamental.Core.ViewModel
{
    public class EmailMessage
    {
        public EmailAddress ToAddresses;
        public EmailAddress FromAddresses;

        public EmailMessage()
        {
            ToAddresses = new EmailAddress();
            FromAddresses = new EmailAddress();
        }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}