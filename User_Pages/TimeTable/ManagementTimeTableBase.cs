using AdminDashboard.Server.API_Service.Interface.TimeTableSetUp;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AdminDashboard.Server.User_Pages.TimeTable
{
    public class ManagementTimeTableBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        public ViewTimeTableModel viewTimeTableModel = new ViewTimeTableModel();

        [Inject]
        public ITimeTableSetUpService _itimetableService { get; set; }
        public List<TimeTableOutputModel> _studentTableList = new List<TimeTableOutputModel>();
        public SfGrid<TimeTableOutputModel> sfTimeTableList;

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
                reportType = ReportType.All,
                subjectID = 0,
                userID = _sessionModel.UserId
            };
            _studentTableList = (await _itimetableService.Get_Time_Table_List(time_Table_Input_Para_Model)).ToList();
        }
    }
}
