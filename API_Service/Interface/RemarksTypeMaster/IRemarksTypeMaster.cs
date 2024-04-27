using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.QuestionSetUp;
using AIS.Data.RequestResponseModel.RemarksTypeMaster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.RemarksTypeMaster
{
    public interface IRemarksTypeMasterService
    {
        Task<APIReturnModel> CRUD_RemarksType(RemarksTypeAPIModel remarksTypeAPIModel);
        Task<IEnumerable<RemarksTypeListModel>> Get_RemarksTypeList_List(RemarksType_Input_Para_Model remarksType_Input_Para_Model);
        Task<APIReturnModel> CRUD_RemarksTypeDescription(RemarksTypeDescriptionAPIModel remarksTypeDescriptionAPIModel);
        Task<IEnumerable<RemarksTypeDescriptionListModel>> Get_RemarksTypeDescription(RemarksTypeDescription_Input_Para_Model remarksTypeDescription_Input_Para_Model);
    }
}
