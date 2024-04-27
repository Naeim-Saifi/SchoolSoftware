using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.Models.Holiday;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.OData.Edm.Vocabularies;
using MudBlazor;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.Syllabus
{
    public class SyllabusPlanerSetupBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        //list Model
        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> _subjectList = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public List<Unit_Output_Model> _unitList = new List<Unit_Output_Model>();
        public List<Chapter_Output_Model> _chapterList = new List<Chapter_Output_Model>();
        public List<Topic_Output_Model> _topicList = new List<Topic_Output_Model>();
        public List<Syllabus_Output_Model> _syllabusList = new List<Syllabus_Output_Model>();
        public List<MonthNameListModel> _monthNameList = new List<MonthNameListModel>();
        //data binding for blazor page
        public SyllabusSetupViewModel  syllabusSetupViewModel = new SyllabusSetupViewModel();
        public SyllabusSetupModel  syllabusSetupModel = new SyllabusSetupModel();
        public APIReturnModel aPIReturnModel;
        public SessionModel _sessionModel;
        public string OperationType = "";

        public DateTime? fromValue { get; set; } = DateTime.Now;
        public DateTime? toValue { get; set; } = DateTime.Now;
        public DateTime? fromDate { get; set; } = DateTime.Now;
        public DateTime? toDate { get; set; } = DateTime.Now;
        
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<Syllabus_Output_Model> sfSyllabusGrid;

        public List<string> toolBarItems = (new List<string>() {  "Add Syllabus", "ExcelExport", "Collapse All", "Expand All", "Print", "Search" });
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            _monthNameList = (await syllabusService.GetMonthNameList()).ToList();
            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            Syllabus_Input_Para_Model syllabus_Input_Para_Model = new Syllabus_Input_Para_Model()
            {
                classId = 0,
                subjectId = 0,              
                unitID = 0,
                chapterID = 0,
                monthID = 0,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All,

            };
            _syllabusList = (await syllabusService.Get_Syllabus_List(syllabus_Input_Para_Model)).ToList();

        }
        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    int classid = args.ItemData.classId;

                    Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
                    {
                        classID = classid,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.ClassId,
                        userId = _sessionModel.UserId
                    };

                    _subjectList = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public async Task OnChangeSubject(ChangeEventArgs<string, Master_Map_Subject_With_Class_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.masterClassId != 0)
                {
                    int classid = args.ItemData.masterClassId;
                    int subjectID = args.ItemData.subjectId;

                    Unit_Input_Para_Model unit_Input_Para_Model = new Unit_Input_Para_Model()
                    {
                        classId = classid,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = SyllabusReportType.UnitlistClassWithSubject,
                        subjectId = subjectID,
                        userId = _sessionModel.UserId,
                        userRoleId = _sessionModel.RoleId
                    };
                    _unitList = (await syllabusService.Get_Unit_List(unit_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public async Task OnChangeUnit(ChangeEventArgs<string, Unit_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    int classid = args.ItemData.classId;
                    int unitID = args.ItemData.unitId;
                    int subjectID = args.ItemData.subjectID;

                    Chapter_Input_Para_Model chapter_Input_Para_Model = new Chapter_Input_Para_Model()
                    {
                        classId = classid,
                        unitID = unitID,
                        subjectId = subjectID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.GetByMasterId,
                    };
                    _chapterList = (await syllabusService.Get_Chapter_List(chapter_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public async Task OnChangeChapter(ChangeEventArgs<string, Chapter_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    int classid = args.ItemData.classId;                   
                    int subjectID = args.ItemData.subjectId;
                    int unitID = args.ItemData.unitId;
                    int chapterID = args.ItemData.chapterID;

                    Topic_Input_Para_Model chapter_Input_Para_Model = new Topic_Input_Para_Model()
                    {
                        classId = classid,
                        unitID = unitID,
                        subjectId = subjectID,
                        chapterID= chapterID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.GetByMasterId,
                    };
                    _topicList = (await syllabusService.Get_Topic_List(chapter_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Syllabus")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Syllabus Details";
                OperationType = "Add";
                HeaderText = "Add Syllabus";
                ddEnable = true;
                syllabusSetupViewModel.classId = null;
                syllabusSetupViewModel.subjectId = null;
                syllabusSetupViewModel.unitId = null;
                syllabusSetupViewModel.chapterId = null;
                syllabusSetupViewModel.topicId = null;
                syllabusSetupViewModel.syllabusDescription = null;

            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfSyllabusGrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfSyllabusGrid.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {


                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Syllabus Plan  Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "SyllabusPlan.xlsx";
                this.sfSyllabusGrid.ExcelExport(ExcelProperties);
            }
        }
        public void onChange(RangePickerEventArgs<DateTime?> args)
        {
            syllabusSetupViewModel.fromDate =Convert.ToDateTime(args.StartDate);
            syllabusSetupViewModel.toDate = Convert.ToDateTime(args.EndDate);
           
            StateHasChanged();
        }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                syllabusSetupModel = new SyllabusSetupModel()
                {
                    monthId= Convert.ToInt16(syllabusSetupViewModel.monthId),
                    classId = Convert.ToInt16(syllabusSetupViewModel.classId),
                    subjectId = Convert.ToInt16(syllabusSetupViewModel.subjectId),
                    unitId = Convert.ToInt16(syllabusSetupViewModel.unitId),
                    chapterId = Convert.ToInt16(syllabusSetupViewModel.chapterId),
                    topicId = Convert.ToInt16(syllabusSetupViewModel.topicId),
                    fromDate= syllabusSetupViewModel.fromDate,
                    toDate = syllabusSetupViewModel.toDate,                    
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    syllabusSetupModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    syllabusSetupModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    syllabusSetupModel.OperationType = OperationAction.Delete;
                }
                SyllabusDetailsSave(syllabusSetupModel);
            }
        }

        private async void SyllabusDetailsSave(SyllabusSetupModel  syllabusSetupModel)
        {
            try
            {
                if (syllabusSetupModel.OperationType != "NA")
                {
                    aPIReturnModel = (await syllabusService.CRUD_SyllabusSetup(syllabusSetupModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);

                        Syllabus_Input_Para_Model syllabus_Input_Para_Model = new Syllabus_Input_Para_Model()
                        {
                            classId = 0,
                            subjectId = 0,
                            unitID = 0,
                            chapterID = 0,
                            monthID = 0,
                            schoolCode = _sessionModel.SchoolCode,
                            financialYear = _sessionModel.FinancialYear,
                            reportType = ReportType.All,
                        };
                        _syllabusList = (await syllabusService.Get_Syllabus_List(syllabus_Input_Para_Model)).ToList();
                        StateHasChanged();
                        ClearData();
                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClearData()
        {
            syllabusSetupViewModel = new SyllabusSetupViewModel();

        }

        public void EditSyllabusMaster(CommandClickEventArgs<Syllabus_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                IsVisible = true;
                DialogHeaderName = "Update Syllabus Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                syllabusSetupViewModel = new SyllabusSetupViewModel()
                {
                    syllabusId = args.RowData.syllabusID,
                    monthId = args.RowData.monthID.ToString(),
                    classId = args.RowData.classId.ToString(),
                    subjectId = args.RowData.subjectID.ToString(),
                    unitId = args.RowData.unitId.ToString(),
                    chapterId = args.RowData.chapterID.ToString(),
                    topicId = args.RowData.topicID.ToString(), 
                    syllabusDescription = args.RowData.syllabusDescription,
                   
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Syllabus Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                syllabusSetupViewModel = new SyllabusSetupViewModel()
                {
                    syllabusId = args.RowData.syllabusID,
                    monthId = args.RowData.monthID.ToString(),
                    classId = args.RowData.classId.ToString(),
                    subjectId = args.RowData.subjectID.ToString(),
                    unitId = args.RowData.unitId.ToString(),
                    chapterId = args.RowData.chapterID.ToString(),
                    topicId = args.RowData.topicID.ToString(),
                    syllabusDescription = args.RowData.syllabusDescription,

                };
            }

        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
    }
}
