using AdminDashboard.Server.Models.Enquiry;
using AIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.Enquiry
{
    
        public interface IEnquiryService
        {
            Task<IEnumerable<EnquiryListModel>> GetEnquiryList(InputEnquiryModel inputEnquiryModel);
            Task<APIReturnModel> AddUpdateEnquiry(EnquiryModel enquiryModel  );

        }
     
}
