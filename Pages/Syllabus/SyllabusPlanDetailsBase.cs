using AdminDashboard.Server.Models.Holiday;
using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.SyllabusDetails;
using AdminDashboard.Server.Models.SyllabusSetup;
using AdminDashboard.Server.Service.Interface.Holiday;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Interface.MasterData;
using AdminDashboard.Server.Service.Interface.SyllabusSetup;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdminDashboard.Server.Pages.Syllabus
{
    public class SyllabusPlanDetailsBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public List<SyllabusSetupListModel> syllabusSetupListModels = new List<SyllabusSetupListModel>();
        // Time Table service
        public SyllabusviewModel  syllabusviewModel =new SyllabusviewModel();
        [Inject]
        public IMasterDataService masterDataService { get; set; }
        [Inject]
        public ISyllabusSetupService _syllabusSetupService { get; set; }
        public List<MasterClassListModel> masterClassLists = new List<MasterClassListModel>();
        public SubjectDetailsModel subjectDetailsModel;
        [Inject]
        public IUnitMasterService unitmasterService { get; set; }
        public List<MasterTopicListModel> topicMasterListModels = new List<MasterTopicListModel>();
        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }
        public List<Models.MasterConfiguration.SubjectMaster.MasterSubjectListModel> mapSubjectList = new List<Models.MasterConfiguration.SubjectMaster.MasterSubjectListModel>();
        [Inject]
        public IHolidayService _holidayService { get; set; }
        public List<MonthNameList> monthNameLists = new List<MonthNameList>();
        protected override async Task OnInitializedAsync()
        {
            try
            {
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
                monthNameLists = (await _holidayService.GetMonthNameList()).ToList();
                ////------
                DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
                {
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                    OperationType = ReportType.All,
                    Userid = _sessionModel.UserId

                };
                masterClassLists = (await masterDataService.GetMasterClassList(defaultInputParameterModel)).ToList();
                GetSyllabusLoad();
                LoadTopicDetails();
                subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
                mapSubjectList = subjectDetailsModel.masterSubjectListModels;
                _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
                SubjectList(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        private async void LoadTopicDetails()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All,
                Userid = _sessionModel.UserId

            };
            topicMasterListModels = (await unitmasterService.GetTopicMasterDetails(defaultInputParameterModel)).ToList();
        }
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
        public List<MapsubjectwithClassModel> _mapSubjectList;
        public List<MasterTopicListModel> _unitList;
        public List<MasterTopicListModel> _chapterList;
        public List<MasterTopicListModel> _topicList;
        public void OnChangeSubject(ChangeEventArgs<string, MapsubjectwithClassModel> args)
        {
            try
            {
                if (args.ItemData.masterSubjectId.ToString() != null)
                {
                    int subjectId = args.ItemData.masterSubjectId;
                    int classID = args.ItemData.masterClassId;
                    //_unitList = (topicMasterListModels.FindAll(e => e.masterSubjectId == subjectId && e.masterClassId == classID)).Distinct().ToList();

                    _unitList = topicMasterListModels.FindAll(e => e.masterSubjectId == subjectId && e.masterClassId == classID)
                .GroupBy(x => new { x.subjectUnitId, x.unitTitle })
                .Select(p => p.FirstOrDefault())
                .Select(p => new MasterTopicListModel
                {
                     unitTitle = p.unitTitle,
                    subjectUnitId = p.subjectUnitId,
                    //Location = p.Location,
                    //Active = p.Active,
                    //Archived = p.Archived
                })
                .ToList();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<MapsubjectwithClassModel> _mastersubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<MasterTopicListModel> masterTopicListModels = new List<MasterTopicListModel>();

        public SyllabusSetupAPIModel  syllabusSetupAPIModel { get; set; }
        

        private void SubjectList(int ClassID)
        {
            if (ClassID == 0)
            {
                _mastersubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
                _mapsubjectlistModel = _mastersubjectlistModel.ToList();
            }
            else
            {
                _mapsubjectlistModel = _mastersubjectlistModel.Where(e => e.masterClassId == ClassID).ToList();
            }

        }
        public void OnChangeUnit(ChangeEventArgs<string, MasterTopicListModel> args)
        {
            try
            {
                if (args.ItemData.subjectUnitId.ToString() != null)
                {
                    int unitId = args.ItemData.subjectUnitId;

                    //_chapterList = topicMasterListModels.Where(e => e.subjectUnitId == unitId).ToList();
                    _chapterList = topicMasterListModels.FindAll(e => e.subjectUnitId == unitId )
                .GroupBy(x => new { x.subjectChapterId, x.chapterTitle })
                .Select(p => p.FirstOrDefault())
                .Select(p => new MasterTopicListModel
                {
                    chapterTitle = p.chapterTitle,
                    subjectChapterId = p.subjectChapterId,
                    //Location = p.Location,
                    //Active = p.Active,
                    //Archived = p.Archived
                })
                .ToList();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void OnChangeChapter(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MasterTopicListModel> args)
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
            //grid_syllabusSetup.PreventRender(false);
        }
        private async void GetSyllabusLoad()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All,
                Userid = _sessionModel.UserId

            };
            syllabusSetupListModels = (await _syllabusSetupService.GetSyllabusSetupDetails(defaultInputParameterModel)).ToList();
        }
       
        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Syllabus", "Print", "Search" });

        public SfGrid<SyllabusSetupListModel> sfSyllabusGrid;
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string btncss = "";
        public string HeaderText = string.Empty;
        public void EditSyllabusMaster(CommandClickEventArgs<SyllabusSetupListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Chapter Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                syllabusviewModel = new SyllabusviewModel()
                {
                    SyllabusId= args.RowData.syllabusId.ToString(),
                      MonthId = args.RowData.monthId.ToString(),
                      ClassId= args.RowData.classId.ToString(),
                      SubjectId= args.RowData.subjectId.ToString(),
                      UnitId = args.RowData.unitId.ToString(),
                      ChapterId= args.RowData.chapterId.ToString(),
                      TopicId= args.RowData.topicId.ToString(),

                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete chapter details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                syllabusviewModel = new SyllabusviewModel()
                {
                    SyllabusId = args.RowData.syllabusId.ToString(),
                    MonthId = args.RowData.monthId.ToString(),
                    ClassId = args.RowData.classId.ToString(),
                    SubjectId = args.RowData.subjectId.ToString(),
                    UnitId = args.RowData.unitId.ToString(),
                    ChapterId = args.RowData.chapterId.ToString(),
                    TopicId = args.RowData.topicId.ToString(),

                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

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
                //ClearAllFields();
            }
        }
        public IEnumerable<MasterTopicListModel> unitDetails;
        public IEnumerable<MasterTopicListModel> chapterDetails;
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                syllabusSetupAPIModel = new SyllabusSetupAPIModel()
                {
                    SyllabusId = Convert.ToInt32(syllabusviewModel.SyllabusId),
                    ChapterId = Convert.ToInt32(syllabusviewModel.ChapterId),
                    MonthId = Convert.ToInt32(syllabusviewModel.MonthId),
                    ClassId = Convert.ToInt32(syllabusviewModel.ClassId),
                    SubjectId = Convert.ToInt32(syllabusviewModel.SubjectId),
                    TopicId = Convert.ToInt32(syllabusviewModel.TopicId),
                    UnitId = Convert.ToInt32(syllabusviewModel.UnitId),
                    CreatedByUserId = _sessionModel.UserId,
                    FinancialYear = _sessionModel.FinancialYear,
                    SchoolCode = _sessionModel.SchoolCode,

                };

                if (OperationType == "Add")
                {
                    syllabusSetupAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    syllabusSetupAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    syllabusSetupAPIModel.OperationType = OperationAction.Delete;
                }
                 SaveSyllabusDetails(syllabusSetupAPIModel);
            };
        }

        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveSyllabusDetails(SyllabusSetupAPIModel  syllabusSetupAPIModel)
        {
            aPIReturnmodel = (await _syllabusSetupService.AddUpdateSyllabusSetup(syllabusSetupAPIModel));
            if (aPIReturnmodel.status == false)
            {
                if (syllabusSetupAPIModel.OperationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (syllabusSetupAPIModel.OperationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                GetSyllabusLoad();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {
            syllabusviewModel.MonthId = null;
            syllabusviewModel.ClassId = null;
            syllabusviewModel.SubjectId = null;
            syllabusviewModel.UnitId = null;
            syllabusviewModel.ChapterId = null;
            syllabusviewModel.TopicId = null;
        }
    }
}
