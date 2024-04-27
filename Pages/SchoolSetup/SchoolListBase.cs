using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.Academic;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.SchoolSetup;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.SchoolSetup
{
    public class SchoolSetupListBase : ComponentBase
    {
        [Inject]
        public ISchoolSetupService schoolSetupService { get; set; }

        public List<SchoolListModel> schoolListModel = new List<SchoolListModel>();
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel sessionModel { get; }
        

        public SessionModel _sessionModel;
        protected override async Task OnInitializedAsync()
        {

            //schoolListModel = (await schoolSetupService.GetMasterSchoolList(2, 0, 0, ReportType.All)).ToList();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            schoolListModel = (await schoolSetupService.GetMasterSchoolList(2, 0, 0, _sessionModel.SchoolCode, ReportType.All)).ToList();

        }
        

        public string _searchTerm = "";
        public IEnumerable<SchoolListModel> SchoolSetupList() => schoolListModel.Where(e => e.schoolName.Contains(_searchTerm));

        public SchoolListModel selectedItem = new SchoolListModel();

        //for sorting
        public bool enabled = true;
        int MasterId = 0;
        public string ActionName = "Save";
        [Inject]
        public ISnackbar snackBar { get; set; }

        public bool success;

        public void Edit(int schoolId)
        { }

        public void DeleteRecord(int schoolId)
        {

        }
    }
}
