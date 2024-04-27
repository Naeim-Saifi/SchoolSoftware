using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Employee;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.TimeTableSetUp;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.TimeTableSetUp
{

    public interface ITimeTableSetUpService
    {
        Task<APIReturnModel> CRUD_TimeTableSetup(TimetableModel timetableModel);
        Task<IEnumerable<TimeTableOutputModel>> Get_Time_Table_List(Time_Table_Input_Para_Model time_Table_Input_Para_Model);
        Task<IEnumerable<PeriodOutputModel>> Get_Period_List(Period_Input_Para_Model period_Input_Para_Model);
        Task<APIReturnModel> CRUD_PeriodMaster(PeriodMasterModel periodMasterModel);
        Task<IEnumerable<DayModelList>> GetDaysNameList();
        
         Task<IEnumerable<TimeTableTabularFormate>> GetTimeTableTabularFormat(string _ReportType, int UserID, int _ClassID, int _SectionID,
            List<DayModelList> dayModelLists,
            List<PeriodOutputModel> _periodListModels,
            IEnumerable<TimeTableOutputModel> timeTableOutputModels);

        Task<IEnumerable<TimeTableTabularFormate>> GetTimeTableTabularFormatAll(
                                                                        string RepotType,
                                                                        List<Employee_User_List_Output_Model> userList,
                                                                        List<Master_CLass_List_Output_Model> classList,
                                                                        List<Master_Section_List_Output_Model> sectionList,
                                                                        List<DayModelList> dayModelLists,
                                                                        List<PeriodOutputModel> _periodListModels,
                                                                        IEnumerable<TimeTableOutputModel> timeTableOutputModels
                                                                        );
    }
} 