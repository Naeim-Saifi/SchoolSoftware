using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.TimeTable;
using AdminDashboard.Server.Service.Interface.Employee;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.TimeTable;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.Employee;
using AIS.Model.GeneralConversion;
using AIS.Model.Mapping;
using AIS.Model.MasterData;
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


namespace AdminDashboard.Server.Pages.TimeTable
{
    public class TimetableSetupBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public bool IsVisible { get; set; } = false;
        public ViewTimeTableModel viewTimeTableModel { get; set; }
        /*Call Class service*/
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();
        /*Section */
        [Inject]
        public IMasterSectionService masterSectionService { get; set; }
        public List<MasterSectionListModel> masterSectionListModel = new List<MasterSectionListModel>();
        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }
        public SubjectDetailsModel subjectDetailsModel;
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        public IEnumerable<MapsubjectwithClassModel> subjectList;
        /*Map Teacher List*/
        [Inject]
        public IEmployeeService employeeService { get; set; }
        public List<EmployeeDetailsModel> employeeDetailsModels = new List<EmployeeDetailsModel>();
        [Inject]
        public IMapTeacherWithSubjectService mapTeacherWithSubjectService { get; set; }
        public List<MapSubjectWithTeacherList> subjectWithTeacherList = new List<MapSubjectWithTeacherList>();

        [Inject]
        public ITimeTableService _itimetableService { get; set; }
        public List<PeriodListModel> periodListModels = new List<PeriodListModel>();
        public List<DayModelList> dayModelLists = new List<DayModelList>();


       
        public List<TimeTableListModel> timeTableListModels = new List<TimeTableListModel>();
        public SfGrid<TimeTableListModel> viewTimeTable_Grid;
        public string[] GroupedColumns = new string[] { "className" };

       public SfDialog Dialog;
        public object SelectedData;
        public bool flag = true;
        public void Closed()
        {
            flag = true;
        }

        public void OkClick()
        {
            ViewTimeTableModel viewTimeTableModel = new ViewTimeTableModel()
            {
                //MasterClassId = Convert.ToInt32(0),
                //MasterSectionId = Convert.ToInt32(0),
                //MasterSubjectId = Convert.ToInt32(0),
                //MasterTeacherId = Convert.ToInt32(0),
                //PeriodId = Convert.ToInt32(0),
                //DayId = Convert.ToInt32(0),
                //ActiveStatus = 2,
                //CreatedByUserId = _sessionModel.UserId,
                //FinancialYear = _sessionModel.FinancialYear,
                //OperationType = OperationAction.Delete,
                //SchoolCode = _sessionModel.SchoolCode,
                //TimeTableId = timetableId,
                //UpdatedByUserId = 0,
            };

            //string msg = (await _itimetableService.AddUpdateTimeTable(viewTimeTableModel)).message.ToString();
            //snackBar.Add(msg, Severity.Success);
            SaveTimeTableDetails(viewTimeTableModel);
            StateHasChanged();
            viewTimeTable_Grid.Refresh();
            //viewTimeTable_Grid.DeleteRecord();   //Delete the record programmatically while clicking OK button.
            Dialog.Hide();
        }
        public void CancelClick()
        {
            Dialog.Hide();
        }
        public int timetableId = 0;
        public void RowSelectHandler(RowSelectEventArgs<TimeTableListModel> args)
        {

            timetableId = args.Data.timeTableID;
            SelectedData = args.Data.className + "-" + args.Data.sectionName+"-"+ args.Data.teacherName;


        }
        public void OnActionBegin(ActionEventArgs<TimeTableListModel> Args)
        {
            if (Args.RequestType.ToString() == "Delete" && flag)
            {
                Args.Cancel = true;  //Cancel default delete action.
                Dialog.Show();
                flag = false;
            }
            
        }

        //public void OnOpenPopup(Syncfusion.Blazor.DropDowns.PopupEventArgs args)
        //{
        //    args.Popup.OffsetY = 1;
        //}

        public void ToolClick(Syncfusion.Blazor.Navigations.ClickEventArgs Args)
        {
            if (Args.Item.Text == "Add")
            {
                Args.Cancel = true;
                ShowDialog();
                flag = false;
                //NavigationManager.NavigateTo("addRecord/0");
            }
            else if (Args.Item.Text == "Edit")
            {
                //NavigationManager.NavigateTo($"editRecord/{SelectedRecord.OrderID}");
            }
        }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                ViewTimeTableModel viewTimeTableModel = new ViewTimeTableModel()
                {
                    //MasterClassId = Convert.ToInt32(dropdownModel.ClassName),
                    //MasterSectionId = Convert.ToInt32(dropdownModel.SectionName),
                    //MasterSubjectId = Convert.ToInt32(dropdownModel.SubjectName),
                    //MasterTeacherId = Convert.ToInt32(dropdownModel.TeacherName),
                    //PeriodId = Convert.ToInt32(dropdownModel.PeriodName),
                    //DayId = Convert.ToInt32(dropdownModel.DayName),
                    //ActiveStatus = 1,
                    //CreatedByUserId = _sessionModel.UserId,
                    //FinancialYear = _sessionModel.FinancialYear,
                    //OperationType = OperationAction.Add,
                    //SchoolCode = _sessionModel.SchoolCode,
                    //TimeTableId = 0,
                    //UpdatedByUserId = 0,
                };

                //string msg = (await _itimetableService.AddUpdateTimeTable(viewTimeTableModel)).message.ToString();
                //snackBar.Add(msg, Severity.Success);
                SaveTimeTableDetails(viewTimeTableModel);
            }
            else
            {
                // Form has invalid inputs.
            }


            StateHasChanged();
        }
        [Inject]
        public ISnackbar snackBar { get; set; }

        public APIReturnModel aPIReturnModel;
        private async void SaveTimeTableDetails(ViewTimeTableModel viewTimeTableModel)
        {
            try
            {
                    aPIReturnModel = (await _itimetableService.AddUpdateTimeTable(viewTimeTableModel));

                    if(aPIReturnModel.status==false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                    else
                    {                       
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                    GettimetableList();
                    StateHasChanged();
                        viewTimeTable_Grid.Refresh();
                }
            }
            catch(Exception ex)
            {

            }

        }
        
        
        public DialogEffect AnimationEffect = DialogEffect.Zoom;

        public DropdownModel dropdownModel = new DropdownModel();

        private async void  GettimetableList()
        {
            //time table list show
            timeTableListModels = (await _itimetableService.GetTimeTableList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();

        }
        protected override async Task OnInitializedAsync()
        {
           
            
            viewTimeTableModel = new ViewTimeTableModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
             GettimetableList();

            //Class List creation
            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();
            masterSectionListModel = (await masterSectionService.GetMasterSectionList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
            //Subject with Class List creation
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
            //Get subject list base on class id
            _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
            // teacher List Details
           // employeeDetailsModels = (await employeeService.GetemployeeDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
            subjectWithTeacherList = (await mapTeacherWithSubjectService.GetMapSubjectWithTeacherList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, 1, ReportType.All)).ToList();

            //period list
            periodListModels = (await _itimetableService.GetPeriodList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();

            //Day List
            dayModelLists = (await _itimetableService.GetDaysNameList()).ToList();
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
        //public void OnOpenPopup(Syncfusion.Blazor.DropDowns.PopupEventArgs  args)
        //{
        //    args.Popup.OffsetY = 1;
        //}
        public SfDialog DialogRef;
        public async Task OpenDialog()
        {
            await this.DialogRef.ShowAsync();
        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "600px";
        }

        public void ExportToExcel()
        {
            this.viewTimeTable_Grid.ExcelExport();
        }
        //public async void OnCommandClicked(CommandClickEventArgs<TimeTableListModel> args)
        //{
        //    // Perform required operations here
        //    string index = args.RowData.timeTableID.ToString();

           
        //}
        
        public void ShowDialog()
        {
            IsVisible = true;
        }

    }
}
