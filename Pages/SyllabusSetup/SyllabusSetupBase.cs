using AdminDashboard.Server.Models.Holiday;
using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.SyllabusSetup;
using AdminDashboard.Server.Service.Interface.Holiday;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Interface.SyllabusSetup;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.SyllabusSetup
{
    public class SyllabusSetupBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        // Time Table service

        [Inject]
        public ISyllabusSetupService _syllabusSetupService { get; set; }
        [Inject]
        public IHolidayService _holidayService { get; set; }
        public List<SyllabusSetupListModel> syllabusSetupListModels = new List<SyllabusSetupListModel>();
        public List<MonthNameList> monthNameLists = new List<MonthNameList>();


        //-------------
        [Inject]
        public IUnitMasterService unitmasterService { get; set; }
        public List<TopicMasterListModel> topicMasterListModels = new List<TopicMasterListModel>();

        [Inject]
        public IMasterClassService masterClassService { get; set; }
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();

        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }

        public SubjectDetailsModel subjectDetailsModel;
        public List<MasterSubjectListModel> mapSubjectList = new List<MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();

        //---------
        public DateTime? fromValue { get; set; } = DateTime.Now;
        public DateTime? toValue { get; set; } = DateTime.Now;
        public DateTime? fromDate { get; set; } = DateTime.Now;
        public DateTime? toDate { get; set; } = DateTime.Now;

        public DialogSettings DialogParams = new DialogSettings { MinHeight = "550px", Width = "450px" };
        public SfGrid<SyllabusSetupListModel> Grid;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
                monthNameLists = (await _holidayService.GetMonthNameList()).ToList();
                //------
                GetSyllabusLoad();
                LoadTopicDetails();
                masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();
                subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
                mapSubjectList = subjectDetailsModel.masterSubjectListModels;
                _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
                //-----
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private async void GetSyllabusLoad()
        {
            //syllabusSetupListModels = (await _syllabusSetupService.GetSyllabusSetupDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
        }
        private async void LoadTopicDetails()
        {
            //topicMasterListModels = (await unitmasterService.GetTopicMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, "All")).ToList();
        }
        
        [Inject]
        public ISnackbar snackBar { get; set; }
        public List<MapsubjectwithClassModel> _mapSubjectList;
        public List<TopicMasterListModel> _unitList;
        public List<TopicMasterListModel> _chapterList;
        public List<TopicMasterListModel> _topicList;
        public void OnChangeClass(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MasterClassListModel> args)
        {
            try
            {
                if (args.ItemData.masterClassId.ToString() != null)
                {
                    int classid = args.ItemData.masterClassId;

                    _mapSubjectList =  (_mapsubjectlistModel.Where(e => e.masterClassId == classid).ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Grid.PreventRender(false);

        }

        public void OnChangeSubject(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MapsubjectwithClassModel> args)
        {
            try
            {
                if (args.ItemData.masterSubjectId.ToString() != null)
                {
                    int subjectId = args.ItemData.masterSubjectId;

                    _unitList =(topicMasterListModels.Where(e => e.masterSubjectId == subjectId).ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Grid.PreventRender(false);
        }
        public void OnChangeUnit(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, TopicMasterListModel> args)
        {
            try
            {
                if (args.ItemData.subjectUnitId.ToString() != null)
                {
                    int unitId = args.ItemData.subjectUnitId;

                    _chapterList = topicMasterListModels.Where(e => e.subjectUnitId == unitId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Grid.PreventRender(false);
        }

        public void OnChangeChapter(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, TopicMasterListModel> args)
        {
            try
            {
                if (args.ItemData.subjectChapterId.ToString() != null)
                {
                    int chapterId = args.ItemData.subjectChapterId;

                    _topicList = topicMasterListModels.Where(e => e.subjectChapterId == chapterId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Grid.PreventRender(false);
        }

       public SyllabusviewModel syllabusviewModel = new SyllabusviewModel();
      
        public void ActionBegin(ActionEventArgs<SyllabusSetupListModel> args)
        {
            SyllabusSetupAPIModel syllabusSetupModel = new SyllabusSetupAPIModel();
            MasterClassListModel masterClassListModel = new MasterClassListModel();
            SyllabusSetupListModel _syllabusSetupListModel = new SyllabusSetupListModel();

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                // Triggers before editing operation starts
                //syllabusSetupListModel.monthNameDetails = args.Data.monthNameDetails;//Convert.ToInt32(args.Data.monthId);
                masterClassListModel.name = args.Data.className;
                masterClassListModel.masterClassId = args.Data.classId;

                _syllabusSetupListModel.classId= args.Data.classId;
                _syllabusSetupListModel.className = args.Data.className;
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
            {
                // Triggers before add operation starts

               
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Refresh)
            {
                // Triggers before add operation starts
                //this.Grid.Refresh();

            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Cancel)
            {
                // Triggers before cancel operation starts
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                syllabusSetupModel.SyllabusId = Convert.ToInt32(args.Data.syllabusId);
                syllabusSetupModel.MonthId = Convert.ToInt32(args.Data.monthNameDetails);
                syllabusSetupModel.ClassId = Convert.ToInt32(args.Data.className);
                syllabusSetupModel.SubjectId = Convert.ToInt32(args.Data.subjectName);
                syllabusSetupModel.UnitId = Convert.ToInt32(args.Data.unitName);
                syllabusSetupModel.ChapterId = Convert.ToInt32(args.Data.chapterName);
                syllabusSetupModel.TopicId = Convert.ToInt32(args.Data.topicName);
                syllabusSetupModel.SchoolCode = _sessionModel.SchoolCode;
                syllabusSetupModel.FinancialYear = _sessionModel.FinancialYear;
                syllabusSetupModel.CreatedByUserId = _sessionModel.UserId;
                syllabusSetupModel.OperationType = OperationAction.Add;
                SaveSyllabusSetupDetails(syllabusSetupModel);
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                // Triggers before delete operation starts
            }
        }
        private async Task SaveSyllabusSetupDetails(SyllabusSetupAPIModel syllabusSetupModel)
        {
            try
            {
                string msg = (await _syllabusSetupService.AddUpdateSyllabusSetup(syllabusSetupModel)).message.ToString();
                if (msg == "Exam Schedule Details created successfully")

                {
                    snackBar.Add(msg, Severity.Success);
                    this.Grid.Refresh();
                    GetSyllabusLoad();
                    
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    GetSyllabusLoad();
                    this.Grid.Refresh();
                   
                }
            }
            catch (Exception ex)
            {
                snackBar.Add(ex.Message, Severity.Error);
            }
        }

        public void ExportToExcel()
        {
            //if (args.Item.Id == "Grid_excelexport")  //Id is combination of Grid's ID and itemname
            //{
            ExcelExportProperties ExportProperties = new ExcelExportProperties();
            ExcelHeader header = new ExcelHeader();
            header.HeaderRows = 2;
            List<ExcelCell> cell = new List<ExcelCell>
        {
                new ExcelCell() { RowSpan= 2,ColSpan=5 , Value= "PO Report from " + "" + " to " + "" + " Vendor All", Style = new ExcelStyle() { Bold = true, FontSize = 13, Italic= true }  }
            };

            List<ExcelRow> HeaderContent = new List<ExcelRow>
        {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
            header.Rows = HeaderContent;
            ExportProperties.Header = header;
            this.Grid.ExcelExport(ExportProperties);
            //}

            //this.Grid.ExcelExport();
        }
        private SfDateRangePicker<DateTime?> DateRange;
        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "Grid_excelexport")  //Id is combination of Grid's ID and itemname      
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=5 , Value= "PO Report from " + DateRange.StartDate.ToString() + " to " + DateRange.EndDate.ToString() + " Vendor All", Style = new ExcelStyle() { Bold = true, FontSize = 13, Italic= true }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExportProperties.Header = header;
                await this.Grid.ExcelExport(ExportProperties);
            }
        }
         
    }
}
