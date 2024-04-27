using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.Transaction
{
    public class StudentTransactionDetailsBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionSetUpService transactionSetUp { get; set; }
        public SessionModel _sessionModel;
        public SfGrid<Transaction_List_Output_Model> _sfgridTransaction;
        //list Model
        public List<Transaction_List_Output_Model> _transaction_List = new List<Transaction_List_Output_Model>();
        public List<Transaction_Header_List_Output_Model> _transaction_Header_List = new List<Transaction_Header_List_Output_Model>();
        public List<string> toolBarItems = (new List<string>() {  "Print", "ExportExcel",   "Search" });
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            Transaction_List_Input_Para_Model transaction_List_Input_Para_Model = new Transaction_List_Input_Para_Model()
            {
                fromDate = DateTime.Today.ToString(),
                toDate = DateTime.Today.ToString(),
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.GetByStudentUserID
            };

            _transaction_List = (await transactionSetUp.GET_Transaction_List(transaction_List_Input_Para_Model)).ToList();

            Transaction_Header_List_Input_Para_Model transactionHeaderList = new Transaction_Header_List_Input_Para_Model()
            {
                fromDate = DateTime.Today.ToString(),
                toDate = DateTime.Today.ToString(),
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.GetByStudentUserID
            };

            _transaction_Header_List = (await transactionSetUp.GET_TransactionHeader_List(transactionHeaderList)).ToList();
        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
             if (args.Item.Text == "ExportExcel")
            {
                this._sfgridTransaction.ExportToExcelAsync();
            }
            //else if (args.Item.Text == "Collapse All")
            //{
            //    this._sfgridTransaction.CollapseAllGroupAsync();
            //}
            //else if (args.Item.Text == "Expand All")
            //{
            //    this._sfgridTransaction.ExpandAllGroupAsync();
            //}
        }

    }
}
