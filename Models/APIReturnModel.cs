using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AIS.Model
{
    public class APIReturnModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public object result { get; set; }
        public dynamic data { get; set; }
        public HttpStatusCode responseCode { get; set; }
    }
}
