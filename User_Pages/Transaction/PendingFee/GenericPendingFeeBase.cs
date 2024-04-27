using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.Transaction.PendingFee
{
    public class GenericPendingFeeBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionSetUpService _transactionSetUpService { get; set; }

        public SessionModel _sessionModel;

        public List<MonthWise_PendingFee_LIST_Output_Model> _monthWise_PendingFee_List = new List<MonthWise_PendingFee_LIST_Output_Model>();
        public SfGrid<MonthWise_PendingFee_LIST_Output_Model> _sfMonthWise;

        public List<BusStopWise_PendingFee_LIST_Output_Model> _busStopWise_PendingFee_List = new List<BusStopWise_PendingFee_LIST_Output_Model>();
        public SfGrid<BusStopWise_PendingFee_LIST_Output_Model> _sfbusStopWise;

        public List<ClassWise_PendingFee_LIST_Output_Model> _classWise_PendingFee_List = new List<ClassWise_PendingFee_LIST_Output_Model>();
        public SfGrid<ClassWise_PendingFee_LIST_Output_Model> _sfclassWise;
        public string[] GroupedColumns = new string[] { "className" };
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            Generic_PendingFee_Input_Para_Model pendingFeeMonthWiseList = new Generic_PendingFee_Input_Para_Model()
            {

                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.MonthWise
            };
            _monthWise_PendingFee_List = (await _transactionSetUpService.GET_PendingFee_MonthWise_List(pendingFeeMonthWiseList)).ToList();

            Generic_PendingFee_Input_Para_Model pendingFeeBusStopWiseList = new Generic_PendingFee_Input_Para_Model()
            {

                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.BusStopWise
            };
            _busStopWise_PendingFee_List = (await _transactionSetUpService.GET_PendingFee_BustStopWise_List(pendingFeeBusStopWiseList)).ToList();

            Generic_PendingFee_Input_Para_Model pendingFeeClassWiseList = new Generic_PendingFee_Input_Para_Model()
            {

                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.ClassWise
            };
            _classWise_PendingFee_List = (await _transactionSetUpService.GET_PendingFee_ClassWise_List(pendingFeeClassWiseList)).ToList();
        }


        public void ExportToExcel_MonthWise()
        {
            this._sfMonthWise.ExcelExport();
        }
        public void ExportToExcel_BusStopWise()
        {
            this._sfbusStopWise.ExcelExport();
        }
        public void ExportToExcel_lassWise()
        {
            this._sfclassWise.ExcelExport();
        }
    }
}
