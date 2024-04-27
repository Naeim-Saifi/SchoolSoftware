using AdminDashboard.Server.Models.Dashboard.Student;
using AdminDashboard.Server.Models.Transaction;
using AdminDashboard.Server.Service.Interface.Dashboard.Student;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.StudentModule.Transaction
{
    public class StudentTransactionDetailsBase : ComponentBase
    {
        [Inject]
        public IStudentDashboardService studentDashboardService { get; set; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        public StudentDashboardModel studentDashboardModels;//= new StudentDashboardModel();

        public SessionModel _sessionModel;

        //public List<PaymentDetailsListModel> paymentDetails { get; set; }
        public List<PaymentDetailsListModel> paymentDetails = new List<PaymentDetailsListModel>();
        public List<StudentTransactionHeaderDetailsModel> studentTransactionHeaderDetailsModels = new List<StudentTransactionHeaderDetailsModel>();

        public string pendingMonthName = string.Empty;
        public int pendingFeeAmount = 0;
        public string[] GroupedColumn = new string[] { "paymentMode" };
        public SfGrid<PaymentDetailsListModel> Grid;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            studentDashboardModels = (await studentDashboardService.StudentDashboard(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));

            paymentDetails = studentDashboardModels.paymentDetailsListModel;
            studentTransactionHeaderDetailsModels = studentDashboardModels.studentTransactionHeaderDetailsModels;

            pendingMonthName = studentDashboardModels.pendingFee[0].monthName;
            pendingFeeAmount = Convert.ToInt32(studentDashboardModels.pendingFee[0].totalDue);
           
        //paymentDetails = studentDashboardModels.paymentDetailsListModel;
        //    studentTransactionHeaderDetailsModels = studentDashboardModels.studentTransactionHeaderDetailsModels;
        }
    }
}
