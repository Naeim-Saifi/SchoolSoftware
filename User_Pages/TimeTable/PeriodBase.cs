
using AdminDashboard.Server.API_Service.Interface.TimeTableSetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.TimeTable
{
    public class PeriodBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        public PeriodViewModel _periodViewModel = new PeriodViewModel();
        public PeriodMasterModel _periodModel = new PeriodMasterModel();
        [Inject]
        public ITimeTableSetUpService _itimetableService { get; set; }
        public List<PeriodOutputModel> periodListModels = new List<PeriodOutputModel>();

        public SfGrid<PeriodOutputModel> sfPeriodListGrid;
        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Period", "Print", "ExportExcel", "Collapse All", "Expand All", "Search", "ColumnChooser"  });
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            GetPeriodList();
        }

        private async void GetPeriodList()
        {
            Period_Input_Para_Model period_Input_Para_Model = new Period_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                periodId = 0,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };

            periodListModels = (await _itimetableService.Get_Period_List(period_Input_Para_Model)).ToList();
        }
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
        public void EditPeriodMaster(CommandClickEventArgs<PeriodOutputModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Period Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                _periodViewModel = new PeriodViewModel()
                {
                      PeriodId = args.RowData.periodId,
                    EndTime  = args.RowData.endTime,
                    PeriodName = args.RowData.periodName,
                    StartTime = args.RowData.startTime,
                };


            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Period Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                _periodViewModel = new PeriodViewModel()
                {
                    PeriodId = args.RowData.periodId,
                    EndTime = args.RowData.endTime,
                    PeriodName = args.RowData.periodName,
                    StartTime = args.RowData.startTime,
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Period")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Period Details";
                OperationType = "Add";
                HeaderText = "Add Period";
                ddEnable = true;

            }
           else if (args.Item.Text == "ExportExcel")
            {
                this.sfPeriodListGrid.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfPeriodListGrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfPeriodListGrid.ExpandAllGroupAsync();
            }
        }


        [Inject]
        public ISnackbar snackBar { get; set; }



        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                _periodModel = new PeriodMasterModel()
                {
                    periodID = _periodViewModel.PeriodId,
                    periodName = _periodViewModel.PeriodName.Trim().ToUpper(),
                    startTime = _periodViewModel.StartTime.Trim(),
                    endTime = _periodViewModel.EndTime.Trim(),                     
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    _periodModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    _periodModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    _periodModel.OperationType = OperationAction.Delete;
                }
                SavePeriodDetails(_periodModel);
            };
        }
        public APIReturnModel aPIReturnmodel;


        public async void SavePeriodDetails(PeriodMasterModel periodModel)
        {
            aPIReturnmodel = (await _itimetableService.CRUD_PeriodMaster(periodModel));
            if (aPIReturnmodel.status == false)
            {
                if (periodModel.OperationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (periodModel.OperationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                GetPeriodList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {

            _periodViewModel.PeriodName = null;
            _periodViewModel.StartTime = null;
            _periodViewModel.EndTime = null;
        }
    }
}
