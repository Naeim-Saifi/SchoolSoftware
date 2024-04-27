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
    public class UnitMasterSetupBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public ISyllabusService  syllabusService { get; set; }

        //list Model
        public List<Unit_Output_Model> AllUnitMasterList = new List<Unit_Output_Model>();
        public List<Unit_Output_Model> classAndSubjectWseUnitMasterList = new List<Unit_Output_Model>();

        public List<Master_CLass_List_Output_Model>  master_CLass_List = new List<Master_CLass_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> map_classwithsubject_List = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public SessionModel sessionModel { get; }
       
        //data binding for blazor page
        public UnitMasterViewModel masterUnitViewModel =
            new UnitMasterViewModel();

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<Unit_Output_Model> sfUnitGrid;
        public SfGrid<Unit_Output_Model> sfclassAndSubjectUnitGrid;
        

        public List<string> classMasterItem = (new List<string>() { "Add Unit", "ExcelExport", "Collapse All", "Expand All", "Print", "Search"  });
        public List<string> unitlistMasterItem = (new List<string>() {"Clear All",  "ExcelExport", "Print", "Search" });

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
        public UnitMasterModel unitMasterAPIModel { get; set; }
        public SessionModel _sessionModel;
        public string OperationType = "";
        public int pageSize = 50;
        public void UnitToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Unit")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Unit Details";
                OperationType = "Add";
                HeaderText = "Add Unit";
                ddEnable = true;
                masterUnitViewModel.unitTitle = null;
                masterUnitViewModel.unitDescription = null;
                //masterUnitViewModel.classId = null;
                //masterUnitViewModel.subjectId = null;
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfUnitGrid.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfUnitGrid.ExpandAllGroupAsync();
            }
            else if(args.Item.Text == "Excel Export")                 
            {
                this.sfUnitGrid.ExportToExcelAsync();

                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= " Class Wise Unit Details", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExcelProperties.Header = header;
                ExcelProperties.FileName = "Class_With_UnitMasterList.xlsx";
                  this.sfUnitGrid.ExcelExport(ExcelProperties);
            }
            else
            {
                this.sfUnitGrid.Columns[0].AutoFit = true;
            }
        }
        public void EditUnitToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Clear All")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Unit Details";
                OperationType = "Add";
                HeaderText = "Add Unit";
                ddEnable = true;
                masterUnitViewModel.unitTitle = null;
                masterUnitViewModel.unitDescription = null;
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
                ExcelProperties.FileName = "Class_With_UnitList.xlsx";
                this.sfclassAndSubjectUnitGrid.ExcelExport(ExcelProperties);
            }
            else
            {
                this.sfUnitGrid.Columns[0].AutoFit = true;
            }
        }
        public void EditUnitMaster(CommandClickEventArgs<Unit_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
               // IsEditVisible = true;
                DialogHeaderName = "Update Unit Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                //btncss = "e-flat e-info e-outline";
                btncss = "pa-4 blue darken-1 shades-text text-white";
                ddEnable = false;
                masterUnitViewModel = new UnitMasterViewModel()
                {
                    classId = args.RowData.classId.ToString(),
                    subjectId = args.RowData.subjectID.ToString(),
                    unitId = args.RowData.unitId,
                    unitDescription = args.RowData.unitDescription,
                    unitTitle = args.RowData.unitTitle,
                };
            }
            else
            {
               // IsEditVisible = true;
                DialogHeaderName = "Delete Unit Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                masterUnitViewModel = new UnitMasterViewModel()
                {
                    classId = args.RowData.classId.ToString(),
                    subjectId = args.RowData.subjectID.ToString(),
                    unitId = args.RowData.unitId,
                    unitDescription = args.RowData.unitDescription,
                    unitTitle = args.RowData.unitTitle,
                };
            }
            
        }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                unitMasterAPIModel = new UnitMasterModel()
                {
                    unitId = masterUnitViewModel.unitId,
                    classId =Convert.ToInt16(masterUnitViewModel.classId),
                    subjectId = Convert.ToInt16(masterUnitViewModel.subjectId),
                    unitTitle = masterUnitViewModel.unitTitle,
                    unitDescription = masterUnitViewModel.unitDescription,                    
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    unitMasterAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    unitMasterAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    unitMasterAPIModel.OperationType = OperationAction.Delete;
                }
                UnitDetailsSave(unitMasterAPIModel);
            };
        }
        public APIReturnModel aPIReturnModel;
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SfGrid<Unit_Output_Model> sfUnitMaster;
        private   async void UnitDetailsSave(UnitMasterModel unitMasterModel)
        {
            try
            {
                if (unitMasterModel.OperationType != "NA")
                {
                      aPIReturnModel = (await syllabusService.CRUD_MasterUnit(unitMasterModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                       GetUnitMasterList();
                        //sfUnitMaster.Refresh();
                        //StateHasChanged();
                        ClearData();
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
            //masterUnitViewModel = new UnitMasterViewModel();
            masterUnitViewModel.unitTitle = string.Empty;
            masterUnitViewModel.unitDescription = string.Empty;
        }
        private async void GetUnitMasterList()
        {
            Unit_Input_Para_Model unit_Input_Para_Model = new Unit_Input_Para_Model()
            {
                classId = 0,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = SyllabusReportType.UnitList,
                userId = _sessionModel.UserId,
                subjectId = 0,
                 userRoleId = _sessionModel.RoleId
                 
            };
            AllUnitMasterList = (await syllabusService.Get_Unit_List(unit_Input_Para_Model)).ToList();
           
            //sfclassAndSubjectUnitGrid.Refresh();
            classAndSubjectWseUnitMasterList = AllUnitMasterList.Where(m => m.subjectID == subjectID & m.classId == classid).ToList();
            
            if(classAndSubjectWseUnitMasterList.Count>0)
            {
                StateHasChanged();
                sfclassAndSubjectUnitGrid.Refresh();
            }
           
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";
           
        }
       public int classid = 0;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId=_sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.GetTeacherClass
            };
            master_CLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            GetUnitMasterList();

        }
        public   async   Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                     classid = args.ItemData.classId;

                    Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass    = new Master_Map_Subject_With_ClassList_Input_Para_Model()
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
       
        public int subjectID = 0;
        public async Task OnChangeSubject(ChangeEventArgs<string, Master_Map_Subject_With_Class_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.subjectId != 0)
                {
                     subjectID = args.ItemData.subjectId;

                    classAndSubjectWseUnitMasterList= AllUnitMasterList.Where(m=>m.subjectID==subjectID & m.classId==classid).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
            //masterUnitViewModel.unitTitle = null;
            //masterUnitViewModel.unitDescription = null;
            //masterUnitViewModel.classId = null;
            //masterUnitViewModel.subjectId = null;

            masterUnitViewModel=new UnitMasterViewModel();

            classAndSubjectWseUnitMasterList.Clear();
        }

    }
}
