using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.QuestionSetup;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.QuestionSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch.Operations;
using MudBlazor;
using Syncfusion.Blazor.Charts;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.QuestionBank
{
    public class QuestionBankBase : ComponentBase
    {
        /*Start Dialog */
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> QuestionBankListItems = (new List<string>() { "Add Question", "Print", "Search" });
                
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = true;

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }

        [Inject]
        public IQuestionsetup questionsetup { get; set; }   

        //list Model
        public List<Unit_Output_Model> unitMasterList = new List<Unit_Output_Model>();
        public List<Chapter_Output_Model> chapterMasterList = new List<Chapter_Output_Model>();
      

        public List<Topic_Output_Model> topicMasterList = new List<Topic_Output_Model>();
        public List<Topic_Output_Model> _topicMasterList = new List<Topic_Output_Model>();

        public List<Master_CLass_List_Output_Model> master_CLass_List = new List<Master_CLass_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> map_classwithsubject_List = new List<Master_Map_Subject_With_Class_List_Output_Model>();

        public List<QuestionBankListModel> questionBankListModels = new List<QuestionBankListModel>();
        
        public List<string> questionBankToolBar = (new List<string>() { "Clear All", "ExcelExport", "Print", "Search" });
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }


        public QuestionBankViewModel questionBankViewModel = new QuestionBankViewModel();

        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string DialogHeaderName = "Question Bank";// string.Empty;
        public SessionModel _sessionModel;
        public SfGrid<QuestionBankListModel> sfquestionListgrid;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            //{
            //    classId = 0,
            //    userId = _sessionModel.UserId,
            //    financialYear = _sessionModel.FinancialYear,
            //    schoolCode = _sessionModel.SchoolCode,
            //    reportType = ReportType.All
            //};
            //master_CLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();
            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.GetTeacherClass
            };
            master_CLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();


        }

        int _classID = 0;
        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if(unitMasterList.Count>0)
                { unitMasterList.Clear(); StateHasChanged(); }

                if (args.ItemData.classId != 0)
                {
                    _classID = args.ItemData.classId;

                    Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
                    {
                        classID = _classID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.ClassId,
                        userId = _sessionModel.UserId
                    };

                    map_classwithsubject_List = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
       int _subjectID=0;
        public async Task OnChangeSubject(ChangeEventArgs<string, Master_Map_Subject_With_Class_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.masterClassId != 0)
                {
                    _classID = args.ItemData.masterClassId;
                    _subjectID = args.ItemData.subjectId;

                    Unit_Input_Para_Model unit_Input_Para_Model = new Unit_Input_Para_Model()
                    {
                        classId = _classID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = SyllabusReportType.UnitlistClassWithSubject,
                        subjectId = _subjectID,
                        userId = _sessionModel.UserId,
                        userRoleId = _sessionModel.RoleId
                    };
                    if(unitMasterList.Count > 0) { unitMasterList.Clear(); StateHasChanged(); }
                    unitMasterList = (await syllabusService.Get_Unit_List(unit_Input_Para_Model)).ToList();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        int _unitID = 0;
        public async Task OnChangeUnit(ChangeEventArgs<string, Unit_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    //int classid = args.ItemData.classId;
                    _unitID = args.ItemData.unitId;
                    //int subjectID = args.ItemData.subjectID;

                    Chapter_Input_Para_Model chapter_Input_Para_Model = new Chapter_Input_Para_Model()
                    {
                        classId = _classID,
                        unitID = _unitID,
                        subjectId = _subjectID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.GetByMasterId,
                    };
                    chapterMasterList = (await syllabusService.Get_Chapter_List(chapter_Input_Para_Model)).ToList();
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
                    //_topicMasterList = topicMasterList.Where(m => m.chapterID == args.ItemData.chapterID).ToList();
                    Topic_Input_Para_Model chapter_Input_Para_Model = new Topic_Input_Para_Model()
                    {
                        classId = _classID,
                        unitID = _unitID,
                        subjectId = _subjectID,
                        chapterID = _chapterID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.GetByMasterId,
                    };
                    _topicMasterList = (await syllabusService.Get_Topic_List(chapter_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        int _topicID = 0;
        public async Task OnChangeTopic(ChangeEventArgs<string, Topic_Output_Model> args)
        {
            try
            {
                if (args.ItemData.topicID != 0)
                {

                    _topicID = args.ItemData.topicID;
                    //_topicMasterList = topicMasterList.Where(m => m.chapterID == args.ItemData.chapterID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        int _questiontypeID = 0;
        public async Task OnChangeQuestionType(ChangeEventArgs<string, QuestionType> args)
        {
            try
            {
                if (args.ItemData.Id != 0)
                {

                    _questiontypeID = args.ItemData.Id;
                    //_topicMasterList = topicMasterList.Where(m => m.chapterID == args.ItemData.chapterID).ToList();
                    QuestionBankList_ParaModel questionBankList_ParaModel = new QuestionBankList_ParaModel()
                    {
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        classId = _classID,
                        subjectId = _subjectID,
                        unitId = _unitID,
                        chapterId = _chapterID,
                        topicId = _topicID,
                        questionTypeId = _questiontypeID,
                        userRoleId = _sessionModel.RoleId,
                        reportType = ReportType.QuestionTypeList
                    };
                    questionBankListModels = (await questionsetup.GET_QuestionList(questionBankList_ParaModel)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }


        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {

                _classID = Convert.ToInt16(questionBankViewModel.masterClassId);
                _subjectID = Convert.ToInt16(questionBankViewModel.masterSubjectId);
                _unitID = Convert.ToInt16(questionBankViewModel.SubjectUnitId);
                _chapterID = Convert.ToInt16(questionBankViewModel.subjectChapterId);
                _topicID = Convert.ToInt16(questionBankViewModel.subjectTopicId);

                QuestionBankAPIModel questionBankAPIModel = new QuestionBankAPIModel()
                {
                    SchoolCode= _sessionModel.SchoolCode,
                    FinancialYear= _sessionModel.FinancialYear,

                    classId = questionBankViewModel.masterClassId,
                    subjectId= questionBankViewModel.masterSubjectId,
                    unitId= questionBankViewModel.SubjectUnitId,
                    chapterId= questionBankViewModel.subjectChapterId,
                    topicId= questionBankViewModel.subjectTopicId,

                    questionId = questionBankViewModel.QuestionBankId,
                    questionTypeId = questionBankViewModel.QuestionTypeId,

                    questionDetails= questionBankViewModel.ObjectiveQuestionDetails,
                    answeer_A= questionBankViewModel.Answeer_A,
                    answeer_B = questionBankViewModel.Answeer_B,
                    answeer_C= questionBankViewModel.Answeer_C,
                    answeer_D= questionBankViewModel.Answeer_D,
                    correctAnsweer= questionBankViewModel.CorrectAnsweer,
                    marks=Convert.ToInt32(questionBankViewModel.MarksNo),
                     
                    questionLevel= questionBankViewModel.QuestionLevel,
                    questionReview = 0,
                      
                    CreatedByUserId=_sessionModel.UserId,
                    UpdatedByUserId=_sessionModel.UserId,
                };




                if (questionBankViewModel.QuestionBankId == 0)
                {
                    questionBankAPIModel.OperationType = OperationAction.Add;
                    QuestionBankDetailsSave(questionBankAPIModel);
                }
                else
                {
                    questionBankAPIModel.OperationType = OperationAction.Update;
                    QuestionBankDetailsSave(questionBankAPIModel);
                }
            }
            else
            {
                // Form has invalid inputs.
            }

        }

        public APIReturnModel aPIReturnModel;
        private async void QuestionBankDetailsSave(QuestionBankAPIModel questionBankAPIModel)
        {
            try
            {
                if (questionBankAPIModel.OperationType != "NA")
                {
                    aPIReturnModel = (await questionsetup.CRUD_QuestionBank(questionBankAPIModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                        GetQuestionBankList();
                        sfquestionListgrid.Refresh();
                        ClearData();
                        
                        StateHasChanged();
                        
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

        public async void GetQuestionBankList()
        {

            try
            {
                //QuestionBankList_ParaModel questionBankList_ParaModel = new QuestionBankList_ParaModel()
                //{
                //    schoolCode = _sessionModel.SchoolCode,
                //    financialYear = _sessionModel.FinancialYear,
                //    classId = _classID,
                //    subjectId = _subjectID,
                //    unitId = _unitID,
                //    chapterId = _chapterID,
                //    topicId = _topicID,
                //    questionTypeId = _questiontypeID,
                //    userRoleId = _sessionModel.RoleId,
                //    reportType = ReportType.All
                //};
                //questionBankListModels = (await questionsetup.GET_QuestionList(questionBankList_ParaModel)).ToList();

                QuestionBankList_ParaModel questionBankList_ParaModel = new QuestionBankList_ParaModel()
                {
                    schoolCode = _sessionModel.SchoolCode,
                    financialYear = _sessionModel.FinancialYear,
                    classId = _classID,
                    subjectId = _subjectID,
                    unitId = _unitID,
                    chapterId = _chapterID,
                    topicId = _topicID,
                    questionTypeId = _questiontypeID,
                    userRoleId = _sessionModel.RoleId,
                    reportType = ReportType.QuestionTypeList
                };
                questionBankListModels = (await questionsetup.GET_QuestionList(questionBankList_ParaModel)).ToList();

            }
            catch { }

        }
        public void ClearData()
        {
            questionBankViewModel.ObjectiveQuestionDetails=string.Empty;
            questionBankViewModel.Answeer_A = string.Empty;
            questionBankViewModel.Answeer_B = string.Empty;
            questionBankViewModel.Answeer_D = string.Empty;
            questionBankViewModel.Answeer_C = string.Empty;


        }
        public Boolean ddEnable = true;
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public class QuestionType
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }


        public List<QuestionType> questionTypes = new List<QuestionType>()
        {
        new QuestionType(){ Id= 1, Text= "Objective Question" },
        new QuestionType(){ Id= 2, Text= "Rearrange the words" },
        new QuestionType(){ Id= 3, Text= "True or False." },
        new QuestionType(){ Id= 4, Text= "Complete the spelling" },
        new QuestionType(){ Id= 5, Text= "Choose and write" },
        new QuestionType(){ Id= 6, Text= "Write the correct answer" },
        new QuestionType(){ Id= 7, Text= "Write word meaning" },
        new QuestionType(){ Id= 8, Text= "Match the following" },
        new QuestionType(){ Id= 9, Text= "Long questions" },
        new QuestionType(){ Id= 10, Text= "Case Study Question" },

        };
        public class QuestionTypeLevel
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        public List<QuestionTypeLevel> questionTypesLevel = new List<QuestionTypeLevel>()
        {
        new QuestionTypeLevel(){ Id= 1, Text= "Level-1" },
        new QuestionTypeLevel(){ Id= 2, Text= "Level-2" },
        new QuestionTypeLevel(){ Id= 3, Text= "Level-3" },
        new QuestionTypeLevel(){ Id= 4, Text= "Level-4" }, 
        };
        public Syncfusion.Blazor.Navigations.SfTab Tab;
        public void Selected(Syncfusion.Blazor.Navigations.SelectEventArgs args)
        {
            if (args.SelectedIndex == 0)
            {
                // subjective question bind value
            }
            else
            {
                //objective question bind value
            }

            //  this.EventLog = args.IsInteracted ? "Tab Item selected by user interaction"
            //: "Tab Item selected by programmatically";
        }

        public class CorrectAnsweer
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        public List<CorrectAnsweer> correctAnsweer = new List<CorrectAnsweer>()
        {
        new CorrectAnsweer(){ Id= 1, Text= "A" },
        new CorrectAnsweer(){ Id= 2, Text= "B" },
        new CorrectAnsweer(){ Id= 3, Text= "C" },
        new CorrectAnsweer(){ Id= 4, Text= "D" },
        new CorrectAnsweer(){ Id= 5, Text= "Other" },

        };
        public class MaxMarks
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        public List<MaxMarks> maxMarks = new List<MaxMarks>()
        {
        new MaxMarks(){ Id= 1, Text= "1" },
        new MaxMarks(){ Id= 2, Text= "2" },
        new MaxMarks(){ Id= 3, Text= "3" },
        new MaxMarks(){ Id= 4, Text= "4" },
        new MaxMarks(){ Id= 5, Text= "5" },
        new MaxMarks(){ Id= 6, Text= "6" },
        new MaxMarks(){ Id= 7, Text= "7" },
        new MaxMarks(){ Id= 8, Text= "8" },
        new MaxMarks(){ Id= 9, Text= "9" },
        new MaxMarks(){ Id= 10, Text= "10" },
        };

        public void EditQuestionMaster(CommandClickEventArgs<QuestionBankListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            //if (buttontext == "Edit")
            //{
            //    //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
            //    //click to open edit dialog
            //    IsEditVisible = true;
            //    DialogHeaderName = "Update Chapter Details";
            //    HeaderText = "Update Record";
            //    OperationType = "Update";
            //    btncss = "e-flat e-info e-outline";
            //    ddEnable = false;
            //    topicMasterViewModel = new TopicMasterViewModel()
            //    {
            //        classId = Convert.ToString(args.RowData.classId),
            //        subjectId = Convert.ToString(args.RowData.subjectID),
            //        topicId = args.RowData.topicID,
            //        chapterId = args.RowData.chapterID.ToString(),
            //        unitId = args.RowData.unitId.ToString(),
            //        topicDescription = args.RowData.topicDescription,
            //        topicTitle = args.RowData.topicTitle

            //    };
            //}
            //else
            //{
            //    IsEditVisible = true;
            //    DialogHeaderName = "Delete Topic Details";
            //    OperationType = "Delete";
            //    HeaderText = "Delete Record";
            //    btncss = "e-flat e-danger e-outline";
            //    ddEnable = false;
            //    topicMasterViewModel = new TopicMasterViewModel()
            //    {
            //        classId = Convert.ToString(args.RowData.classId),
            //        subjectId = Convert.ToString(args.RowData.subjectID),
            //        topicId = args.RowData.topicID,
            //        chapterId = args.RowData.chapterID.ToString(),
            //        unitId = args.RowData.unitId.ToString(),
            //        topicDescription = args.RowData.topicDescription,
            //        topicTitle = args.RowData.topicTitle

            //    };
            //}

        }

        public void EditQuestionToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            //if (args.Item.Text == "Add Topic")
            //{
            //    //navigationManager.NavigateTo("/OnlineExam/TestList");
            //    //perform your actions here
            //    IsVisible = true;
            //    OperationType = "";
            //    btncss = "e-flat e-primary e-outline";
            //    DialogHeaderName = "Add New Topic Details";
            //    OperationType = "Add";
            //    HeaderText = "Add Topic";
            //    ddEnable = true;
            //    topicMasterViewModel.classId = null;
            //    topicMasterViewModel.subjectId = null;
            //    topicMasterViewModel.unitId = null;
            //    topicMasterViewModel.chapterId = null;
            //    topicMasterViewModel.topicTitle = null;
            //    topicMasterViewModel.topicDescription = null;
            //    if (_topicMasterList.Count > 0)
            //    {
            //        _chapterID = 0;
            //        _topicMasterList.Clear();
            //    }


            //}
            if (args.Item.Text == "Collapse All")
            {
                this.sfquestionListgrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfquestionListgrid.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {

                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Question List Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "QuestionList.xlsx";
                this.sfquestionListgrid.ExcelExport(ExcelProperties);
            }
        }
    }
}
