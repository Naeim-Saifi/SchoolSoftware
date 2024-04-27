using AdminDashboard.Server.Service.Interface.Employee;
using AIS.Data.BaseClass;
using AIS.Model;
using AIS.Model.Employee;
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

namespace AdminDashboard.Server.Pages.Employee
{
    public class EmployeeDetailsBase:ComponentBase
    {
        public class DropdwonGenderList
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }
        public class DropdwonRoleList
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

        public List<DropdwonRoleList> userRole = new List<DropdwonRoleList>()
        {
        new DropdwonRoleList(){ ID= 1, Text= "Owner" },
        new DropdwonRoleList(){ ID= 2, Text= "Admin" },
        new DropdwonRoleList(){ ID= 3, Text= "Management" },
        new DropdwonRoleList(){ ID= 4, Text= "Principal" },
        new DropdwonRoleList(){ ID= 5, Text= "Student" },
        new DropdwonRoleList(){ ID= 6, Text= "Teacher" },

        };
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]

        public IEmployeeService employeeService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public EmployeeApiModel employeeapiModel { get; set; }
        //data binding for blazor page

        public EmployeeViewModel employeeviewModel  = new EmployeeViewModel();

        //list Model
        public List<EmployeeDetailsModel> employeeDetailsListModels = new List<EmployeeDetailsModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";
      
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                employeeapiModel = new EmployeeApiModel()
                {
                    EmployeeId = employeeviewModel.userId,
                    EmployeeCode = employeeviewModel.EmployeeCode,
                    FirstName = employeeviewModel.FirstName,
                    MiddleName = employeeviewModel.MiddleName,
                    LastName = employeeviewModel.LastName,
                    GenderID = Convert.ToInt16(employeeviewModel.DropdwonGender),
                    PhoneNumber = employeeviewModel.PhoneNumber,
                    Email = employeeviewModel.Email,
                    EmployeeRole = (int)employeeviewModel.DropdwonUserRole,
                    Schoolcode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                    ActiveStatus = 1

                };

                if (OperationType == "Add")
                {
                    employeeapiModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    employeeapiModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    employeeapiModel.OperationType = OperationAction.Delete;
                }
                SaveEmployeeDetails(employeeapiModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveEmployeeDetails(EmployeeApiModel employeeapiModel)
        {
            aPIReturnmodel = (await employeeService.AddUpdateEmployee(employeeapiModel));
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

        private async Task GetEmployeeList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                 Userid=_sessionModel.UserId,
                OperationType = ReportType.All
            };
            //Get all list from API.
            employeeDetailsListModels = (await employeeService.GetemployeeDetails(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            employeeviewModel.EmployeeCode = null;
            employeeviewModel.FirstName = null;
            employeeviewModel.MiddleName = null;
            employeeviewModel.LastName = null;
            employeeviewModel.DropdwonGender = null;
            employeeviewModel.Email = null;
            employeeviewModel.PhoneNumber = null;
            employeeviewModel.DropdwonGender = null;
        }

        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Get Batch List Details
            GetEmployeeList();
            //Get all list from API.


            //masterBatchListModels = (await masterDataService.GetMasterBatchList(defaultInputParameterModel)).ToList();
        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        //public void RowSelectHandler(RowSelectEventArgs<MasterBatchListModel> args)
        //{
        //    masterBatchViewModel = new MasterBatchViewModel()
        //    {
        //        batchName = args.Data.batchName,
        //        batchCode = args.Data.batchCode,
        //        displayOrder = Convert.ToInt16(args.Data.displayOrder),
        //        masterBatchId = args.Data.masterBatchId
        //    };
        //    HeaderTest = "Update Record";

        //}
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

        public SfGrid<EmployeeDetailsModel> sfEmployeeDetailsList;
        public string btncss = "";

      
        public void EditEmployeeMaster(CommandClickEventArgs<EmployeeDetailsModel> args)
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
                employeeviewModel = new EmployeeViewModel()
                {
                    EmployeeCode = args.RowData.employeeCode,
                    FirstName = args.RowData.firstName,
                    MiddleName = args.RowData.middleName,
                    LastName = args.RowData.lastName,
                    Email = args.RowData.email,
                    DropdwonGender =Convert.ToString( args.RowData.gender),
                    PhoneNumber = args.RowData.phoneNumber,
                     DropdwonUserRole = args.RowData.userRole,
                    userId = args.RowData.userId,
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Employee Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                employeeviewModel = new EmployeeViewModel()
                {
                    EmployeeCode = args.RowData.employeeCode,
                    FirstName = args.RowData.firstName,
                    MiddleName = args.RowData.middleName,
                    LastName = args.RowData.lastName,
                    Email = args.RowData.email,
                    DropdwonGender = Convert.ToString(args.RowData.gender),
                    PhoneNumber = args.RowData.phoneNumber,
                    DropdwonUserRole = args.RowData.userRole,
                    userId = args.RowData.userId,
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
               // ClearAllFields();
            }
        }

    }
}
