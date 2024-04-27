using AdminDashboard.Server.Models.Transaction;
using AdminDashboard.Server.Service.Interface.Transaction;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIS.Model.UserLogin;
using AIS.Model.GeneralConversion;
using Syncfusion.Blazor.Grids;
using AdminDashboard.Server.Models.PendingFee;
using AdminDashboard.Server.Service.Interface.PendingFee;

namespace AdminDashboard.Server.Pages.Transaction
{
    public class StudentPendingFeeListBase: ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
         

        [Inject]
        public IPendingFeeService pendingFeeService { get; set; }
        public StudentPendingFeeModel studentPendingFeeModel { get; set; } 
       
        public List<AllStudentPendingFee> allStudentPendingFees = new List<AllStudentPendingFee>();

        public List<MonthWisePendingList> monthWisePendingLists = new List<MonthWisePendingList>();
        public List<ClassAndSectionPendingList> classAndSectionPendingLists = new List<ClassAndSectionPendingList>();

       

        public SessionModel _sessionModel;

        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            studentPendingFeeModel = (await pendingFeeService.GetStudentPendingFee(_sessionModel.SchoolCode, _sessionModel.FinancialYear,  0, ReportType.All));
            //studentTransactionDetailsModels = transactionDetails.studentTransactionDetailsModels;
            allStudentPendingFees = studentPendingFeeModel.allStudentPendingFeesModels;
            monthWisePendingLists = studentPendingFeeModel.monthWisePendingsModels;
            classAndSectionPendingLists = studentPendingFeeModel.classAndSectionPendingListsModels;

        }
        
        public string[] GroupedColumns = new string[] { "classSection" };
        public string[] GroupByClass = new string[] { "className" };
        public SfGrid<AllStudentPendingFee> Grid;
        public SfGrid<MonthWisePendingList> monthwisependingGrid;
        public SfGrid<ClassAndSectionPendingList> classandSectionGrid;

        public void ExportToExcel()
        {
            this.Grid.ExcelExport();
        }
    }
}
