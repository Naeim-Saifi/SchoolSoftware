using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.MasterConfiguration
{
    public class SubjectMasterBase : ComponentBase
    {
        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        public SubjectDetailsModel subjectDetailsModel;//= new StudentDashboardModel();

        public SessionModel _sessionModel;

        //public List<PaymentDetailsListModel> paymentDetails { get; set; }
        public List<MasterSubjectListModel> masterSubjectListModels = new List<MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> mapsubjectwithClassModels = new List<MapsubjectwithClassModel>();
        public string[] GroupedColumns = new string[] { "className" };
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode,_sessionModel.FinancialYear, _sessionModel.UserId, "All"));

            masterSubjectListModels = subjectDetailsModel.masterSubjectListModels;
            mapsubjectwithClassModels = subjectDetailsModel.mapsubjectwithClassModels;


        }
    }
}
