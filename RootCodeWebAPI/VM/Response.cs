using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RootCodeWebAPI.VM
{
    public class Response
    {
        public string Status { set; get; }
        public string Message { set; get; }
        public string Data { set; get; }
    }
}