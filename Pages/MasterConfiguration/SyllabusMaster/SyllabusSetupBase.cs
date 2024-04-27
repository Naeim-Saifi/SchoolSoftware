using AdminDashboard.Server.Models.Holiday;
using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.SyllabusSetup;
using AdminDashboard.Server.Service.Interface.Holiday;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Interface.SyllabusSetup;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.MasterConfiguration.SyllabusMaster
{
    public class SyllabusSetupBase : ComponentBase
    {

        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<SyllabusSetupListModel> grid_syllabusSetup;

        public string[] GroupedColumns = new string[] { "monthNameDetails","className", "subjectName" };
        public DialogSettings DialogParams = new DialogSettings { MinHeight = "460px", Width = "650px" };

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
           // topicMasterListModels = (await unitmasterService.GetTopicMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, "All")).ToList();
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

                    _mapSubjectList = (_mapsubjectlistModel.Where(e => e.masterClassId == classid).ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //grid_syllabusSetup.PreventRender(false);

        }
        public void OnChangeSubject(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MapsubjectwithClassModel> args)
        {
            try
            {
                if (args.ItemData.masterSubjectId.ToString() != null)
                {
                    int subjectId = args.ItemData.masterSubjectId;
                    int classID = args.ItemData.masterClassId;
                    _unitList = (topicMasterListModels.FindAll(e => e.masterSubjectId == subjectId && e.masterClassId == classID)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //grid_syllabusSetup.PreventRender(false);
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
            //grid_syllabusSetup.PreventRender(false);
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
            //grid_syllabusSetup.PreventRender(false);
        }
        public object SelectedData1;
        public async void OnValidSubmit(Microsoft.AspNetCore.Components.Forms.EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                SyllabusSetupAPIModel  syllabusSetupModel = new SyllabusSetupAPIModel()
                {
                    SyllabusId=Convert.ToInt32(syllabusviewModel.SyllabusId),
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
                if (syllabusSetupModel.SyllabusId == 0)
                {
                    syllabusSetupModel.OperationType = OperationAction.Add;
                    SyllabusDetailsSave(syllabusSetupModel);
                }
                else
                {
                    syllabusSetupModel.OperationType = OperationAction.Update;
                    SyllabusDetailsSave(syllabusSetupModel);
                }
            }
            else
            {
                // Form has invalid inputs.
            }

        }
        public APIReturnModel aPIReturnModel;
        private async Task SyllabusDetailsSave(SyllabusSetupAPIModel topicMasterModel)
        {
            try
            {
                //aPIReturnModel = (await unitmasterService.AddUpdateTopicMaster(topicMasterModel));
                aPIReturnModel = await _syllabusSetupService.AddUpdateSyllabusSetup(topicMasterModel);

                if (aPIReturnModel.status == false)
                {
                    snackBar.Add(aPIReturnModel.message, Severity.Success);
                    GetSyllabusLoad();
                    grid_syllabusSetup.Refresh();
                    StateHasChanged();
                    ClearData();
                }
                else
                {
                    snackBar.Add(aPIReturnModel.message, Severity.Error);
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void ClearData()
        {
            syllabusviewModel = new SyllabusviewModel();
        }
        public void RowSelectHandler(RowSelectEventArgs<SyllabusSetupListModel> args)
        {
            SelectedData1 = args.Data.syllabusId;

            if (args.Data.chapterId.ToString() != null)
            {
                
                syllabusviewModel = new SyllabusviewModel()
                {
                    
                    ChapterId = Convert.ToString(args.Data.chapterId),
                    ClassId = Convert.ToString(args.Data.classId),
                    MonthId = Convert.ToString(args.Data.monthId),
                    SubjectId = Convert.ToString(args.Data.subjectId),
                    SyllabusId = Convert.ToString(args.Data.syllabusId),
                    TopicId = Convert.ToString(args.Data.topicId),
                    UnitId = Convert.ToString(args.Data.unitId)
                };

            }
            else
            {
                ShowDialog();
            }

        }
        public void RowDeSelectHandler(RowDeselectEventArgs<SyllabusSetupListModel> args)
        {
            //topicMasterViewModel = new TopicMasterViewModel();
            //this.Disabled = true;
            //this.Enabled = false;
        }

        public SyllabusviewModel syllabusviewModel = new SyllabusviewModel();

        public void QueryCellInfoHandler(QueryCellInfoEventArgs<SyllabusSetupListModel> args)
        {
            if (!args.Column.AllowEditing) // check for your condition here
            {
                args.Cell.AddClass(new string[] { "e-attr" });
            }
        }
        public bool flag = true;
        
        public void OkClick()
        {

            SyllabusSetupAPIModel syllabusSetupModel = new SyllabusSetupAPIModel()
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
            syllabusSetupModel.OperationType = OperationAction.Delete;
            SyllabusDetailsSave(syllabusSetupModel);
            //sfTopicMaster.DeleteRecord();   //Delete the record programmatically while clicking OK button.
            Dialog.Hide();
        }
        public SfDialog Dialog;
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "600px";
        }
        public void OnActionBegin(ActionEventArgs<SyllabusSetupListModel> Args)
        {
            if (Args.RequestType.ToString() == "Delete" && flag)
            {
                Args.Cancel = true;  //Cancel default delete action.
                Dialog.Show();
                confirmflag = false;
            }

        }
        public string DialogHeaderName = string.Empty;
        public Boolean ddEnable = false;
        public void ToolClick(Syncfusion.Blazor.Navigations.ClickEventArgs Args)
        {
            if (Args.Item.Text == "Add")
            {
                Args.Cancel = true;
                DialogHeaderName = "Add Syllabus";
                ShowDialog();
                ddEnable = true;
                syllabusviewModel = new SyllabusviewModel();
                //NavigationManager.NavigateTo("addRecord/0");
            }
            else if (Args.Item.Text == "Edit")
            {
                //NavigationManager.NavigateTo($"editRecord/{SelectedRecord.OrderID}");
                Args.Cancel = true;
                DialogHeaderName = "Update Syllabus";
                ShowDialog();
                ddEnable = false;
            }
        }
        public void CancelClick()
        {
            Dialog.Hide();
        }

        public void ShowDialog()
        {
            IsVisible = true;
        }
        public void ExportToExcel()
        {
            this.grid_syllabusSetup.ExcelExport();
        }
        public bool IsVisible { get; set; } = false;
        public SfDialog DialogRef;
        public bool confirmflag = true;
        public void Closed()
        {
            confirmflag = true;
        }
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
    }
}
