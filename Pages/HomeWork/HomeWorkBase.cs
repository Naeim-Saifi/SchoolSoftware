using AdminDashboard.Server.Models.HomeWork;
using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Service.Interface.HomeWork;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Calendars;
using AIS.Model;

namespace AdminDashboard.Server.Pages.HomeWork
{
    public class HomeWorkBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IUnitMasterService unitmasterService { get; set; }
        public List<TopicMasterListModel> topicMasterListModels = new List<TopicMasterListModel>();
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();
        [Inject]
        public IMasterSectionService masterSectionService { get; set; }
        public List<MasterSectionListModel> masterSectionListModels = new List<MasterSectionListModel>();
        [Inject]
        public IHomeWorkService homeWorkService { get; set; } 
        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }
        public SubjectDetailsModel subjectDetailsModel;
        public List<MasterSubjectListModel> mapSubjectList = new List<MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<HomeworkListModel> homeworkListModels = new List<HomeworkListModel>();
        public DateTime? fromValue { get; set; } = DateTime.Now;

        public DateTime? toValue { get; set; } = DateTime.Now;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            
            // chapterMasterListModels = (await unitmasterService.GetChapterMasterDetails(_sessionModel.SchoolCode, "All")).ToList();
            LoadTopicDetails();
            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, 0, 1, ReportType.GetTeacherClass)).ToList();
            masterSectionListModels = (await masterSectionService.GetMasterSectionList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, 0, 0, ReportType.GetTeacherSection)).ToList();
            // (await masterSectionService.GetMasterSectionList(_sessionModel.SchoolCode,_sessionModel.FinancialYear,1,1,1,"AllSection")).ToList();
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode,_sessionModel.FinancialYear ,_sessionModel.UserId, ReportType.SubjectTeacherList));
            mapSubjectList = subjectDetailsModel.masterSubjectListModels;
           _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
            homeworkListModels = (await homeWorkService.GetHomeWorkList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, fromValue.ToString(), toValue.ToString(), _sessionModel.UserId, ReportType.All)).ToList();
            //expand all grid item list.
          
        }
        public Boolean Check = false;
        public Boolean Disabled = true;
        public Boolean Enabled = false;
        public TopicMasterListModel SelectedData = new TopicMasterListModel();

        public string classname = "";
        public void BtnClick()
        {
              this.HomeworkList_grid.DetailExpandAll();
        }
        private async void LoadTopicDetails()
        {
           // topicMasterListModels = (await unitmasterService.GetTopicMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, "All")).ToList();
        }

        public SfGrid<TopicMasterListModel> Grid;
        public SfGrid<HomeworkListModel> HomeworkList_grid;

        public IEnumerable<MapsubjectwithClassModel> subj;

        public int classid = 0;
        public void OnChangeClass(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MasterClassListModel> args)
        {
            try
            {
                if (args.ItemData.masterClassId.ToString() != null)
                {
                     classid = args.ItemData.masterClassId;

                    subj = _mapsubjectlistModel.Where(e => e.masterClassId == classid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public IEnumerable<TopicMasterListModel> unitDetails;
        public IEnumerable<TopicMasterListModel> chapterDetails;
        public IEnumerable<TopicMasterListModel> topicDetails;
        public void OnChangeSubject(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MapsubjectwithClassModel> args)
        {
            try
            {
                if (args.ItemData.masterSubjectId.ToString() != null)
                {
                    int subjectId = args.ItemData.masterSubjectId;

                    //unitDetails = topicMasterListModels.Where (e => e.masterSubjectId == subjectId && e => e. == subjectId);

                    unitDetails = from course in topicMasterListModels
                                  where course.masterClassId == classid && course.masterSubjectId == subjectId
                                select course;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public void OnChangeUnit(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, TopicMasterListModel> args)
        {
            try
            {
                if (args.ItemData.subjectUnitId.ToString() != null)
                {
                    int unitId = args.ItemData.subjectUnitId;

                    chapterDetails = topicMasterListModels.Where(e => e.subjectUnitId == unitId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public void OnChangeChapter(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, TopicMasterListModel> args)
        {
            try
            {
                if (args.ItemData.subjectChapterId.ToString() != null)
                {
                    int chapterId = args.ItemData.subjectChapterId;

                    topicDetails = topicMasterListModels.Where(e => e.subjectChapterId == chapterId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
       
        [Inject]
        public ISnackbar snackBar { get; set; }

        public unitWiseMarksEntryViewModel _unitWiseMarksEntryViewModel = new unitWiseMarksEntryViewModel();
        
        public DateTime? StartValue { get; set; } = DateTime.Now;
        public DateTime? EndValue { get; set; } = DateTime.Now;
        public class HomeWorkType
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }

        public List<HomeWorkType>  homeWorkTypes = new List<HomeWorkType>()
        {
        new HomeWorkType(){ ID= 1, Text= "Improve Reading" },
        new HomeWorkType(){ ID= 2, Text= "Improve Writing" },
        new HomeWorkType(){ ID= 3, Text= "Improve Memorising" }
        };

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                HomeWorkModel homeWorkModel = new HomeWorkModel()
                {   
                    HomeWorkID=0,
                    ClassID = Convert.ToInt32(_unitWiseMarksEntryViewModel.ClassName),
                    SectionId = Convert.ToInt32(_unitWiseMarksEntryViewModel.SectionName),
                    SubjectID = Convert.ToInt32(_unitWiseMarksEntryViewModel.SubjectName),
                    UnitId = Convert.ToInt32(_unitWiseMarksEntryViewModel.UnitName),
                    ChapterId = Convert.ToInt32(_unitWiseMarksEntryViewModel.ChapterName),
                    TopicID = Convert.ToInt32(_unitWiseMarksEntryViewModel.TopicName),
                    HomeWorkTypeid=Convert.ToInt32(_unitWiseMarksEntryViewModel.HomeWorkType),
                    //FromDate= Convert.ToString(StartValue("dd/MM/yyyy")).ToString(),
                    //EndDate = Convert.ToString(StartValue("dd/MM/yyyy")).ToString(),
                    HomeWorkTitle=_unitWiseMarksEntryViewModel.HomeWorkTitle,
                    HomeworkDescription=_unitWiseMarksEntryViewModel.HomeworkDescription,
                    OperationType= OperationAction.Add,
                    SchoolCode=_sessionModel.SchoolCode,
                    FinancialYear=_sessionModel.FinancialYear,
                    TeacherId = _sessionModel.UserId,
                    CreatedByUserId =_sessionModel.UserId
                };
                AddUpdateHomeWork(homeWorkModel);
            }
        }
        public APIReturnModel aPIReturnmodel;
        private async void AddUpdateHomeWork(HomeWorkModel homeWorkModel)
        {
            try
            {
                if(homeWorkModel.HomeWorkID==0)
                {
                    aPIReturnmodel = (await homeWorkService.AddUpdateHomeWork(homeWorkModel));
                    if (aPIReturnmodel.status == true)
                    {
                        snackBar.Add(aPIReturnmodel.message, Severity.Success);
                        _unitWiseMarksEntryViewModel.ClassName = null;
                        _unitWiseMarksEntryViewModel.SectionName = null;
                        _unitWiseMarksEntryViewModel.SubjectName = null;
                        _unitWiseMarksEntryViewModel.UnitName = null;
                        _unitWiseMarksEntryViewModel.ChapterName = null;
                        _unitWiseMarksEntryViewModel.TopicName = null;
                        _unitWiseMarksEntryViewModel.HomeWorkTitle = null;
                        _unitWiseMarksEntryViewModel.HomeworkDescription = null;
                    }
                    else
                    {
                        snackBar.Add(aPIReturnmodel.message, Severity.Info);
                    }
                }
                
            }
            catch (Exception ex)
            {

            }

        }

        public void Cancel()
        {
            this.IsVisible = false;
        }
        public string Message = string.Empty;
        

       
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } 
        public async Task OpenDialog()
        {
            await this.DialogRef.ShowAsync();
        }
        public async Task CloseDialog()
        {
            await this.DialogRef.HideAsync();
        }
       public void AddHomeWorkDialog()
        {
            this.IsVisible = true;
        }
        public void onOpen(BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "600px";
        }
        public string[] GroupedColumns = new string[] { "className", "sectionName" };
        public void ExportToExcel()
        {
            this.HomeworkList_grid.ExcelExport();


        }
    }

   

}

