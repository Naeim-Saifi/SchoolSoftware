using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.QuestionBank;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Lists;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.QuestionBank
{
    public class QuestionPaperGenerateBase : ComponentBase
    {
        /*Start Dialog */
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

        public SfGrid<TopicMasterListModel> sfQuestionBank;
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = true;
        public QuestionPaperGenerate  questionPaperGenerate = new QuestionPaperGenerate();

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        [Inject]
        public IUnitMasterService unitmasterService { get; set; }
        public List<TopicMasterListModel> topicMasterListModels = new List<TopicMasterListModel>();
        public List<ChapterMasterListModel> chapterMasterListModels = new List<ChapterMasterListModel>();
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();

        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }

        public SubjectDetailsModel subjectDetailsModel;
        public List<MasterSubjectListModel> mapSubjectList = new List<MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<MapsubjectwithClassModel> _mastersubjectlistModel = new List<MapsubjectwithClassModel>();

        [Inject]
        public ISnackbar snackBar { get; set; }


        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string DialogHeaderName = "Question Bank";// string.Empty;
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                //TopicMasterModel topicMasterModel = new TopicMasterModel()
                //{
                //    SubjectChapterId = Convert.ToInt32(topicMasterViewModel.subjectChapterId),
                //    SubjectTopicId = Convert.ToInt32(topicMasterViewModel.subjectTopicId),
                //    TopicTitle = topicMasterViewModel.subjectTopicTitle.ToUpper(),
                //    TopicDescription = topicMasterViewModel.subjectTopicDescription.ToUpper(),
                //    ActiveStatus = 1,
                //    CreatedByUserId = _sessionModel.UserId,
                //    FinancialYear = _sessionModel.FinancialYear,
                //    SchoolCode = _sessionModel.SchoolCode,
                //    UpdatedByUserId = 0,
                //};

                //if (topicMasterModel.SubjectTopicId == 0)
                //{
                //    topicMasterModel.OperationType = OperationAction.Add;
                //    TopicDetailsSave(topicMasterModel);
                //}
                //else
                //{
                //    topicMasterModel.OperationType = OperationAction.Update;
                //    TopicDetailsSave(topicMasterModel);
                //}
            }
            else
            {
                // Form has invalid inputs.
            }

        }

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            // chapterMasterListModels = (await unitmasterService.GetChapterMasterDetails(_sessionModel.SchoolCode, "All")).ToList();
            LoadTopicDetails();
            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
            mapSubjectList = subjectDetailsModel.masterSubjectListModels;
            _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
            SubjectList(0);
        }
        private async void LoadTopicDetails()
        {
            //topicMasterListModels = (await unitmasterService.GetTopicMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, "All")).ToList();
        }
        public Boolean ddEnable = true;
        public IEnumerable<TopicMasterListModel> unitDetails;
        public IEnumerable<TopicMasterListModel> chapterDetails;
        public IEnumerable<TopicMasterListModel> topicDetails;
        public void OnChangeClass(ChangeEventArgs<string, MasterClassListModel> args)
        {
            try
            {
                if (args.ItemData.masterClassId != 0 && args.ItemData.masterClassId != null)
                {
                    int classid = args.ItemData.masterClassId;
                    SubjectList(classid);

                    //_mapsubjectlistModel = subj.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        private void SubjectList(int ClassID)
        {
            if (ClassID == 0)
            {
                _mastersubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
                _mapsubjectlistModel = _mastersubjectlistModel.ToList();
                unitDetails = topicMasterListModels.ToList();
                chapterDetails = topicMasterListModels.ToList();
            }
            else
            {
                _mapsubjectlistModel = _mastersubjectlistModel.Where(e => e.masterClassId == ClassID).ToList();
            }

        }
        public void OnChangeSubject(ChangeEventArgs<string, MapsubjectwithClassModel> args)
        {
            try
            {
                if (args.ItemData.masterSubjectId.ToString() != null)
                {
                    int subjectId = Convert.ToInt32(args.ItemData.masterSubjectId);

                    unitDetails = topicMasterListModels.Where(e => e.masterSubjectId == subjectId);
                    unitDetails.Distinct();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void OnChangeUnit(ChangeEventArgs<string, TopicMasterListModel> args)
        {
            try
            {
                if (args.ItemData.subjectUnitId.ToString() != null)
                {
                    int unitId = Convert.ToInt32(args.ItemData.subjectUnitId);

                    chapterDetails = topicMasterListModels.Where(e => e.subjectUnitId == unitId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void OnChangeChapter(ChangeEventArgs<string, TopicMasterListModel> args)
        {
            try
            {
                if (args.ItemData.subjectUnitId.ToString() != null)
                {
                    int chapterId = Convert.ToInt32(args.ItemData.subjectChapterId);

                    topicDetails = topicMasterListModels.Where(e => e.subjectChapterId == chapterId);
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
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        /*End Dialog */
        public class QuestionSection
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }

        public List<QuestionSection> questionSection = new List<QuestionSection>()
        {
        new QuestionSection(){ Id= 1, Text= "Section-A" },
        new QuestionSection(){ Id= 2, Text= "Section-B" },
        new QuestionSection(){ Id= 3, Text= "Section-C" },
        new QuestionSection(){ Id= 1, Text= "Section-D" },
        new QuestionSection(){ Id= 2, Text= "Section-E" },
        new QuestionSection(){ Id= 3, Text= "Section-F" }
        };
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
        public class ExamType
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }

        public List<ExamType>  examTypes = new List<ExamType>()
        {
        new ExamType(){ Id= 1, Text= "FA-I" },
        new ExamType(){ Id= 2, Text= "FA-II" },
        new ExamType(){ Id= 1, Text= "SA-I" },
        new ExamType(){ Id= 2, Text= "FA-III" },
        new ExamType(){ Id= 1, Text= "FA-IV" },
        new ExamType(){ Id= 2, Text= "SA-II" },
        new ExamType(){ Id= 1, Text= "For Online" },
        new ExamType(){ Id= 2, Text= "Other" },
        };

        public DataModel[] Data = {

        new DataModel { Text ="Hennessey Venom", Id = "list-01" },
        new DataModel { Text = "Bugatti Chiron", Id = "list-02" },
        new DataModel { Text = "Bugatti Veyron Super Sport", Id = "list-03"},
        new DataModel { Text = "SSC Ultimate Aero", Id = "list-04" },
        new DataModel { Text = "Koenigsegg CCR", Id = "list-05" },
        new DataModel { Text = "McLaren F1", Id = "list-06" },
        new DataModel { Text = "Aston Martin One- 77", Id = "list-07" },
        new DataModel { Text = "Jaguar XJ220", Id = "list-08" },
        new DataModel { Text = "McLaren P1", Id = "list-09" },
        new DataModel { Text = "Ferrari LaFerrari", Id = "list-10" },
        new DataModel { Text ="Hennessey Venom", Id = "list-011" },
        new DataModel { Text = "Bugatti Chiron", Id = "list-012" },
        new DataModel { Text = "Bugatti Veyron Super Sport Bugatti Veyron Super Sport Bugatti Veyron Super Sport", Id = "list-013"},
        new DataModel { Text = "SSC Ultimate Aero", Id = "list-014" },
        new DataModel { Text = "Koenigsegg CCR", Id = "list-015" },
        new DataModel { Text = "McLaren F1", Id = "list-016" },
        new DataModel { Text = "Aston Martin One- 77", Id = "list-017" },
        new DataModel { Text = "Jaguar XJ220", Id = "list-018" },
        new DataModel { Text = "Bugatti Veyron Super Sport Bugatti Veyron Super Sport Bugatti Veyron Super Sport", Id = "list-019" },
        new DataModel { Text = "Ferrari LaFerrari", Id = "list-20" }

        };
        public class DataModel
        {
            public string Text { get; set; }
            public string Id { get; set; }
        }

       public List<DataModel> SelectedItems = new List<DataModel>();
       
       public SfListView<DataModel> SfList;

        public SfListView<DataModel> SfSelectedList;
       public async void OnSelect()
        {
            var items = await SfList.GetCheckedItemsAsync();
            if (items.Data != null)
            {
                SelectedItems = items.Data;
                this.StateHasChanged();
            }
        }
        public async void OnDelete()
        {
            var items = await SfSelectedList.GetCheckedItemsAsync();

            for(int x=0;x<=items.Data.Count;x++)
            {
                string xxitem =items.Data[x].Text.ToString();
                //SelectedItems.RemoveAt(items.Index);

            }
           

            //SfSelectedList.RemoveAt(SfSelectedList.ToList<DataModel>().FindIndex(e => e.Id == items..Id));
        }
    }
}
