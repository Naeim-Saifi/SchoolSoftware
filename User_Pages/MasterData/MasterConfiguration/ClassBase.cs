//using AdminDashboard.Server.API_Service.Service.Inventory;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;
//using AIS.Data.RequestResponseModel.Inventory.ItemVender;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using AdminDashboard.Server.API_Service.Interface.Inventory;
using System.Linq;
using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Class;
using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Section;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using AdminDashboard.Server.API_Service.Interface.MasterData;

namespace AdminDashboard.Server.User_Pages.MasterData.MasterConfiguration
{
    public class ClassBase : ComponentBase
    {



        public ClassViewModel classViewModel = new ClassViewModel();


        public List<ClassOutputModel> _ClassListModel = new List<ClassOutputModel>();
        public SfGrid<ClassOutputModel> sfClassDetails;
        public ClassApiModel classApiModel { get; set; }



        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        [Inject]
        public ISnackbar snackBar { get; set; }

       // [Inject]
     //   public IMasterDataService masterDataService { get; set; }

        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;

        List<string> IList = new List<string>();


        public bool IsVisible { get; set; } = false;
        public bool IsDeleteVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";

        public APIReturnModel aPIReturnModel;


        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

        public List<string> ToolBarItems = (new List<string>() { "Add Class", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });




        public class ClassName
        {
            public int Id;
            public string Value;
        }

        public List<ClassName> classNamedetails = new List<ClassName>()
        {
            new ClassName{Id=1,Value="KG"},
             new ClassName{Id=1,Value="UKG"},
        new ClassName{Id=1,Value="I"},
           new ClassName{Id=1,Value="II"},
       new ClassName{Id=1,Value="III"},
         new ClassName{Id=1,Value="IV"},
     new ClassName{Id=1,Value="V"},

        };


     




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
            //_classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            ClassParaModel classParaModel = new ClassParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                ClassId = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            //    _ClassListModel = (await masterDataService.GET_ClassDetails_List(classParaModel)).ToList();
        }







        public void EditItemDetail(CommandClickEventArgs<ClassOutputModel> args)
        {

            
                // Perform required operations here
                string buttontext = args.CommandColumn.ButtonOption.Content;
                //int testId = args.RowData.testID;
                if (buttontext == "Edit")
                {
                    //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                    IsVisible = true;
                    DialogHeaderName = "Update Class Details";
                    HeaderText = "Update Record";
                    OperationType = "Update";
                    btncss = "e-flat e-info e-outline";
                    // ddEnable = false;
                    classViewModel = new ClassViewModel()
                    {
                        classId = args.RowData.classId,
                        classCode = args.RowData.classCode,
                        className = args.RowData.className,
                        displayOrder = args.RowData.displayOrder,


                    };
                }
                else
                {
                    IsVisible = true;
                    DialogHeaderName = "Delete Class Details";
                    OperationType = "Delete";
                    HeaderText = "Delete Record";
                    btncss = "e-flat e-danger e-outline";
                    ddEnable = false;
                    classViewModel = new ClassViewModel()
                    {

                        classId = args.RowData.classId,
                        classCode = args.RowData.classCode,
                        className = args.RowData.className,
                        displayOrder = args.RowData.displayOrder,

                    };
                }
                //else
                //{
                //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
                //}

            }


            public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
            {
                if (args.Item.Text == "Add Class")
                {
                    //navigationManager.NavigateTo("/OnlineExam/TestList");
                    //perform your actions here
                    IsVisible = true;
                    OperationType = "";
                    btncss = "e-flat e-primary e-outline";
                    DialogHeaderName = "Add Class Details";
                    OperationType = "Add";
                    HeaderText = "Add Class";
                    ddEnable = true;

                }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfClassDetails.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfClassDetails.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfClassDetails.ExpandAllGroupAsync();
            }
        }




            public async void OnValidSubmit(EditContext contex)
            {
                bool isValid = contex.Validate();
                if (isValid)
                {
                    classApiModel = new ClassApiModel()
                    {
                        ClassName = classViewModel.className,
                        ClassCode = classViewModel.classCode,
                        DisplayOrder = classViewModel.displayOrder,




                    };
                    if (OperationType == "Add")
                    {
                        classApiModel.OperationType = OperationAction.Add;
                    }
                    else if (OperationType == "Update")
                    {
                        classApiModel.OperationType = OperationAction.Update;
                    }
                    //Delete Operation
                    else
                    {
                        classApiModel.OperationType = OperationAction.Delete;
                    }

                    SaveItemMasterDetails(classApiModel);

                }
                else
                {
                    // Form has invalid inputs.
                }

            }

            private async void SaveItemMasterDetails(ClassApiModel classApiModel)
            {
            try
            {
                if (classApiModel.OperationType != "NA")
                {
                    //  aPIReturnModel = await masterDataService.CRUD_ClassDetails(classApiModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);


                        ClassParaModel classParaModel = new ClassParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            ClassId = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                       //   _ClassListModel  = (await masterDataService.GET_ClassDetails_List(classParaModel)).ToList();

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
                classViewModel = new ClassViewModel();

            }


            public void ShowDialog()
            {
                IsVisible = true;
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
        }
    }