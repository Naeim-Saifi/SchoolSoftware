using AdminDashboard.Server.API_Service.Interface.Exam;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.QuestionSetup;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.API_Service.Service.QuestionSetup;
using AdminDashboard.Server.Models.TimeTable;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.QuestionSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.UserLogin;
using Create_Word_document;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch.Operations;
using MudBlazor;
using Syncfusion.Blazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;


namespace AdminDashboard.Server.User_Pages.MasterData.ExamConfiguration
{
    public class MapSubjectWithClassBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }

        public Boolean ddEnable = true;
            /*Start Dialog */
            public List<string> ToolBarClassWithSubject = (new List<string>() { "Map Class With Subject", "Print", "ExportExcel", "Collapse All", "Expand All", "Search", "ColumnChooser" });
            public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
            public List<string> _ToolBarSubjectList = (new List<string>() { "Save", "Print", "Search" });

            public List<string> ToolBarAlreadySubject= (new List<string>() { "Add", "ExcelExport", "Print", "Search" }); 
            public SfDialog DialogRef;
            public bool IsVisible { get; set; } = false;

            
         

           
        public List<Master_CLass_List_Output_Model> master_CLass_List = new List<Master_CLass_List_Output_Model>(); 
        public List<Master_Subject_List_Output_Model> _subject_List = new List<Master_Subject_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> map_classwithsubject_List = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        //API Model

        public SfGrid<Master_Map_Subject_With_Class_List_Output_Model> sfMapSubjectWithClass;

        public SfGrid<Master_Subject_List_Output_Model> sfsubjectList;




        public AnimationEffect ExpandEffect = AnimationEffect.SlideDown;
            public AnimationEffect CollapseEffect = AnimationEffect.SlideUp;

            [Inject]
            public ISnackbar snackBar { get; set; }
            public SessionModel sessionModel { get; }



            public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
            {
                // setting maximum height to the Dialog
                args.MaxHeight = "850px";

            }
            public string DialogHeaderName = "Question Bank";// string.Empty;
            public SessionModel _sessionModel;
            public SfGrid<QuestionBankListModel> sfquestionListgrid;
            public SfGrid<SelectedQuestionListModel> sfSelectedquestionListgrid;
            public SfGrid<QuestionPaperListOutPutModel> questionPaperListgrid;

            public DialogEffect dialogAnimationEffect = DialogEffect.Zoom;


            protected override async Task OnInitializedAsync()
            {
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.GetTeacherClass
            };
            master_CLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            Master_Subject_List_Input_Para_Model master_Subject_List_Input_Para_Model = new Master_Subject_List_Input_Para_Model()
            {
                subjectID = 0,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _subject_List = (await masterDataSetupService.GET_Master_SubjectLIST(master_Subject_List_Input_Para_Model)).ToList();



            Master_Map_Subject_With_ClassList_Input_Para_Model master_Map_Subject_With_ClassList_Input_Para_Model = new Master_Map_Subject_With_ClassList_Input_Para_Model()
            {
                classID = 0,
                userId = _sessionModel.UserId,
                subjectID = 0,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,

                reportType = ReportType.All
            };
            map_classwithsubject_List = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(master_Map_Subject_With_ClassList_Input_Para_Model)).ToList();

            }
        public int classid = 0;
        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    classid = args.ItemData.classId;

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
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Map Class With Subject")
            {

                IsVisible = true;
                //  OperationType = "";
                //btncss = "e-flat e-primary e-outline";
                //DialogHeaderName = "Add ExamType Details";
                //OperationType = "Add";
                //HeaderText = "Add ExamType";
                //ddEnable = true;

            }
        } 

         

            public async void OnValidSubmit(EditContext contex)
            {
                bool isValid = contex.Validate();
                if (isValid)
                {
        
                }
                else
                {
                    // Form has invalid inputs.
                }

            }
            public APIReturnModel aPIReturnModel;


            int _classID = 0;
            int _subjectID = 0;
             

            public async Task CloseDialog()
            {
                IsVisible = false;
                await this.DialogRef.HideAsync();
            }
           

        }
    }