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
    public class VenderMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterVenderDetailsAPIInputModel masterVenderDetailsAPIInputModel { get; set; }
        //data binding for blazor page
        public MasterVenderDetailsViewModel masterVenderDetailsViewModel = new MasterVenderDetailsViewModel();

        //list Model
        public List<MasterVenderDetailsListModel> masterVenderDetailsListModels = new List<MasterVenderDetailsListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {

                masterVenderDetailsAPIInputModel = new MasterVenderDetailsAPIInputModel()
                {
                    venderName = masterVenderDetailsViewModel.venderName,
                    venderCode = masterVenderDetailsViewModel.venderCode,
                    gSTNO = masterVenderDetailsViewModel.gSTNO,
                    address = masterVenderDetailsViewModel.address,
                    contactNo = masterVenderDetailsViewModel.contactNo,
                    venderType = masterVenderDetailsViewModel.contactNo,
                    masterVenderId = masterVenderDetailsViewModel.masterVenderId,
                    displayOrder = Convert.ToInt16( masterVenderDetailsViewModel.displayOrder),
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterVenderDetailsAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterVenderDetailsAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterVenderDetailsAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveVenderMasterDetails(masterVenderDetailsAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveVenderMasterDetails(MasterVenderDetailsAPIInputModel masterVenderDetailsAPIInputModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteMasterVenderDetails(masterVenderDetailsAPIInputModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetVenderMasterList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Subject List
        private async Task GetVenderMasterList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterVenderDetailsListModels = (await masterDataService.GetMasterVenderDetailsList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterVenderDetailsViewModel.displayOrder = null;
            masterVenderDetailsViewModel.venderName = null;
            masterVenderDetailsViewModel.venderCode = null;
            masterVenderDetailsViewModel.gSTNO = null;
            masterVenderDetailsViewModel.contactNo = null;
            masterVenderDetailsViewModel.address = null;
            masterVenderDetailsViewModel.venderType = null;
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            //Get all list from API.
            GetVenderMasterList();

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
        public List<string> classMasterItem = (new List<string>() { "Add Vender", "Print", "Search" });

        public SfGrid<MasterVenderDetailsListModel> sfVenderMaster;
        public string btncss = "";
        public void EditVenderMaster(CommandClickEventArgs<MasterVenderDetailsListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Vender Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterVenderDetailsViewModel = new MasterVenderDetailsViewModel()
                {
                    venderName = args.RowData.venderName,
                    venderCode = args.RowData.venderCode,
                    gSTNO = args.RowData.gSTNO,
                    address = args.RowData.address,
                    contactNo = args.RowData.contactNo,
                    venderType = args.RowData.venderType,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterVenderId = args.RowData.masterVenderId,
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Vender Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterVenderDetailsViewModel = new MasterVenderDetailsViewModel()
                {
                    venderName = args.RowData.venderName,
                    venderCode = args.RowData.venderCode,
                    gSTNO = args.RowData.gSTNO,
                    address = args.RowData.address,
                    contactNo = args.RowData.contactNo,
                    venderType = args.RowData.venderType,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterVenderId = args.RowData.masterVenderId,
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Vender")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Vender";
                OperationType = "Add";
                HeaderTest = "Add Vender";
                masterVenderDetailsViewModel.venderName = null;
                masterVenderDetailsViewModel.venderCode = null;
                masterVenderDetailsViewModel.gSTNO = null;
                masterVenderDetailsViewModel.address = null;
                masterVenderDetailsViewModel.contactNo = null;
                masterVenderDetailsViewModel.venderType = null;
                masterVenderDetailsViewModel.displayOrder = null;
            }
        }
    }
}



