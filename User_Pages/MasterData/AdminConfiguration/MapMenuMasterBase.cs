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
namespace AdminDashboard.Server.User_Pages.MasterData.AdminConfiguration
{
    public class MapMenuMasterBase : ComponentBase
    {


  
            public List<ItemMasterListMoel> _ItemMasterListMoel = new List<ItemMasterListMoel>();
            public SfGrid<ItemMasterListMoel> sfItemDetails;

            [Inject]
            Blazored.SessionStorage.ISessionStorageService session { get; set; }
            public SessionModel _sessionModel;

            [Inject]
            public ISnackbar snackBar { get; set; }


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

            public List<string> EnquirytoolBarItems = (new List<string>() { "Add I", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });







            protected override async Task OnInitializedAsync()
            {
                //_sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
                ////Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
                ////{
                ////    classId = 0,
                ////    userId = _sessionModel.UserId,
                ////    financialYear = _sessionModel.FinancialYear,
                ////    schoolCode = _sessionModel.SchoolCode,
                ////    reportType = ReportType.All
                ////};
                ////_classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

                //ItemMasterParaModel itemMasterParaModel = new ItemMasterParaModel()
                //{
                //    financialYear = _sessionModel.FinancialYear,
                //    schoolCode = _sessionModel.SchoolCode,
                //    ItemId = 0,
                //    userRoleId = _sessionModel.RoleId,
                //    reportType = ReportType.All
                //};

                // _ItemVenderListModel = (await enquiryService.GET_EnquiryDetails_List(itemMasterParaModel)).ToList();
            }







            public void EditItemDetail(CommandClickEventArgs<ItemMasterListMoel> args)
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

