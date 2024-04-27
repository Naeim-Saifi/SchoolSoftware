using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.UserLogin;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace AIS.FrontEnd_07062021.Service.Implementation
{
    public class UserAccountService : IUserAccountService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        private NavigationManager _navigationManager;
        // public AuthenticateUserResponseModel sessionModel1 { get;  set; }
        public AuthenticateUserResponseModel sessionModel1;
        public UserAccountService(HttpClient httpClient, ILocalStorageService localStorageService,
             NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this._localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }
        public async Task Initialize()
        {
            //sessionModel = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");
        }
        public async Task<IEnumerable<AuthenticateUserResponseModel>> GetUserAccountList(UserLogin userLogin)
        {
            var sessionModel1 =new  AuthenticateUserResponseModel();
            //MasterClassList[] masterClassList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            //var r = await httpClient.GetJsonAsync<AuthenticateUserResponseModel>($"api/UserAccount/GetUserAccountList?SchoolId={SchoolId}&UserAccountId={UserAccountId}&Status={Status}&OperationType={OperationType}");
            var r = await httpClient.GetJsonAsync<Response>($"/api/UserAccount/AuthenticateUser?UserId={userLogin.UserId}&UserPassword={userLogin.UserPassword}&FinancialYear={userLogin.FinancialYear}&Status=1&OperationType=AuthencateUser");
           // var r1 = await httpClient.GetJsonAsync<SessionModel>($"/api/UserAccount/AuthenticateUser?UserId={userLogin.UserId}&UserPassword={userLogin.UserPassword}&Status=1&OperationType=AuthencateUser");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                // await _localStorageService.SetItem("user", r);
                var userAccountDetailsList = System.Text.Json.JsonSerializer.Deserialize<AuthenticateUserResponseModel[]>(el.GetRawText());

                if (userAccountDetailsList.Length > 0)
                {
                    foreach (var _UserAccountDetails in userAccountDetailsList)
                    {
                        sessionModel1.userName = _UserAccountDetails.userName;
                        sessionModel1.firstName = _UserAccountDetails.firstName;
                        sessionModel1.middleName = _UserAccountDetails.middleName;
                        sessionModel1.userId =_UserAccountDetails.userId;                       
                        sessionModel1.lastName = _UserAccountDetails.lastName;
                        sessionModel1.email = _UserAccountDetails.email;
                        sessionModel1.activeStatus = _UserAccountDetails.activeStatus;
                        sessionModel1.activeStatusDetails = _UserAccountDetails.activeStatusDetails;
                        sessionModel1.phoneNumber = _UserAccountDetails.phoneNumber;
                        sessionModel1.schoolCode = _UserAccountDetails.schoolCode;
                        sessionModel1.schoolName = _UserAccountDetails.schoolName;
                        sessionModel1.schoolId = _UserAccountDetails.schoolId;
                        sessionModel1.roleId = _UserAccountDetails.roleId;
                        sessionModel1.userRoleName = _UserAccountDetails.userRoleName;
                        sessionModel1.latestLoginDate = _UserAccountDetails.latestLoginDate;
                        await _localStorageService.SetItem("user", sessionModel1);

                    }
                }
                   // await _localStorageService.SetItem("user", userSession);
                return System.Text.Json.JsonSerializer.Deserialize<AuthenticateUserResponseModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task Logout()
        {
            sessionModel1 = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("");
        }

    }
}
