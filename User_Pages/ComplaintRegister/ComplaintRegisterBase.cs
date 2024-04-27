using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.API_Service.Service.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Service.Syllabus;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AIS.Data.RequestResponseModel.ComplaintRegister;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.ComplaintRegister
{
    public class ComplaintRegisterBase : ComponentBase
    {


        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }


        /*Default page setup varible*/
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsDeleteVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";
        public SfGrid<ComplaintRegisterListModel> sfcomplintList;

       

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        /*End default */

        /*Toolbar setup */

        public List<string> toolBarItems = (new List<string>() { "Add Complain", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });
        /*Page Initlize page setup*/

        public List<Master_CLass_List_Output_Model> master_CLass_List = new List<Master_CLass_List_Output_Model>();

        public List<Master_Map_Subject_With_Class_List_Output_Model> map_classwithsubject_List = new List<Master_Map_Subject_With_Class_List_Output_Model>();

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.ClassId


            };
            master_CLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            Topic_Input_Para_Model topic_Input_Para_Model = new Topic_Input_Para_Model()
            {
                subjectId = 0,
                classId = 0,
                unitID = 0,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All,

            };
            topicMasterList = (await syllabusService.Get_Topic_List(topic_Input_Para_Model)).ToList();
        }

        /*Get Data Service*/

        public List<Unit_Output_Model> unitMasterList = new List<Unit_Output_Model>();
        public List<Chapter_Output_Model> chapterMasterList = new List<Chapter_Output_Model>();

        public List<Topic_Output_Model> topicMasterList = new List<Topic_Output_Model>();
        public List<Topic_Output_Model> _topicMasterList = new List<Topic_Output_Model>();

        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    int classid = args.ItemData.classId;

                    Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
                    {
                        classID = classid,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.ClassId,
                        userId = _sessionModel.UserId
                    };

                    map_classwithsubject_List = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public async Task OnChangeSubject(ChangeEventArgs<string, Master_Map_Subject_With_Class_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.masterClassId != 0)
                {
                    int classid = args.ItemData.masterClassId;
                    int subjectID = args.ItemData.subjectId;

                    Unit_Input_Para_Model unit_Input_Para_Model = new Unit_Input_Para_Model()
                    {
                        classId = classid,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = SyllabusReportType.UnitlistClassWithSubject,
                        subjectId = subjectID,
                        userId = _sessionModel.UserId,
                        userRoleId = _sessionModel.RoleId
                    };
                    unitMasterList = (await syllabusService.Get_Unit_List(unit_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public async Task OnChangeUnit(ChangeEventArgs<string, Unit_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    int classid = args.ItemData.classId;
                    int unitID = args.ItemData.unitId;
                    int subjectID = args.ItemData.subjectID;

                    Chapter_Input_Para_Model chapter_Input_Para_Model = new Chapter_Input_Para_Model()
                    {
                        classId = classid,
                        unitID = unitID,
                        subjectId = subjectID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.GetByMasterId,
                    };
                    chapterMasterList = (await syllabusService.Get_Chapter_List(chapter_Input_Para_Model)).ToList();
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

                    _chapterID = args.ItemData.chapterID;
                    _topicMasterList = topicMasterList.Where(m => m.chapterID == args.ItemData.chapterID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        

        public ComplaintRegisterViewModel complaintRegisterViewModel = new ComplaintRegisterViewModel();

        public List<ComplaintRegisterListModel> complaintRegisterListModels = new List<ComplaintRegisterListModel>()
        { new ComplaintRegisterListModel (){ studentId=1, studentName="Gagan Gulati", classId=2, className="second", sectionId=3, sectionName="A", subjectId=4, subjectName="Physics", unitId=9, unitName="Physical", chapterId=6, chapterName="Laws of motion", topicId=8, topicName="Newtons laws",    createdByUser="Sameer", description="Problem in motion laws", statusDetails="closed", updatedByUser="Sameer", updatedDate="third", complaintDate="First April" } };

        /*Start CodeTool Bar click Event*/

        public void OnToolbarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Complain")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Complain Details";
                OperationType = "Add";
                HeaderText = "Add Complain";
                ddEnable = true;

            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfcomplintList.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfcomplintList.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfcomplintList.ExpandAllGroupAsync();
            }
        }
        /*end tool click Event*/


        /*Complaint fom submit event*/

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                //topicAPIModel = new TopicMasterModel()
                //{
                //    topicId = Convert.ToInt16(topicMasterViewModel.topicId),
                //    chapterId = Convert.ToInt16(topicMasterViewModel.chapterId),
                //    topicTitle = topicMasterViewModel.topicTitle,
                //    topicDescription = topicMasterViewModel.topicDescription,
                //    CreatedByUserId = _sessionModel.UserId,
                //    UpdatedByUserId = _sessionModel.UserId,
                //    SchoolCode = _sessionModel.SchoolCode,
                //    FinancialYear = _sessionModel.FinancialYear,
                //};

                //if (OperationType == "Add")
                //{
                //    topicAPIModel.OperationType = OperationAction.Add;
                //}
                //else if (OperationType == "Update")
                //{
                //    topicAPIModel.OperationType = OperationAction.Update;
                //}
                ////Delete Operation
                //else
                //{
                //    topicAPIModel.OperationType = OperationAction.Delete;
                //}
                //TopicDetailsSave(topicAPIModel);
            };
        }
        /*Dialog Close event*/
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

        public class ComplainType
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<ComplainType> complainType = new List<ComplainType>
        {
            new ComplainType(){Id=1,Value="Difficulty understanding course material."},
            new ComplainType(){Id=2,Value="Lack of clarity in instructions"},
            new ComplainType(){Id=3,Value="Unfair grading practices" },
            new ComplainType(){Id=4,Value="Excessive workload"},
            new ComplainType(){Id=5,Value="Lack of academic support (tutoring, extra help sessions, etc.)"},
            new ComplainType(){Id=6,Value="Classroom disruptions affecting learning" }, 
            new ComplainType(){Id=7,Value=" Teacher-student communication issues"},
        };
    }
}
