using AdminDashboard.Server.Service.Interface.UserRegistration;
using AIS.Model.MasterUserAccount;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.UserRegistration
{
    public class UserRegistrationBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IUserRegistrationService userRegistrationService { get; set; }

        public SessionModel _sessionModel;
        public UserRegistrationModel_old userRegistrationModel { get; set; }
        protected override async Task OnInitializedAsync()
        {

            userRegistrationModel = new UserRegistrationModel_old();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
        }

        public void OnValidSubmit()
        {
            userRegistrationService.AddUpdateUserRegistration(userRegistrationModel);
            StateHasChanged();
        }
        public void Cancel()
        {
            //ActionName = "Save";
            //MasterId = 0;
            //MasterBatchModel masterBatchModel = null;
        }
    }
}
