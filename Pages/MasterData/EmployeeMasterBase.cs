using AdminDashboard.Server.Models.MasterData;
using AdminDashboard.Server.Service.Interface.MasterData;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
using AIS.Model.GeneralConversion;
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

namespace AdminDashboard.Server.Pages.MasterData
{
    public class EmployeeMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterEmployeeAPIInputModel masterEmployeeAPIInputModel { get; set; }
        //data binding for blazor page
        public EmployeeMasterViewModel masterEmployeeViewModel = new EmployeeMasterViewModel();

        //list Model
        public List<MasterEmployeeListModel> masterEmployeeListModels = new List<MasterEmployeeListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                masterEmployeeAPIInputModel = new MasterEmployeeAPIInputModel()
                {
                     employeeName = masterEmployeeViewModel.employeeName,
                    employeeCode = masterEmployeeViewModel.employeeCode,
                    displayOrder = Convert.ToInt16(masterEmployeeViewModel.displayOrder),
                    masterEmployeeId = masterEmployeeViewModel.masterEmployeeId,
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterEmployeeAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterEmployeeAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterEmployeeAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveEmployeeDetails(masterEmployeeAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveEmployeeDetails(MasterEmployeeAPIInputModel masterEmployeeModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteEmpolyeeMaster(masterEmployeeModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetEmployeeList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Employee List
        private async Task GetEmployeeList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterEmployeeListModels = (await masterDataService.GetEmployeeList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterEmployeeViewModel.employeeName = null;
            masterEmployeeViewModel.employeeCode = null;
            masterEmployeeViewModel.displayOrder = null;
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Get Employee List Details
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            GetEmployeeList();

            //masterEmployeeListModels = (await masterDataService.GetMasterEmployeeList(defaultInputParameterModel)).ToList();
        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        public void RowSelectHandler(RowSelectEventArgs<MasterEmployeeListModel> args)
        {
            masterEmployeeViewModel = new EmployeeMasterViewModel()
            {
                employeeName = args.Data.employeeName,
                employeeCode = args.Data.employeeCode,
                displayOrder = Convert.ToInt16( args.Data.displayOrder),
                masterEmployeeId = args.Data.masterEmployeeId
            };
            HeaderTest = "Update Record";

        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Employee", "Print", "Search" });

        public SfGrid<MasterEmployeeListModel> sfEmployeeMaster;
        public string btncss = "";
        public void EditEmployeeMaster(CommandClickEventArgs<MasterEmployeeListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Employee Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterEmployeeViewModel = new EmployeeMasterViewModel()
                {
                    employeeName = args.RowData.employeeName,
                    employeeCode = args.RowData.employeeCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterEmployeeId = args.RowData.masterEmployeeId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Employee Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterEmployeeViewModel = new EmployeeMasterViewModel()
                {
                    employeeName = args.RowData.employeeName,
                    employeeCode = args.RowData.employeeCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterEmployeeId = args.RowData.masterEmployeeId
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Employee")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Employee Details";
                OperationType = "Add";
                HeaderTest = "Add Employee";
                masterEmployeeViewModel.employeeName = null;
                masterEmployeeViewModel.employeeCode = null;
                masterEmployeeViewModel.displayOrder = null;
            }
        }
    }
}
