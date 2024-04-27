using AdminDashboard.Server.API_Service.Interface.Exam;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.ExamSetup
{
    public class ExamScheduleForStudentBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IExamMasterSetupService examMasterSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        public List<Exam_Type_List_Output_Model> _examTypeList = new List<Exam_Type_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> _subjectList = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();

        public List<TermWiseExamSchedule_List_Output_Model> _examScheduleList = new List<TermWiseExamSchedule_List_Output_Model>();

        public List<TermWiseExmSetViewModel> _terWiseExamSetList = new List<TermWiseExmSetViewModel>();
        public List<TermWiseExamSchedule_List_Output_Model> _allterWiseExamSetList = new List<TermWiseExamSchedule_List_Output_Model>();
        //view model for databinding.

        public TermWiseExmViewModel termWiseExmViewModel = new TermWiseExmViewModel();
        public Exam_schedule_Details_Model exam_Schedule_Details_Model { get; set; }

        public List<Exam_schedule_Details_Model> exam_Schedule_Details_Models = new List<Exam_schedule_Details_Model>();

        public List<string> toolBarPendingFee = (new List<string>() { "Marks Entry", "ExcelExport", "Collapse All", "Expand All", "Print", "Search", "ColumnChooser" });
        public List<string> toolbarExam = (new List<string>() { "Add Exam Schedule", "ExcelExport", "Collapse All", "Expand All", "Print", "Search" });
        public List<string> toolBarItems = (new List<string>() { "Print", "Search" });
        public SessionModel _sessionModel;
        public string[] GroupedColumns = new string[] { "fatherContactNumber" };
        public DialogEffect AnimationEffect = DialogEffect.Zoom;

        public int pageSize = 50;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsEditVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";

        public string btnFindcss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";

        public SfGrid<TermWiseExamSchedule_List_Output_Model> _sfterWiseExamSetList;
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");


            Exam_Schedule_Details_List_Input_Model examSchedule = new Exam_Schedule_Details_List_Input_Model()
            {
                classId = _sessionModel.classID,
                sectionId = 0,
                unitId = 0,
                chapterId = 0,
                topicID = 0,
                examTypeId = 0,
                examScheduleID = 0,
                monthId = 0,
                subjectId = 0,
                userRoleId = _sessionModel.RoleId,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = "ForStudentExamSchedule",
                userId = _sessionModel.UserId
            };

            _allterWiseExamSetList = (await examMasterSetupService.GET_TermWiseExamSchedule_Details_LIST(examSchedule)).ToList();

        }
        public void toolBarItemsToolBar(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Collapse All")
            {
                this._sfterWiseExamSetList.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this._sfterWiseExamSetList.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {
                this._sfterWiseExamSetList.ExportToExcelAsync();

                //    ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                //    ExcelHeader header = new ExcelHeader();
                //    header.HeaderRows = 2;
                //    List<ExcelCell> cell = new List<ExcelCell>
                //{
                //    new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Exam Schedule Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
                //};

                //    List<ExcelRow> HeaderContent = new List<ExcelRow>
                //{
                //    new ExcelRow() {  Cells = cell, Index = 1 }
                //};
                //    header.Rows = HeaderContent;
                //    ExcelProperties.Header = header;
                //    ExcelProperties.FileName = "ExamScheduleList.xlsx";
                //    this._sfterWiseExamSetList.ExcelExport(ExcelProperties);
                //}
                //else
                //{
                //    //this.sfExamSchedule.Columns[0].AutoFit = true;
                //}
            }
        }
    }
}
