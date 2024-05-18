﻿
//using AdminDashboard.Server.API_Service.Service.Inventory;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;
//using AIS.Data.RequestResponseModel.Inventory.ItemVender;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using AdminDashboard.Server.API_Service.Interface.Inventory;
using System.Linq;
using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Class;
using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Section;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using AIS.Data.RequestResponseModel.MasterData.TransportConfiguration.BusStop;
using AIS.Data.RequestResponseModel.MasterData.TransportConfiguration.TransportDetails;
//using AdminDashboard.Server.API_Service.Interface.MasterData;

namespace AdminDashboard.Server.User_Pages.MasterData.TransportConfiguration
{
    public class TransportDetailsBase : ComponentBase
    {



            public TransportDetailsViewModel transportDetailsViewModel = new TransportDetailsViewModel();


            public List<TransportDetailsOutputModel> _TranportDetailsistModel = new List<TransportDetailsOutputModel>();
            public SfGrid<TransportDetailsOutputModel> sfTransporDetails;
            public TransportDetailsApiModel transportDetailsApiModel { get; set; }



            [Inject]
            Blazored.SessionStorage.ISessionStorageService session { get; set; }
            public SessionModel _sessionModel;

            [Inject]
            public ISnackbar snackBar { get; set; }

            // [Inject]
            //   public IMasterDataService masterDataService { get; set; }

            public DialogEffect AnimationEffect = DialogEffect.Zoom;
            public string HeaderStyles { get; set; } = "e-background e-accent";
            public SfDialog DialogRef;

            List<string> IList = new List<string>();


            public bool IsVisible { get; set; } = false;
            public bool IsDeleteVisible { get; set; } = false;
            public string DialogHeaderName = string.Empty;
            public bool ddEnable = true;
            public string btncss = "";
            public string HeaderText = string.Empty;
            public string OperationType = "";

            public APIReturnModel aPIReturnModel;


            public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

            public List<string> ToolBarItems = (new List<string>() { "Add Transport", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });








        public class FeuelType

        {
            public int Id;
            public string Value;
        }

        public List<FeuelType> FuelTypeDetails = new List<FeuelType>()
        {
            new FeuelType{Id=1,Value="petrol"},
            new FeuelType{Id=2,Value="Diseal"},
        };


            protected override async Task OnInitializedAsync()
            {
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            //{
            //    classId = 0,
            //    userId = _sessionModel.UserId,
            //    financialYear = _sessionModel.FinancialYear,
            //    schoolCode = _sessionModel.SchoolCode,
            //    reportType = ReportType.All
            //};
            //_classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            TransportDetailsParaModel transportDetailsParaModel = new TransportDetailsParaModel()
            {
                    financialYear = _sessionModel.FinancialYear,
                    schoolCode = _sessionModel.SchoolCode,
                    TransportDetailsId = 0,
                    userRoleId = _sessionModel.RoleId,
                    reportType = ReportType.All
                };

                //    _ClassListModel = (await masterDataService.GET_ClassDetails_List(classParaModel)).ToList();
            }







            public void EditItemDetail(CommandClickEventArgs<TransportDetailsOutputModel> args)
            {


                // Perform required operations here
                string buttontext = args.CommandColumn.ButtonOption.Content;
                //int testId = args.RowData.testID;
                if (buttontext == "Edit")
                {
                    //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                    IsVisible = true;
                    DialogHeaderName = "Update Transpor Details";
                    HeaderText = "Update Record";
                    OperationType = "Update";
                    btncss = "e-flat e-info e-outline";
                // ddEnable = false;
                transportDetailsViewModel = new TransportDetailsViewModel()
                    {

                    busNo = args.RowData.busNo,
                    insuranceValidDate= args.RowData.insuranceValidDate,
                    registrationNo= args.RowData.registrationNo,
                    totalNoOfSeat= args.RowData.totalNoOfSeat,
                    busOwnerName= args.RowData.busOwnerName,
                    fitsnessUpto= args.RowData.fitsnessUpto,
                    ownerAddress= args.RowData.ownerAddress,
                    purchaseDate= args.RowData.purchaseDate,
                    manufactureBy= args.RowData.manufactureBy,
                    manufacturYeaer= args.RowData.manufacturYeaer,
                    fuelType= args.RowData.fuelType,
                    chasisNo= args.RowData.chasisNo,
                    insuranceCompanyBy= args.RowData.insuranceCompanyBy,


                };
                }
                else
                {
                    IsVisible = true;
                    DialogHeaderName = "Delete Transport Details";
                    OperationType = "Delete";
                    HeaderText = "Delete Record";
                    btncss = "e-flat e-danger e-outline";
                    ddEnable = false;
                transportDetailsViewModel = new TransportDetailsViewModel()
                {


                    busNo = args.RowData.busNo,
                    insuranceValidDate = args.RowData.insuranceValidDate,
                    registrationNo = args.RowData.registrationNo,
                    totalNoOfSeat = args.RowData.totalNoOfSeat,
                    busOwnerName = args.RowData.busOwnerName,
                    fitsnessUpto = args.RowData.fitsnessUpto,
                    ownerAddress = args.RowData.ownerAddress,
                    purchaseDate = args.RowData.purchaseDate,
                    manufactureBy = args.RowData.manufactureBy,
                    manufacturYeaer = args.RowData.manufacturYeaer,
                    fuelType = args.RowData.fuelType,
                    chasisNo = args.RowData.chasisNo,
                    insuranceCompanyBy = args.RowData.insuranceCompanyBy,

                };
            }
                //else
                //{
                //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
                //}

            }


