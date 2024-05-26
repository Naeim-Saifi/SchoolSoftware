using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;

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

using System.Linq;
using AdminDashboard.Server.API_Service.Interface.Employee;
using AIS.Data.RequestResponseModel.Employee;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;

namespace AdminDashboard.Server.User_Pages.MasterData.AdminConfiguration
{
    public class UserMasterBase : ComponentBase
    {



        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public IEmployeeSetupService employeeService { get; set; }
        //list Model
        public List<Employee_User_List_Output_Model> _employee_List = new List<Employee_User_List_Output_Model>();
        public List<Master_Role_List_Output_Model> _role_List = new List<Master_Role_List_Output_Model>();
        //data binding for blazor page
        public EmployeeViewModel employeeViewModel = new EmployeeViewModel();
        public APIReturnModel aPIReturnModel;
        [Inject]
        NavigationManager navigationManager { get; set; }
        public Employee_API_Model employee_API_Model { get; set; }

        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<Employee_User_List_Output_Model> sfEmployee;

        public List<string> toolBarItems = (new List<string>() { "Add User",  "Print", "ExportExcel", "Collapse All", "Expand All", "Search", "ColumnChooser" });
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        //this model use for send data to API ,binding view model with API model

        public SessionModel _sessionModel;
        public string OperationType = "";

        public class DropdwonGenderList
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }
        public List<DropdwonGenderList> genderDetails = new List<DropdwonGenderList>()
        {
        new DropdwonGenderList(){ ID= 1, Text= "Male" },
        new DropdwonGenderList(){ ID= 2, Text= "Female" },
        new DropdwonGenderList(){ ID= 3, Text= "Other" }
        };
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            Employee_User_List_Input_Model employee_User_List_Input_Model = new Employee_User_List_Input_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                userRoleID = 0,
                userAccountID = 0,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            _employee_List = (await employeeService.GET_USER_LIST(employee_User_List_Input_Model)).ToList();

            Master_Role_List_Input_Para_Model master_Role_List_Input_Para_Model = new Master_Role_List_Input_Para_Model()
            {
                roleID = 0,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _role_List = (await masterDataSetupService.GET_Master_RoleList(master_Role_List_Input_Para_Model)).ToList();
        }

        public void EditUser(CommandClickEventArgs<Employee_User_List_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                IsVisible = true;
                DialogHeaderName = "Update User Details";
                HeaderText = "Update User";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                employeeViewModel = new EmployeeViewModel()
                {
                    accountID = args.RowData.id,
                    employeeCode = args.RowData.employeeCode,
                    firstName = args.RowData.firstName,
                    middleName = args.RowData.middleName,
                    lastName = args.RowData.lastName,
                    emailId = args.RowData.email,
                    genderID = args.RowData.genderID.ToString(),
                    mobileNo = args.RowData.mobileNo,
                    userRoleID = args.RowData.roleID.ToString()
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete User Details";
                OperationType = "Delete";
                HeaderText = "Delete User";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                employeeViewModel = new EmployeeViewModel()
                {
                    accountID = args.RowData.id,
                    employeeCode = args.RowData.employeeCode,
                    firstName = args.RowData.firstName,
                    middleName = args.RowData.middleName,
                    lastName = args.RowData.lastName,
                    emailId = args.RowData.email,
                    genderID = args.RowData.genderID.ToString(),
                    mobileNo = args.RowData.mobileNo,
                    userRoleID = args.RowData.roleID.ToString()
                };
            }

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add User")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add User Details";
                OperationType = "Add";
                HeaderText = "Add User";
                ddEnable = true;
                employeeViewModel.employeeCode = null;
                employeeViewModel.firstName = null;
                employeeViewModel.middleName = null;
                employeeViewModel.lastName = null;
                employeeViewModel.emailId = null;
                employeeViewModel.mobileNo = null;
                employeeViewModel.userRoleID = null;
                employeeViewModel.genderID = null;
            }
            //else if (args.Item.Text == "Add Role")
            //{
            //    navigationManager.NavigateTo("/User_Pages/MasterData/MasterDataList");
            //}
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfEmployee.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfEmployee.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfEmployee.ExpandAllGroupAsync();
            }
        }

        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                employee_API_Model = new Employee_API_Model()
                {
                    accountID = employeeViewModel.accountID,
                    employeeCode = employeeViewModel.employeeCode,
                    firstName = employeeViewModel.firstName,
                    middleName = employeeViewModel.middleName,
                    lastName = employeeViewModel.lastName,
                    emailId = employeeViewModel.emailId,
                    mobileNo = employeeViewModel.mobileNo,
                    genderID = Convert.ToInt16(employeeViewModel.genderID),
                    userRoleID = Convert.ToInt16(employeeViewModel.userRoleID),
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,

                };

                if (OperationType == "Add")
                {
                    employee_API_Model.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    employee_API_Model.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    employee_API_Model.OperationType = OperationAction.Delete;
                }
                EmployeeDetailsSave(employee_API_Model);
            };
        }

        private async void EmployeeDetailsSave(Employee_API_Model employee_API_Model)
        {
            try
            {
                if (employee_API_Model.OperationType != "NA")
                {
                    aPIReturnModel = (await employeeService.CRUD_UserDetails(employee_API_Model));

                    if (aPIReturnModel.status == false)
                    {
                        if (employee_API_Model.OperationType == "Add")
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Success);
                        }
                        else if (employee_API_Model.OperationType == "Update")
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Info);
                        }
                        else
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Error);
                        }


                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                    Employee_User_List_Input_Model employee_User_List_Input_Model = new Employee_User_List_Input_Model()
                    {
                        schoolCode = _sessionModel.SchoolCode,
                        userRoleID = 0,
                        userAccountID = 0,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = ReportType.All
                    };
                    _employee_List = (await employeeService.GET_USER_LIST(employee_User_List_Input_Model)).ToList();
                    StateHasChanged();
                    ClearData();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ClearData()
        {
            employeeViewModel = new EmployeeViewModel();

        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
    }
    }
