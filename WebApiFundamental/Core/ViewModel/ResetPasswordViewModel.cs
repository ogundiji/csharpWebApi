﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiFundamental.Core.ViewModel
{
    public class ResetPasswordViewModel
    {
        public string Code { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}