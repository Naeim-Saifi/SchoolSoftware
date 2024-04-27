using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Data.RequestResponseModel.SyllabusVideo;
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
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.SyllabusVideo
{
    public class SyllabusVideoListBase : ComponentBase
    {
        public Boolean ddEnable = true;
        /*Start Dialog */
       
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> QuestionBankListItems = (new List<string>() { "Add Question", "Print", "Search" });

        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IscurrentVideoVisible { get; set; }=false;
        public bool IsVisibleQuestionDetails { get; set; } = false;
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }

        //list Model
        public List<Unit_Output_Model> unitMasterList = new List<Unit_Output_Model>();
        public List<Chapter_Output_Model> chapterMasterList = new List<Chapter_Output_Model>();


        public List<Topic_Output_Model> topicMasterList = new List<Topic_Output_Model>();
        public List<Topic_Output_Model> _topicMasterList = new List<Topic_Output_Model>();

        public List<Master_CLass_List_Output_Model> master_CLass_List = new List<Master_CLass_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> map_classwithsubject_List = new List<Master_Map_Subject_With_Class_List_Output_Model>();

        public List<SyllabusVideo_Output_Model>  syllabusVideo_Output_Models = new List<SyllabusVideo_Output_Model>();
 
        public SfGrid<SyllabusVideo_Output_Model> sfsyllabusVideoListGrid;

       

        public List<string> toolBarItems = (new List<string>() { "Add Syllabus Video", "ExcelExport", "Collapse All", "Expand All", "Print", "Search" });
        public int pageSize = 50;
        
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";


        public SyllabusVideoVewModel  syllabusVideoVewModel = new SyllabusVideoVewModel();
        public DialogEffect dialogAnimationEffect = DialogEffect.Zoom;
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string DialogHeaderName = "Question Bank";// string.Empty;
        public SessionModel _sessionModel;

        public string  btncss = "e-flat e-primary e-outline";
        public string HeaderText = "Add Video";

        public string _videoPreviewLink = string.Empty;
        public string _currentvideoLink = string.Empty;
        public void VideoPreview()
        {
            //_videoPreviewLink = "https://www.youtube.com/embed/RXWZhBwPr_g"; //syllabusVideoVewModel.syllabusVideoLink;
            _videoPreviewLink = syllabusVideoVewModel.syllabusVideoLink;
        }
        public async Task CloseDialog()
        {
             
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public string OperationType = "";
       
        
        public void toolBarItemsClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Syllabus Video")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Syllabus Video Details";
                OperationType = "Add";
                HeaderText = "Add Syllabus Video";
                ddEnable = true;
                

            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfsyllabusVideoListGrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfsyllabusVideoListGrid.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {
                this.sfsyllabusVideoListGrid.ExportToExcelAsync();

                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Class Wise Chapter Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "Class_With_ChapterMasterList.xlsx";
                this.sfsyllabusVideoListGrid.ExcelExport(ExcelProperties);
            }
            else
            {
                this.sfsyllabusVideoListGrid.Columns[0].AutoFit = true;
            }
        }
        public void EditSyllabusVideoCommandClick(CommandClickEventArgs<SyllabusVideo_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "View Video")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                IscurrentVideoVisible = true;
                DialogHeaderName = args.RowData.syllabusVideoDescription;
                //HeaderText = "Update Record";
                //OperationType = "Update";
                //btncss = "e-flat e-info e-outline";
                //ddEnable = false;
                _currentvideoLink = args.RowData.syllabusVedioLink;
                //chapterMasterViewModel = new ChapterMasterViewModel()
                //{
                //    classId = Convert.ToString(args.RowData.classId),
                //    subjectId = Convert.ToString(args.RowData.subjectId),
                //    chapterId = args.RowData.chapterID,
                //    unitId = args.RowData.unitId.ToString(),
                //    chapterDescription = args.RowData.chapterDescription,
                //    chapterTitile = args.RowData.chapterTitle

                //};
            }
            else
            {
                //IsEditVisible = true;
                DialogHeaderName = "Delete Chapter Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                //chapterMasterViewModel = new ChapterMasterViewModel()
                //{
                //    classId = Convert.ToString(args.RowData.classId),
                //    subjectId = Convert.ToString(args.RowData.subjectId),
                //    chapterId = args.RowData.chapterID,
                //    unitId = args.RowData.unitId.ToString(),
                //    chapterDescription = args.RowData.chapterDescription,
                //    chapterTitile = args.RowData.chapterTitle

                //};
            }

        }
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            //{
            //    classId = 0,
            //    userId = _sessionModel.UserId,
            //    financialYear = _sessionModel.FinancialYear,
            //    schoolCode = _sessionModel.SchoolCode,
            //    reportType = ReportType.All
            //};
            //master_CLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();
            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.GetTeacherClass
            };
            master_CLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();
            SyllabusVideo_Input_Para_Model syllabusVideo_Input_Para_Model = new SyllabusVideo_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                classId = _classID,
                subjectId = _subjectID,
                unitID = _unitID,
                chapterID = _chapterID,
                topicID = _topicID,
                userID = _sessionModel.UserId,
                userRoleId = _sessionModel.RoleId,
                reportType = SyllabusReportType.AllVideoList
            };
            syllabusVideo_Output_Models = (await syllabusService.Get_SyllabusVideo_List(syllabusVideo_Input_Para_Model)).ToList();
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

                    map_classwithsubject_List = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();
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
                    int classid = args.ItemData.masterClassId;
                    _subjectID = args.ItemData.subjectId;

                    Unit_Input_Para_Model unit_Input_Para_Model = new Unit_Input_Para_Model()
                    {
                        classId = classid,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = SyllabusReportType.UnitlistClassWithSubject,
                        subjectId = _subjectID,
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
                    //_topicMasterList = topicMasterList.Where(m => m.chapterID == args.ItemData.chapterID).ToList();
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
                    _topicMasterList = (await syllabusService.Get_Topic_List(chapter_Input_Para_Model)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        int _topicID = 0;
        public async Task OnChangeTopic(ChangeEventArgs<string, Topic_Output_Model> args)
        {
            try
            {
                if (args.ItemData.topicID != 0)
                {

                    _topicID = args.ItemData.topicID;
                    //_topicMasterList = topicMasterList.Where(m => m.chapterID == args.ItemData.chapterID).ToList();
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
             SyllabusVideoAPIModel syllabusVideoAPIModel = new SyllabusVideoAPIModel()
                {
                     masterClassId = Convert.ToInt16(syllabusVideoVewModel.masterClassId),
                     masterSubjectId = Convert.ToInt16(syllabusVideoVewModel.masterSubjectId),
                     SubjectUnitId = Convert.ToInt16(syllabusVideoVewModel.SubjectUnitId),
                     subjectChapterId = Convert.ToInt16(syllabusVideoVewModel.subjectChapterId),
                     subjectTopicId = Convert.ToInt16(syllabusVideoVewModel.subjectTopicId),
                     syllabusVideoLink =  syllabusVideoVewModel.syllabusVideoLink,
                     syllabusVideoDescription = syllabusVideoVewModel.syllabusVideoDescription,
                     CreatedByUserId = _sessionModel.UserId,
                     UpdatedByUserId = _sessionModel.UserId,
                     SchoolCode = _sessionModel.SchoolCode,
                     FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    syllabusVideoAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    syllabusVideoAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    syllabusVideoAPIModel.OperationType = OperationAction.Delete;
                }
                SyllabusVideoSave(syllabusVideoAPIModel);
            };
        }
        public APIReturnModel aPIReturnModel;
        private async void SyllabusVideoSave(SyllabusVideoAPIModel syllabusVideoAPIModel)
        {
            try
            {
                if (syllabusVideoAPIModel.OperationType != "NA")
                {
                    aPIReturnModel = (await syllabusService.CRUD_SyllabusVideoDetails(syllabusVideoAPIModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);

                        SyllabusVideo_Input_Para_Model syllabusVideo_Input_Para_Model = new SyllabusVideo_Input_Para_Model()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            classId = _classID,
                            subjectId = _subjectID,
                            unitID = _unitID,
                            chapterID = _chapterID,
                            topicID = _topicID,
                            userID = _sessionModel.UserId,
                            userRoleId = _sessionModel.RoleId,
                            reportType = SyllabusReportType.AllVideoList
                        };
                        syllabusVideo_Output_Models = (await syllabusService.Get_SyllabusVideo_List(syllabusVideo_Input_Para_Model)).ToList(); 
                        //ClearData(); 
                        StateHasChanged();
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
    }
}
