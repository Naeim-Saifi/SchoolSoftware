using AdminDashboard.Server.API_Service.Interface.Attendance;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.StudentSetUp;
using AdminDashboard.Server.API_Service.Service.Attendance;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Attendance;
using AIS.Data.RequestResponseModel.HolidaySetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using MudBlazor;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AdminDashboard.Server.API_Service.Service.Attendance.AttendanceService;

namespace AdminDashboard.Server.User_Pages.Attendance
{
    public class StudentAttendanceBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }
        [Inject]
        public IStudentSetupService _studentSetupService { get; set; }
        [Inject]
        public IAttendanceService _attendanceService { get; set; }

        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();
        public List<Master_Section_List_Output_Model> _sectionList = new List<Master_Section_List_Output_Model>();
        public List<Student_List_Output_Model> _studentList = new List<Student_List_Output_Model>();
        public List<StudentAttendanceListModel> _studentAttendanceList = new List<StudentAttendanceListModel>();

        public List<TodayAttendanceList>  _todayAttendanceLists = new List<TodayAttendanceList>();

        public List<AttendanceAPIModel> _attendanceAPIModel =new List<AttendanceAPIModel>();

        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }

        public StudentAttendanceViewModel _studentAttendanceViewModel = new StudentAttendanceViewModel();
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsDeleteVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";
        public APIReturnModel aPIReturnModel;
        public SfGrid<StudentAttendanceListModel> sfstudentDetails;
        public SfGrid<TodayAttendanceList> sfTodayAttendanceList;
        public void ShowDialog()
        {
            IsVisible = true;
        }
        public List<string> toolBarItems = (new List<string>() { "Add Attendance", "ExcelExport", "Collapse All", "Expand All", "Print", "Search" });
        public int pageSize = 50;
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");



            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            Master_Section_List_Input_Para_Model master_section_List_Input_Para_Model = new Master_Section_List_Input_Para_Model()
            {
                sectionId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _sectionList = (await masterDataSetupService.GET_Master_SectionLIST(master_section_List_Input_Para_Model)).ToList();

            AttendanceInputParaModel attendanceInputParaModel = new AttendanceInputParaModel() {
                classId = 0,
                userid = _sessionModel.UserId,
                fromDate = DateTime.Today.Date.ToString(),
                toDate = DateTime.Today.Date.ToString(),
                sectionId = 0,
                 userRoleId = _sessionModel.RoleId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = AttendanceReportType.TodayAttendanceList
            };

            _todayAttendanceLists =(await _attendanceService.GET_StudentAttendanceList(attendanceInputParaModel)).ToList();


            foreach (string item in Enum.GetNames(typeof(AttendanceStatus)))
            {
                DropDownEnumValue.Add(item);
            }
        }
        public void toolBarItemsClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Attendance")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;

                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Student Attendance";
                OperationType = "Add";
                HeaderText = "Add Student Attendance";
                ddEnable = true;


            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfTodayAttendanceList.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfTodayAttendanceList.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {
                this.sfTodayAttendanceList.ExportToExcelAsync();

                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Class & Section Attendance List Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "Class_With_ChapterMasterList.xlsx";
                this.sfTodayAttendanceList.ExcelExport(ExcelProperties);
            }
            else
            {
                this.sfTodayAttendanceList.Columns[0].AutoFit = true;
            }
        }
        public void EditCommandClick(CommandClickEventArgs<TodayAttendanceList> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = Convert.ToInt16(args.RowData.syllabusStatusID);
            //if (buttontext == "Edit")
            //{
            //    //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
            //    //click to open edit dialog
            //    IsVisible = true;
            //    DialogHeaderName = "Update Chapter Details";
            //    HeaderText = "Update Record";
            //    OperationType = "Update";
            //    btncss = "e-flat e-info e-outline";
            //    ddEnable = false;
            //    syllabusStatusViewModel = new SyllabusStatusViewModel()
            //    {
            //        classID = args.RowData.classId.ToString(),
            //        sectionId = args.RowData.sectionId.ToString(),
            //        subjectID = args.RowData.subjectID.ToString(),
            //        syllabusStatusID = Convert.ToInt16(args.RowData.syllabusStatusID),
            //        unitId = args.RowData.unitId.ToString(),
            //        chapterId = args.RowData.chapterID.ToString(),
            //        topicID = args.RowData.topicID.ToString(),
            //        remarks = args.RowData.remarksDetails.ToString(),
            //        statusID = args.RowData.syllabusStatusID.ToString(),
            //        fromDate = args.RowData.fromDate.ToString(),
            //        endDate = args.RowData.toDate.ToString(),

            //    };
            //}
            //else
            //{
            //    IsVisible = true;
            //    DialogHeaderName = "Delete Chapter Details";
            //    OperationType = "Delete";
            //    HeaderText = "Delete Record";
            //    btncss = "e-flat e-danger e-outline";
            //    ddEnable = false;
            //    syllabusStatusViewModel = new SyllabusStatusViewModel()
            //    {
            //        classID = args.RowData.classId.ToString(),
            //        sectionId = args.RowData.sectionId.ToString(),
            //        subjectID = args.RowData.subjectID.ToString(),
            //        syllabusStatusID = Convert.ToInt16(args.RowData.syllabusStatusID),
            //        unitId = args.RowData.unitId.ToString(),
            //        chapterId = args.RowData.chapterID.ToString(),
            //        topicID = args.RowData.topicID.ToString(),
            //        remarks = args.RowData.remarksDetails.ToString(),
            //        statusID = args.RowData.syllabusStatusID.ToString(),
            //        fromDate = args.RowData.fromDate.ToString(),
            //        endDate = args.RowData.toDate.ToString(),

            //    };
        }

       

        int classID = 0;
        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    classID = args.ItemData.classId;
                    Student_List_Input_Para_Model student_List_Input_Para_Model = new Student_List_Input_Para_Model()
                    {
                        busStopID = 0,
                        classID = classID,
                        sectionID = sectionID,
                        userId = _sessionModel.UserId,
                        financialYear = _sessionModel.FinancialYear,
                        schoolCode = _sessionModel.SchoolCode,
                        reportType = ReportType.All


                    };
                    _studentList = (await _studentSetupService.GET_Student_List(student_List_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        int sectionID = 0;
        public async Task OnChangeSection(ChangeEventArgs<string, Master_Section_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.sectionId != 0)
                {
                     sectionID = args.ItemData.sectionId;
                    Student_List_Input_Para_Model student_List_Input_Para_Model = new Student_List_Input_Para_Model()
                    {
                        busStopID = 0,
                        classID = classID,
                        sectionID = sectionID,
                        userId = _sessionModel.UserId,
                        financialYear = _sessionModel.FinancialYear,
                        schoolCode = _sessionModel.SchoolCode,
                        reportType = ReportType.ClassWise


                    };
                    _studentList = (await _studentSetupService.GET_Student_List(student_List_Input_Para_Model)).ToList();

                    if(_studentAttendanceList.Count>0)
                    {
                        _studentAttendanceList.Clear();
                            _presentStudent = 0;
                            _absentStudent = 0;
                            _leaveStudent = 0;
                            _totalStudent = 0;

                    }

                    for(int i = 0;i< _studentList.Count;i++)
                    {
                        _studentAttendanceList.Add( new StudentAttendanceListModel 
                        {
                             studentID= _studentList[i].accountID.ToString(),
                             firstName=   _studentList[i].firstName,
                             fatherName=  _studentList[i].fatherName,
                             rollNo= _studentList[i].rollNo,
                             status= AttendanceStatus.Present
                        }
                        );
                    }
                    _totalStudent=_studentAttendanceList.Count;

                    sfstudentDetails.Refresh();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public async void OnValidSubmit(EditContext contex)
        {
            //bool isValid = contex.Validate();
            //if (isValid)
            //{
            //    _timetableApiModel = new TimetableModel()
            //    {

            //        classId = Convert.ToInt32(viewTimeTableModel.classId),
            //        sectionId = Convert.ToInt32(viewTimeTableModel.sectionId),
            //        subjectId = Convert.ToInt32(viewTimeTableModel.subjectId),
            //        teacherId = Convert.ToInt32(viewTimeTableModel.teacherId),
            //        periodId = Convert.ToInt32(viewTimeTableModel.periodId),
            //        dayId = Convert.ToInt32(viewTimeTableModel.dayId),
            //        timeTableID = Convert.ToInt32(viewTimeTableModel.timeTableID),

            //        CreatedByUserId = _sessionModel.UserId,
            //        UpdatedByUserId = _sessionModel.UserId,
            //        SchoolCode = _sessionModel.SchoolCode,
            //        FinancialYear = _sessionModel.FinancialYear,

            //    };

            //    if (OperationType == "Add")
            //    {
            //        _timetableApiModel.OperationType = OperationAction.Add;
            //    }
            //    else if (OperationType == "Update")
            //    {
            //        _timetableApiModel.OperationType = OperationAction.Update;
            //    }
            //    //Delete Operation
            //    else
            //    {
            //        _timetableApiModel.OperationType = OperationAction.Delete;
            //    }
            //    TimeTablesSave(_timetableApiModel);
        }
    
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public void OnCellEdit(CellEditArgs<StudentAttendanceListModel> args)
        {
            //if (args.RowData.status == "INDIA" && args.ColumnName == "Status")
            //{
            //    args.Cancel = true;
            //}
        }

       public SfDropDownList<AttendanceStatus, string> DropDownList { get; set; }
        public List<string> DropDownEnumValue = new List<string>();

       public string statusValue = "";
       public int _presentStudent=0;
        public int _absentStudent=0;
        public int _leaveStudent = 0;
        public int _totalStudent = 0;
        public void CustomizeCell(QueryCellInfoEventArgs<StudentAttendanceListModel> args)
        {
            if (args.Column.Field == "status")
            {
                statusValue= args.Data.status.ToString();

                if (statusValue == "Absent")
                {
                    args.Cell.AddClass(new string[] { "absent" });
                    _absentStudent = _absentStudent + 1;
                }
                if (statusValue == "Leave")
                {
                    args.Cell.AddClass(new string[] { "leave" });
                    _leaveStudent = _leaveStudent + 1;
                }
                if (statusValue == "Holiday")
                {
                    args.Cell.AddClass(new string[] { "holiday" });
                    _absentStudent = _absentStudent + 1;
                }
                if (statusValue == "Present")
                {
                    //args.Cell.AddClass(new string[] { "absent" });
                    _presentStudent = _presentStudent + 1;
                }
                
            }
        }
        
        public DateTime? DateValue { get; set; } = DateTime.Now;
        public void onChange(ChangedEventArgs<DateTime?> args)
        {
            DateValue = Convert.ToDateTime(args.Value);
           // syllabusSetupViewModel.toDate = Convert.ToDateTime(args.EndDate);

            //StateHasChanged();
        }
        public async Task SaveStudentAttendanceAsync()
        {
            var attendanStatus = "";
            int _present = 0, _absent = 0, leave = 0, holiday = 0;
            if(_attendanceAPIModel.Count>0)
            {
                _attendanceAPIModel.Clear();
            }

            for(int icount=0;icount< _studentAttendanceList.Count;icount++)
            {
                attendanStatus = _studentAttendanceList[icount].status.ToString();
               
                if(attendanStatus=="Present")
                {
                    _present = 1;
                    _absent = 0; leave = 0; holiday = 0;
                }
                else if(attendanStatus == "Absent" )
                {
                    _present = 0;
                    _absent = 1; leave = 0; holiday = 0;
                }
                else if (attendanStatus == "Leave")
                {
                    _present = 0;
                    _absent = 0; leave = 1; holiday = 0;
                }
                else if (attendanStatus == "Holiday")
                {
                    _present = 0;
                    _absent = 0; leave = 0; holiday = 1;
                }

                _attendanceAPIModel.Add(new AttendanceAPIModel
                {
                    classId = classID,
                    sectionId=sectionID,
                    userid = Convert.ToInt16(_studentAttendanceList[icount].studentID),
                    presentStatus = _present,
                    absentStatus=_absent,
                    leaveStatus = leave,
                    holidayStatus = holiday,
                    SchoolCode=_sessionModel.SchoolCode,
                    attendanceID=0,
                    attendanceDate= Convert.ToDateTime(DateValue),
                    UpdatedByUserId = _sessionModel.UserId,
                    FinancialYear =_sessionModel.FinancialYear,
                    CreatedByUserId=_sessionModel.UserId,
                    OperationType  = OperationAction.Add,

                });
            }
            try
            {
                if (_attendanceAPIModel.Count>0)
                {
                    aPIReturnModel = (await _attendanceService.CRUD_StudentAttendance(_attendanceAPIModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                        _studentAttendanceList.Clear();
                        sfstudentDetails.Refresh();
                        StateHasChanged();
                        //Topic_Input_Para_Model topic_Input_Para_Model = new Topic_Input_Para_Model()
                        //{
                        //    subjectId = 0,
                        //    classId = 0,
                        //    unitID = 0,
                        //    chapterID = 0,
                        //    schoolCode = _sessionModel.SchoolCode,
                        //    financialYear = _sessionModel.FinancialYear,
                        //    reportType = ReportType.All,

                        //};
                        //topicMasterList = (await syllabusService.Get_Topic_List(topic_Input_Para_Model)).ToList();

                        //_topicMasterList = topicMasterList.Where(m => m.chapterID == _chapterID).ToList();

                        //ClearData(); StateHasChanged();
                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}
 
