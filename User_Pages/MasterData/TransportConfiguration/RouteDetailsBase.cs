using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
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

namespace AdminDashboard.Server.User_Pages.MasterData.TransportConfiguration
{
    public class RouteDetailsBase: ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;



        [Inject]
        public ISnackbar snackBar { get; set; }

        public RouteDetailsViewModel routeDetailsViewModel = new RouteDetailsViewModel();
        public List<RouteDetailsOutputModel> _RouteDetailsistModel = new List<RouteDetailsOutputModel>();
        public SfGrid<RouteDetailsOutputModel> sfRouteDetails;

        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        public RouteDetailsApiModel routeDetailsApiModel { get; set; }

        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";

        public APIReturnModel aPIReturnModel;
        public List<object> MenuItems = new List<object>()
        { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
            "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
        };

        public List<string> ToolBarItems = (new List<string>() { "Add Route", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });


        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");


            RouteDetailsParaModel routeDetailsParaModel = new RouteDetailsParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                RouteDetailsId = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            _RouteDetailsistModel = (await masterDataSetupService.GET_Transport_RouteMaster(routeDetailsParaModel)).ToList();
        }

        public void EditItemDetail(CommandClickEventArgs<RouteDetailsOutputModel> args)
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
                //routeDetailsViewModel = new RouteDetailsViewModel()

                //    {



                //    };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Bus Stop Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                //routeDetailsViewModel = new RouteDetailsViewModel()

                //    {




                //    };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Route")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Route Details";
                OperationType = "Add";
                HeaderText = "Add Route";
                ddEnable = true;

            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfRouteDetails.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfRouteDetails.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfRouteDetails.ExpandAllGroupAsync();
            }
        }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                routeDetailsApiModel = new RouteDetailsApiModel()
                {
                    RouteDetailsId= routeDetailsViewModel.routeDetailsId,
                    RouteName= routeDetailsViewModel.routeName,
                    Description= routeDetailsViewModel.description,
                    DisplayOrder=Convert.ToInt16(routeDetailsViewModel.displayOrder),
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear

                };
                if (OperationType == "Add")
                {
                    routeDetailsApiModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    routeDetailsApiModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    routeDetailsApiModel.OperationType = OperationAction.Delete;
                }

                SaveItemMasterDetails(routeDetailsApiModel);

            }
            else
            {
                // Form has invalid inputs.
            }

        }

        private async void SaveItemMasterDetails(RouteDetailsApiModel routeDetailsApiModel)
        {
            try
            {
                if (routeDetailsApiModel.OperationType != "NA")
                {
                    aPIReturnModel = await masterDataSetupService.CRUD_Transport_RouteMaster(routeDetailsApiModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);


                        RouteDetailsParaModel routeDetailsParaModel = new RouteDetailsParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            RouteDetailsId = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                        _RouteDetailsistModel = (await masterDataSetupService.GET_Transport_RouteMaster(routeDetailsParaModel)).ToList();

                        //ClearData();
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


        public SfDialog DialogRef;

        List<string> IList = new List<string>();
        public bool IsVisible { get; set; } = false;
        public bool IsDeleteVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";







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
