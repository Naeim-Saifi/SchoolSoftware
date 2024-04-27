//using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
//using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
//using AdminDashboard.Server.Models.QuestionBank;
//using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
//using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
//using AIS.FrontEnd_07062021.Service.Interface;
//using AIS.Model;
//using AIS.Model.GeneralConversion;
//using AIS.Model.MasterData;
//using AIS.Model.UserLogin;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Forms;
//using MudBlazor;
//using Syncfusion.Blazor.DropDowns;
//using Syncfusion.Blazor.Grids;
//using Syncfusion.Blazor.Popups;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AdminDashboard.Server.Pages.QuestionBank
//{
//    public class QuestionBankBase : ComponentBase
//    {
//        /*Start Dialog */
//        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
//        public List<object> MenuItems = new List<object>()
//            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
//                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
//            };
//        public List<string> QuestionBankListItems = (new List<string>() { "Add Question", "Print", "Search" });

//        public SfGrid<TopicMasterListModel> sfQuestionBank;
//        public SfGrid<QuestionBankDetailsList> sfQuestionBankList;
//        public SfDialog DialogRef;
//        public bool IsVisible { get; set; } = false;
//        public QuestionBankViewModel  questionBankViewModel = new QuestionBankViewModel();

//        [Inject]
//        Blazored.SessionStorage.ISessionStorageService session { get; set; }
//        public SessionModel _sessionModel;

//        [Inject]
//        public IUnitMasterService unitmasterService { get; set; }
//        public List<TopicMasterListModel> topicMasterListModels = new List<TopicMasterListModel>();
//        public List<ChapterMasterListModel> chapterMasterListModels = new List<ChapterMasterListModel>();
//        [Inject]
//        public IMasterClassService masterClassService { get; set; }
//        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();

//        [Inject]
//        public ISubjectMasterService _subjectMasterService { get; set; }

//        public SubjectDetailsModel subjectDetailsModel;
//        public List<MasterSubjectListModel> mapSubjectList = new List<MasterSubjectListModel>();
//        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
//        public List<MapsubjectwithClassModel> _mastersubjectlistModel = new List<MapsubjectwithClassModel>();

//        [Inject]
//        public ISnackbar snackBar { get; set; }


//        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
//        {
//            // setting maximum height to the Dialog
//            args.MaxHeight = "750px";
           
//        }
//        public string DialogHeaderName = "Question Bank";// string.Empty;
//        public async void OnValidSubmit(EditContext contex)
//        {
//            bool isValid = contex.Validate();
//            if (isValid)
//            {
//                //TopicMasterModel topicMasterModel = new TopicMasterModel()
//                //{
//                //    SubjectChapterId = Convert.ToInt32(topicMasterViewModel.subjectChapterId),
//                //    SubjectTopicId = Convert.ToInt32(topicMasterViewModel.subjectTopicId),
//                //    TopicTitle = topicMasterViewModel.subjectTopicTitle.ToUpper(),
//                //    TopicDescription = topicMasterViewModel.subjectTopicDescription.ToUpper(),
//                //    ActiveStatus = 1,
//                //    CreatedByUserId = _sessionModel.UserId,
//                //    FinancialYear = _sessionModel.FinancialYear,
//                //    SchoolCode = _sessionModel.SchoolCode,
//                //    UpdatedByUserId = 0,
//                //};

//                //if (topicMasterModel.SubjectTopicId == 0)
//                //{
//                //    topicMasterModel.OperationType = OperationAction.Add;
//                //    TopicDetailsSave(topicMasterModel);
//                //}
//                //else
//                //{
//                //    topicMasterModel.OperationType = OperationAction.Update;
//                //    TopicDetailsSave(topicMasterModel);
//                //}
//            }
//            else
//            {
//                // Form has invalid inputs.
//            }

//        }

