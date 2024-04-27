using AdminDashboard.Server.Service.Implementation.LocalStorage;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.UserLogin;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Shared
{
    public class NavMenuBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        public SessionModel _sessionModel;
        public string UserRole { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        GetLocalStorageData getLocalStorageData = new GetLocalStorageData();
        protected override async Task OnInitializedAsync()
        {
          _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            // UserRole = await getLocalStorageData.GetRoleName();
            UserRole = _sessionModel.UserRoleName;
            if (UserRole == null)
            {
                navigationManager.NavigateTo("/");
            }

        }
        protected async Task OnAfterRenderAsync()
        {
            //  _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");
            //UserRole = user.userRoleName;
            //if (user == null)
            //{
            //    navigationManager.NavigateTo("/");
            //}
        }
    }
}
