using AIS.Data.UserLogin;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.ProtectedBrowserStorage;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Pages.Authentication
{
    public class LoginBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
       public IUserAccountService userAccountService { get; set; }

        public List<AuthenticateUserResponseModel> userAccountDetailsList=new List<AuthenticateUserResponseModel>();

        public class DropdwonGenderList
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }
        //public List<SessionModel> sessionModels = new List<SessionModel>();
        //SessionModel sessionModel = new SessionModel();
        public List<DropdwonGenderList> financialYear = new List<DropdwonGenderList>()
        {       new DropdwonGenderList(){ ID= 1, Text= "2024-25" },
                new DropdwonGenderList(){ ID= 2, Text= "2023-24" },       
        };
        public UserLogin userLogin { get; set; }
        protected override async Task OnInitializedAsync()
        {
            userLogin = new UserLogin();
            //protectedSessionStorage =new ProtectedSessionStorage();
        }
        public bool success;

        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        public ISnackbar snackBar { get; set; }

      
        public int SchoolId = 2;
        public int UserRole = 0;
        public string SchoolName = "";
        public string TokenID = "";
        string sfinancialYear = "";
        
        public async void OnValidSubmit()
        {
            success = true;
          var  sessionModel = new SessionModel();

            if(userLogin.FinancialYear.ToString()=="1")
            {
                
                sfinancialYear = "2024-25";
            }
            else
            {
                // userLogin.FinancialYear = "2022-23";
                sfinancialYear = "2023-24";
            }
              userLogin.FinancialYear = sfinancialYear;
            userAccountDetailsList = (await userAccountService.GetUserAccountList(userLogin)).ToList();

            //if (userAccountDetailsList.Count > 0 || userAccountDetailsList == null)
            //{
            //    snackBar.Add("Invalid UserId or Password", Severity.Error);
            //}
            //else
            //{
            if (userAccountDetailsList.Count == 0)
            {
                snackBar.Add("Invalid  Password", Severity.Error);
            }
            else if (userAccountDetailsList[0].roleId == 0)
            {
                snackBar.Add("Invalid UserId or Password", Severity.Error);

            }
            else
            {
                foreach (var _UserAccountDetails in userAccountDetailsList)
                {
                    sessionModel.UserId = _UserAccountDetails.userId;
                    sessionModel.UserName = _UserAccountDetails.userName;
                    sessionModel.FirstName = _UserAccountDetails.firstName;
                    sessionModel.MiddleName = _UserAccountDetails.middleName;
                    sessionModel.LastName = _UserAccountDetails.lastName;
                    sessionModel.Email = _UserAccountDetails.email;
                    sessionModel.ActiveStatus = _UserAccountDetails.activeStatus;
                    sessionModel.ActiveStatusDetails = _UserAccountDetails.activeStatusDetails;
                    sessionModel.PhoneNumber = _UserAccountDetails.phoneNumber;
                    sessionModel.SchoolCode = _UserAccountDetails.schoolCode;
                    sessionModel.SchoolName = _UserAccountDetails.schoolName;
                    sessionModel.SchoolId = _UserAccountDetails.schoolId;
                    sessionModel.RoleId = _UserAccountDetails.roleId;
                    sessionModel.classID = _UserAccountDetails.classID;
                    sessionModel.sectionID = _UserAccountDetails.sectionID;
                    sessionModel.UserRoleName = _UserAccountDetails.userRoleName;
                    sessionModel.LatestLoginDate = _UserAccountDetails.latestLoginDate;
                    sessionModel.FinancialYear = sfinancialYear;// userLogin.FinancialYear;
                    await session.SetItemAsync("sessionUser", sessionModel);

                }

                if (userAccountDetailsList[0].schoolCode == "AI")
                {
                    if (userAccountDetailsList[0].roleId == 1)
                    {
                        // navigationManager.NavigateTo("/personal/dashboard");
                        navigationManager.NavigateTo("/AISSchoolDetails/AISDashboard");
                    }
                    else if (userAccountDetailsList[0].roleId == 2)
                    {
                        // navigationManager.NavigateTo("/personal/dashboard");
                        navigationManager.NavigateTo("/AISSchoolDetails/AISDashboard");
                    }
                }
                else
                {

                    if (userAccountDetailsList[0].roleId == 1)
                    {
                        // navigationManager.NavigateTo("/personal/dashboard");
                        //navigationManager.NavigateTo("/Dashboard/Student/StudentDashboard");
                        navigationManager.NavigateTo("/User_Pages/Dashboard/Student");
                    }

                    else if (userAccountDetailsList[0].roleId == 2)
                    {
                        // navigationManager.NavigateTo("/personal/dashboard");
                        //navigationManager.NavigateTo("/Dashboard/Teacher/TeacherDashboard");
                        navigationManager.NavigateTo("/User_Pages/Dashboard/Teacher");
                    }
                    else if (userAccountDetailsList[0].roleId == 3)
                    {
                        // navigationManager.NavigateTo("/personal/dashboard");
                        //navigationManager.NavigateTo("/Dashboard/Management/ManagementDashboard");
                        navigationManager.NavigateTo("/User_Pages/Dashboard/Management");
                    }
                    else if (userAccountDetailsList[0].roleId == 4)
                    {
                        // navigationManager.NavigateTo("/personal/dashboard");
                        //navigationManager.NavigateTo("/Dashboard/Management/ManagementDashboard");
                        navigationManager.NavigateTo("/User_Pages/Dashboard/Admin");
                    }

                }
            } 
                     
                //else if (userAccountDetailsList[0].roleId == 15)
                //{
                //    // navigationManager.NavigateTo("/personal/dashboard");
                //    navigationManager.NavigateTo("/AISSchoolDetails/AISDashboard");
                //}
                //else
                //    {
                //        // navigationManager.NavigateTo("/personal/dashboard");
                //        navigationManager.NavigateTo("/Dashboard/Student/StudentDashboard");
                //    }
                //}
            //}
        }

        
       
       public bool PasswordVisibility;
       public InputType PasswordInput = InputType.Password;
       public string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

     public   void TogglePasswordVisibility()
        {
           if (PasswordVisibility)
            {
                PasswordVisibility = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
        else
            {
                PasswordVisibility = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}
