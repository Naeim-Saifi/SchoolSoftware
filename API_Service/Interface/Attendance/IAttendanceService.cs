using AdminDashboard.Server.API_Service.Service.Attendance;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Attendance;
using AIS.Data.RequestResponseModel.Dashboard;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.Attendance
{
    public interface IAttendanceService
    {

        Task<APIReturnModel> CRUD_StudentAttendance(List<AttendanceAPIModel>  attendanceAPIModels);
        Task<IEnumerable<TodayAttendanceList>> GET_StudentAttendanceList(AttendanceInputParaModel  attendanceInputParaModel);
        Task<IEnumerable<StudentAttendanceList_UserIDWise>> StudentAttendanceList_UserIDWise(AttendanceInputParaModel attendanceInputParaModel);
        Task<IEnumerable<StudentAttendanceList_MonthWise>> StudentAttendanceList_MonthWise(AttendanceInputParaModel attendanceInputParaModel);
    }
}
