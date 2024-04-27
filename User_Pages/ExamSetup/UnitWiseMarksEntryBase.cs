using AdminDashboard.Server.API_Service.Interface.Exam;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.StudentSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.User_Pages.RND;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.Attendance;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using MudBlazor;
using Syncfusion.Blazor.Charts;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.ExamSetup
{
    public class UnitWiseMarksEntryBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IExamMasterSetupService examMasterSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public IStudentSetupService _studentSetupService { get; set; }

        public UnitWiseMarksDetails UnitWiseMarksDetails;

        public SfGrid<UnitWiseMarksDetails_List_Output_Model> _sfUnitMarksDetailsList;
        public List<Student_List_Output_Model> _studentList = new List<Student_List_Output_Model>();

        public List<StudentUnitMarksDetails> studentUnitMarksDetails = new List<StudentUnitMarksDetails>();

        public List<UnitWiseMarksDetails_List_Output_Model> _unitWiseMarksDetailsList = new List<UnitWiseMarksDetails_List_Output_Model>();
        public List<UnitWiseMarks_List_Output_Model> _unitWiseMarksList = new List<UnitWiseMarks_List_Output_Model>();

        public UnitWiseMarksEntryAPIModel unitWiseMarksEntryAPIModel = new UnitWiseMarksEntryAPIModel ();
        public List<UnitWiseMarksEntryAPIModel> unitWiseMarksEntryAPIModelList = new List<UnitWiseMarksEntryAPIModel>();

        public UnitWiseMarksEntryViewModel unitWiseMarksEntryViewModel = new UnitWiseMarksEntryViewModel();

        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();
        public List<Master_Section_List_Output_Model> _sectionList = new List<Master_Section_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> _subjectList = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public List<Unit_Output_Model> _unitList = new List<Unit_Output_Model>();
        public List<Chapter_Output_Model> _chapterList = new List<Chapter_Output_Model>();
        public List<Topic_Output_Model> _topicList = new List<Topic_Output_Model>();
        public List<MonthNameListModel> _monthNameList = new List<MonthNameListModel>();

        public SfDropDownList<AttendanceStatus, string> DropDownList { get; set; }
        public List<string> DropDownEnumValue = new List<string>();


        public List<string> toolBarPendingFee = (new List<string>() { "Marks Entry", "ExcelExport", "Collapse All", "Expand All", "Print", "Search", "ColumnChooser" });

        public SessionModel _sessionModel;
        public string[] GroupedColumns = new string[] { "fatherContactNumber" };
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsEditVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";

        public string btnFindcss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";
        public SfGrid<StudentUnitMarksDetails> _sfStudentMarksDetails;

        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            _monthNameList = (await syllabusService.GetMonthNameList()).ToList();
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
                userID = 0,
                userRoleId = 0,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.All
            };
           
            UnitWiseMarksDetails = (await examMasterSetupService.GET_UnitWiseMarksEntry(unitWiseMasrks_Input_Model));

            _unitWiseMarksDetailsList = UnitWiseMarksDetails.unitWiseMarksDetails_List_Output_Models;
            _unitWiseMarksList = UnitWiseMarksDetails.unitWiseMarks_List_Output; 
            _unitWiseMarksList= _unitWiseMarksList.OrderBy(x=>x.rollNo).ToList();   
        }

        public void CustomizeCell(QueryCellInfoEventArgs<UnitWiseMarksDetails_List_Output_Model> args)
        {
            //if (args.Column.Field == "flowupStatus")
            //{
            //    if (args.Data.flowupStatus == "No")
            //    {
            //        args.Cell.AddClass(new string[] { "below-No" });
            //    }
            //    else
            //    {
            //        args.Cell.AddClass(new string[] { "above-Yes" });
            //    }
            //}
            //if (args.Column.Field == "flowupCount")
            //{
            //    if (args.Data.flowupCount == 0)
            //    {
            //        args.Cell.AddClass(new string[] { "below-0 " });
            //    }
            //    else if (args.Data.flowupCount == 1)
            //    {
            //        args.Cell.AddClass(new string[] { "below-1" });
            //    }
            //    else if (args.Data.flowupCount == 2)
            //    {
            //        args.Cell.AddClass(new string[] { "below-3" });
            //    }
            //}
        }

        public void EditFlowUp(CommandClickEventArgs<UnitWiseMarksDetails_List_Output_Model> args)
        {
            //// Perform required operations here
            //string buttontext = args.CommandColumn.ButtonOption.Content;
            //int studentID = args.RowData.accountId;
            ////int testId = args.RowData.testID;
            //if (buttontext == "FlowUp")
            //{
            //    //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
            //    //click to open edit dialog

            //    IsEditVisible = true;
            //    DialogHeaderName = "Add Student Flow up Details";
            //    HeaderText = "Add Record";
            //    OperationType = "Add";
            //    btncss = "e-flat e-info e-outline";
            //    ddEnable = false;

            //    studentName = args.RowData.studentName;
            //    fatherName = args.RowData.fatherName;
            //    pendingFeeMonth = args.RowData.monthName;
            //    fatherMobileNo = args.RowData.fatherContactNumber;
            //    netDueAmount = args.RowData.netDueAmount;
            //    currentDate = DateTime.Now.ToString("dd MMMM yyyy");
            //    pendingFeeFollowUpDetailsViewModel.FollowUpDate = currentDate;

            //    currentTime = DateTime.Now.ToString("h:mm tt");
            //    pendingFeeFollowUpDetailsViewModel.FollowUptime = currentTime;
            //    pendingFeeFollowUpDetailsViewModel.CallStart = currentTime;
            //    pendingFeeFollowUpDetailsViewModel.StudentID = studentID;
            //    pendingFeeFollowUpDetailsViewModel.fatherContactNo = fatherMobileNo;
            //    GetStudentTransactionDetails(studentID);
            //    //topicMasterViewModel = new TopicMasterViewModel()
            //    //{
            //    //    classId = Convert.ToString(args.RowData.classId),
            //    //    subjectId = Convert.ToString(args.RowData.subjectID),
            //    //    topicId = args.RowData.topicID,
            //    //    chapterId = args.RowData.chapterID.ToString(),
            //    //    unitId = args.RowData.unitId.ToString(),
            //    //    topicDescription = args.RowData.topicDescription,
            //    //    topicTitle = args.RowData.topicTitle

            //    //};
            //}
            //else
            //{
            //    //IsEditVisible = true;
            //    //DialogHeaderName = "Delete Topic Details";
            //    //OperationType = "Delete";
            //    //HeaderText = "Delete Record";
            //    //btncss = "e-flat e-danger e-outline";
            //    //ddEnable = false;
            //    //topicMasterViewModel = new TopicMasterViewModel()
            //    //{
            //    //    classId = Convert.ToString(args.RowData.classId),
            //    //    subjectId = Convert.ToString(args.RowData.subjectID),
            //    //    topicId = args.RowData.topicID,
            //    //    chapterId = args.RowData.chapterID.ToString(),
            //    //    unitId = args.RowData.unitId.ToString(),
            //    //    topicDescription = args.RowData.topicDescription,
            //    //    topicTitle = args.RowData.topicTitle

            //    //};
        }
        public void ToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Marks Entry")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                btnFindcss = "e-flat e-info e-outline";
                DialogHeaderName = "Add Unit wise marks entry";
                OperationType = "Add";
                HeaderText = "Add Marks";
                ddEnable = true;


            }
            else if (args.Item.Text == "Collapse All")
            {
                this._sfUnitMarksDetailsList.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this._sfUnitMarksDetailsList.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {

                this._sfUnitMarksDetailsList.ExcelExport();
            }
        }

        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }

        int _classID = 0;
        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    _classID = args.ItemData.classId;

                    Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
                    {
                        classID = _classID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.ClassId,
                        userId = _sessionModel.UserId
                    };

                    _subjectList = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();
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


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        int _subjectID = 0;
        public async Task OnChangeSubject(ChangeEventArgs<string, Master_Map_Subject_With_Class_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.masterClassId != 0)
                {
                    // int classid = args.ItemData.masterClassId;
                    _subjectID = args.ItemData.subjectId;

                    Unit_Input_Para_Model unit_Input_Para_Model = new Unit_Input_Para_Model()
                    {
                        classId = _classID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = SyllabusReportType.UnitlistClassWithSubject,
                        subjectId = _subjectID,
                        userId = _sessionModel.UserId,
                        userRoleId = _sessionModel.RoleId
                    };
                    _unitList = (await syllabusService.Get_Unit_List(unit_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        int _unitID = 0;
        public async Task OnChangeUnit(ChangeEventArgs<string, Unit_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    _classID = args.ItemData.classId;
                    _unitID = args.ItemData.unitId;
                    _subjectID = args.ItemData.subjectID;

                    //Chapter_Input_Para_Model chapter_Input_Para_Model = new Chapter_Input_Para_Model()
                    //{
                    //    classId = _classID,
                    //    unitID = _unitID,
                    //    subjectId = _subjectID,
                    //    schoolCode = _sessionModel.SchoolCode,
                    //    financialYear = _sessionModel.FinancialYear,
                    //    reportType = ReportType.GetByMasterId,
                    //};
                    //_chapterList = (await syllabusService.Get_Chapter_List(chapter_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        int _chapterID = 0;
        public async Task OnChangeChapter(ChangeEventArgs<string, Chapter_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    _classID = args.ItemData.classId;
                    _subjectID = args.ItemData.subjectId;
                    _unitID = args.ItemData.unitId;
                    _chapterID = args.ItemData.chapterID;

                    Topic_Input_Para_Model chapter_Input_Para_Model = new Topic_Input_Para_Model()
                    {
                        classId = _classID,
                        unitID = _unitID,
                        subjectId = _subjectID,
                        chapterID = _chapterID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = AIS.Model.GeneralConversion.ReportType.GetByMasterId,
                    };
                    _topicList = (await syllabusService.Get_Topic_List(chapter_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }


        int _monthID = 0;
        public async Task OnChangeMonth(ChangeEventArgs<string, MonthNameListModel> args)
        {
            try
            {
                if (args.ItemData.MonthId != "0")
                {
                    _monthID =Convert.ToInt32(args.ItemData.MonthId);
                     
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

        public int _totalStudent = 0;
        public async Task FindStudentDetails()
        {

            if(_classID == 0||_subjectID==0 || _unitID==0||_monthID==0)
            {
                snackBar.Add("Please select all details", Severity.Error);
            }
            else
            {

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

                if (studentUnitMarksDetails.Count > 0)
                {
                    studentUnitMarksDetails.Clear();


                }

                for (int i = 0; i < _studentList.Count; i++)
                {
                    studentUnitMarksDetails.Add(new StudentUnitMarksDetails
                    {
                        StudentId = _studentList[i].accountID,
                        StudentName = _studentList[i].firstName,
                        FatherName = _studentList[i].fatherName,
                        ClassID = _classID,
                        SectionID = _sectionID,
                        RollNo = Convert.ToInt16(_studentList[i].rollNo),
                        MaxMarks = unitWiseMarksEntryViewModel.MaxMarks,
                        MonthID = _monthID,
                        ChapterID = 0,
                        TopicID = 0,
                        UnitID = _unitID,
                        Grade = "NA",
                        ObtainMarks = 0,
                        Percentage = "0",
                        SubjectID = _subjectID,
                        TeacherRemarks = "NA"
                        //status = AttendanceStatus.Present
                    }
                    );
                }
                _totalStudent = studentUnitMarksDetails.Count;
                studentUnitMarksDetails = studentUnitMarksDetails.OrderBy(x => x.RollNo).ToList();
                _sfStudentMarksDetails.Refresh();
                StateHasChanged();
            }

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
        [Inject]
        public ISnackbar snackBar { get; set; }
        public async Task CellSavedHandler(CellSaveArgs<StudentUnitMarksDetails> args)
        {
            // Here, you can customize your code.
            double index = await _sfStudentMarksDetails.GetRowIndexByPrimaryKey(args.RowData.StudentId);
            int rowIndex = Convert.ToInt32(index);

            int maxMarks = args.Data.MaxMarks;
            decimal obtainMarks = args.Data.ObtainMarks;
            decimal percentage = (obtainMarks / maxMarks) * 100;

            string vGrade = "";

            if (args.ColumnName == "ObtainMarks")
            {
                //if (IsAdd)
                //{
                   // args.RowData.Quantity = (int?)args.Value;
                if(obtainMarks> maxMarks)
                {
                    await _sfStudentMarksDetails.UpdateCellAsync(index, "ObtainMarks", 0);
                    snackBar.Add("Obtain Marks is grater then max marks", Severity.Error);
                }
                else
                {
                    vGrade = await examMasterSetupService.GetGradeDetails(percentage);

                    studentUnitMarksDetails[rowIndex].ClassID = args.RowData.ClassID;
                    studentUnitMarksDetails[rowIndex].SectionID = args.RowData.SectionID;
                    studentUnitMarksDetails[rowIndex].SubjectID = args.RowData.SubjectID;
                    studentUnitMarksDetails[rowIndex].MonthID = args.RowData.MonthID;
                    studentUnitMarksDetails[rowIndex].StudentId = args.RowData.StudentId;                   
                    studentUnitMarksDetails[rowIndex].UnitID = args.RowData.UnitID;
                    studentUnitMarksDetails[rowIndex].ChapterID = 0;
                    studentUnitMarksDetails[rowIndex].TopicID = 0;
                    studentUnitMarksDetails[rowIndex].MaxMarks = Convert.ToInt32(maxMarks);
                    studentUnitMarksDetails[rowIndex].ObtainMarks = Convert.ToInt32(obtainMarks);
                    studentUnitMarksDetails[rowIndex].Percentage = percentage.ToString();
                    studentUnitMarksDetails[rowIndex].Grade = vGrade.ToString();
                    studentUnitMarksDetails[rowIndex].TeacherRemarks = "NA";



                    await _sfStudentMarksDetails.UpdateCellAsync(index, "Percentage", percentage.ToString());
                    await _sfStudentMarksDetails.UpdateCellAsync(index, "Grade", vGrade.ToString());

                }
                
                // _sfStudentMarksDetails.Refresh();
                StateHasChanged();
                //    await Grid.UpdateCell(index, "Sum", Convert.ToInt32(args.Value) + 0);
                //}
                //await Grid.UpdateCell(index, "Amount", Convert.ToInt32(args.Value) * args.RowData.UnitPrice);
                //await Grid.UpdateCell(index, "Sum", Convert.ToInt32(args.Value) + args.RowData.UnitPrice);
            }

            // args.Cell.SetAttribute = percentage;

            //}
        }


        public void SaveStudentMarksDetails()
        {
            if (studentUnitMarksDetails.Count>0)
            {
                Guid myuuid = Guid.NewGuid();
                string _unitWiseMarksID = myuuid.ToString();

                for (int count = 0; count < studentUnitMarksDetails.Count; count++)
                {
                    unitWiseMarksEntryAPIModel = new UnitWiseMarksEntryAPIModel()
                    {

                        UnitWiseMarksID = 0,
                        UnitMarksID = _unitWiseMarksID,
                        StudentID = studentUnitMarksDetails[count].StudentId,
                        ClassID = studentUnitMarksDetails[count].ClassID,
                        SectionID = studentUnitMarksDetails[count].SectionID,
                        SubjectID = studentUnitMarksDetails[count].SubjectID,
                        UnitID = studentUnitMarksDetails[count].UnitID,
                        ChapterID = studentUnitMarksDetails[count].ChapterID,
                        TopicID = studentUnitMarksDetails[count].TopicID,
                        MonthID = studentUnitMarksDetails[count].MonthID,
                        MaxMarks = studentUnitMarksDetails[count].MaxMarks,
                        ObtainMarks = studentUnitMarksDetails[count].ObtainMarks,
                        Grade = studentUnitMarksDetails[count].Grade,
                        Percentage = studentUnitMarksDetails[count].Percentage,
                        TeacherRemarks = studentUnitMarksDetails[count].TeacherRemarks,


                        AttendanceStatusID = 0,
                        Subject_Optional = 0,
                        OperationType = "Add",
                        SchoolCode = _sessionModel.SchoolCode,
                        FinancialYear = _sessionModel.FinancialYear,
                        CreatedByUserId = _sessionModel.UserId,
                        UpdatedByUserId = _sessionModel.UserId,
                    };

                    unitWiseMarksEntryAPIModelList.Add(unitWiseMarksEntryAPIModel);



                }

                SaveStudentMarks(unitWiseMarksEntryAPIModelList);
            }
            else
            {
                snackBar.Add("Please find student details", Severity.Warning);
            }
           
        }
       
        public APIReturnModel aPIReturnModel;
        private async void SaveStudentMarks(List<UnitWiseMarksEntryAPIModel> unitWiseMarksEntryAPIModelList)
        {
            try
            {
                if (unitWiseMarksEntryAPIModelList[0].OperationType != "NA")
                {
                    aPIReturnModel = (await examMasterSetupService.CRUD_UnitWiseMarksEntry(unitWiseMarksEntryAPIModelList));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                        if(unitWiseMarksEntryAPIModelList.Count > 1) 
                        { 
                            unitWiseMarksEntryAPIModelList.Clear();
                            studentUnitMarksDetails.Clear();
                            _sfStudentMarksDetails.Refresh();
                            StateHasChanged();
                        }

                        //Topic_Input_Para_Model topic_Input_Para_Model = new Topic_Input_Para_Model()
                        //{
                        //    subjectId = 0,
                        //    classId = 0,
                        //    unitID = 0,
                        //    chapterID = 0,
                        //    schoolCode = _sessionModel.SchoolCode,
                        //    financialYear = _sessionModel.FinancialYear,
                        //    reportType = AIS.Model.GeneralConversion.ReportType.All,

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

        [Inject]
        public IJSRuntime jsRuntime { get; set; }
        [Inject]
        private IConfiguration _config { get; set; }
        public async Task marksreportAsync()
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
                       $"&UserID={_sessionModel.UserId}" +
                       $"&ReportType={"StudentWiseMarksDetails"}");
                await jsRuntime.InvokeAsync<object>("open", fullUrl, "_blank");
            }
            catch(Exception ex) 
            {
                Console.Write(ex.Message.ToString());

             }

        }
    }
}