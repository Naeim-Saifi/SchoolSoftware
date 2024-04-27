using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.SyllabusDetails;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Interface.MasterData;

using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
using AIS.Model.Academic;
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
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Syllabus
{
    public class TopicMasterBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        [Inject]
        public IMasterDataService masterDataService { get; set; }
        //this is use for API Function
        [Inject]
        public IUnitMasterService unitmasterService { get; set; }
        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }

        public MasterTopicViewModel masterTopicViewModel = new MasterTopicViewModel();
        public MasterTopicAPIModel topicMasterAPIModel { get; set; }
        public SubjectDetailsModel subjectDetailsModel;
        public List<MasterClassListModel> masterClassLists = new List<MasterClassListModel>();

        public List<Models.MasterConfiguration.SubjectMaster.MasterSubjectListModel> mapSubjectList = new List<Models.MasterConfiguration.SubjectMaster.MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<MapsubjectwithClassModel> _mastersubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<MasterTopicListModel> masterTopicListModels = new List<MasterTopicListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Topic", "Print", "Search" });

        public SfGrid<MasterTopicListModel> sfTopicGrid;
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

        public void EditTopicMaster(CommandClickEventArgs<MasterTopicListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Topic Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                masterTopicViewModel = new MasterTopicViewModel()
                {
                    subjectTopicId = args.RowData.subjectTopicId,
                    masterClassId = args.RowData.masterClassId.ToString(),
                    masterSubjectId = args.RowData.masterSubjectId.ToString(),
                    subjectChapterId= args.RowData.subjectChapterId.ToString(),
                    SubjectUnitId = args.RowData.subjectUnitId.ToString(),
                    subjectTopicTitle = args.RowData.topicTitle,
                    subjectTopicDescription = args.RowData.topicDescription,
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Topic details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                masterTopicViewModel = new MasterTopicViewModel()
                {
                    subjectTopicId = args.RowData.subjectTopicId,
                    masterClassId = args.RowData.masterClassId.ToString(),
                    masterSubjectId = args.RowData.masterSubjectId.ToString(),
                    subjectChapterId = args.RowData.subjectChapterId.ToString(),
                    SubjectUnitId = args.RowData.subjectUnitId.ToString(),
                    subjectTopicTitle = args.RowData.topicTitle,
                    subjectTopicDescription = args.RowData.topicDescription,
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                topicMasterAPIModel = new MasterTopicAPIModel()
                {
                    subjectChapterId = Convert.ToInt16(masterTopicViewModel.subjectChapterId),                    
                    topicTitle = masterTopicViewModel.subjectTopicTitle,
                    topicDescription = masterTopicViewModel.subjectTopicDescription,
                    activeStatus = 1,
                    createdByUserId = _sessionModel.UserId,
                    schoolCode = _sessionModel.SchoolCode,
                    financialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    topicMasterAPIModel.operationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    topicMasterAPIModel.operationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    topicMasterAPIModel.operationType = OperationAction.Delete;
                }
                SaveTopicDetails(topicMasterAPIModel);
            };
        }

        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveTopicDetails(MasterTopicAPIModel topicMasterAPIModel)
        {
            aPIReturnmodel = (await unitmasterService.AddUpdateTopicMaster(topicMasterAPIModel));
            if (aPIReturnmodel.status == false)
            {
                if (topicMasterAPIModel.operationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (topicMasterAPIModel.operationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                GetTopicMasterList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {
            masterTopicViewModel.masterClassId = null;
            masterTopicViewModel.masterSubjectId = null;
            masterTopicViewModel.SubjectUnitId = null;
            masterTopicViewModel.subjectChapterId = null;
            masterTopicViewModel.subjectTopicTitle = null;
            masterTopicViewModel.subjectTopicDescription = null;
        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Topic")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Topic Details";
                OperationType = "Add";
                HeaderText = "Add Topic";
                ddEnable = true;
                //ClearAllFields();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            GetTopicMasterList();
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All,
                Userid = _sessionModel.UserId

            };
            
            masterClassLists = (await masterDataService.GetMasterClassList(defaultInputParameterModel)).ToList();
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
            mapSubjectList = subjectDetailsModel.masterSubjectListModels;
            SubjectList(0);

        }
        private async void GetTopicMasterList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All,
                Userid = _sessionModel.UserId

            };
            masterTopicListModels = (await unitmasterService.GetTopicMasterDetails(defaultInputParameterModel)).ToList();
            //unitDetails = masterChapterListModels.ToList();
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
        public IEnumerable<MasterTopicListModel> unitDetails;
        public IEnumerable<MasterTopicListModel> chapterDetails;

        public void OnChangeSubject(ChangeEventArgs<string, MapsubjectwithClassModel> args)
        {
            try
            {
                if (args.ItemData.masterSubjectId.ToString() != null)
                {
                    int subjectId = args.ItemData.masterSubjectId;

                    //unitDetails = (IEnumerable<MasterTopicListModel>)masterTopicListModels.Where(e => e.masterSubjectId == subjectId);
                    int classID = args.ItemData.masterClassId;
                    //_unitList = (topicMasterListModels.FindAll(e => e.masterSubjectId == subjectId && e.masterClassId == classID)).Distinct().ToList();

                    unitDetails = (IEnumerable<MasterTopicListModel>)masterTopicListModels.FindAll(e => e.masterSubjectId == subjectId && e.masterClassId == classID)
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
        public void OnChangeUnit(ChangeEventArgs<string, MasterTopicListModel> args)
        {
            try
            {
                if (args.ItemData.subjectUnitId.ToString() != null)
                {
                    int unitId = Convert.ToInt32(args.ItemData.subjectUnitId);

                    //chapterDetails = masterTopicListModels.Where(e => e.subjectUnitId == unitId);
                    chapterDetails = masterTopicListModels.FindAll(e => e.subjectUnitId == unitId)
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
    }
}
