using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AIS.Model
{
    public class Response
    {
        public bool isError { get; set; }
        public HttpStatusCode errorCode { get; set; }
        public dynamic data { get; set; }
        public string message { get; set; }
    }
}
