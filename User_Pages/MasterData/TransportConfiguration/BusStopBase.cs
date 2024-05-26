
 
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;
 
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
 
using System.Linq;
using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Class;
using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Section;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;

namespace AdminDashboard.Server.User_Pages.MasterData.TransportConfiguration
{
    public class BusStopBase:ComponentBase
    { 
            public BusStopViewModel busStopViewModel = new BusStopViewModel(); 
            public List<BusStopOutputModel> _BusStopistModel = new List<BusStopOutputModel>();
            public SfGrid<BusStopOutputModel> sfBusStopDetails;
            public BusStopApiModel busStopApiModel { get; set; }



            [Inject]
            Blazored.SessionStorage.ISessionStorageService session { get; set; }
            public SessionModel _sessionModel;
            [Inject]
            public IMasterDataSetupService masterDataSetupService { get; set; }
            [Inject]
            public ISnackbar snackBar { get; set; }

            public DateTime MinVal { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 08, 00, 00);
            public DateTime MaxVal { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 16, 45, 00);


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
            
            public DateTime? TimeValue { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 11, 40, 00);



            public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

            public List<string> ToolBarItems = (new List<string>() { "Add Bus Stop", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });
         
            public class Designation

            {
                public int Id;
                public string Value;
            }

            public List<Designation> DesignationDetils = new List<Designation>()
            {
            new Designation{Id=1,Value="Driver"}
            };


            protected override async Task OnInitializedAsync()
            {
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
             

            BusStopParaModel busStopParaModel = new BusStopParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                BusStopId = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            _BusStopistModel = (await masterDataSetupService.GET_Transport_BusStopMaster(busStopParaModel)).ToList();
            }

        public void EditItemDetail(CommandClickEventArgs<BusStopOutputModel> args)
            {


                // Perform required operations here
                string buttontext = args.CommandColumn.ButtonOption.Content;
                //int testId = args.RowData.testID;
                if (buttontext == "Edit")
                {
                    //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                    IsVisible = true;
                    DialogHeaderName = "Update Bus Stop Details";
                    HeaderText = "Update Record";
                    OperationType = "Update";
                    btncss = "e-flat e-info e-outline";
                // ddEnable = false;
                    busStopViewModel= new BusStopViewModel()

                    {
                        busStopName = args.RowData.busStopName,
                        kM = args.RowData.kM,
                        distanceCharge = args.RowData.distanceCharge,
                        pickupTime =Convert.ToDateTime(args.RowData.pickupTime),
                        dropTIme = Convert.ToDateTime(args.RowData.dropTIme),


                    };
                }
                else
                {
                    IsVisible = true;
                    DialogHeaderName = "Delete Bus Stop Details";
                    OperationType = "Delete";
                    HeaderText = "Delete Record";
                    btncss = "e-flat e-danger e-outline";
                    ddEnable = false;
                busStopViewModel = new BusStopViewModel()

                {
                    busStopName = args.RowData.busStopName,
                    kM = args.RowData.kM,
                    distanceCharge = args.RowData.distanceCharge,
                    pickupTime = Convert.ToDateTime(args.RowData.pickupTime),
                    dropTIme = Convert.ToDateTime(args.RowData.dropTIme),



                };
            }
                //else
                //{
                //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
                //}

            }
         
            public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
            {
                if (args.Item.Text == "Add Bus Stop")
                {
                    //navigationManager.NavigateTo("/OnlineExam/TestList");
                    //perform your actions here
                    IsVisible = true;
                    OperationType = "";
                    btncss = "e-flat e-primary e-outline";
                    DialogHeaderName = "Add  Bus Stop Details";
                    OperationType = "Add";
                    HeaderText = "Add Bus Stop";
                    ddEnable = true;

                }
                else if (args.Item.Text == "ExportExcel")
                {
                    this.sfBusStopDetails.ExportToExcelAsync();
                }
                else if (args.Item.Text == "Collapse All")
                {
                    this.sfBusStopDetails.CollapseAllGroupAsync();
                }
                else if (args.Item.Text == "Expand All")
                {
                    this.sfBusStopDetails.ExpandAllGroupAsync();
                }
            }

            public async void OnValidSubmit(EditContext contex)
            {
                    bool isValid = contex.Validate();
                    if (isValid)
                    {
                    busStopApiModel = new BusStopApiModel()
                        {
                        BusStopName = busStopViewModel.busStopName,
                        KM =Convert.ToInt16(busStopViewModel.kM),
                        DistanceCharge = Convert.ToInt16(busStopViewModel.distanceCharge),
                        PickupTime = DateTime.Now.ToShortTimeString(),//Convert.(busStopViewModel.pickupTime),
                        DropTime = DateTime.Now.ToShortTimeString(),// Convert.ToDateTime(busStopViewModel.dropTIme),
                         BusStopId = busStopViewModel.busStopId,
                        CreatedByUserId = _sessionModel.UserId,
                        UpdatedByUserId = _sessionModel.UserId,
                        SchoolCode = _sessionModel.SchoolCode,
                        FinancialYear = _sessionModel.FinancialYear,

                    };
                    if (OperationType == "Add")
                    {
                    busStopApiModel.OperationType = OperationAction.Add;
                    }
                    else if (OperationType == "Update")
                    {
                    busStopApiModel.OperationType = OperationAction.Update;
                    }
                    //Delete Operation
                    else
                    {
                    busStopApiModel.OperationType = OperationAction.Delete;
                    }

                    SaveBusStopDetails(busStopApiModel);

                }
                else
                {
                    // Form has invalid inputs.
                }

            }

            private async void SaveBusStopDetails(BusStopApiModel busStopApiModel)
            {
                try
                {
                    if (busStopApiModel.OperationType != "NA")
                    {
                    aPIReturnModel = await masterDataSetupService.CRUD_Transport_BusStopMaster(busStopApiModel);

                    if (aPIReturnModel.status == false)
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Success);

                            BusStopParaModel busStopParaModel = new BusStopParaModel()
                            {
                                financialYear = _sessionModel.FinancialYear,
                                schoolCode = _sessionModel.SchoolCode,
                                BusStopId = 0,
                                userRoleId = _sessionModel.RoleId,
                                reportType = ReportType.All
                            };
                            _BusStopistModel = (await masterDataSetupService.GET_Transport_BusStopMaster(busStopParaModel)).ToList();

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
            busStopViewModel = new BusStopViewModel();


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