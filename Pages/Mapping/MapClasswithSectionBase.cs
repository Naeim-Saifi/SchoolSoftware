using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.Mapping;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Mapping
{
    public class MapClasswithSectionBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterBatchService masterBatchService { get; set; }
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        [Inject]
        public IMasterSectionService masterSectionService { get; set; }

        [Inject]
        public IMapClassWithSectionService mapClassWithSectionService { get; set; }

        public List<MasterBatchListModel> masterBatchListModel = new List<MasterBatchListModel>();
        public List<MasterClassListModel> masterClassListModel = new List<MasterClassListModel>();
        public List<MasterSectionListModel> masterSectionListModel = new List<MasterSectionListModel>();

        public List<MapClassWithSectionList> mapClassWithSectionList = new List<MapClassWithSectionList>();

        public MapClassWithSectionList selectedItem = new MapClassWithSectionList();

        public string _searchTerm = "";
        public bool enabled = true;
        public IEnumerable<MapClassWithSectionList> ClassWithSectionList() => mapClassWithSectionList.Where(e => e.masterClassName.Contains(_searchTerm)|e.sectionName.Contains(_searchTerm));


        public SessionModel _sessionModel;

        public MapClassWithSectionModel mapClassWithSectionModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                mapClassWithSectionModel = new MapClassWithSectionModel();
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

                masterBatchListModel = (await masterBatchService.GetMasterBatchList(_sessionModel.SchoolCode, _sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
                masterClassListModel = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear,_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
                masterSectionListModel = (await masterSectionService.GetMasterSectionList(_sessionModel.SchoolCode, _sessionModel.FinancialYear,_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
                mapClassWithSectionList = (await mapClassWithSectionService.GetMapClassWithSectionList( 0, 0, ReportType.GetBySchoolId)).ToList();

            }
            catch (Exception ex)
            {
                //message
            }

        }
        public string StatusValue1 { get; set; }
        public void OnValidSubmit()
        {


        }
        private bool multiselectionTextChoice;
        public string batchvalue { get; set; } = "Nothing selected";
        public HashSet<string> batchoptions { get; set; } = new HashSet<string>() { "Alaska" };
        public string GetMultiBatchText(List<string> selectedValues)
        {
            if (multiselectionTextChoice)
            {
                return $"Selected state{(selectedValues.Count > 1 ? "s" : "")}: {string.Join(", ", selectedValues.Select(x => x))}";
            }
            else
            {
                mapClassWithSectionModel.BatchName = selectedValues.ToList();
                return $"{selectedValues.Count} Batch{(selectedValues.Count > 1 ? "s have" : " has")} been selected";
            }

            
        }
        
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

        public string GetMultiSectionText(List<string> selectedValues)
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
    } 
}
