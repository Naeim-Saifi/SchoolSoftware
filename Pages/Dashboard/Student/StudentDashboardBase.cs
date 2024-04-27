using AdminDashboard.Server.API_Service.Interface.Dashboard;
using AdminDashboard.Server.Models.Dashboard.Student;
using AdminDashboard.Server.Models.TimeTable;
using AdminDashboard.Server.Service.Interface.Dashboard.Student;
using AdminDashboard.Server.Service.Interface.TimeTable;
using AIS.Data.RequestResponseModel.Dashboard;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Dashboard.Student
{
    public class StudentDashboardBase:ComponentBase
    {

        [Inject]
        public IStudentDashboardService studentDashboardService { get; set; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        public StudentDashboardModel studentDashboardModels;//= new StudentDashboardModel();

        public SessionModel _sessionModel;

        //public List<PaymentDetailsListModel> paymentDetails { get; set; }
        public List<PaymentDetailsListModel> paymentDetails = new List<PaymentDetailsListModel>();
        [Inject]
        public ITimeTableService _itimetableService { get; set; }
        public List<TimeTableListModel> timeTableListModels = new List<TimeTableListModel>();

        public SfGrid<TimeTableListModel> Timetable_Grid;
        public string[] GroupedColumns = new string[] { "dayDetails" };

        [Inject]
        public IDashboardService dashboardService { get; set; }
        public StudentDashBoardOutPutModel studentDashBoardOutPutModel;
        public List<Pending_Fee_List_Output_Model> pending_Fee_List_Output_Models = new List<Pending_Fee_List_Output_Model>();
        public int _pendingAmount = 0;
        public string _pendingMonth = "NA";

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            StudentDashboard_Input_Para_Model studentDashboard_Input_Para_Model = new StudentDashboard_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                endDate = DateTime.Now,
                startDate = DateTime.Now,
                userId = _sessionModel.UserId,
                reportType = ReportType.All
            };
            //studentDashBoardOutPutModel = (await dashboardService.GET_StudentDashBoard_LIST(studentDashboard_Input_Para_Model));
            //pending_Fee_List_Output_Models = studentDashBoardOutPutModel.pending_Fee_List_Output_Models;
            
            if(pending_Fee_List_Output_Models.Count>0)
            {
                _pendingAmount = pending_Fee_List_Output_Models[0].netDueAmount;
                _pendingMonth = pending_Fee_List_Output_Models[0].monthName;
            }
           else
            {
                _pendingAmount = 0;
                _pendingMonth = "NA";
            }

            //studentDashboardModels = (await studentDashboardService.StudentDashboard(_sessionModel.SchoolCode,_sessionModel.FinancialYear ,_sessionModel.UserId, "All"));

            //pendingMonthName = "April to July";
            
            //pendingFeeAmount = 5000;
            
            //pendingMonthName = studentDashboardModels.pendingFee[0].monthName;
            
            
            //pendingFeeAmount = Convert.ToInt32(studentDashboardModels.pendingFee[0].totalDue);

            //paymentDetails = studentDashboardModels.paymentDetailsListModel;
            
            //Start Time Table
            
            //timeTableListModels = (await _itimetableService.GetTimeTableList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, ReportType.StudentTimeTable)).ToList();

            //End Time Table

        }
        public void TimeTable_ExportToExcel()
        {
           // this.Timetable_Grid.ExcelExport();
        }
    }
}