            public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
            {
                if (args.Item.Text == "Add Transport")
                {
                    //navigationManager.NavigateTo("/OnlineExam/TestList");
                    //perform your actions here
                    IsVisible = true;
                    OperationType = "";
                    btncss = "e-flat e-primary e-outline";
                    DialogHeaderName = "Add Transport Details";
                    OperationType = "Add";
                    HeaderText = "Add Tranport";
                    ddEnable = true;

                }
                else if (args.Item.Text == "ExportExcel")
                {
                    this.sfTransporDetails.ExportToExcelAsync();
                }
                else if (args.Item.Text == "Collapse All")
                {
                    this.sfTransporDetails.CollapseAllGroupAsync();
                }
                else if (args.Item.Text == "Expand All")
                {
                    this.sfTransporDetails.ExpandAllGroupAsync();
                }
            }




            public async void OnValidSubmit(EditContext contex)
            {
                bool isValid = contex.Validate();
                if (isValid)
                {
                transportDetailsApiModel = new TransportDetailsApiModel()
                    {



                    BusNo =Convert.ToInt32(transportDetailsViewModel.busNo),
                    InsuranceValidDate = transportDetailsViewModel.insuranceValidDate,
                    RegistrationNo = Convert.ToInt32(transportDetailsViewModel.registrationNo),
                    TotalNoOfSeat = Convert.ToInt32(transportDetailsViewModel.totalNoOfSeat),
                    BusOwnerName = transportDetailsViewModel.busOwnerName,
                    FitsnessUpto = transportDetailsViewModel.fitsnessUpto,
                    OwnerAddress = transportDetailsViewModel.ownerAddress,
                    PurchaseDate = transportDetailsViewModel.purchaseDate,
                    ManufactureBy = transportDetailsViewModel.manufactureBy,
                    ManufacturYeaer = transportDetailsViewModel.manufacturYeaer,
                    FuelType = transportDetailsViewModel.fuelType,
                    ChasisNo = Convert.ToInt32(transportDetailsViewModel.chasisNo),
                    InsuranceCompanyBy = transportDetailsViewModel.insuranceCompanyBy,




                };
                    if (OperationType == "Add")
                    {
                    transportDetailsApiModel.OperationType = OperationAction.Add;
                    }
                    else if (OperationType == "Update")
                    {
                    transportDetailsApiModel.OperationType = OperationAction.Update;
                    }
                    //Delete Operation
                    else
                    {
                    transportDetailsApiModel.OperationType = OperationAction.Delete;
                    }

                    SaveItemMasterDetails(transportDetailsApiModel);

                }
                else
                {
                    // Form has invalid inputs.
                }

            }

            private async void SaveItemMasterDetails(TransportDetailsApiModel transportDetailsApiModel)
            {
                try
                {
                    if (transportDetailsApiModel.OperationType != "NA")
                    {
                        //  aPIReturnModel = await masterDataService.CRUD_ClassDetails(classApiModel);

                        if (aPIReturnModel.status == false)
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Success);


                        TransportDetailsParaModel transportDetailsParaModel = new TransportDetailsParaModel()
                            {
                                financialYear = _sessionModel.FinancialYear,
                                schoolCode = _sessionModel.SchoolCode,
                              TransportDetailsId = 0,
                                userRoleId = _sessionModel.RoleId,
                                reportType = ReportType.All
                            };

                            //   _ClassListModel  = (await masterDataService.GET_ClassDetails_List(classParaModel)).ToList();

                            ClearData();
                            StateHasChanged();
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








            private void ClearData()
            {
            transportDetailsViewModel = new TransportDetailsViewModel();

            }


            public void ShowDialog()
            {
                IsVisible = true;
            }
            public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
            {
                // setting maximum height to the Dialog
                args.MaxHeight = "750px";

            }
            public async Task CloseDialog()
            {
                IsVisible = false;
                await this.DialogRef.HideAsync();
            }
        }
    }