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
    public class PersonCardBase:ComponentBase
    {
        
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public PersonCardBase()
        {
        }
        public string UserName { get; set; }
        public string UserRoleName { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        public SessionModel _sessionModel;
        protected override async Task OnInitializedAsync()
        {            
                 _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
               // var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");
                UserName = _sessionModel.FirstName + " " + _sessionModel.MiddleName + " " + _sessionModel.LastName;
                UserRoleName = _sessionModel.UserRoleName;
                if (_sessionModel == null)
                {
                    navigationManager.NavigateTo("/");
                }
        }
        protected async Task OnAfterRenderAsync()
        {    
        }
    }
}
