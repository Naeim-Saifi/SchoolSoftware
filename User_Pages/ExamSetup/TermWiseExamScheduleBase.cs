using AdminDashboard.Server.API_Service.Interface.Exam;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AdminDashboard.Server.User_Pages.ExamSetup
{
    public class TermWiseExamScheduleBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IExamMasterSetupService examMasterSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        public List<Exam_Type_List_Output_Model> _examTypeList = new List<Exam_Type_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> _subjectList = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();

        public List<TermWiseExamSchedule_List_Output_Model> _examScheduleList = new List<TermWiseExamSchedule_List_Output_Model>();

        public List<TermWiseExmSetViewModel> _terWiseExamSetList = new List<TermWiseExmSetViewModel>();
        public List<TermWiseExamSchedule_List_Output_Model> _allterWiseExamSetList = new List<TermWiseExamSchedule_List_Output_Model>();
        //view model for databinding.

        public TermWiseExmViewModel termWiseExmViewModel = new TermWiseExmViewModel();
        public Exam_schedule_Details_Model exam_Schedule_Details_Model { get; set; }

        public List<Exam_schedule_Details_Model> exam_Schedule_Details_Models = new List<Exam_schedule_Details_Model>();

        public List<string> toolBarPendingFee = (new List<string>() { "Marks Entry", "ExcelExport", "Collapse All", "Expand All", "Print", "Search", "ColumnChooser" });
        public List<string> toolbarExam = (new List<string>() { "Add Exam Schedule", "ExcelExport", "Collapse All", "Expand All", "Print", "Search" });
        public List<string> toolBarItems = (new List<string>() {   "Print", "Search" });
        public SessionModel _sessionModel;
        public string[] GroupedColumns = new string[] { "fatherContactNumber" };
        public DialogEffect AnimationEffect = DialogEffect.Zoom;

        public int pageSize = 50;
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

        //time formate code
        public char PromptCharacter { get; set; } = '_';
        public class PromptCharTypes
        {
            public string ID { get; set; }
            public char Text { get; set; }
        }
        private int? Index { get; set; } = 0;
        private List<PromptCharTypes> Data = new List<PromptCharTypes>()
        {
            new PromptCharTypes(){ ID= "1", Text= '_' },
            new PromptCharTypes(){ ID= "2", Text= '#' },
            new PromptCharTypes(){ ID= "3", Text= '*' }
        };
        private void OnPromptCharChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, PromptCharTypes> args)
        {
            this.PromptCharacter = args.ItemData.Text;
        }
        public Dictionary<string, string> customMask = new Dictionary<string, string>()
        {
            {"P" , "P,p,A,a" },
            {"M" , "m,M" }
        };

        //end time formate code

        public SfGrid<TermWiseExamSchedule_List_Output_Model> _sfterWiseExamSetList;
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
        public SfGrid<TermWiseExmSetViewModel> sfExamSchedule;
        public void toolBarItemsToolBar(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Exam Schedule")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Exam Configuration";
                OperationType = "Add";
                HeaderText = "Add Exam Schedule";
                ddEnable = true;
                

            }
            else if (args.Item.Text == "Collapse All")
            {
                this._sfterWiseExamSetList.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this._sfterWiseExamSetList.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {
                this._sfterWiseExamSetList.ExportToExcelAsync();

                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Exam Schedule Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "ExamScheduleList.xlsx";
                this._sfterWiseExamSetList.ExcelExport(ExcelProperties);
            }
            else
            {
                //this.sfExamSchedule.Columns[0].AutoFit = true;
            }
        }
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

            Exam_Type_List_Input_Model exam_Type_List_Input_Model = new Exam_Type_List_Input_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                examTypeId = 0,
                financialYear = _sessionModel.FinancialYear,
                reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.All
            };
            _examTypeList = (await examMasterSetupService.GET_Exam_Type_MasterLIST(exam_Type_List_Input_Model)).ToList();
            Exam_Schedule_Details_List_Input_Model examSchedule = new Exam_Schedule_Details_List_Input_Model()
            {
                classId = 0,
                sectionId = 0,
                unitId = 0,
                chapterId = 0,
                topicID = 0,
                examTypeId = 0,
                examScheduleID = 0,
                monthId = 0,
                subjectId = 0,
                userRoleId = _sessionModel.RoleId,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = "ALLExamList",
                userId = _sessionModel.UserId
            };

            _allterWiseExamSetList = (await examMasterSetupService.GET_TermWiseExamSchedule_Details_LIST(examSchedule)).ToList();

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

                    // _subjectList = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        int _examTypeID = 0;
        public async Task OnChangeExamType(ChangeEventArgs<string, Exam_Type_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.examTypeId != 0)
                {
                    _examTypeID = args.ItemData.examTypeId;

                    Exam_Schedule_Details_List_Input_Model examSchedule = new Exam_Schedule_Details_List_Input_Model()
                    {
                        classId = _classID,
                        sectionId = 0,
                        unitId = 0,
                        chapterId = 0,
                        topicID = 0,
                        examTypeId = _examTypeID,
                        examScheduleID = 0,
                        monthId = 0,
                        subjectId = 0,
                        userRoleId = _sessionModel.RoleId,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = "TermWiseExamSchedule",
                        userId = _sessionModel.UserId
                    };

                    _examScheduleList = (await examMasterSetupService.GET_TermWiseExamSchedule_Details_LIST(examSchedule)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 
        public async Task FindSubjectDetails()
        {
            if (_terWiseExamSetList.Count > 0)
            {
                _terWiseExamSetList.Clear();
            }
            string sfirstTwoChars = "";
            string efirstTwoChars = "";
            if ((termWiseExmViewModel.startTime.Length==0|| termWiseExmViewModel.endtime.Length==0))
            {
                 sfirstTwoChars ="00:00PM";
                 efirstTwoChars ="00:00PM";
            }
            else
            {
                 sfirstTwoChars = termWiseExmViewModel.startTime.Substring(0, 2);
                string slatTwoChars = termWiseExmViewModel.startTime.Substring(2, 2);
                string sTwoChars = termWiseExmViewModel.startTime.Substring(4, 2);
                sfirstTwoChars = sfirstTwoChars + ":" + slatTwoChars + " " + sTwoChars;

                 efirstTwoChars = termWiseExmViewModel.endtime.Substring(0, 2);
                string elatTwoChars = termWiseExmViewModel.endtime.Substring(2, 2);
                string eTwoChars = termWiseExmViewModel.endtime.Substring(4, 2);
                efirstTwoChars = efirstTwoChars + ":" + elatTwoChars + " " + eTwoChars;
            }
            

            Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
            {
                classID = _classID,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.ClassId,
                userId = _sessionModel.UserId
            };

            _subjectList = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();


            if (_subjectList.Count > 0)
            {

                for (int count = 0; count < _subjectList.Count; count++)
                {
                    _terWiseExamSetList.Add(new TermWiseExmSetViewModel()
                    {
                        classId = _classID,
                        subjectId = _subjectList[count].subjectId,
                        SubjectName = _subjectList[count].subjectName,
                        className = _subjectList[count].className,
                        startTime = sfirstTwoChars,
                        endTime = efirstTwoChars,
                        examDate = DateTime.Now,
                        examID = Convert.ToInt16(termWiseExmViewModel.examType),                        
                        ExamType = termWiseExmViewModel.examType,
                        roomNo = "0",

                    });
                }
            }
            sfExamSchedule.Refresh();
            StateHasChanged();
        }
        [Inject]
        public ISnackbar snackBar { get; set; }
        public async Task CellSavedHandler(CellSaveArgs<TermWiseExmSetViewModel> args)
        {
            double index = await sfExamSchedule.GetRowIndexByPrimaryKey(args.RowData.subjectId);
            int rowIndex = Convert.ToInt32(index);
            string _startTime = args.Data.startTime;
            string _endTime = args.Data.endTime;
            string _roomNo = args.Data.roomNo;
            DateTime _examDate = args.Data.examDate;
            //DateTime date = new DateTime(2024, 1, 28);
            //DateTime _examDate = args.Data.examDate;// new DateTime();
            //CultureInfo culture = CultureInfo.CurrentCulture;

            //string fullDayName = culture.DateTimeFormat.GetDayName(_examDate);


            DateTime date = args.Data.examDate; // Today's date
            DayOfWeek dayOfWeek = date.DayOfWeek;

            //string _fullDayName = dayOfWeek.ToString(); // Monday
            //string abbreviatedDayName = dayOfWeek.ToString("ddd"); // Mon



            _terWiseExamSetList[rowIndex].classId = args.RowData.classId;
            _terWiseExamSetList[rowIndex].subjectId = args.RowData.subjectId;
            _terWiseExamSetList[rowIndex].examID =Convert.ToInt16(termWiseExmViewModel.examType);
            _terWiseExamSetList[rowIndex].startTime = _startTime;
            _terWiseExamSetList[rowIndex].endTime = _endTime;
            _terWiseExamSetList[rowIndex].endTime = _endTime;
            _terWiseExamSetList[rowIndex].roomNo = _roomNo;
            _terWiseExamSetList[rowIndex].examDate = _examDate;
            _terWiseExamSetList[rowIndex].dayname = dayOfWeek.ToString();

            //if (_startTime == "0:0"  )
            //{
            //    await _sfterWiseExamSetList.UpdateCellAsync(index, "startTime", "0:0");               
            //    snackBar.Add("Please Enter Start Time", Severity.Error);
            //}
            //else if(_endTime == "0:0")
            //{
            //    await _sfterWiseExamSetList.UpdateCellAsync(index, "endTime", "0:0"); 
            //    snackBar.Add("Please Enter End Time", Severity.Error);
            //}
            // else
            //{
            await sfExamSchedule.UpdateCellAsync(index, "startTime", _startTime.ToString());
                await sfExamSchedule.UpdateCellAsync(index, "endTime", _endTime.ToString());
                await sfExamSchedule.UpdateCellAsync(index, "roomNo", _roomNo.ToString());
                await sfExamSchedule.UpdateCellAsync(index, "examDate", _examDate.ToString());
            await sfExamSchedule.UpdateCellAsync(index, "dayname", dayOfWeek.ToString());

            //}


            StateHasChanged();
        }
        public APIReturnModel aPIReturnModel;
        public async void SaveExamSchedule()
        {
            Guid myuuid = Guid.NewGuid();
            string _unitWiseMarksID = myuuid.ToString();
            int currentMonthNumber = DateTime.Now.Month;

            for (int count = 0; count < _terWiseExamSetList.Count; count++)
            {
                exam_Schedule_Details_Model = new Exam_schedule_Details_Model()
                {
                     SchoolCode=_sessionModel.SchoolCode,
                     FinancialYear=_sessionModel.FinancialYear, 
                     classId= _terWiseExamSetList[count].classId,
                     sectionId=1,
                     unitId=1,    
                     chapterId=1,
                     topicID=1,
                     examTypeId= _terWiseExamSetList[count].examID,
                     CreatedByUserId= _sessionModel.UserId,
                     examScheduleID= 0,
                     subjectId=_terWiseExamSetList[count].subjectId,
                     startTime= _terWiseExamSetList[count].startTime,
                     endTime = _terWiseExamSetList[count].endTime,
                     roomNo = _terWiseExamSetList[count].roomNo,
                     fromDate= _terWiseExamSetList[count].examDate,
                     monthId= currentMonthNumber,
                     OperationType="Insert",
                     UpdatedByUserId= _sessionModel.UserId,
                };
                exam_Schedule_Details_Models.Add(exam_Schedule_Details_Model);
            }
            aPIReturnModel = await examMasterSetupService.CRUD_TermWiseExamSchedule(exam_Schedule_Details_Models);

            if (aPIReturnModel.status == false)
            {
                snackBar.Add(aPIReturnModel.message, Severity.Success);

                exam_Schedule_Details_Models.Clear();
                sfExamSchedule.Refresh();
                //Exam_Type_List_Input_Model exam_Type_List_Input_Model = new Exam_Type_List_Input_Model()
                //{
                //    schoolCode = _sessionModel.SchoolCode,
                //    examTypeId = 0,
                //    financialYear = _sessionModel.FinancialYear,
                //    reportType = ReportType.All
                //};
                //_examTypeList = (await examMasterSetupService.GET_Exam_Type_MasterLIST(exam_Type_List_Input_Model)).ToList();
                StateHasChanged();
                //ClearData();
            }
            else
            {
                snackBar.Add(aPIReturnModel.message, Severity.Error);
            }
        }
    }
}