//        protected override async Task OnInitializedAsync()
//        {
//            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
//            // chapterMasterListModels = (await unitmasterService.GetChapterMasterDetails(_sessionModel.SchoolCode, "All")).ToList();
//            LoadTopicDetails();
//            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();
//            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
//            mapSubjectList = subjectDetailsModel.masterSubjectListModels;
//            _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
//            SubjectList(0);

//            questionBankDetailsLists = new List<QuestionBankDetailsList>()
//            { 
//            new QuestionBankDetailsList(){ questionId=1001, classId=10,className="IV",
//                                            subjectName="English", unit_chapter_topic="Unit-I Chapter-I Topic-I",
//                                           questionType="Objective Question",
//                                            questionDetails="Objective Question details description",
//            answeer_A="Answeer-A",
//            answeer_B="Answeer-B",
//            answeer_C="Answeer-C",
//            answeer_D="Answeer-D"}
//            };

//        }

//        public void EditQuestion(CommandClickEventArgs<QuestionBankDetailsList> args)
//        {
//            // Perform required operations here
//            string buttontext = args.CommandColumn.ButtonOption.Content;
//            //int testId = args.RowData.testID;
//            if (buttontext == "Edit")
//            {
//                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
//                IsVisible = true;
//            }
//            //else
//            //{
//            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
//            //}

//        }
//        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
//        {


//            if (args.Item.Text == "Add Question")
//            {
//                //navigationManager.NavigateTo("/OnlineExam/TestList");
//                //perform your actions here
//                IsVisible = true;
//            }
//        }

//        private async void LoadTopicDetails()
//        {
//           // topicMasterListModels = (await unitmasterService.GetTopicMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, "All")).ToList();
//        }
//        public Boolean ddEnable = true;
//        public IEnumerable<TopicMasterListModel> unitDetails;
//        public IEnumerable<TopicMasterListModel> chapterDetails;
//        public IEnumerable<TopicMasterListModel> topicDetails;
//        public void OnChangeClass(ChangeEventArgs<string, MasterClassListModel> args)
//        {
//            try
//            {
//                if (args.ItemData.masterClassId != 0 && args.ItemData.masterClassId != null)
//                {
//                    int classid = args.ItemData.masterClassId;
//                    SubjectList(classid);

//                    //_mapsubjectlistModel = subj.ToList();
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }


//        }
//        private void SubjectList(int ClassID)
//        {
//            if (ClassID == 0)
//            {
//                _mastersubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
//                _mapsubjectlistModel = _mastersubjectlistModel.ToList();
//                unitDetails = topicMasterListModels.ToList();
//                chapterDetails = topicMasterListModels.ToList();
//            }
//            else
//            {
//                _mapsubjectlistModel = _mastersubjectlistModel.Where(e => e.masterClassId == ClassID).ToList();
//            }

//        }
//        public void OnChangeSubject(ChangeEventArgs<string, MapsubjectwithClassModel> args)
//        {
//            try
//            {
//                if (args.ItemData.masterSubjectId.ToString() != null)
//                {
//                    int subjectId = Convert.ToInt32(args.ItemData.masterSubjectId);

//                    unitDetails = topicMasterListModels.Where(e => e.masterSubjectId == subjectId);
//                    unitDetails.Distinct();
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }
//        public void OnChangeUnit(ChangeEventArgs<string, TopicMasterListModel> args)
//        {
//            try
//            {
//                if (args.ItemData.subjectUnitId.ToString() != null)
//                {
//                    int unitId = Convert.ToInt32(args.ItemData.subjectUnitId);

//                    chapterDetails = topicMasterListModels.Where(e => e.subjectUnitId == unitId);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }
//        public void OnChangeChapter(ChangeEventArgs<string, TopicMasterListModel> args)
//        {
//            try
//            {
//                if (args.ItemData.subjectUnitId.ToString() != null)
//                {
//                    int chapterId = Convert.ToInt32(args.ItemData.subjectChapterId);

