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
    public class FeeTermMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterFeeTermDetailsAPIInputModel masterFeeTermDetailsAPIInputModel { get; set; }
        //data binding for blazor page
        public MasterFeeTermDetailsViewModel masterFeeTermDetailsViewModel = new MasterFeeTermDetailsViewModel();

        //list Model
        public List<MasterFeeTermDetailsListModel> masterFeeTermDetailsListModels = new List<MasterFeeTermDetailsListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {

                masterFeeTermDetailsAPIInputModel = new MasterFeeTermDetailsAPIInputModel()
                {
                    feeTermName = masterFeeTermDetailsViewModel.feeTermName,
                    feeTermId = masterFeeTermDetailsViewModel.feeTermId,
                    displayOrder = Convert.ToInt32( masterFeeTermDetailsViewModel.displayOrder),
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterFeeTermDetailsAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterFeeTermDetailsAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterFeeTermDetailsAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveFeeTermMasterDetails(masterFeeTermDetailsAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveFeeTermMasterDetails(MasterFeeTermDetailsAPIInputModel masterFeeTermDetailsAPIInputModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteMasterFeeTermDetails(masterFeeTermDetailsAPIInputModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetFeeTermMasterList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Subject List
        private async Task GetFeeTermMasterList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterFeeTermDetailsListModels = (await masterDataService.GetMasterFeeTermDetailsList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterFeeTermDetailsViewModel.displayOrder = null;
            masterFeeTermDetailsViewModel.feeTermName = null;

        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            //Get all list from API.
            GetFeeTermMasterList();

        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        //public void RowSelectHandler(RowSelectEventArgs<MasterExamTypeDetailsViewModel> args)
        //{
        //    examTypeDetailsViewModel = new MasterExamTypeDetailsViewModel()
        //    {
        //         examDescription = args.Data.examDescription,
        //        examName = args.Data.examName,
        //         displayOrder = args.Data.displayOrder,
        //         masterExamtypeId = args.Data.masterExamtypeId
        //    };
        //    HeaderTest = "Update Record";

        //}
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
        public List<string> classMasterItem = (new List<string>() { "Add FeeTerm", "Print", "Search" });

        public SfGrid<MasterFeeTermDetailsListModel> sfFeeTermMaster;
        public string btncss = "";
        public void EditFeeTermMaster(CommandClickEventArgs<MasterFeeTermDetailsListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update FeeTerm Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterFeeTermDetailsViewModel = new MasterFeeTermDetailsViewModel()
                {
                    feeTermName = args.RowData.feeTermName,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    feeTermId = args.RowData.feeTermId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete FeeTerm Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterFeeTermDetailsViewModel = new MasterFeeTermDetailsViewModel()
                {
                    feeTermName = args.RowData.feeTermName,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    feeTermId = args.RowData.feeTermId,
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add FeeTerm")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New FeeTerm";
                OperationType = "Add";
                HeaderTest = "Add FeeTerm";
                masterFeeTermDetailsViewModel.feeTermName = null;
                masterFeeTermDetailsViewModel.displayOrder = null;
            }
        }
    }
}




