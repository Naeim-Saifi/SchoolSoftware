 

using AdminDashboard.Server.API_Service.Interface.Attendance;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.StudentSetUp;
using AdminDashboard.Server.API_Service.Service.Attendance;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.Attendance;
using AIS.Data.RequestResponseModel.HolidaySetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using MudBlazor;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AdminDashboard.Server.API_Service.Service.Attendance.AttendanceService;

namespace AdminDashboard.Server.User_Pages.Attendance
{
    public class AllStudentAttendanceListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        [Inject]
        public IAttendanceService _attendanceService { get; set; }
        public List<StudentAttendanceList_MonthWise> _studentAttendanceList = new List<StudentAttendanceList_MonthWise>();
        public SfGrid<StudentAttendanceList_MonthWise> sfgridUserIDWise;
        public List<string> toolBarItems = (new List<string>() { "ExcelExport", "Collapse All", "Expand All", "Print", "Search" });
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            try
            {
                AttendanceInputParaModel attendanceInputParaModel = new AttendanceInputParaModel()
                {
                    classId = 0,
                    userid = _sessionModel.UserId,
                    fromDate = DateTime.Today.Date.ToString(),
                    toDate = DateTime.Today.Date.ToString(),
                    sectionId = 0,
                    userRoleId = _sessionModel.RoleId,
                    financialYear = _sessionModel.FinancialYear,
                    schoolCode = _sessionModel.SchoolCode,
                    reportType = AttendanceReportType.StudentAttendanceList_MonthWise
                };
                _studentAttendanceList = (await _attendanceService.StudentAttendanceList_MonthWise(attendanceInputParaModel)).ToList();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }



        }

        public void toolBarItemsClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            //if (args.Item.Text == "Add Attendance")
            //{
            //    //navigationManager.NavigateTo("/OnlineExam/TestList");
            //    //perform your actions here
            //    IsVisible = true;

            //    btncss = "e-flat e-primary e-outline";
            //    DialogHeaderName = "Add Student Attendance";
            //    OperationType = "Add";
            //    HeaderText = "Add Student Attendance";
            //    ddEnable = true;


            //}
            if (args.Item.Text == "Collapse All")
            {
                this.sfgridUserIDWise.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfgridUserIDWise.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {
                this.sfgridUserIDWise.ExportToExcelAsync();

                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Attendance Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "AttendanceDetails.xlsx";
                this.sfgridUserIDWise.ExcelExport(ExcelProperties);
            }
            else
            {
                this.sfgridUserIDWise.Columns[0].AutoFit = true;
            }
        }

    }
}
