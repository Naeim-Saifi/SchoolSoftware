using AdminDashboard.Server.Models.UserRegistraion;
using AIS.Model;
using AIS.Model.MasterUserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.UserRegistration
{
    public interface IUserRegistrationService
    {
        //Task<IEnumerable<AdminUserRegistrationListModel>> GetAdminUserRegistrationList(string SchoolCode, int SchoolId, int MasterBatchId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateUserRegistration(UserRegistrationModel_old userRegistrationModel);
    }
}
