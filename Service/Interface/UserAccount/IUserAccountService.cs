using AIS.Data.UserLogin;
using AIS.Model;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IUserAccountService
    {
        Task<IEnumerable<AuthenticateUserResponseModel>> GetUserAccountList(UserLogin userLogin);
      

    }
}
