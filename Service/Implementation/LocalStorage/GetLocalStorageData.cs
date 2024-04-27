using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Model.UserAccount;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Implementation.LocalStorage
{
    public  class GetLocalStorageData
    {
        private ILocalStorageService _localStorageService { get; set; }

      //public  GetLocalStorageData(ILocalStorageService localStorageService)
      //  {
      //      _localStorageService = localStorageService;
      //  }
        public GetLocalStorageData()
        {
            
        }

        public async  Task<string> GetSchoolName()
        {
            var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");

            return  user.SchoolName.ToString();
        }
        public async Task<string> GetRoleName()
        {
            var user = await _localStorageService.GetItem<AuthenticateUserResponseModel>("user");

            return user.UserRoleName.ToString();
        }

    }
}
