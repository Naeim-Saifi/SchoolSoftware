using AdminDashboard.Server.API_Service.Interface.Exam;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.StudentSetUp;
using AdminDashboard.Server.User_Pages.Dashboard;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AdminDashboard.Server.User_Pages.ExamSetup
{
    public class StudentUnitListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IStudentSetupService _studentSetupService { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }

        public SessionModel _sessionModel;
        public SfGrid<Student_List_Output_Model> _sfgridstudent;
        public SfGrid<StudentUnitWiseMarks_List_Output> _sfstudentMarksList;

        public int pageSize = 50;
        //list Model

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> toolBarItems = (new List<string>() { "Search" });
        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();
        public List<Master_Section_List_Output_Model> _sectionList = new List<Master_Section_List_Output_Model>();
        public List<Student_List_Output_Model> _studentList = new List<Student_List_Output_Model>();
        public bool ddEnable = true;

        [Inject]
        public IExamMasterSetupService examMasterSetupService { get; set; }

        public StudentUnitListViewModel studentUnitListViewModel = new StudentUnitListViewModel();
        public List<StudentUnitWiseMarks_List_Output> _unitWiseMarksList = new List<StudentUnitWiseMarks_List_Output>();
        public UnitWiseMarksDetails UnitWiseMarksDetails;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.All
            };
            _classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            Master_Section_List_Input_Para_Model master_section_List_Input_Para_Model = new Master_Section_List_Input_Para_Model()
            {
                sectionId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.All
            };
            _sectionList = (await masterDataSetupService.GET_Master_SectionLIST(master_section_List_Input_Para_Model)).ToList();


            Student_List_Input_Para_Model student_List_Input_Para_Model = new Student_List_Input_Para_Model()
            {
                busStopID = 0,
                classID = 0,
                sectionID = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All


            };
            _studentList = (await _studentSetupService.GET_Student_List(student_List_Input_Para_Model)).ToList();

                                                                                                 
        }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                //CallEnd = DateTime.Now.ToString("h:mm tt");
                //NextFollowUpTime = DateTime.Now.ToString("h:mm tt");
                //_NextFollowUpDate = pendingFeeFollowUpDetailsViewModel.NextFollowUpDate;

                //unitWiseMarksEntryAPIModel = new PendingFeeFollowUpDetailsAPIModel()
                //{
                //    PendingFeeFollowUpID = pendingFeeFollowUpDetailsViewModel.PendingFeeFollowUpID,
                //    StudentID = pendingFeeFollowUpDetailsViewModel.StudentID,
                //    FollowUpDate = pendingFeeFollowUpDetailsViewModel.FollowUpDate,
                //    FollowUptime = pendingFeeFollowUpDetailsViewModel.FollowUptime,
                //    NextFollowUpDate = _NextFollowUpDate,
                //    NextFollowUpTime = NextFollowUpTime,
                //    ResponseType = "NA",
                //    FollowUpRemarks = pendingFeeFollowUpDetailsViewModel.FollowUpRemarks,
                //    CallStatus = pendingFeeFollowUpDetailsViewModel.CallStatus,
                //    CallStart = pendingFeeFollowUpDetailsViewModel.CallStart,
                //    CallEnd = CallEnd,
                //    fatherMobileNo = pendingFeeFollowUpDetailsViewModel.fatherContactNo,
                //    CreatedByUserId = _sessionModel.UserId,
                //    UpdatedByUserId = _sessionModel.UserId,
                //    SchoolCode = _sessionModel.SchoolCode,
                //    FinancialYear = _sessionModel.FinancialYear,
                //};

                //if (OperationType == "Add")
                //{
                //    pendingFeeFollowUpDetailsAPIModel.OperationType = OperationAction.Add;
                //}
                //else if (OperationType == "Update")
                //{
                //    pendingFeeFollowUpDetailsAPIModel.OperationType = OperationAction.Update;
                //}
                ////Delete Operation
                //else
                //{
                //    pendingFeeFollowUpDetailsAPIModel.OperationType = OperationAction.Delete;
                //}
                //SavePendingFeeFollow(pendingFeeFollowUpDetailsAPIModel);
            };
        }
        int _classID = 0;
        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    _classID = args.ItemData.classId;

                    // sectionID = args.ItemData.sectionId;
                    Student_List_Input_Para_Model student_List_Input_Para_Model = new Student_List_Input_Para_Model()
                    {
                        busStopID = 0,
                        classID = _classID,
                        sectionID = _sectionID,
                        userId = _sessionModel.UserId,
                        financialYear = _sessionModel.FinancialYear,
                        schoolCode = _sessionModel.SchoolCode,
                        reportType = AIS.Model.GeneralConversion.ReportType.ClassWise
                    };
                    _studentList = (await _studentSetupService.GET_Student_List(student_List_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        int _sectionID = 0;
        public async Task OnChangeSection(ChangeEventArgs<string, Master_Section_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.sectionId != 0)
                {
                    _sectionID = args.ItemData.sectionId;
                    Student_List_Input_Para_Model student_List_Input_Para_Model = new Student_List_Input_Para_Model()
                    {
                        busStopID = 0,
                        classID = _classID,
                        sectionID = _sectionID,
                        userId = _sessionModel.UserId,
                        financialYear = _sessionModel.FinancialYear,
                        schoolCode = _sessionModel.SchoolCode,
                        reportType = AIS.Model.GeneralConversion.ReportType.ClassWise
                    };
                    _studentList = (await _studentSetupService.GET_Student_List(student_List_Input_Para_Model)).ToList();
                    _studentList = _studentList.OrderBy(x => x.rollNo).ToList();
                    _sfgridstudent.Refresh();
                    StateHasChanged();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task  ViewUnitMarksDetails(CommandClickEventArgs<Student_List_Output_Model> args)
        {            
            string buttontext = args.CommandColumn.ButtonOption.Content;
            int studentID = Convert.ToInt16(args.RowData.accountID);
            if (buttontext == "View")
            {              
                
                

                UnitWiseMasrks_Input_Model unitWiseMasrks_Input_Model = new UnitWiseMasrks_Input_Model
                {
                    classId = 0,
                    sectionId = 0,
                    subjectId = 0,
                    chapterId = 0,
                    examTypeId = 0,
                    monthId = 9,
                    topicID = 0,
                    unitId = 0,
                    unitMarksID = 0,
                    userRoleId = 0,
                    userID = studentID,
                    schoolCode = _sessionModel.SchoolCode,
                    financialYear = _sessionModel.FinancialYear,
                    reportType = AIS.Data.GeneralConversion.GeneralConversion.UnitMarksReportType.StudentWiseMarksDetails
                };

                UnitWiseMarksDetails = (await examMasterSetupService.GET_UnitWiseMarksEntry(unitWiseMasrks_Input_Model));

                if(UnitWiseMarksDetails.studentUnitWiseMarks_List_Outputs.Count>0)
                {
                    _unitWiseMarksList = UnitWiseMarksDetails.studentUnitWiseMarks_List_Outputs;
                }
                // _unitWiseMarksDetailsList = UnitWiseMarksDetails.unitWiseMarksDetails_List_Output_Models;
               
            } 
            else
            {
                marksreportAsync(studentID);

            }



        }


        [Inject]
        public IJSRuntime jsRuntime { get; set; }
        [Inject]
        private IConfiguration _config { get; set; }
        public async Task marksreportAsync(int studentID)
        {
            try
            {
                string baseURL = _config.GetValue<string>("Report:ReportUrl");
                string fullUrl = "";
                fullUrl = baseURL + ($"/api/ExamMasterSetUp/UnitWiseStudentMarks?SchoolCode={_sessionModel.SchoolCode}" +
                        $"&FinancialYear={_sessionModel.FinancialYear}" +
                        $"&ExamTypeId={0}" +
                        $"&ClassId={0}" +
                        $"&SectionId={0}" +
                        $"&MonthId={0}" +
                        $"&SubjectId={0}" +
                        $"&UnitId={0}" +
                        $"&ChapterId={0}" +
                        $"&TopicID={0}" +
                       $"&UserID={studentID}" +
                       $"&ReportType={"StudentWiseMarksDetails"}");
                await jsRuntime.InvokeAsync<object>("open", fullUrl, "_blank");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());

            }

        }
    }
}
