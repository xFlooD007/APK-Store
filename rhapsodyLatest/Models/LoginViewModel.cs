﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhapsodyLatest.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}