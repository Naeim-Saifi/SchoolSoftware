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
    public class DiscountBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterDiscountDetailsAPIInputModel masterDiscountDetailsAPIInputModel { get; set; }
        //data binding for blazor page
        public MasterDiscountDetailsViewModel masterDiscountDetailsViewModel = new MasterDiscountDetailsViewModel();

        //list Model
        public List<MasterDiscountDetailsListModel> masterDiscountDetailsListModels = new List<MasterDiscountDetailsListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {

                masterDiscountDetailsAPIInputModel = new MasterDiscountDetailsAPIInputModel()
                {
                    totalDiscount = masterDiscountDetailsViewModel.totalDiscount,
                    masterDiscountId = masterDiscountDetailsViewModel.masterDiscountId,
                    displayOrder = Convert.ToString( masterDiscountDetailsViewModel.displayOrder),
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterDiscountDetailsAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterDiscountDetailsAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterDiscountDetailsAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveDiscountDetails(masterDiscountDetailsAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveDiscountDetails(MasterDiscountDetailsAPIInputModel masterDiscountDetailsAPIInputModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteMasterDiscountDetails(masterDiscountDetailsAPIInputModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetDiscountList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Subject List
        private async Task GetDiscountList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterDiscountDetailsListModels = (await masterDataService.GetMasterDiscountDetailsList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterDiscountDetailsViewModel.displayOrder = null;
            masterDiscountDetailsViewModel.totalDiscount =0;
            
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            //Get all list from API.
            GetDiscountList();

        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        //public void RowSelectHandler(RowSelectEventArgs<MasterDiscountDetailsListModel> args)
        //{
        //    masterDiscountDetailsViewModel = new MasterDiscountDetailsViewModel()
        //    {
        //        totalDiscount = args.RowData.totalDiscount,
        //        displayOrder = Convert.ToInt16(args.RowData.displayOrder),
        //        masterDiscountId = args.RowData.masterDiscountId
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
        public List<string> classMasterItem = (new List<string>() { "Add Discount", "Print", "Search" });

        public SfGrid<MasterDiscountDetailsListModel> sfDiscount;
        public string btncss = "";
        public void EditDiscount(CommandClickEventArgs<MasterDiscountDetailsListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Discount Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterDiscountDetailsViewModel = new MasterDiscountDetailsViewModel()
                {
                    totalDiscount = args.RowData.totalDiscount,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterDiscountId = args.RowData.masterDiscountId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Discount Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterDiscountDetailsViewModel = new MasterDiscountDetailsViewModel()
                {
                    totalDiscount = args.RowData.totalDiscount,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterDiscountId = args.RowData.masterDiscountId
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Discount")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Discount";
                OperationType = "Add";
                HeaderTest = "Add Discount";
                masterDiscountDetailsViewModel.totalDiscount = 0;
                masterDiscountDetailsViewModel.displayOrder = null;
            }
        }
    }
}



