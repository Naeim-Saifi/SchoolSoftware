using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.Transaction.PendingFee
{
    public class PendingFeeFlowUpListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionSetUpService transactionSetUp { get; set; }

        [Inject]
        public ITransactionSetUpService transactionSetUpService { get; set; }

        public SfGrid<Pending_Fee_List_Output_Model> _pendingFee;
        public List<PendingFeeFollowUpDetailsListModel>  pendingFeeFollowUpDetailsListModels = new List<PendingFeeFollowUpDetailsListModel>();
        public SessionModel _sessionModel;
        public SfGrid<PendingFeeFollowUpDetailsListModel> _sfgridflowuplist;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            PendingFeeFollowUpDetails_Input_Para_Model pendingFeeFollowUpDetails_Input_Para_Model   = new PendingFeeFollowUpDetails_Input_Para_Model()
            { 
                 studentID = 1,
                 userRoleId = 1,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            pendingFeeFollowUpDetailsListModels = (await transactionSetUpService.GET_PendingFeeFollowUpDetails_List(pendingFeeFollowUpDetails_Input_Para_Model)).ToList();

        }
        public class CallStatus
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }


        public List<CallStatus> callStatusList = new List<CallStatus>()
        {
            new CallStatus() {  ID=1, Text="On Call Discuss"},
            new CallStatus() {  ID=2, Text="Invalid Number"},
            new CallStatus() {  ID=3, Text="Switch Off"},
             new CallStatus() {  ID=4, Text="Call Busy"},
            new CallStatus() {  ID=5, Text="Call Rejected"},
            new CallStatus() {  ID=6, Text="Not Answer"},
             new CallStatus() {  ID=7, Text="Pending For Call"},
            new CallStatus() {  ID=8, Text="Other Issue"},

        };

        public async Task OnChange(ChangeEventArgs<string, CallStatus> args)
        {
            try
            {
                //if (args.ItemData.classId != 0)
                //{
                //    int classid = args.ItemData.classId;

                //    Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
                //    {
                //        classID = classid,
                //        schoolCode = _sessionModel.SchoolCode,
                //        financialYear = _sessionModel.FinancialYear,
                //        reportType = ReportType.ClassId,
                //        userId = _sessionModel.UserId
                //    };

                //    map_classwithsubject_List = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
