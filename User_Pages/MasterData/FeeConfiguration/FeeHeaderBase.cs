using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;
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

namespace AdminDashboard.Server.User_Pages.MasterData.FeeConfiguration
{
    public class FeeHeaderBase: ComponentBase
    {
     

            public List<FeeHeaderOutputModel>  feeHeaderOutputModels = new List<FeeHeaderOutputModel>();
            public SfGrid<FeeHeaderOutputModel> sfFeeHeaderDetails;

            [Inject]
            Blazored.SessionStorage.ISessionStorageService session { get; set; }
            public SessionModel _sessionModel;

            [Inject]
            public ISnackbar snackBar { get; set; }
            [Inject]
            public IMasterDataSetupService masterDataSetupService { get; set; }

             public DialogEffect AnimationEffect = DialogEffect.Zoom;
            public string HeaderStyles { get; set; } = "e-background e-accent";
            public SfDialog DialogRef;

            public APIReturnModel aPIReturnModel;

            public FeeHeaderApiModel FeeHeaderApiModel;
            public FeeHeaderViewModel FeeHeaderViewModel=new FeeHeaderViewModel();

            public bool IsVisible { get; set; } = false;
            public bool IsDeleteVisible { get; set; } = false;
            public string DialogHeaderName = string.Empty;
            public bool ddEnable = true;
            public string btncss = "";
            public string HeaderText = string.Empty;
            public string OperationType = "";

          


            public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

            public List<string> EnquirytoolBarItems = (new List<string>() { "Add Fee Header", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                FeeHeaderApiModel = new FeeHeaderApiModel()
                { 
                   feeHeaderId= FeeHeaderViewModel.feeHeaderId,
                   feeHeaderName = FeeHeaderViewModel.feeHeaderName,
                   feeHeaderDescription = FeeHeaderViewModel.feeHeaderDescription,
                     displayOrder = FeeHeaderViewModel.displayOrder,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,

                };

                if (OperationType == "Add")
                {
                    FeeHeaderApiModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    FeeHeaderApiModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    FeeHeaderApiModel.OperationType = OperationAction.Delete;
                }
                FeeHeaderSave(FeeHeaderApiModel);
            };
        }

        private async void FeeHeaderSave(FeeHeaderApiModel FeeHeaderApiModel)
        {
            try
            {
                if (FeeHeaderApiModel.OperationType != "NA")
                {
                    aPIReturnModel = await masterDataSetupService.CRUD_Fee_FeeHeaderMaster(FeeHeaderApiModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success); 
                        FeeHeaderParaModel feeHeaderParaModel = new FeeHeaderParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            feeHeaderID = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                        feeHeaderOutputModels = (await masterDataSetupService.GET_Fee_FeeHeaderList(feeHeaderParaModel)).ToList();
                        StateHasChanged();
                        ClearData();
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
        public void ClearData() { }
        protected override async Task OnInitializedAsync()
            {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            FeeHeaderParaModel feeHeaderParaModel = new FeeHeaderParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                 feeHeaderID = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            feeHeaderOutputModels = (await masterDataSetupService.GET_Fee_FeeHeaderList(feeHeaderParaModel)).ToList();
            }
         
            public void EditItemDetail(CommandClickEventArgs<FeeHeaderOutputModel> args)
            {


            }
         
            public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
            {
                if (args.Item.Text == "Add Fee Header")
                {
                    //navigationManager.NavigateTo("/OnlineExam/TestList");
                    //perform your actions here
                    IsVisible = true;
                    OperationType = "";
                    btncss = "e-flat e-primary e-outline";
                    DialogHeaderName = "Add Fee Header";
                    OperationType = "Add";
                    HeaderText = "Add Fee Header";
                    ddEnable = true;

                }

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

