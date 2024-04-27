using AdminDashboard.Server.API_Service.Interface.StudentSetUp;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.StudentModule.ProfileDetails
{
    public class ProfileDetailsBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        //[Inject]
        //public IStudentListService studentService { get; set; }

       
        //public StudentDetailsListModel studentDetailsListModel { get; set; }

        ////public List<StudentListModel> studentListModel = new List<StudentListModel>();
        ////properties binding 
        //public StudentListModel studentModel = new StudentListModel();

        [Inject]
        public IStudentSetupService studentSetupService { get; set; }
        public List<Student_List_Output_Model> studentListModel = new List<Student_List_Output_Model>();

        public string admissionID="";
          
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            Student_List_Input_Para_Model student_List_Input_Para_Model = new Student_List_Input_Para_Model()
            {
                userId = _sessionModel.UserId,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                busStopID = 0,
                classID = 0,
                sectionID = 0,
                reportType = ReportType.GetByStudentUserID,
            };


            if (studentListModel!=null)
            {

                 //studentDetailsListModel = (await studentService.GetStudentDetailsList(_sessionModel.SchoolCode, _sessionModel.FinancialYear,0,0, _sessionModel.UserId, ReportType.GetByStudentUserID));
                studentListModel = (await studentSetupService.GET_Student_List(student_List_Input_Para_Model)).ToList();
                
                //studentListModel = studentDetailsListModel.studentListModels;
                //admissionID = studentListModel[0].admissionNumber.ToString();
            }
            else
            {
                //studentListModel.Add(studentModel.admissionNumber.ToString());
            }
           
           

        }
    }
}
