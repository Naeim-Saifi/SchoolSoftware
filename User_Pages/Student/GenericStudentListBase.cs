using AdminDashboard.Server.API_Service.Interface.StudentSetUp;
using AdminDashboard.Server.Models.TimeTable;
using AIS.Data.RequestResponseModel.StudentSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AdminDashboard.Server.User_Pages.Student
{
    public class GenericStudentListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IStudentSetupService _studentSetupService { get; set; }

        public SessionModel _sessionModel;

        public long _motherMobileNo_NotFound { get; set; }
        public long _sRNNumber_NotFound { get; set; }
        public long _aadharNumber_NotFound { get; set; }
        public long _withdrawalNumberNotFound { get; set; }

        public List<Class_And_Section_Count> _classSectionCount_List = new List<Class_And_Section_Count>();
        public SfGrid<Class_And_Section_Count> _sfgridclassSectionCount;

        public List<Student_BusStop_Count> _BusStopCounts_List = new List<Student_BusStop_Count>();
        public SfGrid<Student_BusStop_Count> _sfGridBusStop;

        public List<Student_CasteCategory_Count> _student_CasteCategory_List = new List<Student_CasteCategory_Count>();
        public SfGrid<Student_CasteCategory_Count> _sfGridcastCategory;

        public List<Student_Status_Count> _studentStatus_list = new List<Student_Status_Count>();
        public SfGrid<Student_Status_Count> _sfGridStudentStatus;

        public List<Student_Religion_Count> _religionCount_list = new List<Student_Religion_Count>();
        public SfGrid<Student_Religion_Count> _sfGridReligion;

        public List<Student_Gender_Count> _Gender_Counts_List = new List<Student_Gender_Count>();
        public SfGrid<Student_Gender_Count> sfGenderCount;

        public List<Student_FeeCategory_Count> _FeeCategory_Counts_list = new List<Student_FeeCategory_Count>();
        public SfGrid<Student_FeeCategory_Count> sfFeecategoryCount;

        public List<Student_BloodGroup_Count> _bloodgroupcount_list = new List<Student_BloodGroup_Count>();
        public SfGrid<Student_BloodGroup_Count> sfbloodGroupCount;

        //list Model
        public StudentGeneric_Output_Model  _studentGeneric_Output_Model;

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            StudentGeneric_Input_Para_Model studentGeneric_Input_Para_Model = new StudentGeneric_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                iD = 0,
                reportType = ReportType.All
            };
            _studentGeneric_Output_Model = (await _studentSetupService.get_StudentGenericData_List(studentGeneric_Input_Para_Model));
           
            _classSectionCount_List = _studentGeneric_Output_Model._class_And_Section_Counts;
            _BusStopCounts_List = _studentGeneric_Output_Model._BusStop_Counts;
            _student_CasteCategory_List = _studentGeneric_Output_Model._CasteCategory_Counts;
            _studentStatus_list = _studentGeneric_Output_Model._StudentStatus_Counts;
            _religionCount_list = _studentGeneric_Output_Model._Religion_Counts;
            _Gender_Counts_List = _studentGeneric_Output_Model._Gender_Counts;
            _FeeCategory_Counts_list = _studentGeneric_Output_Model._FeeCategory_Counts;
            _bloodgroupcount_list = _studentGeneric_Output_Model._BloodGroup_Counts;
             

            _motherMobileNo_NotFound = _studentGeneric_Output_Model.motherMobileNo_NotFound;
            _sRNNumber_NotFound = _studentGeneric_Output_Model.sRNNumber_NotFound;
            _aadharNumber_NotFound = _studentGeneric_Output_Model.aadharNumber_NotFound;
            _withdrawalNumberNotFound = _studentGeneric_Output_Model.withdrawalNumberNotFound;

        }
        
        public void ExportToExcel()
        {
             this._sfgridclassSectionCount.ExcelExport();
        }
        public void ExportToExcel_StopnName()
        {
            this._sfGridBusStop.ExcelExport();
        }
        public void ExportToExcel_CasteCategory()
        {
            this._sfGridcastCategory.ExcelExport();
        }
    }
}
