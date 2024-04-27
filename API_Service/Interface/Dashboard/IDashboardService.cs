using AIS.Data.RequestResponseModel.Dashboard;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.Dashboard
{
    public interface IDashboardService
    {
      Task<StudentDashBoardOutPutModel>  GET_StudentDashBoard_LIST(StudentDashboard_Input_Para_Model studentDashboard_Input_Para_Model);
      //Task<ManagementDashBoardOutPutModel> GET_ManagementDashBoard_LIST(ManagementDashboard_Input_Para_Model managementDashboard_Input_Para_Model);
    }
}
