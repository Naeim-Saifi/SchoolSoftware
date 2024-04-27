using AdminDashboard.Server.API_Service.Interface.Employee;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.TimeTableSetUp;
using AdminDashboard.Server.User_Pages.RND;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Employee;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Create_Word_document;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch.Operations;
using MudBlazor;
using Syncfusion.Blazor.Charts.Chart.Internal;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.TimeTable
{
    public class TimeTableAllTeacherAndAllClassSectionBase : ComponentBase
    {
        [Inject]
        public IEmployeeSetupService employeeService { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public ViewTimeTableModel viewTimeTableModel = new ViewTimeTableModel();
        public SessionModel sessionModel { get; }

        [Inject]
        public ITimeTableSetUpService _itimetableService { get; set; }

        [Inject]
        public Microsoft.JSInterop.IJSRuntime _JS { get; set; }

        [Inject]

        public ExcelFormatTimeTable excelFormatTimeTable { get; set; }  

        public List<PeriodOutputModel> _periodListModels = new List<PeriodOutputModel>();
        public List<DayModelList> dayModelLists = new List<DayModelList>();
        public List<TimeTableOutputModel> _timeTableList = new List<TimeTableOutputModel>();

        //list Model
        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();
        public List<Master_Section_List_Output_Model> _sectionList = new List<Master_Section_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> _subjectList = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public List<Employee_User_List_Output_Model> _employee_List = new List<Employee_User_List_Output_Model>();

        public List<TimeTableTabularFormate> _timeTableForAllTeacher = new List<TimeTableTabularFormate>();
        public List<TimeTableTabularFormate> _timeTableForAllClassSection = new List<TimeTableTabularFormate>();

        public SfGrid<TimeTableTabularFormate> sfTeacherFormat;
        public SfGrid<TimeTableTabularFormate> sfclassSectionFormat;

        public List<string> toolBarItems = (new List<string>() { "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            Employee_User_List_Input_Model employee_User_List_Input_Model = new Employee_User_List_Input_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                userRoleID = 0,
                userAccountID = 0,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            _employee_List = (await employeeService.GET_USER_LIST(employee_User_List_Input_Model)).ToList();

            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            Master_Section_List_Input_Para_Model master_section_List_Input_Para_Model = new Master_Section_List_Input_Para_Model()
            {
                sectionId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _sectionList = (await masterDataSetupService.GET_Master_SectionLIST(master_section_List_Input_Para_Model)).ToList();
            Period_Input_Para_Model period_Input_Para_Model = new Period_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                periodId = 0,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _periodListModels = (await _itimetableService.Get_Period_List(period_Input_Para_Model)).ToList();
            //Day List
            dayModelLists = (await _itimetableService.GetDaysNameList()).ToList();


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
                userID = 0
            };
            _timeTableList = (await _itimetableService.Get_Time_Table_List(time_Table_Input_Para_Model)).ToList();

            _timeTableForAllTeacher = (await _itimetableService.GetTimeTableTabularFormatAll("AllTeacher", _employee_List, _classList, _sectionList, dayModelLists, _periodListModels, _timeTableList)).ToList();
            _timeTableForAllClassSection = (await _itimetableService.GetTimeTableTabularFormatAll("AllClassSection", _employee_List, _classList, _sectionList, dayModelLists, _periodListModels, _timeTableList)).ToList();

        }
        public void TeacherFormatToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "ExportExcel")
            {
                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                //ExcelExportProperties ExcelProperties = new ExcelExportProperties();

                //ExcelTheme Theme = new ExcelTheme();
                //ExcelStyle ThemeStyle = new ExcelStyle()
                //{
                //    FontName = "Calibri",
                //    FontColor = "#666666",
                //    FontSize = 8,
                //    WrapText = true,
                //    //Borders = { LineStyle= BorderLineStyle.Thick},
                //};

                //ExcelHeader header = new ExcelHeader();
                //header.HeaderRows = 2;

                //List<ExcelCell> cell = new List<ExcelCell>
                //    {
                //    new ExcelCell() { RowSpan= 2,ColSpan=12 , Value= _sessionModel.SchoolName,  Style = new ExcelStyle() { FontName="Calibri",  Bold = true, FontSize = 20, Italic= true }
                //     }


                //};

                //List<ExcelRow> HeaderContent = new List<ExcelRow>
                //    {
                //        new ExcelRow() {  Cells = cell, Index = 1 }
                //    };


                //header.Rows = HeaderContent;
                //ExcelProperties.Header = header;
                //Theme.Header = ThemeStyle;
                //Theme.Record = ThemeStyle;
                //Theme.Caption = ThemeStyle;
                //ExcelProperties.Theme = Theme;
                //ExcelProperties.FileName = "TeacherFormat-" + formattedDateTime + ".xlsx";
                //this.sfTeacherFormat.ExportToExcelAsync(ExcelProperties);
                //MemoryStream excelStream;
                //excelStream = excelFormatTimeTable.CreateExcel();

                //excelStream = excelFormatTimeTable.TimeTableTabularFormatExcel(_sessionModel.SchoolName, _timeTableForAllTeacher, "onlyTeacherTimeTable");
                //_JS.SaveAs("Teacher_TimeTable_Format-" + formattedDateTime + ".xlsx", excelStream.ToArray());

            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfTeacherFormat.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfTeacherFormat.ExpandAllGroupAsync();
            }
        }
        public void ClassSectionFormatToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "ExportExcel")
            {
                this.sfclassSectionFormat.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfclassSectionFormat.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfclassSectionFormat.ExpandAllGroupAsync();
            }
        }

        
    }
}
