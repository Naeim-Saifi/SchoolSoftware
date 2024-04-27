using AdminDashboard.Server.Models.Employee;
using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.Employee;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.Employee;
using AIS.Model.GeneralConversion;
using AIS.Model.Mapping;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MudBlazor;
using Syncfusion.Blazor.Popups;

namespace AdminDashboard.Server.Pages.Employee
{
    public class MapClassAndSubjectwithEmployeeBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public class DropdwonList
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }

        public MapClassAndSubjectwithEmployeeViewModel mapClassAndSubjectwithEmployeeViewModel { get; set; }

        /*Call Class service*/
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();
        /*Map Class with Subject*/

        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }
        public SubjectDetailsModel subjectDetailsModel;
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();

        /*Section */
        [Inject]
        public IMasterSectionService masterSectionService { get; set; }
        public List<MasterSectionListModel> masterSectionListModel = new List<MasterSectionListModel>();

        /*Map Teacher List*/
        [Inject]
        public IEmployeeService employeeService { get; set; }
        public List<EmployeeDetailsModel> employeeDetailsModels = new List<EmployeeDetailsModel>();
        [Inject]
        public IMapTeacherWithSubjectService mapTeacherWithSubjectService { get; set; }
        public List<MapSubjectWithTeacherList> subjectWithTeacherList = new List<MapSubjectWithTeacherList>();

        //For data binding 
        public SfGrid<MapSubjectWithTeacherList> Grid;
        public List<MapSubjectWithTeacherList> EmployeeList;
        protected override async Task OnInitializedAsync()
        {
            mapClassAndSubjectwithEmployeeViewModel = new MapClassAndSubjectwithEmployeeViewModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Class List creation
            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();
            //Subject with Class List creation
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
            //Get subject list base on class id
            _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;

            masterSectionListModel = (await masterSectionService.GetMasterSectionList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
            // teacher List Details
            //employeeDetailsModels = (await employeeService.GetemployeeDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
            subjectWithTeacherList = (await mapTeacherWithSubjectService.GetMapSubjectWithTeacherList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, 1, ReportType.All)).ToList();
        }

        [Inject]
        public ISnackbar snackBar { get; set; }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                // Form has valid inputs.
                MapSubjectWithTeacherModel map = new MapSubjectWithTeacherModel()
                {
                    TeacherUserId = mapClassAndSubjectwithEmployeeViewModel.TeacherId,
                    MasterClassId = mapClassAndSubjectwithEmployeeViewModel.ClassId,
                    MasterSectionId = mapClassAndSubjectwithEmployeeViewModel.SectionID,
                    MasterSubjectId = mapClassAndSubjectwithEmployeeViewModel.SubjectID,
                    FinancialYear = _sessionModel.FinancialYear,
                    OperationType = OperationAction.Add,
                    SchoolCode = _sessionModel.SchoolCode,
                    ActiveStatus = 1,
                    SubjectTeacherMappingId = 0

                };
                if (map.TeacherUserId == 0 || map.MasterClassId == 0 || map.MasterSectionId == 0 || map.MasterSubjectId == 0)
                {
                    //raise error for validation
                }
                else if (map.SubjectTeacherMappingId == 0)
                {
                    map.OperationType = OperationAction.Add;
                }
                else
                {
                    map.OperationType = OperationAction.Update;
                }

                string msg = (await mapTeacherWithSubjectService.AddUpdateMapSubjectWithTeacher(map)).message.ToString();
                //string msg = (await employeeService.AddUpdateEmployee(employeeModel)).message.ToString();

                if (msg == "Employee Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    subjectWithTeacherList = (await mapTeacherWithSubjectService.GetMapSubjectWithTeacherList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, 1, ReportType.All)).ToList();
                }

                map.TeacherUserId = null;
                map.MasterClassId = null;
                map.MasterSectionId = null;
                map.MasterSubjectId = null;
            }
            else
            {
                // Form has invalid inputs.
            }


            StateHasChanged();
        }
        public IEnumerable<MapsubjectwithClassModel> subj;
        public void OnChangeClass(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int, MasterClassListModel> args)
        {
            try
            {
                if (args.ItemData.masterClassId.ToString() != null)
                {
                    int classid = args.ItemData.masterClassId;

                    subj = _mapsubjectlistModel.Where(e => e.masterClassId == classid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public string[] GroupedColumns = new string[] { "teacherUserId" };
        public async void SubjectTeacherlist()
        {
            //Subject with teacher list
            subjectWithTeacherList = (await mapTeacherWithSubjectService.GetMapSubjectWithTeacherList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, 1, ReportType.All)).ToList();
        }
        public MapSubjectWithTeacherList RowDetails { get; set; }
        public async void OnCommandClicked(CommandClickEventArgs<MapSubjectWithTeacherList> args)
        {
            RowDetails = args.RowData;

            MapSubjectWithTeacherModel map = new MapSubjectWithTeacherModel()
            {
                TeacherUserId = RowDetails.teacherUserId,
                MasterClassId = RowDetails.masterClassId,
                MasterSectionId = RowDetails.masterSectionId,
                MasterSubjectId = RowDetails.masterSubjectId,
                //OperationType = OperationAction.Delete,
                SchoolCode = _sessionModel.SchoolCode,
                //ActiveStatus = 1,
                SubjectTeacherMappingId = RowDetails.subjectTeacherMappingId

            };
            // IsVisible = true;
            string action = args.CommandColumn.Title.ToString();
            if (action == "Delete")
            {
                map.OperationType = OperationAction.Delete;
            }
            else
            {
                map.OperationType = OperationAction.ReActive;
            }
            string msg = (await mapTeacherWithSubjectService.AddUpdateMapSubjectWithTeacher(map)).message.ToString();

            if (msg == "Employee Details already exist")
            {
                snackBar.Add(msg, Severity.Error);
            }
            else
            {
                snackBar.Add(msg, Severity.Success);
                subjectWithTeacherList = (await mapTeacherWithSubjectService.GetMapSubjectWithTeacherList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, 1, ReportType.All)).ToList();
            }

        }

        public SfDialog Dialog;
        public object SelectedData;
        public bool flag = true;
        public void OnActionBegin(ActionEventArgs<MapSubjectWithTeacherList> Args)
        {
            if (Args.RequestType.ToString() == "Delete" && flag)
            {
                Args.Cancel = true;  //cancel default delete action
                Dialog.Show();
                flag = false;
            }
        }
        public void RowSelectHandler(RowSelectEventArgs<MapSubjectWithTeacherList> Args)
        {
            SelectedData = Args.Data.subjectTeacherMappingId;
        }
        public void OkClick()
        {
            Grid.DeleteRecord();   //delete the record programatically while clikcing OK button
            Dialog.Hide();
        }
        public void CancelClick()
        {
            Dialog.Hide();
        }
        public void Closed()
        {
            flag = true;
        }

        public void ExportToExcel()
        {
            this.Grid.ExcelExport();
        }
        public bool IsVisible { get; set; } = false;

        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public void CloseDialog()
        {
            this.IsVisible = false;
        }
        public void OpenDialog()
        {

            mapClassAndSubjectwithEmployeeViewModel.ClassId = 0;
            mapClassAndSubjectwithEmployeeViewModel.SectionID = 0;
            mapClassAndSubjectwithEmployeeViewModel.TeacherId = 0;
            mapClassAndSubjectwithEmployeeViewModel.SubjectID = 0;
            this.IsVisible = true;
        }

    }
}
