using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.RemarksDetails;
using AdminDashboard.Server.Models.TimeTable;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.TimeTable;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.Student;
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

namespace AdminDashboard.Server.Pages.RemarksDetails
{
    public class RemarksDetailsBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();
        [Inject]
        public IMasterSectionService masterSectionService { get; set; }
        public List<MasterSectionListModel> masterSectionListModels = new List<MasterSectionListModel>();
        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }
        public SubjectDetailsModel subjectDetailsModel;
        public List<MasterSubjectListModel> mapSubjectList = new List<MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        [Inject]
        public IStudentListService studentService { get; set; }

        public StudentDetailsListModel studentDetailsListModel { get; set; }

        public List<StudentListModel> studentListModel = new List<StudentListModel>();
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, 0, 1, ReportType.GetTeacherClass)).ToList();
            masterSectionListModels = (await masterSectionService.GetMasterSectionList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, 0, 0, ReportType.GetTeacherSection)).ToList();
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, ReportType.SubjectTeacherList));
            mapSubjectList = subjectDetailsModel.masterSubjectListModels;
            _mapsubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
           

        }

        //public SfGrid<StudentListModel> studentdetails_grid;//{ get; set; }
        public void OnCommandClicked(CommandClickEventArgs<StudentListModel> args)
        {
            if (args.CommandColumn.ButtonOption.IconCss == "e-icons e-update")
            {
                args.RowData.studentName = args.RowData.studentName.ToUpper();
                //studentdetails_grid.Refresh();
            }
            else
            {
                //args.RowData.FirstName = args.RowData.FirstName.ToLower();
                //employeeGrid.Refresh();
            }
        }




        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public SfDialog DialogRef;
        public bool IsVisible { get; set; }
        public async Task OpenDialog()
        {
            await this.DialogRef.ShowAsync();
        }
        public async Task CloseDialog()
        {
            await this.DialogRef.HideAsync();
        }

        public void onOpen(BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "600px";
        }
        public void getClassDetails()
        {
            this.IsVisible = true;
        }

        public UserInputViewRemarksModel _userInputRemarksModel = new UserInputViewRemarksModel();

        public class RemarksList
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }

        public List<RemarksList> _remarksLists = new List<RemarksList>()
        {
        new RemarksList(){ ID= 1, Text= "Home Work Type Remarks" },
        new RemarksList(){ ID= 2, Text= "Exam Type Remarks" },
        new RemarksList(){ ID= 3, Text= "Any Other Type" }
        };

        public class RemarksDescriptionListModel
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }
        public List<RemarksDescriptionListModel>  remarksDescriptionListModels = new List<RemarksDescriptionListModel>()
        {
        new RemarksDescriptionListModel(){ ID= 1, Text= "Home Work Type Remarks" },
        new RemarksDescriptionListModel(){ ID= 2, Text= "Exam Type Remarks" },
        new RemarksDescriptionListModel(){ ID= 3, Text= "Any Other Type" }
        };

        public int classid = 0;
        public IEnumerable<MapsubjectwithClassModel> subj;
        public void OnChangeClass(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, MasterClassListModel> args)
        {
            try
            {
                if (args.ItemData.masterClassId.ToString() != null)
                {
                    classid = args.ItemData.masterClassId;

                    subj = _mapsubjectlistModel.Where(e => e.masterClassId == classid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public async void OnValidSubmit(EditContext contex)
        {
            int xClassId = 0;
            bool isValid = contex.Validate();
            if (isValid)
            {
                this.IsVisible = false;
                UserInputRemarksModel userInputRemarksModel = new UserInputRemarksModel()
                {
                    ClassName =Convert.ToInt32(_userInputRemarksModel.ClassName),
                    SectionName =Convert.ToInt32(_userInputRemarksModel.SectionName),
                };

                studentDetailsListModel = (await studentService.GetStudentDetailsList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, userInputRemarksModel.ClassName, userInputRemarksModel.SectionName, 0, ReportType.GetClassAndSectionList));
                studentListModel = studentDetailsListModel.studentListModels;
                //_userInputRemarksModel.ClassName = null;
                //_userInputRemarksModel.SectionName = null;
                //_userInputRemarksModel.SubjectName = null;
                //_userInputRemarksModel.RemarksType = null;
            }
        }
        
    }
}
