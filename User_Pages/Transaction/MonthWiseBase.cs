using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIS.Model.GeneralConversion;
using MudBlazor;

namespace AdminDashboard.Server.User_Pages.Transaction
{
    public class MonthWiseBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionSetUpService transactionSetUp { get; set; }
        public SessionModel _sessionModel;
        public List<MonthWise_TransactionDetails_LIST_Output_Model> _transaction_Details_List = new List<MonthWise_TransactionDetails_LIST_Output_Model>();
        public SfGrid<MonthWise_TransactionDetails_LIST_Output_Model> _sfMonthWise;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            TransactionDetails_Input_Para_Model transactionDetailsMonthWiseList = new TransactionDetails_Input_Para_Model()
            {

                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.MonthWise
            };
            _transaction_Details_List = (await transactionSetUp.GET_Transaction_Details_MonthWise_List(transactionDetailsMonthWiseList)).ToList();
            
        }
        public void ExportToExcel_MonthWise()
        {
            this._sfMonthWise.ExcelExport();
        }
    }
}
