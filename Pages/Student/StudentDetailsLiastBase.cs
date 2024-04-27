using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.Student;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AdminDashboard.Server.Pages.Student
{
    public class StudentDetailsLiastBase: ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IStudentListService studentService { get; set; }

        public StudentDetailsListModel  studentDetailsListModel { get; set; }

        public List<StudentListModel> studentListModel = new List<StudentListModel>();

        //properties binding 
        public StudentListModel studentModel = new StudentListModel();

        public SessionModel _sessionModel;

        public SfGrid<StudentListModel> Grid;

        public string[] GroupedColumns = new string[] { "className" };
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            studentDetailsListModel = (await studentService.GetStudentDetailsList(_sessionModel.SchoolCode,_sessionModel.FinancialYear,0,0 ,0, ReportType.AllStudentDetails));
            studentListModel = studentDetailsListModel.studentListModels;
        }
        public void ExportToExcel()
        {
            this.Grid.ExcelExport();
            

        }
    }
}
