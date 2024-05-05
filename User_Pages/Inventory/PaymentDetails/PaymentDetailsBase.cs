//using AdminDashboard.Server.API_Service.Service.Inventory;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;
using AIS.Data.RequestResponseModel.Inventory.ItemVender;
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
//using AIS.Data.RequestResponseModel.Structure;
using AIS.Data.RequestResponseModel.Inventory.ItemPaymentDetails;

namespace AdminDashboard.Server.User_Pages.Inventory.PaymentDetails
{
    public class PaymentDetailsBase:ComponentBase
    {


        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        [Inject]
        public ISnackbar snackBar { get; set; }


        public ItemPaymentDetailsViewModel itemPaymentDetailsViewModel = new ItemPaymentDetailsViewModel();
        public List<ItemPaymentDetailsListModel> _ItemPaymentDetailsListModel = new List<ItemPaymentDetailsListModel>();
        public ItemPaymentDetailsAPIModel iAPIModel { get; set; }


        public SfGrid<ItemPaymentDetailsListModel> sfDetails;
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;


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

        public List<string> ItoolBarItems = (new List<string>() { "Add Payment", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });


        public class PaymentMode
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<PaymentMode> PaymenModeDetails = new List<PaymentMode>()
        {

            new PaymentMode{Id=1,Value="Cash"},
            new PaymentMode{Id=2,Value="Cheque"},
            new PaymentMode{Id=3,Value="Online"}
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

            ItemPaymentDetailsParaModel iParaModel = new ItemPaymentDetailsParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                // ItemId = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            // _IListModel = (await enquiryService.GET_EnquiryDetails_List(iParaModel)).ToList();
        }






        public void EditItemDetail(CommandClickEventArgs<ItemPaymentDetailsListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update  Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                // ddEnable = false;
                itemPaymentDetailsViewModel = new ItemPaymentDetailsViewModel()
                {





                };


            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                itemPaymentDetailsViewModel = new ItemPaymentDetailsViewModel()
                {



                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Payment")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Payment Details";
                OperationType = "Add";
                HeaderText = "Add";
                ddEnable = true;

            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfDetails.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfDetails.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfDetails.ExpandAllGroupAsync();
            }
        }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                iAPIModel = new ItemPaymentDetailsAPIModel()
                {




                };
                if (OperationType == "Add")
                {
                    iAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    iAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    iAPIModel.OperationType = OperationAction.Delete;
                }

                SaveDetails(iAPIModel);

            }
            else
            {
                // Form has invalid inputs.
            }

        }


        private async void SaveDetails(ItemPaymentDetailsAPIModel iAPIModel)
        {
            try
            {
                if (iAPIModel.OperationType != "NA")
                {
                    //    aPIReturnModel = await inventoryService.CRUD_ItemMasterDetails(itemMasterAPIModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);


                        ItemPaymentDetailsParaModel iParaModel = new ItemPaymentDetailsParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            //   ItemId = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                        //    _ItemMasterListMoel  = (await inventoryService.GET_ItemMasterList(itemMasterParaModel)).ToList();

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
            itemPaymentDetailsViewModel = new ItemPaymentDetailsViewModel();

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