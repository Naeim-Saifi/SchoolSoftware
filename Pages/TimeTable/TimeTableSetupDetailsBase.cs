using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.TimeTable;
using AdminDashboard.Server.Service.Interface.Employee;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterData;
using AdminDashboard.Server.Service.Interface.TimeTable;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.FrontEnd_07062021.Service.Implementation;
using AIS.Model;
using AIS.Model.Employee;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.TimeTable
{
    public class TimeTableSetupDetailsBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public bool IsVisible { get; set; } = false;
        public ViewTimeTableModel viewTimeTableModel = new ViewTimeTableModel();
        [Inject]
        public IMasterDataService masterDataService { get; set; }
        public List<MasterClassListModel> _masterClassList = new List<MasterClassListModel>();
        public List<MasterSectionListModel> _masterSectionList = new List<MasterSectionListModel>();
        [Inject]
        public ITimeTableService _itimetableService { get; set; }
        public List<PeriodListModel> periodListModels = new List<PeriodListModel>();
        public List<DayModelList> dayModelLists = new List<DayModelList>();
        public List<TimeTableListModel> timeTableListModels = new List<TimeTableListModel>();
        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }
        public SubjectDetailsModel subjectDetailsModel;
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        public IEnumerable<MapsubjectwithClassModel> subjectList;
        [Inject]
        public IEmployeeService employeeService { get; set; }
        public List<EmployeeDetailsModel>  employeeList = new List<EmployeeDetailsModel>();
        private async void GettimetableList()
        {
            //time table list show
            timeTableListModels = (await _itimetableService.GetTimeTableList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();

        }
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Time Table", "Print", "Search" });

        public SfGrid<TimeTableListModel> sfTimeTableList;
        protected override async Task OnInitializedAsync()
        {


            //viewTimeTableModel = new ViewTimeTableModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            GettimetableList();
            //DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            //{
            //    SchoolCode = _sessionModel.SchoolCode,
            //    FinancialYear = _sessionModel.FinancialYear,
            //    OperationType = ReportType.All
            //};
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                Userid = _sessionModel.UserId,
                OperationType = ReportType.All
            };
            //Class List creation
            _masterClassList = (await masterDataService.GetMasterClassList(defaultInputParameterModel)).ToList();
            _masterSectionList = (await masterDataService.GetSectionList(defaultInputParameterModel)).ToList();
            //Subject with Class List creation
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
            //Get subject list base on class id
            _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
            // teacher List Details
            employeeList = (await employeeService.GetemployeeDetails(defaultInputParameterModel)).ToList();
            //subjectWithTeacherList = (await mapTeacherWithSubjectService.GetMapSubjectWithTeacherList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, 1, ReportType.All)).ToList();

            //period list
            periodListModels = (await _itimetableService.GetPeriodList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();

            //Day List
            dayModelLists = (await _itimetableService.GetDaysNameList()).ToList();
        }
        public string HeaderText = string.Empty;
        //for API Model to View model data binding.
       
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public string OperationType = "";
        public string btncss = "";
        public bool ddEnable = true;
        public string DialogHeaderName = string.Empty;
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public void EditTimeTableMaster(CommandClickEventArgs<TimeTableListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Time Table Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                //masterBatchViewModel = new MasterBatchViewModel()
                //{
                //    batchName = args.RowData.batchName,
                //    batchCode = args.RowData.batchCode,
                //    displayOrder = Convert.ToInt16(args.RowData.displayOrder),
                //    masterBatchId = args.RowData.masterBatchId
                //};
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Time Table Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                //masterBatchViewModel = new MasterBatchViewModel()
                //{
                //    batchName = args.RowData.batchName,
                //    batchCode = args.RowData.batchCode,
                //    displayOrder = Convert.ToInt16(args.RowData.displayOrder),
                //    masterBatchId = args.RowData.masterBatchId
                //};
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

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
                DialogHeaderName = "Add New Time Table Details";
                OperationType = "Add";
                HeaderText = "Add Time Table";
                
            }
        }
        public void OnChangeClass(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MasterClassListModel> args)
        {
            try
            {
                if (args.ItemData.masterClassId.ToString() != null)
                {
                    int classid = args.ItemData.masterClassId;

                    subjectList = _mapsubjectlistModel.Where(e => e.masterClassId == classid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                //_periodModel = new PeriodModel()
                //{
                //    PeriodId = _periodViewModel.PeriodId,
                //    PeriodName = _periodViewModel.PeriodName.Trim().ToUpper(),
                //    StartTime = _periodViewModel.StartTime.Trim(),
                //    EndTime = _periodViewModel.EndTime.Trim(),
                //    ActiveStatus = 1,
                //    CreatedByUserId = _sessionModel.UserId,
                //    SchoolCode = _sessionModel.SchoolCode,
                //    FinancialYear = _sessionModel.FinancialYear,
                //};

                //if (OperationType == "Add")
                //{
                //    _periodModel.OperationType = OperationAction.Add;
                //}
                //else if (OperationType == "Update")
                //{
                //    _periodModel.OperationType = OperationAction.Update;
                //}
                ////Delete Operation
                //else
                //{
                //    _periodModel.OperationType = OperationAction.Delete;
                //}
                //SavePeriodDetails(_periodModel);
            };
        }
        public APIReturnModel aPIReturnmodel;


        public async void SavePeriodDetails(PeriodModel periodModel)
        {
            //aPIReturnmodel = (await _itimetableService.AddUpdatePeriodDetails(periodModel));
            //if (aPIReturnmodel.status == false)
            //{
            //    if (periodModel.OperationType == OperationAction.Delete)
            //    {
            //        snackBar.Add(aPIReturnmodel.message, Severity.Error);
            //    }
            //    else if (periodModel.OperationType != OperationAction.Add)
            //    {
            //        snackBar.Add(aPIReturnmodel.message, Severity.Info);
            //    }
            //    else
            //    {
            //        snackBar.Add(aPIReturnmodel.message, Severity.Success);
            //    }

            //    HeaderText = "Save Record";
            //    GetPeriodList();
            //    ClearAllFields();
            //}
            //else
            //{
            //    snackBar.Add(aPIReturnmodel.message, Severity.Error);
            //}
        }
        private void ClearAllFields()
        {

            //_periodViewModel.PeriodName = null;
            //_periodViewModel.StartTime = null;
            //_periodViewModel.EndTime = null;
        }
    }
}
