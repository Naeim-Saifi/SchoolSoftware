//using AdminDashboard.Server.Service.Interface.Employee;
//using AIS.Model.Employee;
//using AIS.Model.GeneralConversion;
//using AIS.Model.UserLogin;
//using Microsoft.AspNetCore.Components;
//using MudBlazor;
//using Syncfusion.Blazor.Grids;
//using Syncfusion.Blazor.Notifications;
//using Syncfusion.Blazor.Popups;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AdminDashboard.Server.Pages.Employee
//{
//    public class AddUpdateEmployeeDetailsBase : ComponentBase
//    {
//        public class DropdwonGenderList
//        {
//            public int ID { get; set; }
//            public string Text { get; set; }
//        }
//        public class DropdwonRoleList
//        {
//            public int ID { get; set; }
//            public string Text { get; set; }
//        }

//        public List<DropdwonGenderList> genderDetails = new List<DropdwonGenderList>()
//        {
//        new DropdwonGenderList(){ ID= 1, Text= "Male" },
//        new DropdwonGenderList(){ ID= 2, Text= "Female" },
//        new DropdwonGenderList(){ ID= 3, Text= "Other" }
//        };

//        public List<DropdwonRoleList> userRole = new List<DropdwonRoleList>()
//        {
//        new DropdwonRoleList(){ ID= 1, Text= "Owner" },
//        new DropdwonRoleList(){ ID= 2, Text= "Admin" },
//        new DropdwonRoleList(){ ID= 3, Text= "Management" },
//        new DropdwonRoleList(){ ID= 4, Text= "Principal" },
//        new DropdwonRoleList(){ ID= 5, Text= "Student" },
//        new DropdwonRoleList(){ ID= 6, Text= "Teacher" },

//        };
//        public SessionModel _sessionModel;
//        [Inject]
//        public IEmployeeService employeeService { get; set; }

//        [Inject]
//        Blazored.SessionStorage.ISessionStorageService session { get; set; }
//        public List<EmployeeDetailsModel> employeeDetailsModels = new List<EmployeeDetailsModel>();
//        public EmployeeViewModel employeeviewModel { get; set; }      
//        protected override async Task OnInitializedAsync()
//        {
          
//            employeeviewModel = new EmployeeViewModel();
//            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
//            employeeDetailsModels = (await employeeService.GetemployeeDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
            
//        }
//        public string Message = string.Empty;
//        [Inject]
//        public ISnackbar snackBar { get; set; }
//        public async void OnValidSubmit()
//        {
//            Message = "Form Submitted Successfully!";
//            EmployeeModel employeeModel = new EmployeeModel()
//            {
//                EmployeeId= employeeviewModel.userId,                
//                EmployeeCode = employeeviewModel.EmployeeCode,
//                FirstName = employeeviewModel.FirstName,
//                MiddleName = employeeviewModel.MiddleName,
//                LastName = employeeviewModel.LastName,
//                GenderID =Convert.ToInt16(employeeviewModel.DropdwonGender),
//                PhoneNumber = employeeviewModel.PhoneNumber,
//                Email = employeeviewModel.Email,
//                EmployeeRole = employeeviewModel.DropdwonUserRole,
//                Schoolcode = _sessionModel.SchoolCode,
//                FinancialYear=_sessionModel.FinancialYear,
//                ActiveStatus = 1 
                 
//            };


//            if(employeeModel.EmployeeId==0)
//            {
//                employeeModel.OperationType = OperationAction.Add;
//            }
//            else
//            {
//                employeeModel.OperationType = OperationAction.Update;
//            }
             
//            string msg = (await employeeService.AddUpdateEmployee(employeeModel)).message.ToString();
           
//            if (msg == "Employee Details already exist")
//            {
//                snackBar.Add(msg, Severity.Error);               
//            }
//            else
//            {
//                snackBar.Add(msg, Severity.Success);              
//            }
           
//            employeeviewModel.EmployeeCode = null;
//            employeeviewModel.FirstName = null;
//            employeeviewModel.MiddleName = null;
//            employeeviewModel.LastName = null;
//            employeeviewModel.DropdwonGender = null;
//            employeeviewModel.PhoneNumber = null;
//            employeeviewModel.Email = null;
//            employeeviewModel.DropdwonUserRole = 0;

//            StateHasChanged();
//        }
//        public SfGrid<EmployeeDetailsModel> Grid;


//        public void ToolClick(Syncfusion.Blazor.Navigations.ClickEventArgs Args)
//        {
//            if (Args.Item.Text == "Add")
//            {
//                Args.Cancel = true;
//                OpenDialog();
//                //NavigationManager.NavigateTo("addRecord/0");
//            }
//            else if (Args.Item.Text == "Edit")
//            {
//                //NavigationManager.NavigateTo($"editRecord/{SelectedRecord.OrderID}");
//            }
//        }
//        public void RowSelectHandler(RowSelectEventArgs<EmployeeDetailsModel> args)
//        {
//             employeeviewModel = new EmployeeViewModel()
//            {
//                userId = args.Data.userId,
//                EmployeeCode = args.Data.employeeCode,
//                FirstName = args.Data.firstName,
//                MiddleName = args.Data.middleName,
//                LastName = args.Data.lastName,
//                DropdwonGender =(args.Data.gender).ToString(),
//                PhoneNumber = args.Data.phoneNumber,
//                Email = args.Data.email,
//               DropdwonUserRole = args.Data.userRole               
               
//                //Schoolcode = _sessionModel.SchoolCode,
//                //ActiveStatus = 1
//            };
//            this.IsVisible = true;
//            // classname = args.Data.className;
//            //SelectedData = args.Data;

//            //this.Disabled = false;
//            //this.Enabled = true;
//        }
//        public void RowDeSelectHandler(RowDeselectEventArgs<EmployeeDetailsModel> args)
//        {
//            employeeviewModel = new EmployeeViewModel();
//            //this.Disabled = true;
//            //this.Enabled = false;
//        }

//        public string[] GroupedColumns = new string[] { "roleStatusDetails" };
//        public void ExportToExcel()
//        {
//            this.Grid.ExcelExport();
//        }

//        public bool IsVisible { get; set; } = false;

//        public DialogEffect AnimationEffect = DialogEffect.Zoom;
//        public void OpenDialog()
//        {
//            employeeviewModel.EmployeeCode = null;
//            employeeviewModel.FirstName = null;
//            employeeviewModel.MiddleName = null;
//            employeeviewModel.LastName = null;
//            employeeviewModel.DropdwonGender = null;
//            employeeviewModel.PhoneNumber = null;
//            employeeviewModel.Email = null;
//            employeeviewModel.DropdwonUserRole = 0;
//            this.IsVisible = true;
//        }

//        public void CloseDialog()
//        {
//            this.IsVisible = false;
//        }





//    }

//}
