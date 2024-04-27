
using AdminDashboard.Server.API_Service.Interface.Employee;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.TimeTableSetUp;
using AdminDashboard.Server.Models.SyllabusDetails;
using AdminDashboard.Server.User_Pages.Dashboard;
using AdminDashboard.Server.User_Pages.RND;
//using AdminDashboard.Server.Models.TimeTable;
//using AdminDashboard.Server.User_Pages.RND;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Employee;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Create_Word_document;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Charts.Chart.Internal;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.TimeTable
{
    public class TimeTableSetupBase : ComponentBase
    {
        [Inject]
        public IEmployeeSetupService employeeService { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public ViewTimeTableModel viewTimeTableModel = new ViewTimeTableModel();
        
        [Inject]
        public ITimeTableSetUpService _itimetableService { get; set; }

        public List<PeriodOutputModel> _periodListModels = new List<PeriodOutputModel>();
        public List<DayModelList> dayModelLists = new List<DayModelList>();
       
        public List<TimeTableOutputModel> _timeTableList = new List<TimeTableOutputModel>();
       

        public TimetableModel _timetableApiModel { get; set; }
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        //list Model
        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();
        public List<Master_Section_List_Output_Model> _sectionList = new List<Master_Section_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> _subjectList = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public List<Employee_User_List_Output_Model> _employee_List = new List<Employee_User_List_Output_Model>();
        public List<TimeTableOutputModel> _userWiseTimeTableList = new List<TimeTableOutputModel>();
     
        
        public List<TeacherTimeTableView> _singleTacherTimeTableView = new List<TeacherTimeTableView>();
        public List<ClassSectionTimeTableView> _singleStudentTimeTableView = new List<ClassSectionTimeTableView>();

        public List<TimeTableTabularFormate> _timeTableForTeacher = new List<TimeTableTabularFormate>();
        public List<TimeTableTabularFormate> _timeTableForStudent = new List<TimeTableTabularFormate>();

        public List<TimeTableTabularFormate> _timeTableForAllTeacher = new List<TimeTableTabularFormate>();
        public List<TimeTableTabularFormate> _timeTableForAllClassSection = new List<TimeTableTabularFormate>();
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            Employee_User_List_Input_Model employee_User_List_Input_Model = new Employee_User_List_Input_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                userRoleID = 0,
                userAccountID = 0,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            _employee_List = (await employeeService.GET_USER_LIST(employee_User_List_Input_Model)).ToList();

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
            Period_Input_Para_Model period_Input_Para_Model = new Period_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                periodId = 0,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _periodListModels = (await _itimetableService.Get_Period_List(period_Input_Para_Model)).ToList();
            //Day List
            dayModelLists = (await _itimetableService.GetDaysNameList()).ToList();
           

            //get time table list.
            Time_Table_Input_Para_Model time_Table_Input_Para_Model = new Time_Table_Input_Para_Model()
            { classId = 0, dayId = 0, periodId = 0, schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                sectionID = 0, reportType=ReportType.All, 
             subjectID=0,   userID=0
            };
            _timeTableList = (await _itimetableService.Get_Time_Table_List(time_Table_Input_Para_Model)).ToList();
        }

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<TimeTableOutputModel> sfTimeTableList;
        public SfGrid<TimeTableOutputModel> sfuserWiseTimeTableList;
        public SfGrid<TimeTableTabularFormate> sftacherTimeTableView;
        

        public List<string> toolBarItems = (new List<string>() { "Add Time Table", "Print", "ExportExcel", "Collapse All", "Expand All",  "Search" });
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
        public void ShowDialog()
        {
            IsVisible = true;
        }
        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    int classid = args.ItemData.classId;

                    //Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
                    //{
                    //    classID = classid,
                    //     subjectID=0,
                    //     userId=_sessionModel.UserId,
                    //    schoolCode = _sessionModel.SchoolCode,
                    //    financialYear = _sessionModel.FinancialYear,
                    //    reportType = ReportType.OnlySubjectName,
                    //     userRoleId= _sessionModel.RoleId,
                    //};
                    Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
                    {
                        classID = classid,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.ClassId,
                        userId = _sessionModel.UserId
                    };
                    _subjectList = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();

                    _userWiseTimeTableList= _timeTableList.Where(m=>m.classID == classid).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        int userId = 0;
        public async Task OnChangeUser(ChangeEventArgs<string, Employee_User_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.id != 0)
                {
                     userId = Convert.ToInt16(args.ItemData.id);
                    _singleTacherTimeTableView.Clear();

                    _userWiseTimeTableList = _timeTableList.Where(m=>m.teacherid == userId).ToList();

                    _timeTableForTeacher = (await _itimetableService.GetTimeTableTabularFormat("SingleTeacher", userId, 9, 2, dayModelLists, _periodListModels, _timeTableList)).ToList();
                    _timeTableForStudent = (await _itimetableService.GetTimeTableTabularFormat("SingleStudent", userId, 9, 2, dayModelLists, _periodListModels, _timeTableList)).ToList();

                    _timeTableForAllTeacher = (await _itimetableService.GetTimeTableTabularFormatAll("AllTeacher", _employee_List, _classList, _sectionList, dayModelLists, _periodListModels, _timeTableList)).ToList();
                    _timeTableForAllClassSection = (await _itimetableService.GetTimeTableTabularFormatAll("AllClassSection", _employee_List, _classList, _sectionList, dayModelLists, _periodListModels, _timeTableList)).ToList();
                }

                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                _timetableApiModel = new TimetableModel()
                {
                    
                    classId=Convert.ToInt32(viewTimeTableModel.classId),
                    sectionId= Convert.ToInt32(viewTimeTableModel.sectionId),
                    subjectId  = Convert.ToInt32(viewTimeTableModel.subjectId),
                    teacherId = Convert.ToInt32(viewTimeTableModel.teacherId),
                    periodId = Convert.ToInt32(viewTimeTableModel.periodId),
                    dayId = Convert.ToInt32(viewTimeTableModel.dayId),
                    timeTableID = Convert.ToInt32(viewTimeTableModel.timeTableID),
                    
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,

                };

                if (OperationType == "Add")
                {
                    _timetableApiModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    _timetableApiModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    _timetableApiModel.OperationType = OperationAction.Delete;
                }
               TimeTablesSave(_timetableApiModel);
            };
        }

        private async void TimeTablesSave(TimetableModel  timetableModel)
        {
            try
            {
                if (timetableModel.OperationType != "NA")
                {
                    aPIReturnModel = (await _itimetableService.CRUD_TimeTableSetup(timetableModel));

                    if (aPIReturnModel.status == false)
                    {
                        if (timetableModel.OperationType == "Add")
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Success);
                        }
                        else if (timetableModel.OperationType == "Update")
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Info);
                        }
                        else
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Error);
                        }
                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                    Time_Table_Input_Para_Model time_Table_Input_Para_Model = new Time_Table_Input_Para_Model()
                    {
                        classId = 0,
                        dayId = 0,
                        periodId = 0,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        sectionID = 0,
                        reportType = ReportType.All,
                        subjectID = 0,
                        userID = 0
                    };
                    _timeTableList = (await _itimetableService.Get_Time_Table_List(time_Table_Input_Para_Model)).ToList();

                    if(_userWiseTimeTableList.Count > 0)
                    {
                        _userWiseTimeTableList = _timeTableList.Where(m => m.teacherid == userId).ToList();
                        StateHasChanged();

                    }
                    else
                    {
                        _userWiseTimeTableList = _timeTableList.Where(m => m.teacherid == userId).ToList();
                        StateHasChanged();
                    }
                    //_userWiseTimeTableList = _timeTableList.Where(m => m.teacherid == userId).ToList();
                    //StateHasChanged();
                    //ClearData();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ClearData()
        {
            //viewTimeTableModel = new ViewTimeTableModel();

        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Time Table")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Time Table  ";
                OperationType = "Add";
                HeaderText = "Add Time Table";
                ddEnable = true;
                //viewTimeTableModel.classId = null;
                //viewTimeTableModel.sectionId = null;
                //viewTimeTableModel.subjectId = null;
                //viewTimeTableModel.dayId = null;
                //viewTimeTableModel.timeTableID = 0;
                //viewTimeTableModel.periodId = null;

                viewTimeTableModel=new ViewTimeTableModel();
                if(_userWiseTimeTableList.Count>0)
                {
                    _userWiseTimeTableList.Clear();
                }


            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfTimeTableList.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfTimeTableList.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfTimeTableList.ExpandAllGroupAsync();
            }
        }

        [Inject]
        public Microsoft.JSInterop.IJSRuntime _JS { get; set; }

        [Inject]

        public ExcelFormatTimeTable excelFormatTimeTable { get; set; }
        public void TeacherTimeTableToolBar(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
             string fileName=string.Empty;

              if (args.Item.Text == "ExportExcel")
            {
                

                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                this.sftacherTimeTableView.ExportToExcelAsync();

                //MemoryStream excelStream;
                //excelStream = excelFormatTimeTable.TimeTableTabularFormatExcel(_sessionModel.SchoolName, _timeTableForAllTeacher, "onlyTeacherTimeTable");
                //_JS.SaveAs("Teacher_TimeTable_Format-" + formattedDateTime + ".xlsx", excelStream.ToArray());

            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sftacherTimeTableView.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sftacherTimeTableView.ExpandAllGroupAsync();
            }
        }

        public void TimeTableTabularFormateToolBar(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            string fileName = string.Empty;
            if (args.Item.Text == "ExportExcel")
            {
                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("dd MMMM yyyy HH:mm:ss");
               // this.sftacherTimeTableView.ExportToExcelAsync();
                if(_timeTableForTeacher.Count>0)
                {
                    string _teacherName = _timeTableForTeacher[0].teacherName.ToUpper();
                     fileName = _teacherName+"_" + formattedDateTime + ".xlsx";
                    MemoryStream excelStream;
                    excelStream = excelFormatTimeTable.TimeTableTabularFormatExcel(_sessionModel.SchoolName,_sessionModel.FinancialYear, _timeTableForTeacher, "onlyTeacherTimeTable");
                    _JS.SaveAs(fileName, excelStream.ToArray());
                }
               

            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sftacherTimeTableView.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sftacherTimeTableView.ExpandAllGroupAsync();
            }
        }

        public async void EditTeacherTimeTable(CommandClickEventArgs<TimeTableOutputModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
 
                // IsEditVisible = true;
                DialogHeaderName = "Delete Unit Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
            //masterUnitViewModel = new UnitMasterViewModel()
            //{
            //    classId = args.RowData.classId.ToString(),
            //    subjectId = args.RowData.subjectID.ToString(),
            //    unitId = args.RowData.unitId,
            //    unitDescription = args.RowData.unitDescription,
            //    unitTitle = args.RowData.unitTitle,
            //};


            //bind subject list to dropdown 

            Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
            {
                classID = args.RowData.classID,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.ClassId,
                userId = _sessionModel.UserId
            };
            _subjectList = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();

           

            viewTimeTableModel = new ViewTimeTableModel()
            { 
                classId = args.RowData.classID.ToString(), sectionId= args.RowData.sectionID.ToString(),  teacherId=args.RowData.teacherid.ToString(),
                dayId=args.RowData.dayID.ToString(), periodId= args.RowData.periodId.ToString(), subjectId=args.RowData.subjectID.ToString(), timeTableID= args.RowData.timeTableId
            };
           
        }

         


    }
}
