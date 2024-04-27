using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.Models.SyllabusDetails;
using AIS.Data.APIReturnModel;
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
using static AIS.Data.GeneralConversion.GeneralConversion;
using TopicMasterModel = AIS.Data.RequestResponseModel.Syllabus.TopicMasterModel;
using TopicMasterViewModel = AIS.Data.RequestResponseModel.Syllabus.TopicMasterViewModel;

namespace AdminDashboard.Server.User_Pages.Syllabus
{
    public class TopicMasterSetupBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public ISyllabusService syllabusService { get; set; }

        //list Model
        public List<Unit_Output_Model> unitMasterList = new List<Unit_Output_Model>();
        public List<Chapter_Output_Model> chapterMasterList = new List<Chapter_Output_Model>();

        public List<Topic_Output_Model> topicMasterList = new List<Topic_Output_Model>();
        public List<Topic_Output_Model> _topicMasterList = new List<Topic_Output_Model>();

        public List<Master_CLass_List_Output_Model> master_CLass_List = new List<Master_CLass_List_Output_Model>();

        public List<Master_Map_Subject_With_Class_List_Output_Model> map_classwithsubject_List = new List<Master_Map_Subject_With_Class_List_Output_Model>();

        //data binding for blazor page
        public TopicMasterViewModel  topicMasterViewModel =
            new TopicMasterViewModel();
        public APIReturnModel aPIReturnModel;
        public SessionModel _sessionModel;
        public string OperationType = "";

        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<Topic_Output_Model> sfTopicGrid;
        public SfGrid<Topic_Output_Model> sfeditTopicGrid;
          

        public List<string> toolAllTopictoolBarItems = (new List<string>() { "Add Topic", "ExcelExport", "Collapse All", "Expand All", "Print", "Search" });
        public List<string> editTopicToolBar = (new List<string>() { "Clear All", "ExcelExport", "Print", "Search" });
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsEditVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        public TopicMasterModel topicAPIModel { get; set; }

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
       
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                topicAPIModel = new TopicMasterModel()
                {
                    topicId = Convert.ToInt16(topicMasterViewModel.topicId),
                    chapterId = Convert.ToInt16(topicMasterViewModel.chapterId),                    
                    topicTitle = topicMasterViewModel.topicTitle,
                    topicDescription = topicMasterViewModel.topicDescription,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    topicAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    topicAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    topicAPIModel.OperationType = OperationAction.Delete;
                }
                TopicDetailsSave(topicAPIModel);
            };
        }

        private async void TopicDetailsSave(TopicMasterModel topicAPIModel)
        {
            try
            {
                if (topicAPIModel.OperationType != "NA")
                {
                    aPIReturnModel = (await syllabusService.CRUD_MasterTopic(topicAPIModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);

                        Topic_Input_Para_Model topic_Input_Para_Model = new Topic_Input_Para_Model()
                        {
                            subjectId = 0,
                            classId = 0,
                            unitID = 0,
                            chapterID = 0,
                            schoolCode = _sessionModel.SchoolCode,
                            financialYear = _sessionModel.FinancialYear,
                            reportType = ReportType.All,

                        };
                        topicMasterList = (await syllabusService.Get_Topic_List(topic_Input_Para_Model)).ToList();

                        _topicMasterList = topicMasterList.Where(m => m.chapterID == _chapterID).ToList();
                       
                        ClearData(); StateHasChanged();
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
                         unitID=unitID,
                         subjectId= subjectID,
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

        int   _chapterID = 0;
        public async Task OnChangeChapter(ChangeEventArgs<string, Chapter_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {

                    _chapterID = args.ItemData.chapterID;
                    _topicMasterList = topicMasterList.Where(m=>m.chapterID==args.ItemData.chapterID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public void EditTopicMaster(CommandClickEventArgs<Topic_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                IsEditVisible = true;
                DialogHeaderName = "Update Chapter Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                topicMasterViewModel = new TopicMasterViewModel()
                {
                    classId = Convert.ToString(args.RowData.classId),
                    subjectId = Convert.ToString(args.RowData.subjectID),
                    topicId = args.RowData.topicID,
                    chapterId = args.RowData.chapterID.ToString(),
                    unitId = args.RowData.unitId.ToString(),
                    topicDescription = args.RowData.topicDescription,
                    topicTitle = args.RowData.topicTitle
                      
                };
            }
            else
            {
                IsEditVisible = true;
                DialogHeaderName = "Delete Topic Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                topicMasterViewModel = new TopicMasterViewModel()
                {
                    classId = Convert.ToString(args.RowData.classId),
                    subjectId = Convert.ToString(args.RowData.subjectID),
                    topicId = args.RowData.topicID,
                    chapterId = args.RowData.chapterID.ToString(),
                    unitId = args.RowData.unitId.ToString(),
                    topicDescription = args.RowData.topicDescription,
                    topicTitle = args.RowData.topicTitle

                };
            }

        }
        public void AllTopicToolBar(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Topic")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Topic Details";
                OperationType = "Add";
                HeaderText = "Add Topic";
                ddEnable = true;
                topicMasterViewModel.classId = null;
                topicMasterViewModel.subjectId = null;
                topicMasterViewModel.unitId = null;
                topicMasterViewModel.chapterId = null;
                topicMasterViewModel.topicTitle = null;
                topicMasterViewModel.topicDescription = null;
               if(_topicMasterList.Count>0)
                {
                    _chapterID = 0;
                    _topicMasterList.Clear();
                }
                
                               
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfTopicGrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfTopicGrid.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {
                

                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Class Wise Topic  Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "Class_With_TopicMasterList.xlsx";
                this.sfTopicGrid.ExcelExport(ExcelProperties);
            }
        }
        
        public void EditTopicToolBar(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Clear All")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Topic Details";
                OperationType = "Add";
                HeaderText = "Add Unit";
                ddEnable = true;
                topicMasterViewModel.topicTitle = null;
                topicMasterViewModel.topicDescription = null;
                //masterUnitViewModel.classId = null;
                //masterUnitViewModel.subjectId = null;
            }
            else if (args.Item.Text == "Excel Export")
            {


                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Class & Subject Unit List", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "Class_With_TopicList.xlsx";
                this.sfeditTopicGrid.ExcelExport(ExcelProperties);
            }
            else
            {
                this.sfeditTopicGrid.Columns[0].AutoFit = true;
            }
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
        private void ClearData()
        {
            //topicMasterViewModel = new AIS.Data.RequestResponseModel.Syllabus.TopicMasterViewModel();
            topicMasterViewModel.topicTitle = string.Empty;
            topicMasterViewModel.topicDescription = string.Empty;
        }

    }
}
