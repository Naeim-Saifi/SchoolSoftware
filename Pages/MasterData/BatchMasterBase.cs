using AdminDashboard.Server.Models.MasterData;
using AdminDashboard.Server.Service.Interface.MasterData;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
using AIS.Model.GeneralConversion;
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

namespace AdminDashboard.Server.Pages.MasterData
{
    public class BatchMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterBatchAPIInputModel masterBatchAPIInputModel { get; set; }
        //data binding for blazor page
        public MasterBatchViewModel   masterBatchViewModel = new MasterBatchViewModel();

        //list Model
        public List<MasterBatchListModel> masterBatchListModels = new List<MasterBatchListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                masterBatchAPIInputModel = new MasterBatchAPIInputModel()
                {
                     batchName = masterBatchViewModel.batchName,
                    batchCode = masterBatchViewModel.batchCode,
                   displayOrder = Convert.ToInt16(masterBatchViewModel.displayOrder),
                    masterBatchId = masterBatchViewModel.masterBatchId,
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterBatchAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterBatchAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterBatchAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveBatchDetails(masterBatchAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveBatchDetails(MasterBatchAPIInputModel masterBatchModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteBatchMaster(masterBatchModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetBatchList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Batch List
        private async Task GetBatchList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterBatchListModels = (await masterDataService.GetBatchList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterBatchViewModel.batchName = null;
            masterBatchViewModel.batchCode = null;
            masterBatchViewModel.displayOrder = null;
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Get Batch List Details
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            GetBatchList();

            //masterBatchListModels = (await masterDataService.GetMasterBatchList(defaultInputParameterModel)).ToList();
        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        public void RowSelectHandler(RowSelectEventArgs<MasterBatchListModel> args)
        {
            masterBatchViewModel = new MasterBatchViewModel()
            {
                batchName = args.Data.batchName,
                batchCode = args.Data.batchCode,
                displayOrder = Convert.ToInt16( args.Data.displayOrder),
                masterBatchId = args.Data.masterBatchId
            };
            HeaderTest = "Update Record";

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
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Batch", "Print", "Search" });

        public SfGrid<MasterBatchListModel> sfBatchMaster;
        public string btncss = "";
        public void EditBatchMaster(CommandClickEventArgs<MasterBatchListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Batch Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterBatchViewModel = new MasterBatchViewModel()
                {
                    batchName = args.RowData.batchName,
                    batchCode = args.RowData.batchCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterBatchId = args.RowData.masterBatchId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Batch Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterBatchViewModel = new MasterBatchViewModel()
                {
                    batchName = args.RowData.batchName,
                    batchCode = args.RowData.batchCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterBatchId = args.RowData.masterBatchId
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Batch")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Batch Details";
                OperationType = "Add";
                HeaderTest = "Add Batch";
                masterBatchViewModel.batchName = null;
                masterBatchViewModel.batchCode = null;
                masterBatchViewModel.displayOrder = null;
            }
        }
    }
}
