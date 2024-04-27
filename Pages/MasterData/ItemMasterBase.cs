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
    public class ItemMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterItemDetailsAPIInputModel masterItemDetailsAPIInputModel { get; set; }
        //data binding for blazor page
        public MasterItemDetailsViewModel masterItemDetailsViewModel = new MasterItemDetailsViewModel();

        //list Model
        public List<MasterItemDetailsListModel> masterItemDetailsListModels = new List<MasterItemDetailsListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {

                masterItemDetailsAPIInputModel = new MasterItemDetailsAPIInputModel()
                {
                    itemName = masterItemDetailsViewModel.itemName,
                    itemType = masterItemDetailsViewModel.itemType,
                    barCodeNo = masterItemDetailsViewModel.barCodeNo,
                    itemCode = masterItemDetailsViewModel.itemCode,
                    itemDescription = masterItemDetailsViewModel.itemDescription,
                    masterItemId = masterItemDetailsViewModel.masterItemId,
                    displayOrder = Convert.ToInt16( masterItemDetailsViewModel.displayOrder),
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterItemDetailsAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterItemDetailsAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterItemDetailsAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveItemMasterDetails(masterItemDetailsAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveItemMasterDetails(MasterItemDetailsAPIInputModel masterItemDetailsAPIInputModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteMasterItemDetails(masterItemDetailsAPIInputModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetItemMasterList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Subject List
        private async Task GetItemMasterList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterItemDetailsListModels = (await masterDataService.GetMasterItemDetailsList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterItemDetailsViewModel.displayOrder = null;
            masterItemDetailsViewModel.itemName = null;
            masterItemDetailsViewModel.itemType = null;
            masterItemDetailsViewModel.barCodeNo = null;
            masterItemDetailsViewModel.itemCode = null;
            masterItemDetailsViewModel.itemDescription = null;
            
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            //Get all list from API.
            GetItemMasterList();

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
        public List<string> classMasterItem = (new List<string>() { "Add Item", "Print", "Search" });

        public SfGrid<MasterItemDetailsListModel> sfItemMaster;
        public string btncss = "";
        public void EditItemMaster(CommandClickEventArgs<MasterItemDetailsListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Item Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterItemDetailsViewModel = new MasterItemDetailsViewModel()
                {
                    itemName = args.RowData.itemName,
                    itemType = args.RowData.itemType,
                    barCodeNo = args.RowData.barCodeNo,
                    itemCode = args.RowData.itemCode,
                    itemDescription = args.RowData.itemDescription,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterItemId = args.RowData.masterItemId,
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Item Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterItemDetailsViewModel = new MasterItemDetailsViewModel()
                {
                    itemName = args.RowData.itemName,
                    itemType = args.RowData.itemType,
                    barCodeNo = args.RowData.barCodeNo,
                    itemCode = args.RowData.itemCode,
                    itemDescription = args.RowData.itemDescription,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterItemId = args.RowData.masterItemId,
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Item")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Item";
                OperationType = "Add";
                HeaderTest = "Add Item";
                masterItemDetailsViewModel.itemName = null;
                masterItemDetailsViewModel.itemType = null;
                masterItemDetailsViewModel.barCodeNo = null;
                masterItemDetailsViewModel.itemCode = null;
                masterItemDetailsViewModel.itemDescription = null;
                masterItemDetailsViewModel.displayOrder = null;
            }
        }
    }
}





