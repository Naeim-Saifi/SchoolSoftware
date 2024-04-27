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

namespace AdminDashboard.Server.User_Pages.Syllabus
{
    public class ChapterMasterSetupBase : ComponentBase
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
        public List<Chapter_Output_Model> _unitchapterMasterList = new List<Chapter_Output_Model>();

        public List<Master_CLass_List_Output_Model> master_CLass_List = new List<Master_CLass_List_Output_Model>();

        public List<Master_Map_Subject_With_Class_List_Output_Model> map_classwithsubject_List = new List<Master_Map_Subject_With_Class_List_Output_Model>();

        //data binding for blazor page
        public ChapterMasterViewModel  chapterMasterViewModel =new ChapterMasterViewModel();
        public APIReturnModel aPIReturnModel;
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<Chapter_Output_Model> sfUnitGrid;
        public SfGrid<Chapter_Output_Model> sfUnitchapterGrid;


        public List<string> toolBarItems = (new List<string>() { "Add Chapter", "ExcelExport", "Collapse All", "Expand All", "Print", "Search" });
        public List<string> chapterlistMasterItem = (new List<string>() { "Clear All", "ExcelExport", "Print", "Search" });
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsEditVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        //this model use for send data to API ,binding view model with API model
       
        public SessionModel _sessionModel;
        public string OperationType = "";

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
            master_CLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            Chapter_Input_Para_Model chapter_Input_Para_Model = new Chapter_Input_Para_Model()
            {
                subjectId = 0,
                classId = 0,
                unitID = 0,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All,
                
            };
            chapterMasterList = (await syllabusService.Get_Chapter_List(chapter_Input_Para_Model)).ToList();

            if(_unitchapterMasterList.Count>0)
            {
                StateHasChanged();
                _unitchapterMasterList = chapterMasterList.Where(m => m.unitId == unitId).ToList();
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
                         userId = _sessionModel.UserId,
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
                        userRoleId=_sessionModel.RoleId
                    };
                    unitMasterList = (await syllabusService.Get_Unit_List(unit_Input_Para_Model)).ToList();
                    
                    //classAndSubjectWseUnitMasterList = AllUnitMasterList.Where(m => m.subjectID == subjectID & m.classId == classid).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        int classid = 0; int subjectID = 0; int unitId = 0;
        public async Task OnChangeUnit(ChangeEventArgs<string, Unit_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                     classid = args.ItemData.classId;
                     subjectID = args.ItemData.subjectID;
                     unitId= args.ItemData.unitId;

                    _unitchapterMasterList = chapterMasterList.Where(m=>m.unitId==unitId).ToList();
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public void EditChapterMaster(CommandClickEventArgs<Chapter_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                //IsEditVisible = true;
                DialogHeaderName = "Update Chapter Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                chapterMasterViewModel = new ChapterMasterViewModel()
                {
                    classId=Convert.ToString(args.RowData.classId),
                    subjectId = Convert.ToString(args.RowData.subjectId),
                    chapterId = args.RowData.chapterID,
                    unitId = args.RowData.unitId.ToString(),
                    chapterDescription = args.RowData.chapterDescription,
                    chapterTitile = args.RowData.chapterTitle
                      
                };
            }
            else
            {
                //IsEditVisible = true;
                DialogHeaderName = "Delete Chapter Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                chapterMasterViewModel = new ChapterMasterViewModel()
                {
                    classId = Convert.ToString(args.RowData.classId),
                    subjectId = Convert.ToString(args.RowData.subjectId),
                    chapterId = args.RowData.chapterID,
                    unitId = args.RowData.unitId.ToString(),
                    chapterDescription = args.RowData.chapterDescription,
                    chapterTitile = args.RowData.chapterTitle

                };
            }

        }
        public void toolBarItemsToolBar(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Chapter")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Chapter Details";
                OperationType = "Add";
                HeaderText = "Add Chapter";
                ddEnable = true;
                chapterMasterViewModel.chapterTitile = null;
                chapterMasterViewModel.chapterDescription = null;
                chapterMasterViewModel.unitId = null;
                
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfUnitGrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfUnitGrid.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {
                this.sfUnitGrid.ExportToExcelAsync();

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
                this.sfUnitGrid.ExcelExport(ExcelProperties);
            }
            else
            {
                this.sfUnitGrid.Columns[0].AutoFit = true;
            }
        }
        public void chapterlistToolBar(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Clear All")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Chapter Details";
                OperationType = "Add";
                HeaderText = "Add Chapter";
                ddEnable = true;
                chapterMasterViewModel.chapterDescription = null;
                chapterMasterViewModel.chapterTitile = null;
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
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Class & Subject Chapter List", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "Class_With_ChapterList.xlsx";
                this.sfUnitchapterGrid.ExcelExport(ExcelProperties);
            }
            else
            {
                this.sfUnitGrid.Columns[0].AutoFit = true;
            }
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public ChapterMasterModel chapterAPIModel { get; set; } 
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                chapterAPIModel = new ChapterMasterModel()
                {
                    
                    chapterId = Convert.ToInt16(chapterMasterViewModel.chapterId),
                    unitId = Convert.ToInt16(chapterMasterViewModel.unitId),                                          
                    chapterTitile = chapterMasterViewModel.chapterTitile,
                    chapterDescription = chapterMasterViewModel.chapterDescription,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    chapterAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    chapterAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    chapterAPIModel.OperationType = OperationAction.Delete;
                }
                ChapterDetailsSave(chapterAPIModel);
            };
        }
        
        public int pageSize = 50;
        private async void ChapterDetailsSave(ChapterMasterModel chapterMasterModel)
        {
            try
            {
                if (chapterMasterModel.OperationType != "NA")
                {
                    aPIReturnModel = (await syllabusService.CRUD_MasterChapter(chapterMasterModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);

                        Chapter_Input_Para_Model chapter_Input_Para_Model = new Chapter_Input_Para_Model()
                        {
                            subjectId = 0,
                            classId = 0,
                            unitID = 0,
                            schoolCode = _sessionModel.SchoolCode,
                            financialYear = _sessionModel.FinancialYear,
                            reportType = ReportType.All,

                        };
                        chapterMasterList = (await syllabusService.Get_Chapter_List(chapter_Input_Para_Model)).ToList();
                        _unitchapterMasterList = chapterMasterList.Where(m => m.unitId == unitId).ToList();
                        ClearData();
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
        private void ClearData()
        {
            //chapterMasterViewModel = new ChapterMasterViewModel();
            chapterMasterViewModel.chapterDescription = "";
            chapterMasterViewModel.chapterTitile = "";

        }
        public async Task CloseDialog()
        {
            classid = 0;   subjectID = 0;   unitId = 0;
            if(_unitchapterMasterList.Count>0)
{ _unitchapterMasterList.Clear(); }
            IsVisible = false;
            await this.DialogRef.HideAsync();


        }
    }
}
