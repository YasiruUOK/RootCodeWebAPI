using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RootCodeWebAPI.VM
{
    public class Login
    {
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
    }
}