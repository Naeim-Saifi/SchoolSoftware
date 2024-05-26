
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
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;

namespace AdminDashboard.Server.User_Pages.MasterData.FeeConfiguration
{
    public class DiscountRemarkBase: ComponentBase
    {
    

            public List<FeeDiscountRemarksOutputModel> _feeDiscountRemarksList = new List<FeeDiscountRemarksOutputModel>();
            public SfGrid<FeeDiscountRemarksOutputModel> sfItemDetails;

            [Inject]
            Blazored.SessionStorage.ISessionStorageService session { get; set; }
            public SessionModel _sessionModel;

            [Inject]
            public ISnackbar snackBar { get; set; }


            public DialogEffect AnimationEffect = DialogEffect.Zoom;
            public string HeaderStyles { get; set; } = "e-background e-accent";
            public SfDialog DialogRef;

             
            [Inject]
            public IMasterDataSetupService masterDataSetupService { get; set; }

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

            public List<string> EnquirytoolBarItems = (new List<string>() { "Add I", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });
         

            protected override async Task OnInitializedAsync()
            {
                 _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");


            FeeDiscountRemarksParaModel feeDiscountRemarksParaModel = new FeeDiscountRemarksParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                 feeDiscountID = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            _feeDiscountRemarksList = (await masterDataSetupService.GET_Fee_DiscountRemarksList(feeDiscountRemarksParaModel)).ToList();
            } 

            public void EditItemDetail(CommandClickEventArgs<FeeDiscountRemarksOutputModel> args)
            {


            }


            public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
            {
                if (args.Item.Text == "Add I")
                {
                    //navigationManager.NavigateTo("/OnlineExam/TestList");
                    //perform your actions here
                    IsVisible = true;
                    OperationType = "";
                    btncss = "e-flat e-primary e-outline";
                    DialogHeaderName = "Add  Details";
                    OperationType = "Add";
                    HeaderText = "Add";
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
