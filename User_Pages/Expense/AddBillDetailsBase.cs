
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
    public class AddBillDetailsBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IExpenseService _expenseService { get; set; }
        public List<VenderMasterListModel> venderMasterList = new List<VenderMasterListModel>();
        //start view model
        public BillItemviewModel  billItemviewModel = new BillItemviewModel();
        public PaymentViewModel _paymentViewModel = new PaymentViewModel();
        //end view model
        public List<BillItemList>  billItemLists = new List<BillItemList>();
        public List<AccountBillDetailsListModel> accountBillDetailsListModels;

        public SfGrid<BillItemList> sfbillItemList;
        public SfGrid<BillDetailsList> sfAccountBillDetail;

        public DialogEffect AnimationEffect = DialogEffect.Zoom;

        public BillDetailsOutPutModel billDetailsOutPutModel = new BillDetailsOutPutModel();

        public List<BillItemDetailsList> billItemDetailsLists = new List<BillItemDetailsList>();
        public List<BillDetailsList>  billDetailsLists = new List<BillDetailsList>();
        public List<BillPaymentDetailsList>  billPaymentDetailsLists = new List<BillPaymentDetailsList>();

        public AccountPaymentAPIModel accountPaymentAPIModel {  get; set; }
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsPaymentVisible { get; set; } = false;
        
        public bool IsDeleteVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";
        public APIReturnModel aPIReturnModel;

        public DateTime? DateValue { get; set; } = DateTime.Now;
        public void onChange(Syncfusion.Blazor.Calendars.ChangedEventArgs<DateTime?> args)
        {
            DateValue = args.Value;
          //  _paymentViewModel.TranDate = Convert.ToDateTime(DateValue);
            StateHasChanged();
        }
        //Payment Model List

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
        //end Payment Model List

        public class Itemtype
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
        public List<Itemtype> itemTypeList = new List<Itemtype>
        {
            new Itemtype(){Id=1,Value="Diesel"},
            new Itemtype(){Id=2,Value="Petrol"},
            new Itemtype(){Id=3,Value="CNG"},
            new Itemtype(){Id=4,Value="Spair Part"},
            new Itemtype(){Id=5,Value="Service"},
            new Itemtype(){Id=6,Value="Item"},
            new Itemtype(){Id=7,Value="Other"},
             new Itemtype(){Id=8,Value="IT Service"},
        };

        public void RowBound(RowDataBoundEventArgs<BillDetailsList> Args)
        {
            if (Args.Data.dueAmount==0)
            {
                Args.Row.AddClass(new string[] { "e-removeEditcommand" });
            }
            else
            {
               // Args.Row.AddClass(new string[] { "e-removeDeletecommand" });
            }
        }
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


            AccountBill_Input_Para_Model accountBill_Input_Para_Model = new AccountBill_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                 AccountID = 0,
                 InvoiceID=0,
                 fromDate= DateTime.Today.Date.ToString("yyyy-MM-dd"),
                 toDate= DateTime.Today.Date.ToString("yyyy-MM-dd"),
                 userRoleId = _sessionModel.RoleId,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All

            };


            billDetailsOutPutModel = (await _expenseService.Get_AccountBillList(accountBill_Input_Para_Model));

            billDetailsLists = billDetailsOutPutModel.billDetailsLists;
            billItemDetailsLists = billDetailsOutPutModel.billItemDetailsLists;
            billPaymentDetailsLists = billDetailsOutPutModel.billPaymentDetailsLists.ToList();


            //accountBillDetailsListModels = new List<AccountBillDetailsListModel>()
            //{ new AccountBillDetailsListModel(){ accountID=0, accountName="JP Petrol Pump",
            //    billAccountId=0, contactName="MD Imran",
            //    invoiceNo="Feb/01/2001", totalBillAmount=101, paidAmount=10,
            //    netDueAmount=10, createdByUser="iman" }
            //};

            StateHasChanged();



        }
        string QuantityType=string.Empty;
        public void OnValueChange(ChangeEventArgs<string, Itemtype> args)
        {
            Console.WriteLine("The DropDownList Value is: ", args.Value);
            QuantityType = args.ItemData.Value;

        }
        int slNo = 0;
        decimal totalAmount = 0;
        decimal discount = 0;
        decimal netAmount = 0;
        decimal totalQTY = 0;
        
        public async Task AddBillItems()
        {
            slNo=slNo+1;
           
          
            billItemLists.Add(new BillItemList() 
            {
                 biilAccountID= billItemviewModel.billAccountID,
                  biilInvoiceID= billItemviewModel.InvoiceID,
                itemSlNo = slNo,
                 purchaseDate = billItemviewModel.PurchaseDate,
                 slipNo = billItemviewModel.SlipNo,
                itemName = billItemviewModel.ItemName ,
                itemId=billItemviewModel.ItemType,
                quantityType= QuantityType,
                Quantity=Convert.ToDecimal(billItemviewModel.Quantity),
                purchaseRate=Convert.ToDecimal(billItemviewModel.PurchaseRate),
                totalAmount=Convert.ToDecimal(Convert.ToDecimal(billItemviewModel.Quantity)* Convert.ToDecimal(billItemviewModel.PurchaseRate))
            });
            totalAmount = totalAmount + (Convert.ToDecimal(Convert.ToDecimal(billItemviewModel.Quantity) * Convert.ToDecimal(billItemviewModel.PurchaseRate)));
            totalQTY = totalQTY + Convert.ToDecimal(billItemviewModel.Quantity);
            
            discount = Convert.ToDecimal(billItemviewModel.Discount);
            billItemviewModel.TotalAmount=totalAmount;

            billItemviewModel.Discount = discount;
            billItemviewModel.NetAmount = (totalAmount -discount);
            billItemviewModel.NetQuantity = totalQTY;

            billItemviewModel.SlipNo=string.Empty;
            billItemviewModel.ItemName = string.Empty;
            billItemviewModel.Quantity = 0;
            billItemviewModel.PurchaseRate = 0;

            sfbillItemList.Refresh();
            StateHasChanged();
            //billItemviewModel.ItemName=string.Empty;
            //billItemviewModel.Quantity = 0;
            //billItemviewModel.PurchaseRate = 0;

        }

        public void ValueChangeHandler(ChangeEventArgs<int?> args)
        {
            // Here you can customize your code
            discount =Convert.ToDecimal(args.ItemData);
            billItemviewModel.NetAmount = (totalAmount - discount);


        }
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

        public void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                CobmineBillDetailsInputModel cobmineBillDetailsInputModel = new CobmineBillDetailsInputModel()
                {
                    accountBillDetailsAPIModel = new AccountBillDetailsAPIModel()
                    {
                        AccountBillDetailsId = billItemviewModel.billAccountID,
                        ContactName = billItemviewModel.ContactName,
                        BillDate =Convert.ToDateTime(billItemviewModel.BiilGenerateDate),
                        TotalQTY = Convert.ToDecimal(billItemviewModel.NetQuantity),
                        TotalAmount = Convert.ToDecimal(billItemviewModel.TotalAmount),
                        NetDiscount = Convert.ToDecimal(billItemviewModel.Discount),
                        NetAmount = Convert.ToDecimal(billItemviewModel.NetAmount),
                        DueAmount = Convert.ToDecimal(billItemviewModel.TotalAmount),
                        BillDescription =billItemviewModel.BillDescription,
                        InvoiceID = billItemviewModel.InvoiceID,
                        CreatedByUserId = _sessionModel.UserId,
                        FinancialYear = _sessionModel.FinancialYear,
                        SchoolCode = _sessionModel.SchoolCode,
                        AccountID= billItemviewModel.billAccountID,
                        PaidAmount=0,
                        UpdatedByUserId = _sessionModel.UserId,
                        OperationType= "Add"
                    }
                };
                cobmineBillDetailsInputModel.billItemDetailsAPIs = new List<BillItemDetailsAPIModel>();

                for(int i = 0; i < billItemLists.Count;i++)
                {
                    cobmineBillDetailsInputModel.billItemDetailsAPIs.Add(new BillItemDetailsAPIModel() 
                    {
                       BillItemDetailsID= billItemLists[i].itemSlNo,
                       AccountID = billItemLists[i].biilAccountID,
                       InvoiceID = billItemLists[i].biilInvoiceID.ToString(),
                       PurchaseDate = Convert.ToDateTime(billItemLists[i].purchaseDate),
                       SlipNo = billItemLists[i].slipNo.ToString(),
                       ItemName = billItemLists[i].itemName.ToString() ,
                       QuantityType = billItemLists[i].quantityType.ToString(),
                       Quantity = billItemLists[i].Quantity,
                       PurchaseRate = billItemLists[i].purchaseRate,
                       TotalAmount= billItemLists[i].totalAmount,
                       CreatedByUserId = _sessionModel.UserId,
                       FinancialYear = _sessionModel.FinancialYear,
                       OperationType = "Add",
                       SchoolCode=_sessionModel.SchoolCode,
                       UpdatedByUserId = _sessionModel.UserId,
                            
                    });

                }
               
                    //(new BillItemDetailsAPIModel()
                    //{ 
                    //    ItemName = "Imran" 
                    //});




                SaveBillDetails(cobmineBillDetailsInputModel);




            }
            else
            {
                // Form has invalid inputs.
            }

        }
        public APIReturnModel aPIReturnmodel;

        public void OnValidPaymentSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            decimal totaldueaomunt = _paymentViewModel.DueAmount;
            decimal netDueAmount = Convert.ToDecimal(_paymentViewModel.DueAmount) - Convert.ToDecimal(_paymentViewModel.PaidAmount);
            if (isValid)
            {
                _previousPaidAmount= _previousPaidAmount+_paymentViewModel.PaidAmount;
                accountPaymentAPIModel = new AccountPaymentAPIModel()
                {
                    AccountID = _paymentViewModel.AccountID,
                    InvoiceNo = _paymentViewModel.InvoiceNo,
                    PaymentMode = _paymentViewModel.PaymentMode,
                    BankName = _paymentViewModel.BankName,
                    ChequeNo = _paymentViewModel.ChequeNo,
                    BaranchName = _paymentViewModel.BaranchName,
                    BillPaymentDetailsID = 0,
                    TotalAmount = totaldueaomunt,
                    PaidAmount = _paymentViewModel.PaidAmount,
                    DueAmount = netDueAmount,
                    TotalPaidAmount = _previousPaidAmount,
                    Description = _paymentViewModel.Description,
                    TranDate = _paymentViewModel.TranDate,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    FinancialYear = _sessionModel.FinancialYear,
                    SchoolCode = _sessionModel.SchoolCode,
                    OperationType = "Add" 
                };
                 
                SavePaymentDetails(accountPaymentAPIModel);


            }
        }

        public async void SavePaymentDetails(AccountPaymentAPIModel  accountPaymentAPIModel)
        {
            aPIReturnmodel = (await _expenseService.CRUD_VendorPaymentDetails(accountPaymentAPIModel));
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
                _paymentViewModel.BankName =   string.Empty;
                _paymentViewModel.BaranchName =   string.Empty;
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

        public async void SaveBillDetails(CobmineBillDetailsInputModel cobmineBillDetailsInputModel)
        {
            aPIReturnmodel = (await _expenseService.CRUD_AccountBillDetails(cobmineBillDetailsInputModel));
            if (aPIReturnmodel.status == false)
            {
                if (cobmineBillDetailsInputModel.accountBillDetailsAPIModel.OperationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (cobmineBillDetailsInputModel.accountBillDetailsAPIModel.OperationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                billItemviewModel.ContactName=string.Empty;
                billItemviewModel.InvoiceID = string.Empty;
                billItemviewModel.ItemName = string.Empty;
                billItemviewModel.Quantity = 0;
                billItemviewModel.PurchaseRate = 0;
                billItemviewModel.BillDescription = string.Empty;
                billItemviewModel.NetQuantity = 0;
                billItemviewModel.TotalAmount= 0;
                billItemviewModel.Discount = 0;
                billItemviewModel.NetAmount = 0;

                billItemviewModel.TotalAmount = 0;

                 totalAmount = 0;
                 discount = 0;
                 netAmount = 0;
                 totalQTY = 0;



                billItemLists.Clear();
                sfbillItemList.Refresh();
                StateHasChanged();
                //GetVendorMasterList();
                //ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }

        public List<string> AccountBilltoolBarItems = (new List<string>() { "Add Bill", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });

        decimal _previousPaidAmount = 0;
        public void BillItemListClick(CommandClickEventArgs<BillItemList> args)
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

             
                //_expenseDetailViewModel = new ExpenseDetailViewModel()
                //{
                //    AccountID = args.RowData.accountID,
                //    BaranchName = args.RowData.baranchName,
                //    ChequeNo = args.RowData.chequeNo,
                //    BankName = args.RowData.bankName,
                //    Description = args.RowData.description,
                //    ExpenseID = args.RowData.expenceID,
                //    InvoiceNo = args.RowData.invoiceNo,
                //    PaidAmount = Convert.ToInt32(args.RowData.paidAmount),
                //    PaymentMode = args.RowData.paymentMode,
                //    // TranDate = args.RowData.tranDate,

                //};


            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Account Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                int slNo = args.RowData.itemSlNo;
                              
                totalQTY = totalQTY - Convert.ToDecimal(args.RowData.Quantity);

                discount = Convert.ToDecimal(billItemviewModel.Discount);

                totalAmount = totalAmount - args.RowData.totalAmount;

                billItemviewModel.TotalAmount = totalAmount;

                billItemviewModel.Discount = discount;
                billItemviewModel.NetAmount = (totalAmount - discount);
                billItemviewModel.NetQuantity = totalQTY;

                billItemLists.RemoveAll(item => item.itemSlNo == slNo);
                sfbillItemList.Refresh();
                StateHasChanged();
                //_expenseDetailViewModel = new ExpenseDetailViewModel()
                //{
                //    AccountID = args.RowData.accountID,
                //    BaranchName = args.RowData.baranchName,
                //    ChequeNo = args.RowData.chequeNo,
                //    BankName = args.RowData.bankName,
                //    Description = args.RowData.description,
                //    ExpenseID = args.RowData.expenceID,
                //    InvoiceNo = args.RowData.invoiceNo,
                //    PaidAmount = Convert.ToInt32(args.RowData.paidAmount),
                //    PaymentMode = args.RowData.paymentMode,

                //};
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void EditAccountBillDetails(CommandClickEventArgs<BillDetailsList> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Pay Amount")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                OperationType = "Update";
                IsPaymentVisible=true;
                DialogHeaderName = "Vendor Payment Details";
                HeaderText = "Save Payment Details";                
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                _previousPaidAmount = args.RowData.paidAmount;
                _paymentViewModel = new PaymentViewModel()
                {
                    AccountID = args.RowData.venderID.ToString(),
                     OrganizationName = args.RowData.organizationName,
                    //BaranchName = args.RowData.baranchName,
                    //ChequeNo = args.RowData.chequeNo,
                    //BankName = args.RowData.bankName,
                    //Description = args.RowData.description,
                    //ExpenseID = args.RowData.expenceID,
                    InvoiceNo = args.RowData.invoiceID,
                    TotalAmount = Convert.ToDecimal(args.RowData.totalAmount),
                     DueAmount= Convert.ToDecimal(args.RowData.dueAmount),
                    //PaymentMode = args.RowData.paymentMode,
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
                //_expenseDetailViewModel = new ExpenseDetailViewModel()
                //{
                //    AccountID = args.RowData.accountID,
                //    BaranchName = args.RowData.baranchName,
                //    ChequeNo = args.RowData.chequeNo,
                //    BankName = args.RowData.bankName,
                //    Description = args.RowData.description,
                //    ExpenseID = args.RowData.expenceID,
                //    InvoiceNo = args.RowData.invoiceNo,
                //    PaidAmount = Convert.ToInt32(args.RowData.paidAmount),
                //    PaymentMode = args.RowData.paymentMode,

                //};
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Bill")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Bill Details";
                OperationType = "Add";
                HeaderText = "Add Bill";
                ddEnable = true;

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
        public void ShowDialog()
        {
            IsVisible = true;
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "850px";

        }
        [Inject]
        public ISnackbar snackBar { get; set; }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public async Task ClosePaymentDialog()
        {
            IsPaymentVisible = false;
            await this.DialogRef.HideAsync();
        }

        

    }
}