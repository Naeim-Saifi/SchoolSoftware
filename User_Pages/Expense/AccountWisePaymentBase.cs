using AdminDashboard.Server.API_Service.Interface.Expense;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.Pages.Personal;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.Expense;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch.Operations;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.Expense
{
    public class AccountWisePaymentBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IExpenseService _expenseService { get; set; }

        public List<AccountBillDetailsList> cccountBillDetailsList = new List<AccountBillDetailsList>();
        public SfGrid<AccountBillDetailsList> sfAccountBillDetail;

        public BillDetailsOutPutModel billDetailsOutPutModel = new BillDetailsOutPutModel();

        public List<BillItemDetailsList> billItemDetailsLists = new List<BillItemDetailsList>();
        public List<BillDetailsList> billDetailsLists = new List<BillDetailsList>();
        public List<BillPaymentDetailsList> billPaymentDetailsLists = new List<BillPaymentDetailsList>();

        public AccountWisePaymentAPIModel accountWisePaymentAPIModel { get; set; }

        public PaymentViewModel _paymentViewModel = new PaymentViewModel();

        protected override async Task OnInitializedAsync()
        {
            try
            {

                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
                AccountBill_Input_Para_Model accountBill_Input_Para_Model = new AccountBill_Input_Para_Model()
                {
                    financialYear = _sessionModel.FinancialYear,
                    AccountID = 0,
                    InvoiceID = 0,
                    fromDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                    toDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                    userRoleId = _sessionModel.RoleId,
                    schoolCode = _sessionModel.SchoolCode,
                    reportType = ReportType.All

                };


                cccountBillDetailsList = (await _expenseService.Get_AccountWiseBillList(accountBill_Input_Para_Model)).ToList();



                AccountBill_Input_Para_Model accountBill_Input_Para_Model1 = new AccountBill_Input_Para_Model()
                {
                    financialYear = _sessionModel.FinancialYear,
                    AccountID = 0,
                    InvoiceID = 0,
                    fromDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                    toDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                    userRoleId = _sessionModel.RoleId,
                    schoolCode = _sessionModel.SchoolCode,
                    reportType = ReportType.All

                }; 
                billDetailsOutPutModel = (await _expenseService.Get_AccountBillList(accountBill_Input_Para_Model1));

                billDetailsLists = billDetailsOutPutModel.billDetailsLists;
                billItemDetailsLists = billDetailsOutPutModel.billItemDetailsLists;
                billPaymentDetailsLists = billDetailsOutPutModel.billPaymentDetailsLists.ToList(); 
                StateHasChanged();
            }
           catch (Exception ex) {Console.WriteLine(ex.Message); }



        }
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> AccountBilltoolBarItems = (new List<string>() { "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });

        public void EditAccountBillDetails(CommandClickEventArgs<AccountBillDetailsList> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Pay Amount")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //OperationType = "Update";
                IsPaymentVisible = true;
                DialogHeaderName = "Vendor Payment Details";
                HeaderText = "Save Payment Details";
                btncss = "e-flat e-info e-outline";
                //ddEnable = false;
                //_previousPaidAmount = args.RowData.paidAmount;
                _paymentViewModel = new PaymentViewModel()
                {
                    AccountID = args.RowData.accountID.ToString(),
                    OrganizationName = args.RowData.organizationName,
                    //BaranchName = args.RowData.baranchName,
                    //ChequeNo = args.RowData.chequeNo,
                    //BankName = args.RowData.bankName,
                    //Description = args.RowData.description,
                    //ExpenseID = args.RowData.expenceID,
                    InvoiceNo = args.RowData.accountID,
                    TotalAmount = Convert.ToDecimal(args.RowData.totalAmount),
                    DueAmount = Convert.ToDecimal(args.RowData.dueAmount),
                    //PaymentMode = args.RowData.paymentMode,
                    // TranDate = args.RowData.tranDate,

                };


                //}
                //else
                //{
                //    IsVisible = true;
                //    DialogHeaderName = "Delete Account Details";
                //    OperationType = "Delete";
                //    HeaderText = "Delete Record";
                //    btncss = "e-flat e-danger e-outline";
                //    ddEnable = false;
                //    //_expenseDetailViewModel = new ExpenseDetailViewModel()
                //    //{
                //    //    AccountID = args.RowData.accountID,
                //    //    BaranchName = args.RowData.baranchName,
                //    //    ChequeNo = args.RowData.chequeNo,
                //    //    BankName = args.RowData.bankName,
                //    //    Description = args.RowData.description,
                //    //    ExpenseID = args.RowData.expenceID,
                //    //    InvoiceNo = args.RowData.invoiceNo,
                //    //    PaidAmount = Convert.ToInt32(args.RowData.paidAmount),
                //    //    PaymentMode = args.RowData.paymentMode,

                //    //};
                //}
                //else
                //{
                //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            }

        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Bill")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                //IsVisible = true;
                //OperationType = "";
                //btncss = "e-flat e-primary e-outline";
                //DialogHeaderName = "Add Bill Details";
                //OperationType = "Add";
                //HeaderText = "Add Bill";
                //ddEnable = true;

            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfAccountBillDetail.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfAccountBillDetail.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfAccountBillDetail.ExpandAllGroupAsync();
            }
        }

        public bool IsPaymentVisible { get; set; } = false;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public string DialogHeaderName = string.Empty;
        public string HeaderText = string.Empty;
        public string btncss = "";
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "850px";

        }

        public void OnValidPaymentSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            decimal totaldueaomunt = _paymentViewModel.DueAmount;
            decimal netDueAmount = Convert.ToDecimal(_paymentViewModel.DueAmount) - Convert.ToDecimal(_paymentViewModel.PaidAmount);
            if (isValid)
            {
                // _previousPaidAmount = _previousPaidAmount + _paymentViewModel.PaidAmount;
                accountWisePaymentAPIModel = new AccountWisePaymentAPIModel()
                {
                    AccountID = _paymentViewModel.AccountID,
                    //InvoiceNo = _paymentViewModel.InvoiceNo,
                    PaymentMode = _paymentViewModel.PaymentMode,
                    BankName = _paymentViewModel.BankName,
                    ChequeNo = _paymentViewModel.ChequeNo,
                    BaranchName = _paymentViewModel.BaranchName,
                    BillPaymentDetailsID = 0,
                    TotalAmount = totaldueaomunt,
                    PaidAmount = _paymentViewModel.PaidAmount,
                    DueAmount = netDueAmount,
                    //TotalPaidAmount = _previousPaidAmount,
                    Description = _paymentViewModel.Description,
                    TranDate = _paymentViewModel.TranDate,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    FinancialYear = _sessionModel.FinancialYear,
                    SchoolCode = _sessionModel.SchoolCode,
                    OperationType = "Add"
                };

                SavePaymentDetails(accountWisePaymentAPIModel);


            }
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }
        public async void SavePaymentDetails(AccountWisePaymentAPIModel accountPaymentAPIModel)
        {
            aPIReturnmodel = (await _expenseService.CRUD_AccountWisePaymentDetails(accountPaymentAPIModel));
            if (aPIReturnmodel.status == false)
            {
                if (accountPaymentAPIModel.OperationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (accountPaymentAPIModel.OperationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                _paymentViewModel.OrganizationName = string.Empty;
                _paymentViewModel.InvoiceNo = string.Empty;
                _paymentViewModel.PaymentMode = string.Empty;
                _paymentViewModel.BankName = string.Empty;
                _paymentViewModel.BaranchName = string.Empty;
                _paymentViewModel.ChequeNo = string.Empty;
                _paymentViewModel.BaranchName = string.Empty;
                _paymentViewModel.TotalAmount = 0;
                _paymentViewModel.PaidAmount = 0;
                _paymentViewModel.DueAmount = 0;
                StateHasChanged();

            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        public class PaymentMode
        {
            public int PaymentID { get; set; }
            public string PaymentType { get; set; }
        }
        
        public List<PaymentMode> PaymentModes = new List<PaymentMode>
        {
        new PaymentMode() { PaymentID=1, PaymentType= "Cash" } ,
        new PaymentMode() { PaymentID=2, PaymentType= "Cheque" } ,
        new PaymentMode() { PaymentID=3, PaymentType= "Phone Pe" } ,
        new PaymentMode() { PaymentID=4, PaymentType= "Google Pay" },
        new PaymentMode() { PaymentID=5, PaymentType= "IMPS Online" } ,
        new PaymentMode() { PaymentID=6, PaymentType= "CREDIT CARD" } ,
        new PaymentMode() { PaymentID=7, PaymentType= "DEBIT CARD_ATM" } ,
        new PaymentMode() { PaymentID=8, PaymentType= "PayTm" } };

        public async Task ClosePaymentDialog()
        {
            IsPaymentVisible = false;
            await this.DialogRef.HideAsync();
        }

    }
}
