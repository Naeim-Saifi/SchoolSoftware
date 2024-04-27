using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.HomeWorkSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.HomeWork
{
    public interface IHomeWorkSetupService
    {
        Task<APIReturnModel> CRUD_HomeWorkSetup(HomeWorkSetUpModel homeWorkSetUpModel);
        Task<IEnumerable<HomeWork_List_Output_Model>> GET_HOMEWORK_LIST(HomeWork_List_Input_Para_Model homeWork_List_Input_Para_Model);
        Task<APIReturnModel> CRUD_HomeWorkType(HomeWorkTypeAPIModel homeWorkTypeAPIModel);
        Task<IEnumerable<HomeWorkType_OutPut_Model>> GET_HomeWorkType_LIST(HomeWork_Type_Input_Para_Model homeWork_Type_Input_Para_Model);
    }
    
}
