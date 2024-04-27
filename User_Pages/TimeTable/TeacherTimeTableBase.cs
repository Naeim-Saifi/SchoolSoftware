
using AdminDashboard.Server.API_Service.Interface.Employee;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.API_Service.Interface.TimeTableSetUp;
using AdminDashboard.Server.Service.Implementation.Employee;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Employee;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.Employee;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch.Operations;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AdminDashboard.Server.User_Pages.TimeTable
{
    public class TeacherTimeTableBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public ViewTimeTableModel viewTimeTableModel = new ViewTimeTableModel();

        [Inject]
        public ITimeTableSetUpService _itimetableService { get; set; }
        public List<TimeTableOutputModel> _studentTableList = new List<TimeTableOutputModel>();
        public SfGrid<TimeTableOutputModel> sfTimeTableList;
        public List<TimeTableOutputModel> _todaytimeTableList = new List<TimeTableOutputModel>();
        public List<string> toolBarItems = (new List<string>() { "Print", "Search" });
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            //get time table list.
            Time_Table_Input_Para_Model time_Table_Input_Para_Model = new Time_Table_Input_Para_Model()
            {
                classId = 0,
                dayId = 0,
                periodId = 0,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                sectionID = 0,
                reportType = ReportType.TeacherTimeTable,
                subjectID = 0,
                userID = _sessionModel.UserId
            };
            _studentTableList = (await _itimetableService.Get_Time_Table_List(time_Table_Input_Para_Model)).ToList();
            DayOfWeek _dayName = DateTime.Today.DayOfWeek;

            _todaytimeTableList = _studentTableList.FindAll(m => m.dayNameDetails == _dayName.ToString());
        }
    }
}
