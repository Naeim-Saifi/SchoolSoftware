using AdminDashboard.Server.API_Service.Interface.StudentSetUp;
using AIS.Data.RequestResponseModel.RemarksTypeMaster;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AdminDashboard.Server.User_Pages.HomeWork
{
    public class AddHomeWorkRemasrksBase : ComponentBase
    {
        [Inject]
        public IStudentSetupService _studentSetupService { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Parameter]
        public string _homeWorkID { get; set; }
        [Parameter]
        public string classID { get; set; }
        [Parameter]
        public string sectionID { get; set; }

        public List<Student_List_Output_Model> _studentList = new List<Student_List_Output_Model>();
        public List<StudentRemarksList> _studentremarksList = new List<StudentRemarksList>();

        public SfGrid<StudentRemarksList> sfstudentRemarksList;
        public SfDropDownList<RemarksTypeListModel, string> DropDownList { get; set; }
        public List<string> DropDownEnumValue = new List<string>();

        public string _className=string.Empty;
        public string _sectionName = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
           
            Student_List_Input_Para_Model student_List_Input_Para_Model = new Student_List_Input_Para_Model()
            {
                busStopID = 0,
                classID =Convert.ToInt32(classID),
                sectionID = Convert.ToInt32(sectionID),
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.ClassWise
            };


            _studentList = (await _studentSetupService.GET_Student_List(student_List_Input_Para_Model)).ToList();
            
            for (int i = 0; i < _studentList.Count; i++)
            {
                _className= _studentList[i].className.ToString();
                _sectionName = _studentList[i].sectionName.ToString();
                _studentremarksList.Add(new StudentRemarksList
                        {
                            userID = _studentList[i].accountID,
                            firstName = _studentList[i].firstName,
                            fatherName = _studentList[i].fatherName,
                            rollNo = _studentList[i].rollNo,
                            classID=Convert.ToInt32(classID),
                            sectionID=Convert.ToInt32(sectionID),
                            className = _studentList[i].className,
                            sectionName = _studentList[i].sectionName,
                            // remarksTypeListModel = RemarksTypeListModel.

                        }
                   );
                _studentremarksList =   _studentremarksList.OrderBy(x=>x.rollNo).ToList();   
            }
            sfstudentRemarksList.Refresh();
            StateHasChanged();
            //foreach (string item in Enum.GetNames(typeof(RemarksTypeListModel)))
            //{
            //    DropDownEnumValue.Add(item);
            //}
        }
    }
}
