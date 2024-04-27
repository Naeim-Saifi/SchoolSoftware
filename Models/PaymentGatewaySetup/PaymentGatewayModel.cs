using AdminDashboard.Server.Models.BaseClassModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIS.Model.PaymentGatewaySetup
{
    public class PaymentGatewayModel: BaseClassInputModel
    {
        
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNo { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string WebsiteUrl { get; set; }
        public string Secret_Key { get; set; }
        public string Api_Key { get; set; }
        
    }

    public class PaymentGatewayListModel: BaseClassOutputModel
    {

        public string contactName { get; set; }
        public string contactEmail { get; set; }
        public string contactNo { get; set; }
        public string businessName { get; set; }
        public string businessType { get; set; }
        public string websiteUrl { get; set; }
        public string secret_Key { get; set; }
        public string api_Key { get; set; }
       
    }
}





    

