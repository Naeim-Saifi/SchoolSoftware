using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.Transaction
{
    public class PaymentModeBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionSetUpService transactionSetUp { get; set; }
        public SessionModel _sessionModel;


        public List<PaymentMode_TransactionDetails_LIST_Output_Model> _transaction_Details_PaymentMode_List = new List<PaymentMode_TransactionDetails_LIST_Output_Model>();
        public SfGrid<PaymentMode_TransactionDetails_LIST_Output_Model> _sfPaymentMode;
        public List<string> toolBarItems = (new List<string>() {  "ExportExcel", "Print", "Collapse All", "Expand All", "Search", "ColumnChooser" });
        public string[] GroupedColumns = new string[] { "className", "sectionName", "paymentMode" };
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");


            TransactionDetails_Input_Para_Model transactionDetailsPaymentModeList = new TransactionDetails_Input_Para_Model()
            {

                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.paymentMode
            };
            _transaction_Details_PaymentMode_List = (await transactionSetUp.GET_Transaction_Details_PaymentMode_List(transactionDetailsPaymentModeList)).ToList();

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
             
              if (args.Item.Text == "ExportExcel")
            {
                this._sfPaymentMode.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this._sfPaymentMode.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this._sfPaymentMode.ExpandAllGroupAsync();
            }
        }

         

    }
}
