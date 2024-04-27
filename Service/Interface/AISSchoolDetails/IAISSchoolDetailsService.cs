using AIS.Data.RequestResponseModel.AISSchoolDetails;
using AIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.AISSchoolDetails
{
    public interface IAISSchoolDetailsService
    {
        Task<IEnumerable<SchoolDetailsListModel>> GetSchoolDetailsList(InputModel  inputModel);
        Task<APIReturnModel> AddUpdateSchoolDetails(SchoolDetailsModel enquiryModel);

    }
}
