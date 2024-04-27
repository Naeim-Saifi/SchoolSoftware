using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.Academic;
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
    public class MapTeacherWithSubjectBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        [Inject]
        public IMasterClassService masterClassService { get; set; }
        [Inject]
        public IMasterSectionService masterSectionService { get; set; }
        [Inject]
        public IMasterSubjectService masterSubjectService { get; set; }
        [Inject]
        public IMapTeacherWithSubjectService mapTeacherWithSubjectService { get; set; }

      

        public List<MasterClassListModel> masterClassListModel = new List<MasterClassListModel>();
        public List<MasterSectionListModel> masterSectionListModel = new List<MasterSectionListModel>();
        public List<MasterSubjectListModel> masterSubjectListModel = new List<MasterSubjectListModel>();
        public SessionModel _sessionModel;


        public List<MapSubjectWithTeacherList> subjectWithTeacherList = new List<MapSubjectWithTeacherList>();
        public MapSubjectWithTeacherList selectedItem = new MapSubjectWithTeacherList();

        public string _searchTerm = "";
        public bool enabled = true;
        public IEnumerable<MapSubjectWithTeacherList> SubjectWithTeacherList() => 
            subjectWithTeacherList.Where(e => e.teacherName.Contains(_searchTerm)| 
            e.classname.Contains(_searchTerm) | e.subjectName.Contains(_searchTerm));

        public MapSubjectWithTeacherModel mapSubjectWithTeacherModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                mapSubjectWithTeacherModel = new MapSubjectWithTeacherModel();
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
                masterClassListModel = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear,_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
                masterSectionListModel = (await masterSectionService.GetMasterSectionList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
                masterSubjectListModel = (await masterSubjectService.GetMasterSubjectList(_sessionModel.SchoolId,  0, 0, ReportType.GetBySchoolId)).ToList();
               // subjectWithTeacherList = (await mapTeacherWithSubjectService.GetMapSubjectWithTeacherList(0, 0, ReportType.GetBySchoolId)).ToList();

            }
            catch (Exception ex)
            {
                //message
            }

        }
        public void OnValidSubmit()
        {


        }

        public string SelectClass { get; set; }
        public string SelectSection { get; set; }

        public void Cancel()
        {
          
        }
    }
}
