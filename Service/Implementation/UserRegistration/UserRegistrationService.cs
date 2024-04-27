using AdminDashboard.Server.Service.Interface.LocalStorage;
using AdminDashboard.Server.Service.Interface.UserRegistration;
using AIS.Model;
using AIS.Model.MasterUserAccount;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Implementation
{
    public class UserRegistrationService: IUserRegistrationService
    {
        private readonly HttpClient httpClient;
        //private ILocalStorageService _localStorageService;

        public UserRegistrationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
           
        }
        public async Task<APIReturnModel> AddUpdateUserRegistration(UserRegistrationModel_old userRegistrationModel)
        {
            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/UserRegistration/AddUpdateUserRegistration", userRegistrationModel);
            return response;
        }
    }
}
