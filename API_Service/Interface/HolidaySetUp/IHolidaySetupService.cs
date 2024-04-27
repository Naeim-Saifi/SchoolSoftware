using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.HolidaySetup;
//using AIS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.HolidaySetUp
{
    public interface IHolidaySetUpService
    {
        Task<APIReturnModel> CRUD_HolidaySetup(HolidaySetUpModel holidaySetUpModel);
        Task<APIReturnModel> CRUD_HolidayTypeList(HolidayModel holidayModel);
        Task<IEnumerable<Holiday_SetUp_List_Output_Model>> GET_HolidaySetup_LIST(Holiday_SetUp_List_Input_Para_Model holiday_SetUp_List_Input_Para_Model);
        Task<IEnumerable<Holiday_Type_List_Output_Model>> GET_HolidayType_LIST(Holiday_Type_List_Input_Para_Model holiday_Type_List_Input_Para_Model);
    }
}
