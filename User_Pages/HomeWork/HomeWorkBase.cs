using AdminDashboard.Server.API_Service.Interface.HomeWork;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.Service.Interface.HomeWork;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.HomeWorkSetUp;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.OData.Edm;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.HomeWork
{
    public class HomeWorkBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IHomeWorkSetupService homeWorkSetupService { get; set; }
        //list Model
        
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }
        [Inject]
        public IHomeWorkService  homeWorkService { get; set; }
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        //list Model
        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();
        public List<Master_Section_List_Output_Model> _sectionList = new List<Master_Section_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> _subjectList = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public List<Unit_Output_Model> _unitList = new List<Unit_Output_Model>();
        public List<Chapter_Output_Model> _chapterList = new List<Chapter_Output_Model>();
        public List<Topic_Output_Model> _topicList = new List<Topic_Output_Model>();
        public List<HomeWorkType_OutPut_Model> _homeWorkTypeList = new List<HomeWorkType_OutPut_Model>();
        public List<HomeWork_List_Output_Model>  _homeWorkList_OutPut_Models = new List<HomeWork_List_Output_Model>();

        //data binding for blazor page
        public unitWiseMarksEntryViewModel  unitWiseMarksEntryViewModel = new unitWiseMarksEntryViewModel();
        //public SyllabusStatusModel syllabusStatusApiModel = new SyllabusStatusModel();
        public HomeWorkSetUpModel homeworkAPIModel = new HomeWorkSetUpModel();

        public APIReturnModel aPIReturnModel;
        public SessionModel _sessionModel;
        public string OperationType = "";

        //public DateTime? fromValue { get; set; } = DateTime.Now;
        //public DateTime? toValue { get; set; } = DateTime.Now;
        public DateTime? fromDate { get; set; } = DateTime.Now;
        public DateTime? toDate { get; set; } = DateTime.Now;

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<HomeWork_List_Output_Model>  _homeworkGrid;

        public List<string> toolBarItems = (new List<string>() { "Add HomeWork", "Print", "Search", "ColumnChooser" });
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;

        // public List<SyllabusStatus> _statusList;

        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

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

            HomeWork_Type_Input_Para_Model homeWork_Type_Input_Para_Model = new HomeWork_Type_Input_Para_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                HomeWorkTypeId = 0,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            _homeWorkTypeList = (await homeWorkSetupService.GET_HomeWorkType_LIST(homeWork_Type_Input_Para_Model)).ToList();
           
            HomeWork_List_Input_Para_Model homeWork_List_Input_Para_Model = new HomeWork_List_Input_Para_Model()
            {
                classId = _classID,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All,
                userId = _sessionModel.UserId,
                fromDate = Date.Now.ToString(),
                subjectId = _subjectID,
                toDate = Date.Now.ToString(), 
                userRoleId= _sessionModel.RoleId

            };

            _homeWorkList_OutPut_Models = (await homeWorkSetupService.GET_HOMEWORK_LIST(homeWork_List_Input_Para_Model)).ToList();

        }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                homeworkAPIModel = new HomeWorkSetUpModel()
                {


                    homeWorkID = unitWiseMarksEntryViewModel.homeWorkID,
                    classID =Convert.ToInt16(unitWiseMarksEntryViewModel.classID),
                    sectionId=Convert.ToInt16(unitWiseMarksEntryViewModel.sectionId),
                     subjectID = Convert.ToInt16(unitWiseMarksEntryViewModel.subjectID),
                     unitId  = Convert.ToInt16(unitWiseMarksEntryViewModel.unitId),
                    chapterId = Convert.ToInt16(unitWiseMarksEntryViewModel.chapterId),
                    topicID = Convert.ToInt16(unitWiseMarksEntryViewModel.topicID),
                    homeWorkTypeid= Convert.ToInt16(unitWiseMarksEntryViewModel.homeWorkTypeid),
                    homeWorkTitle = unitWiseMarksEntryViewModel.homeWorkTitle,
                    homeworkDescription = unitWiseMarksEntryViewModel.homeworkDescription,
                    fromDate=(DateTime) fromDate,
                    toDate = (DateTime)toDate,
                  
                    teacherId=_sessionModel.UserId,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    homeworkAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    homeworkAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    homeworkAPIModel.OperationType = OperationAction.Delete;
                }
                HomeWorkSave(homeworkAPIModel);
            }
        }
        private async void HomeWorkSave(HomeWorkSetUpModel HomeWorkSave)
        {
            try
            {
                if (HomeWorkSave.OperationType != "NA")
                {
                    aPIReturnModel = (await homeWorkSetupService.CRUD_HomeWorkSetup(HomeWorkSave));
                    if (HomeWorkSave.OperationType == OperationAction.Delete)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                    else if (HomeWorkSave.OperationType != OperationAction.Add)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Info);
                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                    }
                }
                else
                {
                    snackBar.Add(aPIReturnModel.message, Severity.Error);
                }

                HomeWork_List_Input_Para_Model homeWork_List_Input_Para_Model = new HomeWork_List_Input_Para_Model()
                {
                    classId = _classID,
                    schoolCode = _sessionModel.SchoolCode,
                    financialYear = _sessionModel.FinancialYear,
                    reportType = ReportType.All,
                    userId = _sessionModel.UserId,
                    fromDate = Date.Now.ToString(),
                    subjectId = _subjectID,
                    toDate = Date.Now.ToString(),
                    userRoleId = _sessionModel.RoleId
                };

                _homeWorkList_OutPut_Models = (await homeWorkSetupService.GET_HOMEWORK_LIST(homeWork_List_Input_Para_Model)).ToList();

                ClearData();
                StateHasChanged();
            }

            catch (Exception ex)
            {

            }
        }
        private void ClearData()
        {
            unitWiseMarksEntryViewModel.classID = null;
            unitWiseMarksEntryViewModel.sectionId = null;
            unitWiseMarksEntryViewModel.subjectID = null;
            unitWiseMarksEntryViewModel.unitId = null;
            unitWiseMarksEntryViewModel.chapterId = null;
            unitWiseMarksEntryViewModel.topicID = null;
            unitWiseMarksEntryViewModel.homeWorkTypeid = null;
            unitWiseMarksEntryViewModel.homeWorkTitle = null;
            unitWiseMarksEntryViewModel.homeworkDescription = null;
           
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
                        reportType = ReportType.ClassId,
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

                    Chapter_Input_Para_Model chapter_Input_Para_Model = new Chapter_Input_Para_Model()
                    {
                        classId = _classID,
                        unitID = _unitID,
                        subjectId = _subjectID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.GetByMasterId,
                    };
                    _chapterList = (await syllabusService.Get_Chapter_List(chapter_Input_Para_Model)).ToList();
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
                        reportType = ReportType.GetByMasterId,
                    };
                    _topicList = (await syllabusService.Get_Topic_List(chapter_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        [Inject]
        NavigationManager navigationManager { get; set; }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add HomeWork")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add HomeWork Details";
                OperationType = "Add";
                HeaderText = "Add HomeWork";
                ddEnable = true;
                //syllabusStatusViewModel.classID = null;
                //syllabusStatusViewModel.subjectID = null;
                //syllabusStatusViewModel.unitId = null;
                //syllabusStatusViewModel.chapterId = null;
                //syllabusStatusViewModel.topicID = null;
                //syllabusStatusViewModel.remarks = null;

            }
        }
        public void EditHomeWork(CommandClickEventArgs<HomeWork_List_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            string _homeWorkID = string.Empty;
            string classID = "";
            string sectionID = "";
            int testId = Convert.ToInt16(args.RowData.homeWorkID);
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                IsVisible = true;
                DialogHeaderName = "Update Homework Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
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
            else if(buttontext == "Remarks")
            {
                _homeWorkID = args.RowData.homeWorkID.ToString();
                classID = args.RowData.classId.ToString();
                sectionID = args.RowData.sectionId.ToString();
                navigationManager.NavigateTo($"/HomeWork/AddHomeWorkRemarks/{_homeWorkID}/{classID}/{sectionID}");
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Homework Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
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

            };
        }

    }

}

