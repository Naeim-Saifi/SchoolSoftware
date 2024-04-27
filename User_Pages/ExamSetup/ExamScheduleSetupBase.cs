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
using Syncfusion.Blazor.Buttons;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Charts;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.ExamSetup
{
    public class ExamScheduleSetupBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IExamMasterSetupService examMasterSetupService { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }

        //list Model
        public List<Exam_Schedule_Details_List_Output_Model> _examScheduleList = new List<Exam_Schedule_Details_List_Output_Model>();
        //data binding for blazor page
        public ExamScheduleViewModel  examScheduleViewModel =             new ExamScheduleViewModel();
        public APIReturnModel aPIReturnModel;


        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> _subjectList = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        //list Model
        public List<Unit_Output_Model> _unitList = new List<Unit_Output_Model>();
        public List<Chapter_Output_Model> _chapterList = new List<Chapter_Output_Model>();
        public List<Topic_Output_Model> _topicList = new List<Topic_Output_Model>();

        public DateTime MinVal { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 08, 00, 00);
        public DateTime MaxVal { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 16, 45, 00);
        public DateTime? TimeValue { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 11, 40, 00);

        public Exam_schedule_Details_Model examScheduleAPIModel { get; set; }

        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<Exam_Schedule_Details_List_Output_Model> sfExamScheduleListGrid;

        public List<string> toolBarItems = (new List<string>() { "Setup Exam Schedue", "Print", "ExportExcel", "Collapse All", "Expand All", "Search", "ColumnChooser" });
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        //this model use for send data to API ,binding view model with API model

        public SessionModel _sessionModel;
        public string OperationType = "";

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            Exam_Schedule_Details_List_Input_Model  exam_Schedule_Details_List_Input = new Exam_Schedule_Details_List_Input_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                examTypeId = 0,
                chapterId = 0,
                classId = 0,
                examScheduleID = 0,
                monthId = 0,
                sectionId = 0,
                subjectId = 0,
                topicID = 0,    
                unitId = 0,
                userId = 0,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };

            _examScheduleList = (await examMasterSetupService.GET_Exam_Schedule_Details_LIST(exam_Schedule_Details_List_Input)).ToList();
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
            //_sectionList = (await masterDataSetupService.GET_Master_SectionLIST(master_section_List_Input_Para_Model)).ToList();

           
        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Setup Exam Schedue")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Setup Exam Schedue";
                OperationType = "Add";
                HeaderText = "Setup Exam Schedue";
                ddEnable = true;
                //syllabusSetupViewModel.classId = null;
                //syllabusSetupViewModel.subjectId = null;
                //syllabusSetupViewModel.unitId = null;
                //syllabusSetupViewModel.chapterId = null;
                //syllabusSetupViewModel.topicId = null;
                //syllabusSetupViewModel.syllabusDescription = null;

            }
           else if (args.Item.Text == "ExportExcel")
            {
                this.sfExamScheduleListGrid.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfExamScheduleListGrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfExamScheduleListGrid.ExpandAllGroupAsync();
            }
        }

        public void EditExamScheduleSetupMaster(CommandClickEventArgs<Exam_Schedule_Details_List_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                IsVisible = true;
                DialogHeaderName = "Update Chapter Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                //examViewModel = new ExamTypeViewModel()
                //{
                //    displayOrder = args.RowData.displayOrder,
                //    examType = args.RowData.examType,
                //    examTypeId = args.RowData.examTypeId,
                //    examTypeCode = args.RowData.examTypeCode,
                //    examTypeDescription = args.RowData.examTypeDescription,
                //};
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Chapter Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                //examViewModel = new ExamTypeViewModel()
                //{
                //    displayOrder = args.RowData.displayOrder,
                //    examType = args.RowData.examType,
                //    examTypeId = args.RowData.examTypeId,
                //    examTypeCode = args.RowData.examTypeCode,
                //    examTypeDescription = args.RowData.examTypeDescription,
                //};
            }

        }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                //examtypeAPIModel = new Exam_Type_Master_Model()
                //{
                //    examTypeId = Convert.ToInt16(examViewModel.examTypeId),
                //    examType = examViewModel.examType,
                //    examTypeDescription = examViewModel.examTypeDescription,
                //    examTypeCode = examViewModel.examTypeCode,
                //    displayOrder = examViewModel.displayOrder,
                //    CreatedByUserId = _sessionModel.UserId,
                //    UpdatedByUserId = _sessionModel.UserId,
                //    SchoolCode = _sessionModel.SchoolCode,
                //    FinancialYear = _sessionModel.FinancialYear,

                //};

                //if (OperationType == "Add")
                //{
                //    examtypeAPIModel.OperationType = OperationAction.Add;
                //}
                //else if (OperationType == "Update")
                //{
                //    examtypeAPIModel.OperationType = OperationAction.Update;
                //}
                ////Delete Operation
                //else
                //{
                //    examtypeAPIModel.OperationType = OperationAction.Delete;
                //}
                //ExamTypeSave(examtypeAPIModel);
            };
        }

        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

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

        int _chapterID = 0;
        public async Task OnChangeChapter(ChangeEventArgs<string, Chapter_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {

                    _chapterID = args.ItemData.chapterID;
                    Topic_Input_Para_Model chapter_Input_Para_Model = new Topic_Input_Para_Model()
                    {
                        classId = args.ItemData.classId,
                        unitID = args.ItemData.unitId,
                        subjectId = args.ItemData.subjectId,
                        chapterID = args.ItemData.chapterID,
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
          
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public DateTime? DateValue { get; set; } = DateTime.Now;
        public void onChange(Syncfusion.Blazor.Calendars.ChangedEventArgs<DateTime?> args)
        {
            DateValue = args.Value;
            examScheduleViewModel.fromDate =Convert.ToDateTime(DateValue);
            StateHasChanged();
        }
        public bool isChecked = false;
        public SfSwitch<bool> SwitchObj;
        public void create(object obj)
        {
            //SwitchObj.Toggle();
        }



       
    }
}
