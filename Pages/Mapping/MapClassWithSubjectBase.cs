using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.Academic;
using AIS.Model.GeneralConversion;
using AIS.Model.Mapping;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Mapping
{
    public class MapClassWithSubjectBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
       
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        [Inject]
        public IMasterSubjectService masterSubjectService { get; set; }
        [Inject]
        public IMapClassWithSubjectService mapClassWithSubjectService { get; set; }


        public List<MasterClassListModel> masterClassListModel = new List<MasterClassListModel>();
        public List<MasterSubjectListModel> masterSubjectListModel = new List<MasterSubjectListModel>();

        public List<MapClassWithSubjectList> mapClassWithSubjectList = new List<MapClassWithSubjectList>();

        public MapClassWithSubjectList selectedItem = new MapClassWithSubjectList();
        public SessionModel _sessionModel;

        public string _searchTerm = "";
        public bool enabled = true;
        public IEnumerable<MapClassWithSubjectList> ClassWithSubjectList() => mapClassWithSubjectList.Where(e => e.masterClassName.Contains(_searchTerm)| e.masterSubjectName.Contains(_searchTerm));

        public MapClassWithSubjectModel mapClassWithSubjectModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                mapClassWithSubjectModel = new MapClassWithSubjectModel();
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
               
                masterClassListModel = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
                masterSubjectListModel = (await masterSubjectService.GetMasterSubjectList(_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
                await LoadSubjectwithClassData();
            }
            catch (Exception ex)
            {
                //message
            }

        }

        private async Task LoadSubjectwithClassData()
        {

            mapClassWithSubjectList = (await mapClassWithSubjectService.GetMapClassWithSubjectList( 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public void OnValidSubmit()
        {


        }
        private bool multiselectionTextChoice;
        public HashSet<string> batchoptions { get; set; } = new HashSet<string>() { "Alaska" };
        public string classvalue { get; set; } = "Nothing selected";
        public string subjectvalue { get; set; } = "Nothing selected";
        public string GetMultiClassText(List<string> selectedValues)
        {
            if (multiselectionTextChoice)
            {
                return $"Selected state{(selectedValues.Count > 1 ? "s" : "")}: {string.Join(", ", selectedValues.Select(x => x))}";
            }
            else
            {
                return $"{selectedValues.Count} Class{(selectedValues.Count > 1 ? "s have" : " has")} been selected";
            }
        }

        public string GetMultiSubjectText(List<string> selectedValues)
        {
            if (multiselectionTextChoice)
            {
                return $"Selected state{(selectedValues.Count > 1 ? "s" : "")}: {string.Join(", ", selectedValues.Select(x => x))}";
            }
            else
            {
                return $"{selectedValues.Count} Class{(selectedValues.Count > 1 ? "s have" : " has")} been selected";
            }
        }
        public void Cancel()
        {

        }

        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int masterBatchId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                //await Delete(masterBatchId);
            }

        }
    }

   
}
