using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.MasterConfiguration
{
    public class ClassMasterBase:ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        public SessionModel sessionModel { get; }
        [Inject]
        public IClassMasterService ClassMasterService { get; set; }

        public List<ClassMasterListModel> classMasterListModel = new List<ClassMasterListModel>();
        public ClassMasterModel classMasterModel { get; set; }

        public SessionModel _sessionModel;
        protected override async Task OnInitializedAsync()
        {
            classMasterModel = new ClassMasterModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            await LoadClassData();

        }
        private async Task LoadClassData()
        {

            classMasterListModel = (await ClassMasterService.GetClassMasterList(_sessionModel.SchoolCode,_sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 0, ReportType.All)).ToList();
        }
    }
}
