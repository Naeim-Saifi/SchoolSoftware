using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
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
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.MasterConfiguration.SyllabusMaster
{
    public class MasterTopicBase : ComponentBase
    {
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

        public SfGrid<TopicMasterListModel> sfTopicMaster;

        public string[] GroupedColumns = new string[] { "className" };
        public DialogSettings DialogParams = new DialogSettings { MinHeight = "460px", Width = "650px" };

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

        public TopicMasterViewModel   topicMasterViewModel = new TopicMasterViewModel();
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
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                TopicMasterModel  topicMasterModel = new TopicMasterModel()
                {
                     SubjectChapterId=Convert.ToInt32(topicMasterViewModel.subjectChapterId),
                     SubjectTopicId=Convert.ToInt32(topicMasterViewModel.subjectTopicId),
                     TopicTitle=topicMasterViewModel.subjectTopicTitle.ToUpper(),
                     TopicDescription=topicMasterViewModel.subjectTopicDescription.ToUpper(),
                     ActiveStatus=1,
                     CreatedByUserId= _sessionModel.UserId,
                     FinancialYear = _sessionModel.FinancialYear,
                     SchoolCode = _sessionModel.SchoolCode,
                     UpdatedByUserId=0,
                };

                if (topicMasterModel.SubjectTopicId == 0)
                {
                    topicMasterModel.OperationType = OperationAction.Add;
                    TopicDetailsSave(topicMasterModel);
                }
                else
                {
                    topicMasterModel.OperationType  = OperationAction.Update;
                    TopicDetailsSave(topicMasterModel);
                }
            }
            else
            {
                // Form has invalid inputs.
            }

        }
        public APIReturnModel aPIReturnModel;
        private async Task TopicDetailsSave(TopicMasterModel topicMasterModel)
        {
            try
            {
                    //aPIReturnModel = (await unitmasterService.AddUpdateTopicMaster(topicMasterModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                        LoadTopicDetails();
                        sfTopicMaster.Refresh();
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
            topicMasterViewModel = new TopicMasterViewModel();
        }
        private  async void LoadTopicDetails()
        {
            //topicMasterListModels = (await unitmasterService.GetTopicMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, "All")).ToList();
        }
        public IEnumerable<TopicMasterListModel> unitDetails;
        public IEnumerable<TopicMasterListModel> chapterDetails;
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

        public TopicMasterListModel SelectedData = new TopicMasterListModel();

        public string classname = "";
         
        public void QueryCellInfoHandler(QueryCellInfoEventArgs<TopicMasterListModel> args)
        {
            if (!args.Column.AllowEditing) // check for your condition here
            {
                args.Cell.AddClass(new string[] { "e-attr" });
            }
        }

        public object SelectedData1;
        public void RowSelectHandler(RowSelectEventArgs<TopicMasterListModel> args)
        {
            SelectedData1 = args.Data.subjectUnitId;

            if (args.Data.topicTitle.ToString() != null)
            {
                topicMasterViewModel = new TopicMasterViewModel()
                {
                    masterClassId = args.Data.masterClassId.ToString(),
                    masterSubjectId = args.Data.masterSubjectId.ToString(),
                    SubjectUnitId = args.Data.subjectUnitId.ToString(),
                    subjectChapterId = args.Data.subjectChapterId.ToString(),
                    subjectTopicTitle = args.Data.topicTitle.ToString(),
                    subjectTopicDescription = args.Data.topicDescription,
                    subjectTopicId =args.Data.subjectTopicId,
                };
            }
            else
            {
                ShowDialog();
            }

        }
        public void RowDeSelectHandler(RowDeselectEventArgs<TopicMasterListModel> args)
        {
             topicMasterViewModel = new TopicMasterViewModel();
            //this.Disabled = true;
            //this.Enabled = false;
        }
        public void CancelClick()
        {
            Dialog.Hide();
        }

        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public void OkClick()
        {

            TopicMasterModel topicMasterModel = new TopicMasterModel()
            {
                SubjectChapterId = Convert.ToInt32(topicMasterViewModel.subjectChapterId),
                SubjectTopicId = Convert.ToInt32(topicMasterViewModel.subjectTopicId),
                TopicTitle = topicMasterViewModel.subjectTopicTitle.ToUpper(),
                TopicDescription = topicMasterViewModel.subjectTopicDescription.ToUpper(),
                ActiveStatus = 1,
                CreatedByUserId = _sessionModel.UserId,
                FinancialYear = _sessionModel.FinancialYear,
                SchoolCode = _sessionModel.SchoolCode,
                UpdatedByUserId = 0,
                OperationType = OperationAction.Update,
        };
           
            TopicDetailsSave(topicMasterModel);
            sfTopicMaster.DeleteRecord();   //Delete the record programmatically while clicking OK button.
            Dialog.Hide();
        }
        public SfDialog Dialog;
        public bool flag = true;
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
        public void OnActionBegin(ActionEventArgs<TopicMasterListModel> Args)
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
                DialogHeaderName = "Add Topic Master";
                ShowDialog();
                ddEnable = true;
                topicMasterViewModel = new TopicMasterViewModel();
                //NavigationManager.NavigateTo("addRecord/0");
            }
            else if (Args.Item.Text == "Edit")
            {
                //NavigationManager.NavigateTo($"editRecord/{SelectedRecord.OrderID}");
                Args.Cancel = true;
                DialogHeaderName = "Update Topic Master";
                ShowDialog();
                ddEnable = false;
            }
        }
        public void ShowDialog()
        {
            IsVisible = true;
        }
        public void ExportToExcel()
        {
            this.sfTopicMaster.ExcelExport();
        }
        public bool IsVisible { get; set; } = false;
        public SfDialog DialogRef;
        public bool confirmflag = true;
        public void Closed()
        {
            confirmflag = true;
        }
    }
}
