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
    public class ClassWiseBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionSetUpService transactionSetUp { get; set; }
        public SessionModel _sessionModel;


        public List<ClassWise_TransactionDetails_LIST_Output_Model> _transaction_Details_ClassWise_List = new List<ClassWise_TransactionDetails_LIST_Output_Model>();
        public SfGrid<ClassWise_TransactionDetails_LIST_Output_Model> _sfClassWise;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            TransactionDetails_Input_Para_Model transactionDetailsClassWiseList = new TransactionDetails_Input_Para_Model()
            {

                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.ClassWise
            };
            _transaction_Details_ClassWise_List = (await transactionSetUp.GET_Transaction_Details_ClassWise_List(transactionDetailsClassWiseList)).ToList();

        }
        public void ExportToExcel_ClassWise()
        {
            this._sfClassWise.ExcelExport();
        }
    }
}
