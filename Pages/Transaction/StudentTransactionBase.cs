using AdminDashboard.Server.Models.Dashboard.Management;
using AdminDashboard.Server.Models.Transaction;
using AdminDashboard.Server.Service.Interface.Dashboard.Management;
using AdminDashboard.Server.Service.Interface.Transaction;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Transaction
{
    public class StudentTransactionBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionService itransactionService { get; set; }
        public SessionModel _sessionModel;
        public StudentTransactionDetailsModel studentTransactionDetails = new StudentTransactionDetailsModel();
        public List<StudentTransactionDetailsModel> studentTransactionDetailsModels = new List<StudentTransactionDetailsModel>();
        public StudentTransactionHeaderDetailsModel studentTransactionHeaderDetail = new StudentTransactionHeaderDetailsModel();
        public List<StudentTransactionHeaderDetailsModel> studentTransactionHeaderDetailsModels = new List<StudentTransactionHeaderDetailsModel>();
        public List<TransactionTypeDetailsModel> transactionTypeDetailsModel = new List<TransactionTypeDetailsModel>();
        public ClasswiseFeeCollectionListModel classwiseFeeCollectionListModel = new ClasswiseFeeCollectionListModel();
        public List<ClasswiseFeeCollectionListModel> classwiseFeeCollectionListModels = new List<ClasswiseFeeCollectionListModel>();
        public List<ModewiseTransactionDetails>  modewiseTransactions = new List<ModewiseTransactionDetails>();

        public TransactionDetailsModel transactionDetails { get; set; }
        [Inject]
        public IManagementDashboardService managementDashboardService { get; set; }

        public ManagementDashboardModel managementDashboardModel;
        public string[] GroupedColumn = new string[] { "paymentMode" };

        public SfGrid<StudentTransactionDetailsModel> Grid;
        public SfGrid<ClasswiseFeeCollectionListModel> ClassWiseFeeCollection;

        public DateTime MinDate { get; set; } = new DateTime(2022, 01, 05);
        public DateTime MaxDate { get; set; } = new DateTime(2022, 01, 05);
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            LoadData();
        }
        public string[] GroupedColumns = new string[] { "paymentDate", "paymentMode" };
        public string[] PaymentByDate = new string[] { "paymentDate" };
        private  async Task LoadData()
        {
          
            transactionDetails = (await itransactionService.GetStudentTransactionDateRange(_sessionModel.SchoolCode, _sessionModel.FinancialYear, StartValue.ToString(), EndValue.ToString(), "", ReportType.GetBySchoolCode));
            studentTransactionDetailsModels = transactionDetails.studentTransactionDetailsModels;
            studentTransactionHeaderDetailsModels = transactionDetails.studentTransactionHeaderDetailsModels;
            classwiseFeeCollectionListModels = transactionDetails.classwiseFeeCollectionListModels;
            modewiseTransactions= transactionDetails.modewiseTransactions;
            
        }

        public SfGrid<ClasswiseFeeCollectionListModel> classwiseFeeCollectionListModels_grid;
        public SfGrid<ModewiseTransactionDetails> modewiseTransaction_Grid;
        public SfGrid<StudentTransactionDetailsModel> StudentTransactionDetails_grid;


        public void ExportToExcel()
        {
            this.classwiseFeeCollectionListModels_grid.ExcelExport();
        }
        public void ExportToExcel_ModewisePayment()
        {
            this.modewiseTransaction_Grid.ExcelExport();
        }
        public void ExportStudentTransactionDetails_grid()
        {
            this.StudentTransactionDetails_grid.ExcelExport();
        }


        public DateTime? StartValue { get; set; } = DateTime.Now;

        public DateTime? EndValue { get; set; } = DateTime.Now;

        public void onChange(RangePickerEventArgs<DateTime?> args)
        {
            StartValue = args.StartDate;
            EndValue = args.EndDate;
            LoadData();
            StateHasChanged();
        }
    }
}
