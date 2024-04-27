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
    public class MasterChapterBase : ComponentBase
    {
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public DialogSettings DialogParams = new DialogSettings { MinHeight = "460px", Width = "650px" };

        public SfGrid<ChapterMasterListModel> sfChapter;
        public string[] GroupedColumns = new string[] { "className" };

        public DialogEffect AnimationEffect = DialogEffect.Zoom;

        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public void ShowDialog()
        {
            IsVisible = true;
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "600px";
        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        [Inject]
        public IUnitMasterService unitmasterService { get; set; }
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
        public ChapterMasterViewModel chapterMasterViewModel = new ChapterMasterViewModel();

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

        public IEnumerable<ChapterMasterListModel> unitDetails;
        public void OnChangeSubject(ChangeEventArgs<string, MapsubjectwithClassModel> args)
        {
            try
            {
                if (args.ItemData.masterSubjectId.ToString() != null)
                {
                    int subjectId = args.ItemData.masterSubjectId;

                    unitDetails = chapterMasterListModels.Where(e => e.masterSubjectId == subjectId);
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
                unitDetails = chapterMasterListModels.ToList();
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
                ChapterMasterModel chapterMasterModel = new ChapterMasterModel()
                {
                     SubjectUnitId=Convert.ToInt32(chapterMasterViewModel.SubjectUnitId),
                     SubjectChapterId=Convert.ToInt32(chapterMasterViewModel.SubjectChapterId),
                     ChapterDescription= chapterMasterViewModel.ChapterDescription,
                     ChapterTitle = chapterMasterViewModel.ChapterTitle,
                     FinancialYear = _sessionModel.FinancialYear,
                     SchoolCode = _sessionModel.SchoolCode,
                     ActiveStatus = 1,
                     CreatedByUserId = _sessionModel.UserId,
                     UpdatedByUserId = 0,

                };

                if (chapterMasterModel.SubjectChapterId == 0)
                {
                    chapterMasterModel.OperationType = OperationAction.Add;
                   await ChapterDetailsSave(chapterMasterModel);
                }
                else
                {
                    chapterMasterModel.OperationType = OperationAction.Update;
                    ChapterDetailsSave(chapterMasterModel);
                }
            }
            else
            {
                // Form has invalid inputs.
            }

        }
        public APIReturnModel aPIReturnModel;
        private async Task ChapterDetailsSave(ChapterMasterModel chapterMasterModel)
        {
            try
            {                
                    //aPIReturnModel = (await unitmasterService.AddUpdateChapterMaster(chapterMasterModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                        LoadChapterDetails();
                        sfChapter.Refresh();
                        StateHasChanged();
                        ClearData(chapterMasterModel);
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
        private void ClearData(ChapterMasterModel chapterMasterModel)
        {
            //chapterMasterModel.OperationType = "";
            //chapterMasterViewModel.masterClassId = "";
            //chapterMasterViewModel.masterSubjectId = "";
            //chapterMasterViewModel.SubjectUnitId = "";
            //chapterMasterViewModel.ChapterTitle = "";
            //chapterMasterViewModel.ChapterDescription = "";
            //unitMasterViewModel.masterClassId = "0";
            //unitMasterViewModel.masterSubjectId = "0";
            chapterMasterViewModel = new ChapterMasterViewModel();
        }
              
        private async void LoadChapterDetails()
        {
           // chapterMasterListModels = (await unitmasterService.GetChapterMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, "All")).ToList();
        }
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");           
            LoadChapterDetails();
            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
            mapSubjectList = subjectDetailsModel.masterSubjectListModels;
            _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
            SubjectList(0);
        }

        public void QueryCellInfoHandler(QueryCellInfoEventArgs<ChapterMasterListModel> args)
        {
            if (!args.Column.AllowEditing) // check for your condition here
            {
                args.Cell.AddClass(new string[] { "e-attr" });
            }
        }

        public object SelectedData1;
            
        public void RowSelectHandler(RowSelectEventArgs<ChapterMasterListModel> args)
        {
            SelectedData1 = args.Data.subjectChapterId;

             chapterMasterViewModel = new ChapterMasterViewModel()
            {
                SubjectUnitId=args.Data.subjectUnitId.ToString(),
                SubjectChapterId=args.Data.subjectChapterId,
                masterClassId=args.Data.masterClassId.ToString(),
                masterSubjectId=args.Data.masterSubjectId.ToString(), 
                ChapterDescription=args.Data.chapterDescription,
                ChapterTitle=args.Data.chapterTitle,
            };


        }
        public void RowDeSelectHandler(RowDeselectEventArgs<ChapterMasterListModel> args)
        {
            chapterMasterViewModel = new ChapterMasterViewModel();
            //this.Disabled = true;
            //this.Enabled = false;
        }
        public void CancelClick()
        {
            Dialog.Hide();
        }
        public void OkClick()
        {

            ChapterMasterModel chapterMasterModel = new ChapterMasterModel()
            {
                SubjectUnitId = Convert.ToInt32(chapterMasterViewModel.SubjectUnitId),
                SubjectChapterId = Convert.ToInt32(chapterMasterViewModel.SubjectChapterId),
                ChapterDescription = chapterMasterViewModel.ChapterDescription,
                ChapterTitle = chapterMasterViewModel.ChapterTitle,
                FinancialYear = _sessionModel.FinancialYear,
                SchoolCode = _sessionModel.SchoolCode,
                ActiveStatus = 1,
                CreatedByUserId = _sessionModel.UserId,
                UpdatedByUserId = 0,
                OperationType= OperationAction.Delete
            };
               ChapterDetailsSave(chapterMasterModel);
            sfChapter.DeleteRecord();   //Delete the record programmatically while clicking OK button.
            Dialog.Hide();
        }
        public SfDialog Dialog;
        public bool flag = true;
        public void OnActionBegin(ActionEventArgs<ChapterMasterListModel> Args)
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
                DialogHeaderName = "Add Unit Master";
                ShowDialog();
                ddEnable = true;
                chapterMasterViewModel = new ChapterMasterViewModel();
                //NavigationManager.NavigateTo("addRecord/0");
            }
            else if (Args.Item.Text == "Edit")
            {
                //NavigationManager.NavigateTo($"editRecord/{SelectedRecord.OrderID}");
                Args.Cancel = true;
                DialogHeaderName = "Update Unit Master";
                ShowDialog();
                ddEnable = false;
            }
        }
        public void ExportToExcel()
        {
            this.sfChapter.ExcelExport();
        }

        public object SelectedData;
        public bool confirmflag = true;
        public void Closed()
        {
            confirmflag = true;
        }

    }
}
