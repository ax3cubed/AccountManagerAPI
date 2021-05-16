using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Models
{
    public class ResponseMessage
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public object Data { get; set; }
    }
}
