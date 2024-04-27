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
using Syncfusion.Blazor.Kanban.Internal;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.MasterConfiguration.SyllabusMaster
{
    public class MasterUnitBase : ComponentBase
    {
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit","Print", "Search" });
        
        
        
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage" 
            };
        
        public DialogSettings DialogParams = new DialogSettings { MinHeight = "460px", Width = "650px" };

        public SfGrid<UnitMasterListModel> sfUnitMaster;

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
        public List<UnitMasterListModel> unitMasterListModels = new List<UnitMasterListModel>();
        [Inject]
        public IMasterClassService masterClassService { get; set; }

        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();

        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }
        public List<MapsubjectwithClassModel> mapsubjectwithClassModels = new List<MapsubjectwithClassModel>();
        public SubjectDetailsModel subjectDetailsModel;

        public List<MasterSubjectListModel> mapSubjectList = new List<MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<MapsubjectwithClassModel> _mastersubjectlistModel = new List<MapsubjectwithClassModel>();
        [Inject]
        public ISnackbar snackBar { get; set; }

        public UnitMasterViewModel unitMasterViewModel  = new UnitMasterViewModel();
        public void OnChangeClass(ChangeEventArgs<string, MasterClassListModel> args)
        {
            try
            {
                if (args.ItemData.masterClassId != 0 && args.ItemData.masterClassId !=null)
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
                UnitMasterModel unitMasterModel = new UnitMasterModel()
                {
                    subjectUnitId = unitMasterViewModel.subjectUnitId,
                    masterClassId = Convert.ToInt32(unitMasterViewModel.masterClassId),
                    masterSubjectId = Convert.ToInt32(unitMasterViewModel.masterSubjectId),
                    unitTitle = unitMasterViewModel.unitTitle,
                    unitDescription = unitMasterViewModel.unitDescription,
                    FinancialYear = _sessionModel.FinancialYear,
                    schoolCode = _sessionModel.SchoolCode,
                    activeStatus = 1,                   
                    createdByUserId = _sessionModel.UserId,
                    updatedByUserId = 0,
                    
                };

                if(unitMasterModel.subjectUnitId==0)
                {
                    unitMasterModel.operationType = OperationAction.Add;
                    UnitDetailsSave(unitMasterModel);
                }
                else
                {
                    unitMasterModel.operationType = OperationAction.Update;
                    UnitDetailsSave(unitMasterModel);
                }
            }
            else
            {
                // Form has invalid inputs.
            }

        }
        public APIReturnModel aPIReturnModel;
        private async Task UnitDetailsSave(UnitMasterModel unitMasterModel)
        {
            try
            {
                if(unitMasterModel.operationType!="NA")
                {
                   // aPIReturnModel = (await unitmasterService.AddUpdateUnitMaster(unitMasterModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                        GetUnitMasterList();
                        sfUnitMaster.Refresh();
                        StateHasChanged();
                        ClearData(unitMasterModel);
                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ClearData(UnitMasterModel unitMasterModel)
        {
            unitMasterViewModel = new UnitMasterViewModel();
           
        }
         private async void GetUnitMasterList()
        {
           // unitMasterListModels = (await unitmasterService.GetUnitMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, "All")).ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            GetUnitMasterList();
            //masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();
            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, 0, 1, ReportType.All)).ToList();
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
            mapSubjectList = subjectDetailsModel.masterSubjectListModels;
            SubjectList(0);
        }
        public void QueryCellInfoHandler(QueryCellInfoEventArgs<UnitMasterListModel> args)
        {
            if (!args.Column.AllowEditing) // check for your condition here
            {
                args.Cell.AddClass(new string[] { "e-attr" });
            }
        }
       
        public object SelectedData1;
        public void RowSelectHandler(RowSelectEventArgs<UnitMasterListModel> args)
        {
            SelectedData1 = args.Data.subjectUnitId;

            unitMasterViewModel = new UnitMasterViewModel()
            {
                masterClassId = args.Data.masterClassId.ToString(),
                masterSubjectId = args.Data.masterSubjectId.ToString(),
                unitTitle = args.Data.unitTitle,
                unitDescription = args.Data.unitDescription,
                subjectUnitId = args.Data.subjectUnitId
            };


        }
        public void RowDeSelectHandler(RowDeselectEventArgs<UnitMasterListModel> args)
        {
            unitMasterViewModel = new UnitMasterViewModel();
            //this.Disabled = true;
            //this.Enabled = false;
        }
        public void CancelClick()
        {
            Dialog.Hide();
        }
        public void OkClick()
        {

            UnitMasterModel unitMasterModel = new UnitMasterModel()
            {
                subjectUnitId =Convert.ToInt32(SelectedData1),
                masterClassId = Convert.ToInt32(unitMasterViewModel.masterClassId),
                masterSubjectId = Convert.ToInt32(unitMasterViewModel.masterSubjectId),
                unitTitle = unitMasterViewModel.unitTitle,
                unitDescription = unitMasterViewModel.unitDescription,
                FinancialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                activeStatus = 1,
                createdByUserId = _sessionModel.UserId,
                updatedByUserId = 0,
                operationType = OperationAction.Delete
            };
            if (unitMasterModel.operationType != "NA")
            {
                UnitDetailsSave(unitMasterModel);
            } 
            sfUnitMaster.DeleteRecord();   //Delete the record programmatically while clicking OK button.
            Dialog.Hide();
        }
        public SfDialog Dialog;
        public bool flag = true;
        public void OnActionBegin(ActionEventArgs<UnitMasterListModel> Args)
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
                unitMasterViewModel = new UnitMasterViewModel();
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
            this.sfUnitMaster.ExcelExport();
        }

        public object SelectedData;
        public bool confirmflag = true;
        public void Closed()
        {
            confirmflag = true;
        }

    }
}

   
 
 
