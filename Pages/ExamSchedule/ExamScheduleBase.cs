using AdminDashboard.Server.Models.ExamSchedule;
using AdminDashboard.Server.Models.Holiday;
using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.SyllabusSetup;
using AdminDashboard.Server.Service.Interface.ExamSchedule;
using AdminDashboard.Server.Service.Interface.Holiday;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Interface.SyllabusSetup;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.HolidaySetup;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.ExamSchedule
{
    public class ExamScheduleBase: ComponentBase
    {

        public class ExamTypeDetails
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }
        
        public List<ExamTypeDetails>  examTypeDetails = new List<ExamTypeDetails>()
        {
        new ExamTypeDetails(){ ID= 1, Text= "Monthly" },
        new ExamTypeDetails(){ ID= 2, Text= "Half Yearly" },
        new ExamTypeDetails(){ ID= 3, Text= "Unit Wise" }
        };
        public SfDialog DialogRef;
        public SessionModel _sessionModel;     

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();
        [Inject]
        public IHolidayService _holidayService { get; set; }      
        public List<MonthNameList> monthNameLists = new List<MonthNameList>();

        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }

        public SubjectDetailsModel subjectDetailsModel;
        public List<MasterSubjectListModel> mapSubjectList = new List<MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();


        [Inject]
        public ISyllabusSetupService _syllabusSetupService { get; set; }
        public List<SyllabusSetupListModel> syllabusSetupListModels = new List<SyllabusSetupListModel>();
        public ExamScheduleViewModel examScheduleViewModel { get; set; }

        [Inject]
        public IUnitMasterService unitmasterService { get; set; }
        public List<TopicMasterListModel> topicMasterListModels = new List<TopicMasterListModel>();

        [Inject]
        public IExamScheduleService  examScheduleService { get; set; }
        public List<ExamScheduleListModel>  examScheduleListModels = new List<ExamScheduleListModel>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                examScheduleViewModel = new ExamScheduleViewModel();
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
                examScheduleListModels =(await examScheduleService.GetExamScheduleList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All")).ToList();
                               
                masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();
                monthNameLists = (await _holidayService.GetMonthNameList()).ToList();
                subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear,_sessionModel.UserId, "All"));
                mapSubjectList = subjectDetailsModel.masterSubjectListModels;
                _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
                LoadTopicDetails();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
        [Inject]
        public ISnackbar snackBar { get; set; }
        public List<MapsubjectwithClassModel> _mapSubjectList;
        public List<TopicMasterListModel> _unitList;
        public List<TopicMasterListModel> _chapterList;
        public List<TopicMasterListModel> _topicList;

        private async void LoadTopicDetails()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All,
                Userid = _sessionModel.UserId

            };
            //topicMasterListModels = (await unitmasterService.GetTopicMasterDetails(defaultInputParameterModel)).ToList();
        }

        public void OnChangeClass(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MasterClassListModel> args)
        {
            try
            {
                if (args.ItemData.masterClassId.ToString() != null)
                {
                    int classid = args.ItemData.masterClassId;

                    _mapSubjectList = (_mapsubjectlistModel.Where(e => e.masterClassId == classid).ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Grid.PreventRender(false);

        }

        private async void GetSyllabusLoad()
        {
            //syllabusSetupListModels = (await _syllabusSetupService.GetSyllabusSetupDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
        }
        public void OnChangeSubject(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MapsubjectwithClassModel> args)
        {
            try
            {
                if (args.ItemData.masterSubjectId.ToString() != null)
                {
                    int subjectId = args.ItemData.masterSubjectId;

                    _unitList = (topicMasterListModels.Where(e => e.masterSubjectId == subjectId).ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Grid.PreventRender(false);
        }

        public void OnChangeUnit(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int, TopicMasterListModel> args)
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
            //Grid.PreventRender(false);
        }

        public void OnChangeChapter(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int, TopicMasterListModel> args)
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
            //Grid.PreventRender(false);
        }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                ExamScheduleModel examScheduleModel = new ExamScheduleModel()
                {
                    ExamScheduleId= examScheduleViewModel.examScheduleId,
                    FinancialYear = _sessionModel.FinancialYear,
                    SchoolCode = _sessionModel.SchoolCode,
                    ExamTypeId = Convert.ToInt32(examScheduleViewModel.ExamType),
                    MonthId = Convert.ToInt32(examScheduleViewModel.MonthName),
                    ClassId = Convert.ToInt32(examScheduleViewModel.ClassDetails),
                    SectionId=0,
                    SubjectId = Convert.ToInt32(examScheduleViewModel.SubjectName),
                    UnitId = Convert.ToInt32(examScheduleViewModel.UnitName),
                    ChapterId = 0,/*Convert.ToInt32(examScheduleViewModel.ChapterName),*/
                    TopicId = 0,/*Convert.ToInt32(examScheduleViewModel.TopicName),*/
                    FromDate = examScheduleViewModel.ExamDate.ToString("dd/MM/yyyy"),
                    StartTime = examScheduleViewModel.StartTime.ToString("hh:mm tt"),
                    EndTime = examScheduleViewModel.EndTime.ToString("hh:mm tt"),
                    RoomName="00",
                    CreatedByUserId= _sessionModel.UserId,
                    OperationType = OperationAction.Add,
                };

                AddUpdateExamSchedule(examScheduleModel);
               
            }
        }

        public APIReturnModel aPIReturnmodel;
        private async void AddUpdateExamSchedule(ExamScheduleModel  examScheduleModel)
        {
            try
            {
                aPIReturnmodel = (await examScheduleService.AddUpdateExamSchedule(examScheduleModel));                   
                if(aPIReturnmodel.status==true)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                    examScheduleViewModel.ExamType = null;
                    examScheduleViewModel.MonthName = null;
                    examScheduleViewModel.ClassDetails = null;
                    examScheduleViewModel.SubjectName = null;
                    examScheduleViewModel.UnitName = null;
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public bool IsVisible { get; set; }

        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public void CloseDialog()
        {
            this.IsVisible = false;
        }
        public void onOpen(BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "600px";
        }

        public DateTime MinVal { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 08, 00, 00);
        public DateTime MaxVal { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 16, 45, 00);
        public DateTime? TimeValue { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 11, 40, 00);

        public DateTime? DateValue { get; set; } = DateTime.Now;

        public string[] GroupedColumns = new string[] { "examTypeName" };
        
        public SfDialog Dialog;
        public object SelectedData;
        public bool flag = true;
        public void Closed()
        {
            flag = true;
        }

        public void OkClick()
        {
            ExamScheduleModel examScheduleModel = new ExamScheduleModel()
            {
                ExamScheduleId = examScheduleID,
                FinancialYear = _sessionModel.FinancialYear,
                SchoolCode = _sessionModel.SchoolCode,
                ExamTypeId = Convert.ToInt32(0),
                MonthId = Convert.ToInt32(0),
                ClassId = Convert.ToInt32(0),
                SectionId = 0,
                SubjectId = Convert.ToInt32(0),
                UnitId = Convert.ToInt32(0),
                ChapterId = 0,/*Convert.ToInt32(examScheduleViewModel.ChapterName),*/
                TopicId = 0,/*Convert.ToInt32(examScheduleViewModel.TopicName),*/
                FromDate = examScheduleViewModel.ExamDate.ToString("dd/MM/yyyy"),
                StartTime = examScheduleViewModel.StartTime.ToString("hh:mm tt"),
                EndTime = examScheduleViewModel.EndTime.ToString("hh:mm tt"),
                RoomName = "00",
                CreatedByUserId = _sessionModel.UserId,
                OperationType = OperationAction.Delete,
            };

            AddUpdateExamSchedule(examScheduleModel);
            Dialog.Hide();
        }
        public void CancelClick()
        {
            Dialog.Hide();
        }
        public int examScheduleID = 0;
        public void RowSelectHandler(RowSelectEventArgs<ExamScheduleListModel> args)
        {

            examScheduleID = args.Data.examScheduleId;
            SelectedData = args.Data.className + "-" + args.Data.examTypeName;


        }
        public void OnActionBegin(ActionEventArgs<ExamScheduleListModel> Args)
        {
            if (Args.RequestType.ToString() == "Delete" && flag)
            {
                Args.Cancel = true;  //Cancel default delete action.
                Dialog.Show();
                flag = false;
            }
        }

        public void AddExamSchedule()
        {
            this.IsVisible = true;
        }
        public SfGrid<ExamScheduleListModel> ExamSchedule_grid;
        public void ExportToExcel()
        {
            this.ExamSchedule_grid.ExcelExport();
        }
        private void onChange(Syncfusion.Blazor.Calendars.ChangedEventArgs<DateTime?> args)
        {
            DateValue = args.Value;
            StateHasChanged();
        }
    }
}
