using Microsoft.AspNetCore.Components;
using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.Transaction
{
    public class MonthWiseModeBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionSetUpService transactionSetUp { get; set; }
        public SessionModel _sessionModel;
        public List<MonthWiseMode_TransactionDetails_LIST_Output_Model> _transaction_Details_MonthWiseMode_List = new List<MonthWiseMode_TransactionDetails_LIST_Output_Model>();
        public SfGrid<MonthWiseMode_TransactionDetails_LIST_Output_Model> _sfMonthWiseMode;
        public string[] GroupedColumns = new string[] { "monthDetails" };
        public List<string> toolBarItems = (new List<string>() { "ExportExcel", "Print", "Collapse All", "Expand All", "Search", "ColumnChooser" });

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            TransactionDetails_Input_Para_Model transactionDetailsMonthWiseModeList = new TransactionDetails_Input_Para_Model()
            {

                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.MonthWiseMode
            };
            _transaction_Details_MonthWiseMode_List = (await transactionSetUp.GET_Transaction_Details_MonthWiseMode_List(transactionDetailsMonthWiseModeList)).ToList();
        }
         
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {

            if (args.Item.Text == "ExportExcel")
            {
                this._sfMonthWiseMode.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this._sfMonthWiseMode.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this._sfMonthWiseMode.ExpandAllGroupAsync();
            }
        }

    }
}