//                    topicDetails = topicMasterListModels.Where(e => e.subjectChapterId == chapterId);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }

//        public async Task CloseDialog()
//        {
//            IsVisible = false;
//            await this.DialogRef.HideAsync();
//        }
//        public DialogEffect AnimationEffect = DialogEffect.Zoom;
//        public string HeaderStyles { get; set; } = "e-background e-accent";
//        /*End Dialog */
//        public class QuestionType
//        {
//            public int Id { get; set; }
//            public string Text { get; set; }
//        }

//        public List<QuestionType>   questionTypes = new List<QuestionType>()
//        {
//        new QuestionType(){ Id= 1, Text= "Objective Question" },
//        new QuestionType(){ Id= 2, Text= "Rearrange the words" },
//        new QuestionType(){ Id= 3, Text= "True or False." },
//        new QuestionType(){ Id= 4, Text= "Complete the spelling" },
//        new QuestionType(){ Id= 5, Text= "Choose and write" },
//        new QuestionType(){ Id= 6, Text= "Write the correct answer" },
//        new QuestionType(){ Id= 7, Text= "Write word meaning" },
//        new QuestionType(){ Id= 8, Text= "Match the following" },
//        new QuestionType(){ Id= 9, Text= "Long questions" },
//        new QuestionType(){ Id= 10, Text= "Case Study Question" },
       
//        };
//        public class CorrectAnsweer
//        {
//            public int Id { get; set; }
//            public string Text { get; set; }
//        }
//        public List<CorrectAnsweer> correctAnsweer = new List<CorrectAnsweer>()
//        {
//        new CorrectAnsweer(){ Id= 1, Text= "A" },
//        new CorrectAnsweer(){ Id= 2, Text= "B" },
//        new CorrectAnsweer(){ Id= 3, Text= "C" },
//        new CorrectAnsweer(){ Id= 4, Text= "D" },
//        new CorrectAnsweer(){ Id= 5, Text= "Other" },
         

//        };
//        public class MaxMarks
//        {
//            public int Id { get; set; }
//            public string Text { get; set; }
//        }
//        public List<MaxMarks>  maxMarks = new List<MaxMarks>()
//        {
//        new MaxMarks(){ Id= 1, Text= "1" },
//        new MaxMarks(){ Id= 2, Text= "2" },
//        new MaxMarks(){ Id= 3, Text= "3" },
//        new MaxMarks(){ Id= 4, Text= "4" },
//        new MaxMarks(){ Id= 5, Text= "5" },
//        new MaxMarks(){ Id= 6, Text= "6" },
//        new MaxMarks(){ Id= 7, Text= "7" },
//        new MaxMarks(){ Id= 8, Text= "8" },
//        new MaxMarks(){ Id= 9, Text= "9" },
//        new MaxMarks(){ Id= 10, Text= "10" },
//        };

//       public Syncfusion.Blazor.Navigations.SfTab Tab;

//        public  String EventLog = null;
//        public void Selected(Syncfusion.Blazor.Navigations.SelectEventArgs args)
//        {
//            if(args.SelectedIndex==0)
//            {
//                // subjective question bind value
//            }
//            else
//            {
//                //objective question bind value
//            }

//          //  this.EventLog = args.IsInteracted ? "Tab Item selected by user interaction"
//          //: "Tab Item selected by programmatically";
//        }

//        public class QuestionBankDetailsList
//        {
//            public int questionId { set; get; }
//            public int classId { get; set; }
//            public string className { get; set; }
//            public string subjectName { get; set; }
//            public string unit_chapter_topic { get; set; }
//            public string questionType { get; set; }
//            public string questionDetails { get; set; }
//            public  string answeer_A { get; set; }
//            public string answeer_B { get; set; }
//            public string answeer_C { get; set; }
//            public string answeer_D { get; set; }
//        }

//        public List<QuestionBankDetailsList> questionBankDetailsLists { get; set; }

        

//    }
//}
