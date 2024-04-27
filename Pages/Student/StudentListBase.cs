using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.Student;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Student
{
    public class StudentListBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IStudentListService studentService { get; set; }

        public List<StudentListModel> studentListModel = new List<StudentListModel>();

        public SessionModel _sessionModel;
       
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            studentListModel = (await studentService.GetStudentList(0,0,0,0,0, ReportType.GetBySchoolId)).ToList();
        }
        public bool enabled = true;
        public string _searchTerm = "";
        public IEnumerable<StudentListModel> StudentListModel() => studentListModel.Where(e => e.studentName.Contains(_searchTerm));

        public StudentListModel selectedItem = new StudentListModel();

    }
}
