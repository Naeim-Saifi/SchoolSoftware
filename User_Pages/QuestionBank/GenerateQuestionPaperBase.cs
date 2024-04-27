using AdminDashboard.Server.API_Service.Interface.Exam;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.QuestionSetup;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.API_Service.Service.QuestionSetup;
using AdminDashboard.Server.Models.TimeTable;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.QuestionSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.UserLogin;
using Create_Word_document;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.QuestionBank
{
    public class GenerateQuestionPaperBase : ComponentBase
    {
        public Boolean ddEnable = true;
        /*Start Dialog */
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> QuestionBankListItems = (new List<string>() { "Add Question", "Print", "Search" });

        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsVisibleQuestionDetails { get; set; } = false;

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }

        [Inject]
        public IQuestionsetup questionsetup { get; set; }
        [Inject]
        public IExamMasterSetupService examMasterSetupService { get; set; }

        //list Model
        public List<Unit_Output_Model> unitMasterList = new List<Unit_Output_Model>();
        public List<Chapter_Output_Model> chapterMasterList = new List<Chapter_Output_Model>();


        public List<Topic_Output_Model> topicMasterList = new List<Topic_Output_Model>();
        public List<Topic_Output_Model> _topicMasterList = new List<Topic_Output_Model>();

        public List<Master_CLass_List_Output_Model> master_CLass_List = new List<Master_CLass_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> map_classwithsubject_List = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public List<SelectedQuestionListModel> selectedQuestionListModels = new List<SelectedQuestionListModel>();
        //API Model
        public List<SelectedQuestionListAPIModel> selectedQuestionListAPIModels = new List<SelectedQuestionListAPIModel>();

        public List<Exam_Type_List_Output_Model> _examTypeList = new List<Exam_Type_List_Output_Model>();

        public List<QuestionBankListModel> questionBankListModels = new List<QuestionBankListModel>();

        public List<QuestionPaperListOutPutModel>  questionPaperListOutPutModels = new List<QuestionPaperListOutPutModel>();
        public List<QuestionPaperDetailsListOutPutModel>  questionPaperDetailsListOutPutModels = new List<QuestionPaperDetailsListOutPutModel>();
        public List<QuestionPaperDetailsListOutPutModel> viewQuestionPaperDetailsList  = new List<QuestionPaperDetailsListOutPutModel>();


        public List<string> questionPaperToolBar = (new List<string>() { "Add Question Paper", "ExcelExport", "Print", "Search" });
        public List<string> questionBankToolBar = (new List<string>() { "Clear All", "ExcelExport", "Print", "Search" });

        public AnimationEffect ExpandEffect = AnimationEffect.SlideDown;
        public AnimationEffect CollapseEffect = AnimationEffect.SlideUp;

        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }


        public CreateQuestionPaperViewModel questionBankViewModel = new CreateQuestionPaperViewModel();

        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string DialogHeaderName = "Question Bank";// string.Empty;
        public SessionModel _sessionModel;
        public SfGrid<QuestionBankListModel> sfquestionListgrid;
        public SfGrid<SelectedQuestionListModel> sfSelectedquestionListgrid;
        public SfGrid<QuestionPaperListOutPutModel> questionPaperListgrid;

        public DialogEffect dialogAnimationEffect = DialogEffect.Zoom;
         
        
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
            Exam_Type_List_Input_Model exam_Type_List_Input_Model = new Exam_Type_List_Input_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                examTypeId = 0,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            _examTypeList = (await examMasterSetupService.GET_Exam_Type_MasterLIST(exam_Type_List_Input_Model)).ToList();

            QuestionPaperList_ParaModel questionPaperList_ParaModel = new QuestionPaperList_ParaModel() { schoolCode = _sessionModel.SchoolCode, financialYear = _sessionModel.FinancialYear,
                classId = 0, subjectId = 0, useriD = _sessionModel.UserId, userRoleId = _sessionModel.RoleId, reportType = "QuestionPaperMaster"
            };

            questionPaperListOutPutModels =(await questionsetup.GET_QuestionPaperList(questionPaperList_ParaModel)).ToList();

            questionPaperDetailsListOutPutModels = (await questionsetup.GET_QuestionPaperDetailsList(questionPaperList_ParaModel)).ToList();
        }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                Guid myuuid = Guid.NewGuid();
                string questionGeneratedID = myuuid.ToString();
                 


                SelectedQuestionListAPIModel selectedQuestionListAPIModel = new SelectedQuestionListAPIModel();

                if (selectedQuestionListModels.Count>0)
                {
                    for(int i = 0; i < selectedQuestionListModels.Count; i++) 
                    {
                        selectedQuestionListAPIModel.SchoolCode=_sessionModel.SchoolCode;
                        selectedQuestionListAPIModel.FinancialYear = _sessionModel.FinancialYear;
                        selectedQuestionListAPIModel.QuestionPaperID = 0;


                        selectedQuestionListAPIModel.QuestionPaperGeneratedID = questionGeneratedID;
                        selectedQuestionListAPIModel.classId = selectedQuestionListModels[i].classId;
                        selectedQuestionListAPIModel.subjectId = selectedQuestionListModels[i].subjectId;

                        selectedQuestionListAPIModel.unitId = selectedQuestionListModels[i].unitId;
                        selectedQuestionListAPIModel.chapterId = selectedQuestionListModels[i].chapterId;
                        selectedQuestionListAPIModel.topicId = selectedQuestionListModels[i].topicId;

                        selectedQuestionListAPIModel.QuestionID = selectedQuestionListModels[i].selectedQuestionID;


                        selectedQuestionListAPIModel.questionTypeId = selectedQuestionListModels[i].questionTypeId;
                       
                       
                        selectedQuestionListAPIModel.examTypeId = selectedQuestionListModels[i].examTypeId;
                        selectedQuestionListAPIModel.questionSectionGroup = selectedQuestionListModels[i].questionSectionGroup;
                        selectedQuestionListAPIModel.questionLevel = selectedQuestionListModels[i].questionLevel;
                       
                        selectedQuestionListAPIModel.CreatedByUserId = _sessionModel.UserId;
                        selectedQuestionListAPIModel.UpdatedByUserId = _sessionModel.UserId;
                        selectedQuestionListAPIModel.OperationType = "Add";
                        
                        selectedQuestionListAPIModels.Add(selectedQuestionListAPIModel);
                        selectedQuestionListAPIModel = new SelectedQuestionListAPIModel();
                    }
                }



                if(selectedQuestionListAPIModels.Count > 0)
                {
                    SaveQuestionPaper(selectedQuestionListAPIModels);
                }






        //if (questionBankViewModel.QuestionBankId == 0)
        //{
        //    questionBankAPIModel.OperationType = OperationAction.Add;
        //    QuestionBankDetailsSave(questionBankAPIModel);
        //}
        //else
        //{
        //    questionBankAPIModel.OperationType = OperationAction.Update;
        //    QuestionBankDetailsSave(questionBankAPIModel);
        //}
    }
            else
            {
                // Form has invalid inputs.
            }

        }
        public APIReturnModel aPIReturnModel;
        public async void SaveQuestionPaper(List<SelectedQuestionListAPIModel> selectedQuestionListAPIModels)
        {
             
                try
                {
                    if (selectedQuestionListAPIModels[0].OperationType != "NA")
                    {
                        aPIReturnModel = (await questionsetup.CRUD_SelectedQuestionPaper(selectedQuestionListAPIModels));

                        if (aPIReturnModel.status == false)
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Success);
                        //GetQuestionBankList();
                        selectedQuestionListModels.Clear();
                        sfSelectedquestionListgrid.Refresh();
                        StateHasChanged();
                            //ClearData();
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


        int _classID = 0;
        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        int _subjectID = 0;
        public async Task OnChangeSubject(ChangeEventArgs<string, Master_Map_Subject_With_Class_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.masterClassId != 0)
                {
                    int classid = args.ItemData.masterClassId;
                    _subjectID = args.ItemData.subjectId;

                    Unit_Input_Para_Model unit_Input_Para_Model = new Unit_Input_Para_Model()
                    {
                        classId = classid,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = SyllabusReportType.UnitlistClassWithSubject,
                        subjectId = _subjectID,
                        userId = _sessionModel.UserId,
                        userRoleId = _sessionModel.RoleId
                    };
                    unitMasterList = (await syllabusService.Get_Unit_List(unit_Input_Para_Model)).ToList();
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
                    _classID = args.ItemData.classId;
                    _unitID = args.ItemData.unitId;
                    _subjectID = args.ItemData.subjectID;

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

        string _questionGroupName=string.Empty;  
        public async Task OnChangeQuestionPaperGroup(ChangeEventArgs<string, QuestionPaperGroup> args)
        {
            try
            {
                if (args.ItemData.Id != 0)
                {

                    _questionGroupName = args.ItemData.Text;
                    //_topicMasterList = topicMasterList.Where(m => m.chapterID == args.ItemData.chapterID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        int _examTypeID = 0;
        public async Task OnChangeExamType(ChangeEventArgs<string, Exam_Type_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.examTypeId != 0)
                {

                    _examTypeID = args.ItemData.examTypeId;
                    //_topicMasterList = topicMasterList.Where(m => m.chapterID == args.ItemData.chapterID).ToList();
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        int _questionLevelID = 0;
        public async Task OnChangeQuestionTypeLevel(ChangeEventArgs<string, QuestionTypeLevel> args)
        {
            try
            {
                if (args.ItemData.Id != 0)
                {

                    _questionLevelID = args.ItemData.Id;
                    //_topicMasterList = topicMasterList.Where(m => m.chapterID == args.ItemData.chapterID).ToList();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
         
         
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

        public class QuestionPaperGroup
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        public List<QuestionPaperGroup> questionPaperGroup = new List<QuestionPaperGroup>()
        {
        new QuestionPaperGroup(){ Id= 1, Text= "Section-A" },
        new QuestionPaperGroup(){ Id= 2, Text= "Section-B" },
        new QuestionPaperGroup(){ Id= 3, Text= "Section-C" },
        new QuestionPaperGroup(){ Id= 4, Text= "Section-D" },
        };
      
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public List<QuestionBankListModel> SelectedRowIndexes { get; set; }
        public double[] TotalValue { get; set; }
        public string SelectedValue;

        public async Task GetSelectedRecords(RowSelectEventArgs<QuestionBankListModel> args)
        {
             

            SelectedQuestionListModel selectedQuestionListModel =new SelectedQuestionListModel();
            selectedQuestionListModel.classId=args.Data.classId;
            selectedQuestionListModel.subjectId = args.Data.subjectId;

            selectedQuestionListModel.unitId = args.Data.unitId;
            selectedQuestionListModel.chapterId = args.Data.chapterId;
            selectedQuestionListModel.topicId = args.Data.topicId;

            selectedQuestionListModel.questionTypeId = _questiontypeID;
            selectedQuestionListModel.questionLevel = _questionLevelID.ToString();

            selectedQuestionListModel.questionSectionGroup = _questionGroupName.ToString();
            selectedQuestionListModel.selectedQuestionID = args.Data.questionId;
            selectedQuestionListModel.questionDetails = args.Data.questionDetails;
            selectedQuestionListModel.examTypeId= _examTypeID.ToString();
            selectedQuestionListModels.Add(selectedQuestionListModel);
            sfSelectedquestionListgrid.Refresh();
 
            StateHasChanged();
        }
        



        public void RowDeselectHandler(RowDeselectEventArgs<QuestionBankListModel> args)
        {
            // Here, you can customize your code.

          var questionID=  selectedQuestionListModels.Where(m=>m.selectedQuestionID==args.Data.questionId).FirstOrDefault();
            if (questionID!=null) 
            { 
                selectedQuestionListModels.Remove(questionID);
                sfSelectedquestionListgrid.Refresh();
            }

        }

        public void SelectedQuestionToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
           
            if (args.Item.Text == "Collapse All")
            {
                this.sfSelectedquestionListgrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfSelectedquestionListgrid.ExpandAllGroupAsync();
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

        public void EditSelectedQuestionListModel(CommandClickEventArgs<SelectedQuestionListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                //IsEditVisible = true;
                //DialogHeaderName = "Update Chapter Details";
                //HeaderText = "Update Record";
                //OperationType = "Update";
                //btncss = "e-flat e-info e-outline";
                //ddEnable = false;
                //topicMasterViewModel = new TopicMasterViewModel()
                //{
                //    classId = Convert.ToString(args.RowData.classId),
                //    subjectId = Convert.ToString(args.RowData.subjectID),
                //    topicId = args.RowData.topicID,
                //    chapterId = args.RowData.chapterID.ToString(),
                //    unitId = args.RowData.unitId.ToString(),
                //    topicDescription = args.RowData.topicDescription,
                //    topicTitle = args.RowData.topicTitle

                //};
            }
            else
            {
                int qID = args.RowData.selectedQuestionID;

                var questionID = selectedQuestionListModels.Where(m => m.selectedQuestionID == qID).FirstOrDefault();
                if (questionID != null)
                {
                    selectedQuestionListModels.Remove(questionID);
                    sfSelectedquestionListgrid.Refresh();
                }


                //IsEditVisible = true;
                //DialogHeaderName = "Delete Topic Details";
                //OperationType = "Delete";
                //HeaderText = "Delete Record";
                //btncss = "e-flat e-danger e-outline";
                //ddEnable = false;
                //topicMasterViewModel = new TopicMasterViewModel()
                //{
                //    classId = Convert.ToString(args.RowData.classId),
                //    subjectId = Convert.ToString(args.RowData.subjectID),
                //    topicId = args.RowData.topicID,
                //    chapterId = args.RowData.chapterID.ToString(),
                //    unitId = args.RowData.unitId.ToString(),
                //    topicDescription = args.RowData.topicDescription,
                //    topicTitle = args.RowData.topicTitle

                //};
            }

        }


        public void questionPaperToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {

            if (args.Item.Text == "Collapse All")
            {
                this.sfSelectedquestionListgrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Add Question Paper")
            {
                //this.sfSelectedquestionListgrid.ExpandAllGroupAsync();
                this.IsVisible=true;
            }

            else if (args.Item.Text == "Expand All")
            {
                this.sfSelectedquestionListgrid.ExpandAllGroupAsync();
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
        
        [Inject]
        public Microsoft.JSInterop.IJSRuntime _JS { get; set; }
        [Inject]
        public WordFormatQuestionPaper wordFormatQuestionPaper { get; set; }    
        public void EditquestionPaper(CommandClickEventArgs<QuestionPaperListOutPutModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                //IsEditVisible = true;
                //DialogHeaderName = "Update Chapter Details";
                //HeaderText = "Update Record";
                //OperationType = "Update";
                //btncss = "e-flat e-info e-outline";
                //ddEnable = false;
                //topicMasterViewModel = new TopicMasterViewModel()
                //{
                //    classId = Convert.ToString(args.RowData.classId),
                //    subjectId = Convert.ToString(args.RowData.subjectID),
                //    topicId = args.RowData.topicID,
                //    chapterId = args.RowData.chapterID.ToString(),
                //    unitId = args.RowData.unitId.ToString(),
                //    topicDescription = args.RowData.topicDescription,
                //    topicTitle = args.RowData.topicTitle

                //};
            }
            else if(buttontext == "Download")
            {
                string fileName = string.Empty;
                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("dd MMMM yyyy HH:mm:ss");

                string questionGeneratedID = args.RowData.questionPaperGeneratedID.Trim().ToString();
                string _className = args.RowData.className.Trim().ToString();
                string _subjectName = args.RowData.subjectName.Trim().ToString();
                fileName = _className+"_"+_subjectName + "_" + formattedDateTime + ".docx";

                viewQuestionPaperDetailsList = questionPaperDetailsListOutPutModels.Where(m => m.questionPaperGeneratedID == questionGeneratedID).ToList();
                MemoryStream excelStream;
                excelStream = wordFormatQuestionPaper.CreateQuestionPaperInWordFormat("",_sessionModel.SchoolName,_sessionModel.FinancialYear, viewQuestionPaperDetailsList,"");
                _JS.SaveAs(fileName, excelStream.ToArray());
            }
            else
            {
               
                this.IsVisibleQuestionDetails = true;
                string questionGeneratedID = args.RowData.questionPaperGeneratedID.Trim().ToString();
                viewQuestionPaperDetailsList= questionPaperDetailsListOutPutModels.Where(m=>m.questionPaperGeneratedID == questionGeneratedID).ToList();    
                ////int qID = args.RowData.selectedQuestionID;

                //var questionID = selectedQuestionListModels.Where(m => m.selectedQuestionID == qID).FirstOrDefault();
                //if (questionID != null)
                //{
                //    selectedQuestionListModels.Remove(questionID);
                //    sfSelectedquestionListgrid.Refresh();
                //}


                //IsEditVisible = true;
                //DialogHeaderName = "Delete Topic Details";
                //OperationType = "Delete";
                //HeaderText = "Delete Record";
                //btncss = "e-flat e-danger e-outline";
                //ddEnable = false;
                //topicMasterViewModel = new TopicMasterViewModel()
                //{
                //    classId = Convert.ToString(args.RowData.classId),
                //    subjectId = Convert.ToString(args.RowData.subjectID),
                //    topicId = args.RowData.topicID,
                //    chapterId = args.RowData.chapterID.ToString(),
                //    unitId = args.RowData.unitId.ToString(),
                //    topicDescription = args.RowData.topicDescription,
                //    topicTitle = args.RowData.topicTitle

                //};
            }

        }

    }
}
