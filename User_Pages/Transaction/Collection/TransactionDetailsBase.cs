using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace AdminDashboard.Server.User_Pages.Transaction.Collection
{
    public class TransactionDetailsBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionSetUpService transactionSetUp { get; set; }
        public SessionModel _sessionModel;
        public SfGrid<UserWiseFeeCollection> _sfgridUserWiseFeeCollection;
        public SfGrid<PaymentModeFeeCollection> _sfgridPaymentModeFeeCollection;
        public SfGrid<Transaction_List_Output_Model> _sfgridTransaction_List_Output_Model;

        public TodayTransactionStatus TodayTransactionStatus { get; set; }

        //list Model
        public List<UserWiseFeeCollection> _userWiseFeeCollection = new List<UserWiseFeeCollection>();
        public List<PaymentModeFeeCollection> _paymentModeFeeCollection = new List<PaymentModeFeeCollection>();
        public List<Transaction_List_Output_Model> _transaction_List_Output_Model = new List<Transaction_List_Output_Model>();

        public List<string> transactionToolBar = (new List<string>() { "Mode wise transaction","User wise collection", "ExcelExport", "Collapse All", "Expand All", "Print", "Search" });

        public string? _netCollection = "0";
        public string? _netCollectioncurrentMonth = "0";
        public string? _netPendingFee = "0";
        public string? _currentMonthName="0";
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            CultureInfo culture = new CultureInfo("en-GB");
            _currentMonthName=DateTime.Now.ToString("MMMM", culture);

            Transaction_List_Input_Para_Model userWiseFeeCollection_inputmodel = new Transaction_List_Input_Para_Model()
            {
                fromDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                toDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.UserWiseFeeCollection,
                userRoleId = _sessionModel.RoleId,
            };

            _userWiseFeeCollection = (await transactionSetUp.Get_UserWiseFeeCollection(userWiseFeeCollection_inputmodel)).ToList();



            Transaction_List_Input_Para_Model _paymentModeFeeCollection_inputModel = new Transaction_List_Input_Para_Model()
                {
                fromDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                toDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                userId = _sessionModel.UserId,
                    financialYear = _sessionModel.FinancialYear,
                    schoolCode = _sessionModel.SchoolCode,
                    reportType = ReportType.PaymentModeFeeCollection,
                };

                _paymentModeFeeCollection = (await transactionSetUp.Get_PaymentModeFeeCollection(_paymentModeFeeCollection_inputModel)).ToList();

                Transaction_List_Input_Para_Model _Transaction_List_inputModel = new Transaction_List_Input_Para_Model()
                {
                    fromDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                    toDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                    userId = _sessionModel.UserId,
                    financialYear = _sessionModel.FinancialYear,
                    schoolCode = _sessionModel.SchoolCode,
                    reportType = ReportType.DateWiseFeeCollection,
                };

                _transaction_List_Output_Model = (await transactionSetUp.GET_Transaction_List(_Transaction_List_inputModel)).ToList();


            Transaction_List_Input_Para_Model userWiseFeeCollection_ = new Transaction_List_Input_Para_Model()
            {
                fromDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                toDate = DateTime.Today.Date.ToString("yyyy-MM-dd"),
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.UserWiseFeeCollection,
                userRoleId = _sessionModel.RoleId,
            };

            TodayTransactionStatus = await transactionSetUp.GET_TodayTransactionStatus(userWiseFeeCollection_);

               _netCollection = TodayTransactionStatus._todayCollectionAmount.ToString();
         _netCollectioncurrentMonth = TodayTransactionStatus._currentMonthCollection.ToString();
            
            if(TodayTransactionStatus._todayNetPendingAmount==null)
            {
                _netPendingFee = "Generate Pending Fee";
            }
            else 
            { _netPendingFee = TodayTransactionStatus._todayNetPendingAmount.ToString();
            }
          

    }
        public SfDialog DialogRef;
        public bool IsVisibleUserWiseCollection { get; set; } = false;
        public bool IsVisibleModeofTransaction { get; set; } = false;

        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public void TransactionToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Mode wise transaction")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                IsVisibleModeofTransaction=true;
            }
            else if(args.Item.Text == "User wise collection")
            {
                IsVisibleUserWiseCollection=true;
            }
            else if (args.Item.Text == "Collapse All")
            {
                this._sfgridTransaction_List_Output_Model.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this._sfgridTransaction_List_Output_Model.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {


                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Transaction Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "TransactionDetails.xlsx";
                this._sfgridTransaction_List_Output_Model.ExcelExport(ExcelProperties);
            }
        }
    }
}
