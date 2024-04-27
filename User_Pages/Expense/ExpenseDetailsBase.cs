using AdminDashboard.Server.API_Service.Interface.Expense;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Expense;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Bunit.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Microsoft.OData.Edm;
using MudBlazor;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Kanban.Internal;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AdminDashboard.Server.User_Pages.Expense
{
    public class ExpenseDetailsBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        public ExpenseDetailViewModel _expenseDetailViewModel = new ExpenseDetailViewModel();
        public ExpenseDetailAPIModel _expenseDetailAPIModel = new ExpenseDetailAPIModel();
        [Inject]
        public IExpenseService _expenseService { get; set; }
        public List<ExpenseDetailListModel> expenseList = new List<ExpenseDetailListModel>();
        public List<VenderMasterListModel> venderMasterList = new List<VenderMasterListModel>();
        public List<ExpenseDetailListModel> accountExpenseList = new List<ExpenseDetailListModel>();
        public SfGrid<ExpenseDetailListModel> sfExpenseDetails;

        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Expense", "Expence and Collection", "ExportExcel", "Collapse All", "Expand All", "Search", "ColumnChooser" });
        public string[] GroupedColumns = new string[] { "tranDate"};

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            Vendor_Input_Para_Model vendor_Input_Para_Model = new Vendor_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                vendorId = 0,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };

            venderMasterList = (await _expenseService.Get_VendorDetails_List(vendor_Input_Para_Model)).ToList();
            GetExpenseList();
        }

        private async void GetExpenseList()
        {
            
            Expense_Input_Para_Model expense_Input_Para_Model = new Expense_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                ExpenseId = 0,
                paymentAccountID = 0,
                fromDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                toDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            expenseList = (await _expenseService.Get_ExpenseDetails_List(expense_Input_Para_Model)).ToList();
            StateHasChanged();
        }
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
        public bool ddEnable = true;
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string btncss = "";
        public string HeaderText = string.Empty;
        public void EditExpenseDetail(CommandClickEventArgs<ExpenseDetailListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Vendor Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                _expenseDetailViewModel = new ExpenseDetailViewModel()
                {
                       AccountID = args.RowData.accountID,
                     BaranchName = args.RowData.baranchName,
                     ChequeNo = args.RowData.chequeNo,
                     BankName = args.RowData.bankName,
                     Description = args.RowData.description,
                     ExpenseID = args.RowData.expenceID,
                     InvoiceNo = args.RowData.invoiceNo,
                     PaidAmount =Convert.ToInt32(args.RowData.paidAmount),
                     PaymentMode = args.RowData.paymentMode,
                    // TranDate = args.RowData.tranDate,

                };


            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Account Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                _expenseDetailViewModel = new ExpenseDetailViewModel()
                {
                    AccountID = args.RowData.accountID,
                    BaranchName = args.RowData.baranchName,
                    ChequeNo = args.RowData.chequeNo,
                    BankName = args.RowData.bankName,
                    Description = args.RowData.description,
                    ExpenseID = args.RowData.expenceID,
                    InvoiceNo = args.RowData.invoiceNo,
                    PaidAmount = Convert.ToInt32(args.RowData.paidAmount),
                    PaymentMode = args.RowData.paymentMode,

                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Expense")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Expense Details";
                OperationType = "Add";
                HeaderText = "Add Expense";
                ddEnable = true;

            }
            else if (args.Item.Text == "Expence and Collection")
            {
              await  TodayTransactionAndExpencReport();
            }

            
            else if (args.Item.Text == "ExportExcel")
            {
               await this.sfExpenseDetails.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
               await this.sfExpenseDetails.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
               await this.sfExpenseDetails.ExpandAllGroupAsync();
            }
        }
        [Inject]
        public ISnackbar snackBar { get; set; }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                _expenseDetailAPIModel = new ExpenseDetailAPIModel()
                {
                    AccountID    = Convert.ToInt32(_expenseDetailViewModel.AccountID),
                    BaranchName = _expenseDetailViewModel.BaranchName,
                    ChequeNo = _expenseDetailViewModel.ChequeNo,
                    BankName = _expenseDetailViewModel.BankName,
                    Description = _expenseDetailViewModel.Description,
                    ExpenseID = _expenseDetailViewModel.ExpenseID,
                    InvoiceNo = _expenseDetailViewModel.InvoiceNo.Trim(),
                    PaidAmount = Convert.ToInt32(_expenseDetailViewModel.PaidAmount),
                    PaymentMode = _expenseDetailViewModel.PaymentMode.Trim(),
                    TranDate = Convert.ToDateTime(DateValue),//_expenseDetailViewModel.TranDate,
                   
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    _expenseDetailAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    _expenseDetailAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    _expenseDetailAPIModel.OperationType = OperationAction.Delete;
                }
                SaveExpenseDetails(_expenseDetailAPIModel);
            };
        }
        public APIReturnModel aPIReturnmodel;

        public async void SaveExpenseDetails(ExpenseDetailAPIModel _expenseDetailAPIModel)
        {
            aPIReturnmodel = (await _expenseService.CRUD_ExpenseDetails(_expenseDetailAPIModel));
            if (aPIReturnmodel.status == false)
            {
                if (_expenseDetailAPIModel.OperationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                    ClearAllFields();
                    Expense_Input_Para_Model expense_Input_Para_Model = new Expense_Input_Para_Model()
                    {
                        financialYear = _sessionModel.FinancialYear,
                        paymentAccountID = paymentAccountID,
                        ExpenseId = 0,
                        fromDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                        toDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                        schoolCode = _sessionModel.SchoolCode,
                        reportType = ExpenseReportType.PaymentAccountType
                    };

                    accountExpenseList = (await _expenseService.Get_ExpenseDetails_List(expense_Input_Para_Model)).ToList();
                    GetExpenseList();
                    StateHasChanged();
                }
                else if (_expenseDetailAPIModel.OperationType == OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                    HeaderText = "Save Record";
                    ClearAllFields();
                    Expense_Input_Para_Model expense_Input_Para_Model = new Expense_Input_Para_Model()
                    {
                        financialYear = _sessionModel.FinancialYear,
                        paymentAccountID = paymentAccountID,
                        ExpenseId = 0,
                        fromDate = null,
                        toDate = null,
                        schoolCode = _sessionModel.SchoolCode,
                        reportType = ExpenseReportType.PaymentAccountType
                    };

                    accountExpenseList = (await _expenseService.Get_ExpenseDetails_List(expense_Input_Para_Model)).ToList();
                    GetExpenseList();
                    StateHasChanged();
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                    HeaderText = "Update Record";
                    ClearAllFields();
                    Expense_Input_Para_Model expense_Input_Para_Model = new Expense_Input_Para_Model()
                    {
                        financialYear = _sessionModel.FinancialYear,
                        paymentAccountID = paymentAccountID,
                        ExpenseId = 0,
                        fromDate = null,
                        toDate = null,
                        schoolCode = _sessionModel.SchoolCode,
                        reportType = ExpenseReportType.PaymentAccountType
                    };

                    accountExpenseList = (await _expenseService.Get_ExpenseDetails_List(expense_Input_Para_Model)).ToList();
                    GetExpenseList();
                    StateHasChanged();
                }

               
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {

             
            _expenseDetailViewModel.InvoiceNo=string.Empty;
            _expenseDetailViewModel.PaymentMode = string.Empty;
            _expenseDetailViewModel.BaranchName = string.Empty;          
            _expenseDetailViewModel.ChequeNo = string.Empty;
            _expenseDetailViewModel.TranDate = DateTime.Now;
            _expenseDetailViewModel.Description = string.Empty;
            _expenseDetailViewModel.BankName = string.Empty;
            _expenseDetailViewModel.PaidAmount = 0;

        }

        public class PaymentMode
        {
            public int PaymentID{ get; set; }
            public string PaymentType { get; set; }
        }
        

        public List<PaymentMode> PaymentModes =new List<PaymentMode> 
        {
        new PaymentMode() { PaymentID=1, PaymentType= "Cash" } ,
        new PaymentMode() { PaymentID=2, PaymentType= "Cheque" } ,       
        new PaymentMode() { PaymentID=3, PaymentType= "Phone Pe" } ,
        new PaymentMode() { PaymentID=4, PaymentType= "Google Pay" },
        new PaymentMode() { PaymentID=5, PaymentType= "IMPS Online" } ,
        new PaymentMode() { PaymentID=6, PaymentType= "CREDIT CARD" } ,
        new PaymentMode() { PaymentID=7, PaymentType= "DEBIT CARD_ATM" } ,
        new PaymentMode() { PaymentID=8, PaymentType= "PayTm" } };

       public SfDropDownList<VenderMasterListModel, VenderMasterListModel> ddlObj;
        public Syncfusion.Blazor.Data.Query query { get; set; }

        public async Task OnFiltering(FilteringEventArgs args)
        {
            args.PreventDefaultAction = true;
            var orWhere = WhereFilter.Or(new List<WhereFilter> {
            new WhereFilter() { Field = "Text", Operator = "contains", value = args.Text, IgnoreCase = true },
            new WhereFilter() { Field = "ID", Operator = "contains", value = args.Text, IgnoreCase = true }
        });
            var query = new Syncfusion.Blazor.Data.Query().Where(orWhere);
            query = !string.IsNullOrEmpty(args.Text) ? query : new Syncfusion.Blazor.Data.Query();
            await (  ddlObj.Filter(venderMasterList, query));
        }
        int paymentAccountID = 0;
        public async Task OnChangeAccount(ChangeEventArgs<string, VenderMasterListModel> args)
        {
           
            try
            {
                if (args.ItemData.accountID != 0)
                {
                    paymentAccountID = args.ItemData.accountID;

                    Expense_Input_Para_Model expense_Input_Para_Model = new Expense_Input_Para_Model()
                    {
                        financialYear = _sessionModel.FinancialYear,
                        paymentAccountID = paymentAccountID,
                         ExpenseId = 0,
                        fromDate = null,
                        toDate = null,
                        schoolCode = _sessionModel.SchoolCode,
                        reportType = ExpenseReportType.PaymentAccountType
                    };

                    accountExpenseList = (await _expenseService.Get_ExpenseDetails_List(expense_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public DateTime? DateValue { get; set; } = DateTime.Now;
        public void onChange(Syncfusion.Blazor.Calendars.ChangedEventArgs<DateTime?> args)
        {
            DateValue = args.Value;
            _expenseDetailViewModel.TranDate = Convert.ToDateTime(DateValue);
            StateHasChanged();
        }


        [Inject]
        public IJSRuntime jsRuntime { get; set; }
        [Inject]
        private IConfiguration _config { get; set; }
        public async Task TodayTransactionAndExpencReport()
        {
            try
            {
                string baseURL = _config.GetValue<string>("Report:ReportUrl");
                string fullUrl = "";
                fullUrl = baseURL + ($"/api/TransactionSetUp/TodayCollectionAndExpenceReport?SchoolCode={_sessionModel.SchoolCode}" +
                        $"&FinancialYear={_sessionModel.FinancialYear}" +
                        $"&FromDate={DateTime.Today.Date.ToString("yyyy-MM-dd")}" +
                        $"&ToDate={DateTime.Today.Date.ToString("yyyy-MM-dd")}" +
                        
                       $"&UserID={_sessionModel.UserId}" +
                       $"&ReportType={"StudentWiseMarksDetails"}");
                await jsRuntime.InvokeAsync<object>("open", fullUrl, "_blank");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());

            }

        }
    }
}