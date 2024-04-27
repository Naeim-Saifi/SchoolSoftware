using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Model.UserAccount;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Personal
{
    public class DashboardBase:ComponentBase
    {
        //[Inject]
        //Blazored.SessionStorage.ISessionStorageService session { get; set; }
        //public SessionModel _sessionModel;

        private ILocalStorageService _localStorageService;

        public DashboardBase() { }
        
        public DashboardBase(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
       
        public string SchoolName { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        bool first = true;
        protected override async Task OnInitializedAsync()
        {
            

        }
        protected async Task OnAfterRenderAsync()
        {
            var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");
            SchoolName = user.SchoolName.ToString();
            if (user == null)
            {
                navigationManager.NavigateTo("/");
            }
        }

    }
}
