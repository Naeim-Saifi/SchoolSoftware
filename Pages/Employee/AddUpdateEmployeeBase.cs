using AdminDashboard.Server.Service.Interface.Employee;
using AIS.Model.Employee;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Employee
{
    public class AddUpdateEmployeeBase : ComponentBase
    {

        public class DropdwonList
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }
        public List<DropdwonList> genderDetails = new List<DropdwonList>()
        {
        new DropdwonList(){ ID= 1, Text= "Male" },
        new DropdwonList(){ ID= 2, Text= "Female" },
        new DropdwonList(){ ID= 3, Text= "Other" }
        };

        public List<DropdwonList> userRole = new List<DropdwonList>()
        {
        new DropdwonList(){ ID= 1, Text= "Owner" },
        new DropdwonList(){ ID= 2, Text= "Admin" },
        new DropdwonList(){ ID= 3, Text= "Management" },
        new DropdwonList(){ ID= 3, Text= "Principal" },
        new DropdwonList(){ ID= 3, Text= "Student" },
        new DropdwonList(){ ID= 3, Text= "Teacher" },

        };

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        public List<EmployeeDetailsModel> employeeDetailsModels = new List<EmployeeDetailsModel>();  

        [Inject]

        public IEmployeeService employeeService { get; set; }

        public SessionModel _sessionModel;
        public EmployeeModel employeeModel { get; set; }
        public EmployeeViewModel employeeviewModel { get; set; }

        public string Message = string.Empty;

        [Inject]
        public ISnackbar snackBar { get; set; }

        public string ToastContent = "";// "Conference Room 01 / Building 135 10:00 AM-10:30 AM";
       
        public SfToast ToastObj;

        public string _employeeCode = "";
        public  async void OnValidSubmit()
        {
            Message = "Form Submitted Successfully!";
        //    EmployeeModel employeeModel1 = new EmployeeModel()
        //    {
        //        OperationType = "Add",
        //        EmployeeCode = SelectedData.employeeCode,
        //        FirstName = SelectedData.firstName,
        //        MiddleName = SelectedData.middleName,
        //        LastName = SelectedData.lastName,
        //        GenderName = SelectedData.genderStatusDetails,
        //        PhoneNumber = SelectedData.phoneNumber,
        //        Email = SelectedData.email,
        //        EmployeeRole = SelectedData.roleStatusDetails,
        //        Schoolcode= _sessionModel.SchoolCode,
        //        ActiveStatus=1
        //};

            //employeeModel.OperationType = "Add";
            //employeeModel.ActiveStatus = 1;
            //employeeModel.Schoolcode = _sessionModel.SchoolCode;
            //_employeeCode = employeeModel.EmployeeCode;
            //string msg = (await employeeService.AddUpdateEmployee(employeeModel)).message.ToString();
            //ToastContent = msg;
            //if (msg == "Employee Details already exist")
            //{
            //    snackBar.Add(msg, Severity.Error);
            // //await this.ToastObj.ShowAsync();
            //}
            //else
            //{

            //     snackBar.Add(msg, Severity.Success);
            //   // await this.ToastObj.ShowAsync();


            //}
            //await Task.Delay(5000);
            //Message = string.Empty;
            //employeeModel.FirstName = null;
             StateHasChanged();
        }
        private void OnInvalidSubmit()
        {
            Message = string.Empty;
        }
        protected override async Task OnInitializedAsync()
        {
            //employeeModel = new EmployeeModel();
            employeeviewModel = new EmployeeViewModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            //employeeDetailsModels = (await employeeService.GetemployeeDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
            //await LoadBatchData();


        }

        public SfGrid<EmployeeDetailsModel> Grid;
        //public void ToolbarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        //{
        //    if (args.Item.Text == "PDF Export")
        //    {
        //        this.Grid.PdfExport();
        //    }
        //    else if (args.Item.Id == "Grid_excelexport")
        //    {
        //        this.Grid.ExcelExport();
        //    }
        //    else
        //    {
        //        this.Grid.CsvExport();
        //    }
        //}

        public EmployeeViewModel SelectedData = new EmployeeViewModel();
        //public void RowSelectHandler(RowSelectEventArgs<EmployeeViewModel> args)
        //{
        //    //SelectedData = new EmployeeDetailsModel()
        //    //{


        //    //     employeeCode = args.Data.employeeCode,
        //    //    firstName = args.Data.firstName,
        //    //    middleName = args.Data.middleName,
        //    //    lastName = args.Data.lastName,
        //    //    gender = args.Data.gender,
        //    //    genderStatusDetails = args.Data.genderStatusDetails,
        //    //    phoneNumber = args.Data.phoneNumber,
        //    //    email = args.Data.email,
        //    //    roleStatusDetails = args.Data.roleStatusDetails,
        //    //    userRole = args.Data.userRole,
        //    //    stateStatusDetails = args.Data.stateStatusDetails,
        //    //    activeStatus = args.Data.activeStatus,

        //    //};
        //   // classname = args.Data.className;
        //    //SelectedData = args.Data;

        //    //this.Disabled = false;
        //    //this.Enabled = true;
        //}
        //public void RowDeSelectHandler(RowDeselectEventArgs<EmployeeViewModel> args)
        //{
        //    SelectedData = new EmployeeViewModel();
        //    //this.Disabled = true;
        //    //this.Enabled = false;
        //}
    }
}
