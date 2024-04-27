using AdminDashboard.Server.Models.TimeTable;
using AdminDashboard.Server.Service.Interface.TimeTable;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.TeacherModule.TimeTable
{
    public class ViewTimeTableBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        // Time Table service

        [Inject]
        public ITimeTableService _itimetableService { get; set; }
        public List<TimeTableListModel> timeTableListModels = new List<TimeTableListModel>();
        public SfGrid<TimeTableListModel> Grid;
        public string[] GroupedColumns = new string[] { "dayDetails" };
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            timeTableListModels = (await _itimetableService.GetTimeTableList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "TeacherTimeTable")).ToList();

        }
        public void ExportToExcel()
        {
            this.Grid.ExcelExport();
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            //if (args.Item.Text == "PDF Export")  //Id is combination of Grid's ID and itemname
            //{ 
            //    PdfExportProperties ExportProperties = new PdfExportProperties();
            //    ExportProperties.FileName = "TimeTable.pdf";
            //    await this.Grid.PdfExport(ExportProperties);
            //}
        }

    }
}
