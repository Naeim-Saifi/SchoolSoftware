using AdminDashboard.Server.API_Service.Interface.StudentSetUp;
using AIS.Data.RequestResponseModel.StudentSetUp; 
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AdminDashboard.Server.User_Pages.Student
{
    public class StudentListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IStudentSetupService _studentSetupService { get; set; }
        public SessionModel _sessionModel;
        public SfGrid<Student_List_Output_Model> _sfgridstudent;

        public int pageSize = 50;
        //list Model
        public List<Student_List_Output_Model> _studentList = new List<Student_List_Output_Model>();
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> toolBarItems = (new List<string>() { "ExcelExport", "Collapse All", "Expand All", "Print", "Search", "ColumnChooser" });
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            Student_List_Input_Para_Model student_List_Input_Para_Model = new Student_List_Input_Para_Model()
            {
                busStopID = 0,
                classID = 0,
                sectionID = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All


            };
            _studentList = (await _studentSetupService.GET_Student_List(student_List_Input_Para_Model)).ToList();


        }

        public async Task ToolbarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Collapse All")
            {
               await this._sfgridstudent.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                await this._sfgridstudent.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {
                await this._sfgridstudent.ExportToExcelAsync();

                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Class Wise Chapter Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "StudentList.xlsx";
                await this._sfgridstudent.ExcelExport(ExcelProperties);
            }
            //ExcelExportProperties Props = new ExcelExportProperties();
            //List<ExcelCell> ExcelCells1 = new List<ExcelCell> 
            //{
            //    new ExcelCell() { Index = 1, ColSpan = 5, Value = _sessionModel.SchoolName, Style = new ExcelStyle()
            //    { FontColor = "#C25050", FontSize = 19, HAlign = ExcelHorizontalAlign.Center, Bold = true } } 
            //};
            //List<ExcelCell> ExcelCells2 = new List<ExcelCell>
            //{
            //    new ExcelCell() { Index = 1, ColSpan = 5, Value = "Student Details", Style = new ExcelStyle() { FontColor = "#C67878", FontSize = 12, Bold = true } },
            //    new ExcelCell() {Index = 2, Value = "" },
            //    new ExcelCell() {Index = 3, Value = "" },
            //    new ExcelCell() { Index = 4,  Value = "INVOICE NUMBER", Style = new ExcelStyle() { FontColor = "#C67878", Bold = true } },
            //    new ExcelCell() { Index = 5,  Value = "DATE",  Style = new ExcelStyle() { FontColor = "#C67878", Bold = true} },
            //};
            //List<ExcelCell> ExcelCells3 = new List<ExcelCell>
            //{
            //    new ExcelCell() { Index = 1,  ColSpan = 2, Value = "" },
            //    new ExcelCell() {Index = 2,  Value = DateTime.Now.ToString("d")},
            //    new ExcelCell() {Index = 3, Value = "" },
            //    new ExcelCell() { Index = 4,   Value = 2034 },
            //    new ExcelCell() { Index = 5,   Value = DateTime.Now.ToString("d") },
            //};
            //List<ExcelCell> ExcelCells4 = new List<ExcelCell>
            //{
            //    new ExcelCell() { Index = 1, ColSpan = 2, Value = "Tel +1 888.936.8638 Fax +1 919.573.0306"},
            //    new ExcelCell() {Index = 2, Value = "" },
            //    new ExcelCell() {Index = 3, Value = "" },
            //    new ExcelCell() { Index = 4,  Value = "CUSTOMER ID", Style = new ExcelStyle() { FontColor = "#C67878", Bold = true}},
            //    new ExcelCell() { Index = 5,  Value = "Terms" , Style = new ExcelStyle() { FontColor = "#C67878", Bold = true}}
            //};
            //List<ExcelCell> ExcelCells5 = new List<ExcelCell>
            //{
            //    new ExcelCell() {Index = 1, Value = "" },
            //    new ExcelCell() {Index = 2, Value = "" },
            //    new ExcelCell() {Index = 3, Value = "" },
            //    new ExcelCell() { Index = 4, Value = 564 },
            //    new ExcelCell() { Index = 5, Value = "Net 30 Days" }
            //};
            //List<ExcelRow> ExcelRows = new List<ExcelRow>
            //{
            //    new ExcelRow() { Index = 1,  Cells = ExcelCells1 },
            //    new ExcelRow() { Index = 2, Cells = ExcelCells2 },
            //    new ExcelRow() { Index = 3,  Cells = ExcelCells2 },
            //    new ExcelRow() { Index = 4,  Cells = ExcelCells3 },
            //    new ExcelRow() { Index = 5,  Cells = ExcelCells4 },
            //    new ExcelRow() { Index = 6,  Cells = ExcelCells5 },
            //    new ExcelRow() { Index = 7 }
            //};

            //List<ExcelCell> FooterCells1 = new List<ExcelCell> { new ExcelCell() { ColSpan = 6, Value = "Thank you for your business!", Style = new ExcelStyle() { FontColor = "#C67878", HAlign = ExcelHorizontalAlign.Center, Bold = true } } };
            //List<ExcelCell> FooterCells2 = new List<ExcelCell> { new ExcelCell() { ColSpan = 6, Value = "! Visit Again !", Style = new ExcelStyle() { FontColor = "#C67878", HAlign = ExcelHorizontalAlign.Center, Bold = true } } };
            //List<ExcelRow> FooterRows = new List<ExcelRow>
            //{
            //    new ExcelRow() { Cells = FooterCells1 },
            //    new ExcelRow() { Cells = FooterCells2 }
            //};
            //ExcelHeader Header = new ExcelHeader()
            //{
            //    HeaderRows = 7,
            //    Rows = ExcelRows
            //};
            //ExcelFooter Footer = new ExcelFooter()
            //{
            //    FooterRows = 5,
            //    Rows = FooterRows
            //};
            //Props.Header = Header;
            //Props.Footer = Footer;
            //await this._sfgridstudent.ExportToExcelAsync(Props);
        }
    }
}
 
