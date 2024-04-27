using AdminDashboard.Server.Models.UserRegistraion;
using AIS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.AdminUserRegistration
{
    public interface IAdminUserRegistrationService
    {
        Task<IEnumerable<AdminUserRegistrationListModel>> GetAdminUserRegistrationList(string SchoolCode, int SchoolId, int Id, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateAdminUserRegistration(AdminUserRegistrationModel userRegistrationModel);
    }
}
