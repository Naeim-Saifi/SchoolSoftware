using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.Enquiry
{
    public interface IEnquiryService
    {
        Task<APIReturnModel> CRUD_EnquiryDetails(EnquiryAPIModel enquiryAPIModel);
        Task<IEnumerable<EnquiryListModel>> GET_EnquiryDetails_List(EnquiryParaModel enquiryParaModel);
    }
}
