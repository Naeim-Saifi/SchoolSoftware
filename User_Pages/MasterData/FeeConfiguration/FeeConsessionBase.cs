
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
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
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.MasterData.FeeConfiguration
{
    public class FeeConsessionBase: ComponentBase
    {
  

            public List<FeeConcessionCategoryOutputModel> _FeeConcessionCategoryList = new List<FeeConcessionCategoryOutputModel>();
            public SfGrid<FeeConcessionCategoryOutputModel> sfItemDetails;

            [Inject]
            Blazored.SessionStorage.ISessionStorageService session { get; set; }
            public SessionModel _sessionModel;

            [Inject]
            public ISnackbar snackBar { get; set; }
            [Inject]
            public IMasterDataSetupService masterDataSetupService { get; set; }

          public FeeConcessionCategoryViewModel feeConcessionCategoryViewModel=new FeeConcessionCategoryViewModel();

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

            public List<string> EnquirytoolBarItems = (new List<string>() { "Add Fee Category", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });


            protected override async Task OnInitializedAsync()
            {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            FeeConcessionCategoryParaModel feeConcessionCategoryParaModel = new FeeConcessionCategoryParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                 feeCategoryID = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            _FeeConcessionCategoryList = (await masterDataSetupService.GET_Fee_ConcessionCategoryList(feeConcessionCategoryParaModel)).ToList();
            }

        public FeeConcessionCategoryApiModel feeConcessionCategoryApiModel;
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                feeConcessionCategoryApiModel = new FeeConcessionCategoryApiModel()
                {
                     FeeConcessionCategoryId = feeConcessionCategoryViewModel.FeeConcessionCategoryId,
                     feeCategoryName = feeConcessionCategoryViewModel.feeCategoryName,
                     feeCategoryDescription = feeConcessionCategoryViewModel.feeCategoryDescription,
                    displayOrder = feeConcessionCategoryViewModel.displayOrder,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,

                };

                if (OperationType == "Add")
                {
                    feeConcessionCategoryApiModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    feeConcessionCategoryApiModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    feeConcessionCategoryApiModel.OperationType = OperationAction.Delete;
                }
                FeeConsissionCategorySave(feeConcessionCategoryApiModel);
            };
        }

        private async void FeeConsissionCategorySave(FeeConcessionCategoryApiModel feeConcessionCategoryApiModel)
        {
            try
            {
                if (feeConcessionCategoryApiModel.OperationType != "NA")
                {
                    aPIReturnModel = await masterDataSetupService.CRUD_Fee_ConcessionCategory(feeConcessionCategoryApiModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                        FeeConcessionCategoryParaModel feeConcessionCategoryParaModel = new FeeConcessionCategoryParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            feeCategoryID = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                        _FeeConcessionCategoryList = (await masterDataSetupService.GET_Fee_ConcessionCategoryList(feeConcessionCategoryParaModel)).ToList();
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



            public void EditItemDetail(CommandClickEventArgs<FeeConcessionCategoryOutputModel> args)
            {


            }


            public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
            {
                if (args.Item.Text == "Add Fee Category")
                {
                    //navigationManager.NavigateTo("/OnlineExam/TestList");
                    //perform your actions here
                    IsVisible = true;
                    OperationType = "";
                    btncss = "e-flat e-primary e-outline";
                    DialogHeaderName = "Add Fee Category";
                    OperationType = "Add";
                    HeaderText = "Add Fee Category";
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

