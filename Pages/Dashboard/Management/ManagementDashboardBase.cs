using AdminDashboard.Server.Models.Dashboard.Management;
using AdminDashboard.Server.Models.TimeTable;
using AdminDashboard.Server.Models.Transaction;
using AdminDashboard.Server.Service.Interface.Dashboard.Management;
using AdminDashboard.Server.Service.Interface.TimeTable;
using AdminDashboard.Server.Service.Interface.Transaction;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Dashboard.Management
{
    public class ManagementDashboardBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IManagementDashboardService managementDashboardService { get; set; }

        public ManagementDashboardModel managementDashboardModel;

        public List<TransactionTypeDetailsModel> transactionTypeDetailsModel =new List<TransactionTypeDetailsModel>();

        public List<ClassWisePendingFeeModel> classWisePendingFees = new List<ClassWisePendingFeeModel>();

        public List<ClassWiseStudentCountModel>  classWiseStudentCount = new List<ClassWiseStudentCountModel>();

        public List<GenderwiseStudentCountModel>  genderwiseStudentCount = new List<GenderwiseStudentCountModel>();

        //Start-Student Payment Details

        [Inject]
        public ITransactionService itransactionService { get; set; }

        public TransactionDetailsModel transactionDetails { get; set; }

        public DateTime MinDate { get; set; }=new DateTime(2022, 01, 05);
        public DateTime MaxDate { get; set; } = new DateTime(2022, 01, 05);

        public StudentTransactionDetailsModel studentTransactionDetails = new StudentTransactionDetailsModel();
        public List<StudentTransactionDetailsModel> studentTransactionDetailsModels = new List<StudentTransactionDetailsModel>(); 
        public StudentTransactionHeaderDetailsModel studentTransactionHeaderDetail = new StudentTransactionHeaderDetailsModel();
        public List<StudentTransactionHeaderDetailsModel> studentTransactionHeaderDetailsModels = new List<StudentTransactionHeaderDetailsModel>();

        public ClasswiseFeeCollectionListModel classwiseFeeCollectionListModel = new ClasswiseFeeCollectionListModel();
        public List<ClasswiseFeeCollectionListModel> classwiseFeeCollectionListModels = new List<ClasswiseFeeCollectionListModel>();
        //end

        /*****/
        [Inject]
        public ITimeTableService _itimetableService { get; set; }
        public List<TimeTableListModel> timeTableListModels = new List<TimeTableListModel>();
        public SfGrid<TimeTableListModel> Timetable_Grid;
        public string[] GroupedColumns = new string[] { "className" };
        /***/


        public SessionModel _sessionModel;

        public long _netFeeCollection = 0;

        public long _netPendingFee = 0;
        public long _netTotalStudent = 0;
        public long _Male = 0;
        public long _Female = 0;
        public string gendername="";
        public string NetFeeCollection="0";
        public string NetPendingFee = "0";
        public string CardSubTitle = "card contain";
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            managementDashboardModel = (await managementDashboardService.ManagementDashboard(_sessionModel.SchoolCode,_sessionModel.FinancialYear));
            StudentTransactionLoad();
            _netFeeCollection = managementDashboardModel.netFeeCollection;

            System.Globalization.CultureInfo indianDefaultCulture = new System.Globalization.CultureInfo("hi-IN");
            NetFeeCollection = _netFeeCollection.ToString("n", indianDefaultCulture);
           
            _netPendingFee = managementDashboardModel.netPendingFee;         
           
            NetPendingFee = _netPendingFee.ToString("n", indianDefaultCulture);

            _netTotalStudent = managementDashboardModel.netTotalStudent;

            //Start Time Table
            timeTableListModels = (await _itimetableService.GetTimeTableList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();

            //End Time Table

            //Transaction type details

            transactionTypeDetailsModel = managementDashboardModel.transactionTypeDetails;
            //Class Wise Pending Fee
            classWisePendingFees= managementDashboardModel.classWisePendingFees;

            genderwiseStudentCount= managementDashboardModel.genderwiseStudentCounts;
            
            for(int xCount=0;xCount<= genderwiseStudentCount.Count-1;xCount++)
            {
                if (genderwiseStudentCount[xCount].genderName == "Male")
                {
                    _Male = genderwiseStudentCount[xCount].studentCount;
                }
                else
                {
                    _Female= genderwiseStudentCount[xCount].studentCount;
                }
            }
            
            //Class wise count
            classWiseStudentCount = managementDashboardModel.classWiseStudentCounts;
        }

        public string[] GroupedColumn = new string[] { "paymentMode" };
        public DateTime? StartValue { get; set; } = DateTime.Now;

        public DateTime? EndValue { get; set; } = DateTime.Now;
        public async void StudentTransactionLoad()
        {
            //transactionDetails = (await itransactionService.GetStudentTransactionDateRange(_sessionModel.SchoolCode, _sessionModel.FinancialYear, StartValue.ToString(), EndValue.ToString(), "", ReportType.GetBySchoolCode));
            //studentTransactionDetailsModels = transactionDetails.studentTransactionDetailsModels;
            //studentTransactionHeaderDetailsModels = transactionDetails.studentTransactionHeaderDetailsModels;
            //classwiseFeeCollectionListModels = transactionDetails.classwiseFeeCollectionListModels;


        }

       public SfGrid<StudentTransactionDetailsModel> Grid;
        public SfGrid<ClasswiseFeeCollectionListModel> ClassWiseFeeCollection;
        public void ToolbarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "PDF Export")
            {
                this.Grid.PdfExport();
            }
            else if (args.Item.Id == "Grid_excelexport")
            {
                this.Grid.ExcelExport();
            }
            else
            {
                this.Grid.CsvExport();
            }
        }

        public void ExportToExcel()
        {
            this.Grid.ExcelExport();
        }
        public void TimeTable_ExportToExcel()
        {
            this.Timetable_Grid.ExcelExport();
        }
    }
}
