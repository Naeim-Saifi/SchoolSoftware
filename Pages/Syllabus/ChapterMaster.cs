using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.SyllabusDetails;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Interface.MasterData;
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
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Syllabus
{
    public  class MasterChapter : ComponentBase
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

        public MasterChapterViewModel masterChapterViewModel = new MasterChapterViewModel();
        public MasterChapterAPIModel chapterMasterAPIModel { get; set; }
        public SubjectDetailsModel subjectDetailsModel;
        public List<MasterClassListModel> masterClassLists = new List<MasterClassListModel>();
        public List<MasterSubjectListModel> mapSubjectList = new List<MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<MapsubjectwithClassModel> _mastersubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<MasterChapterListModel> masterChapterListModels = new List<MasterChapterListModel>();
        
        //this model use for send data to API ,binding view model with API model
        public SessionModel _sessionModel;
        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Chapter", "Print", "Search" });

        public SfGrid<MasterChapterListModel> sfChapterGrid;
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
        public void EditChapterMaster(CommandClickEventArgs<MasterChapterListModel> args)
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
                masterChapterViewModel = new MasterChapterViewModel()
                {
                    SubjectChapterId=args.RowData.subjectChapterId,
                    masterClassId = args.RowData.masterClassId.ToString(),
                    masterSubjectId = args.RowData.masterSubjectId.ToString(),
                    SubjectUnitId = args.RowData.subjectUnitId.ToString(),
                    ChapterTitle = args.RowData.chapterTitle,
                    ChapterDescription = args.RowData.chapterDescription,
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
                masterChapterViewModel = new MasterChapterViewModel()
                {
                    SubjectChapterId = args.RowData.subjectChapterId,
                    masterClassId = args.RowData.masterClassId.ToString(),
                    masterSubjectId = args.RowData.masterSubjectId.ToString(),
                    SubjectUnitId = args.RowData.subjectUnitId.ToString(),
                    ChapterTitle = args.RowData.chapterTitle,
                    ChapterDescription = args.RowData.chapterDescription,
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Chapter")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Chapter Details";
                OperationType = "Add";
                HeaderText = "Add Chapter";
                ddEnable = true;
                ClearAllFields();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All,
                Userid = _sessionModel.UserId

            };
            GetChapterMasterList();
            masterClassLists = (await masterDataService.GetMasterClassList(defaultInputParameterModel)).ToList();
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
            mapSubjectList = subjectDetailsModel.masterSubjectListModels;
            SubjectList(0);
             
        }
        
        private async void GetChapterMasterList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All,
                Userid = _sessionModel.UserId

            };
            masterChapterListModels = (await unitmasterService.GetChapterMasterDetails(defaultInputParameterModel)).ToList();
            unitDetails = masterChapterListModels.ToList();
        }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                chapterMasterAPIModel = new MasterChapterAPIModel()
                {
                     subjectChapterId =Convert.ToInt16(masterChapterViewModel.SubjectChapterId),
                      subjectUnitId= Convert.ToInt16(masterChapterViewModel.SubjectUnitId),
                     chapterTitle = masterChapterViewModel.ChapterTitle,
                     chapterDescription = masterChapterViewModel.ChapterTitle,                    
                    activeStatus = 1,
                    createdByUserId = _sessionModel.UserId,
                    schoolCode = _sessionModel.SchoolCode,
                    financialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    chapterMasterAPIModel.operationType= OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    chapterMasterAPIModel.operationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    chapterMasterAPIModel.operationType = OperationAction.Delete;
                }
                SaveChapterDetails(chapterMasterAPIModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveChapterDetails(MasterChapterAPIModel masterChapterAPIModel)
        {
            aPIReturnmodel = (await unitmasterService.AddUpdateChapterMaster(masterChapterAPIModel));
            if (aPIReturnmodel.status == false)
            {
                if (masterChapterAPIModel.operationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (masterChapterAPIModel.operationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                GetChapterMasterList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {
            masterChapterViewModel.SubjectUnitId = null;
            masterChapterViewModel.masterClassId = null;
            masterChapterViewModel.masterSubjectId = null;
            masterChapterViewModel.ChapterTitle = null;
            masterChapterViewModel.ChapterDescription = null;
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
        public IEnumerable<MasterChapterListModel> unitDetails;
       
        public void OnChangeSubject(ChangeEventArgs<string, MapsubjectwithClassModel> args)
        {
            try
            {
                if (args.ItemData.masterSubjectId.ToString() != null)
                {
                    int subjectId = args.ItemData.masterSubjectId;

                    // unitDetails = (IEnumerable<MasterChapterListModel>)masterChapterListModels.Where(e => e.masterSubjectId == subjectId);

                    int classID = args.ItemData.masterClassId;
                    //_unitList = (topicMasterListModels.FindAll(e => e.masterSubjectId == subjectId && e.masterClassId == classID)).Distinct().ToList();

                    unitDetails = (IEnumerable<MasterChapterListModel>)masterChapterListModels.FindAll(e => e.masterSubjectId == subjectId && e.masterClassId == classID)
                .GroupBy(x => new { x.subjectUnitId, x.unitTitle })
                .Select(p => p.FirstOrDefault())
                .Select(p => new MasterChapterListModel
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
